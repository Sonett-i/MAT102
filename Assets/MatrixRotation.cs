using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathU.Matrices;

public class MatrixRotation : MonoBehaviour
{
	[Header("Objects")]
    [SerializeField] LineRenderer[] lines;

	[Header("Controls")]
	public Vector3 xAxis;
	public Vector3 yAxis;
	public Vector3 trans;
	public Vector3 rotationAngle;

	Vector3[] vertices;
	// Start is called before the first frame update
	void Start()
    {
		vertices = new Vector3[lines.Length];
    }

	void UpdateVertexPositions()
	{
		for (int i = 0; i < lines.Length; i++)
		{
			Vector3 v = lines[i].transform.position;
			Vector3 vRot = Vector3.zero;
			Vector3 vTrans = Vector3.zero;
			

			vRot = Rotate(v, rotationAngle);

			vTrans = Transform(vRot);
			vertices[i] = vTrans;

		}
	}

	void ChangeVertexPositions()
	{
		Debug.Log(lines.Length);
		for (int j = 0; j < lines.Length; j++)
		{
			Debug.Log(j);
			lines[j].GetComponent<LineRenderer>().SetPosition(0, vertices[j]);
			if (j < lines.Length-1)
			{
				lines[j].GetComponent<LineRenderer>().SetPosition(1, vertices[0]);
			}
			else
			{
				//lines[j].GetComponent<LineRenderer>().SetPosition(1, vertices[j+1]);
			}
			
		}
	}
    // Update is called once per frame
    void Update()
    {
		UpdateVertexPositions();
		ChangeVertexPositions();
	}

	private Vector3 Transform(Vector3 vertex)
	{
		Vector3 vertexTransformed = new Vector3(0, 0, 0);

		vertexTransformed.x = xAxis.x * vertex.x + yAxis.x * vertex.y + trans.x * vertex.z;
		vertexTransformed.y = xAxis.y * vertex.x + yAxis.y * vertex.y + trans.y * vertex.z;
		vertexTransformed.z = xAxis.z * vertex.x + yAxis.z * vertex.y + trans.z * vertex.z;

		return vertexTransformed;
	}

	private Vector3 Rotate(Vector3 vertex, Vector3 angle)
	{
		Vector3 vertexRotated = new Vector3(0, 0, 0);

		Matrix rX = new Matrix(3, 3);
		Matrix rY = new Matrix(3, 3);
		Matrix rZ = new Matrix(3, 3);

		float xsinAngle = Mathf.Sin(angle.x);
		float xcosAngle = Mathf.Cos(angle.x);

		float ysinAngle = Mathf.Sin(angle.y);
		float ycosAngle = Mathf.Cos(angle.y);

		float zsinAngle = Mathf.Sin(angle.z);
		float zcosAngle = Mathf.Cos(angle.z);

		rX.data[0, 0] = 1;
		rX.data[1, 1] = xcosAngle;
		rX.data[1, 2] = -xsinAngle;
		rX.data[2, 1] = xsinAngle;
		rX.data[2, 2] = xcosAngle;

		rY.data[0, 0] = ycosAngle;
		rY.data[0, 2] = ysinAngle;
		rY.data[1, 1] = 1;
		rY.data[2, 0] = -ysinAngle;
		rY.data[2, 2] = ycosAngle;

		rZ.data[0, 0] = zcosAngle;
		rZ.data[0, 1] = -zsinAngle;
		rZ.data[1, 0] = zsinAngle;
		rZ.data[1, 1] = zcosAngle;
		rZ.data[2, 2] = 1;

		//vertexRotated = rX * (rY * (rZ * vertex));

		return vertexRotated;
	}
}
