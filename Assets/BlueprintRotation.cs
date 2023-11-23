using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathU.Matrices;

public class BlueprintRotation : MonoBehaviour
{

    [Header("Objects")]
    [SerializeField] GameObject[] Gobjects;

	[Header("Controls")]
	[SerializeField] Vector3 xAxis;
	[SerializeField] Vector3 yAxis;
	[SerializeField] Vector3 zAxis;
	[SerializeField] Vector4 wAxis;
	[SerializeField] Vector3 translate;
	[SerializeField] Vector3 rotate;




	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Gobjects.Length; i++)
		{
            Vector3 vertex = Gobjects[i].transform.position;
			Vector3 vertexRot = Rotate(vertex, rotate);
			Vector3 vertexTrans = Transform(vertexRot);
			Debug.Log(vertexTrans);
			Gobjects[i].transform.position = vertexTrans;

		}
    }

	private Vector3 Rotate(Vector3 vertex, Vector3 angle)
	{
		MathU.Vector3 _vertex = new MathU.Vector3(vertex.x, vertex.y, vertex.z);
		Vector3 vertexRotated = new Vector3(0, 0, 0);

		Matrix rX = new Matrix(3, 3);
		Matrix rY = new Matrix(3, 3);
		Matrix rZ = new Matrix(3, 3);

		float xsinAngle = Mathf.Sin(angle.x * Mathf.Rad2Deg);
		float xcosAngle = Mathf.Cos(angle.x * Mathf.Rad2Deg);

		float ysinAngle = Mathf.Sin(angle.y * Mathf.Rad2Deg);
		float ycosAngle = Mathf.Cos(angle.y * Mathf.Rad2Deg);

		float zsinAngle = Mathf.Sin(angle.z * Mathf.Rad2Deg);
		float zcosAngle = Mathf.Cos(angle.z * Mathf.Rad2Deg);

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

		Matrix m = rX * (rY * (rZ * _vertex.ToColumn()));
		vertexRotated.x = (float)m.data[0, 0];
		vertexRotated.y = (float)m.data[0, 1];
		vertexRotated.z = (float)m.data[0, 2];

		return vertexRotated;
	}

	private Vector3 Transform(Vector3 vertex)
	{
		Vector3 vertexTransformed = new Vector3(0, 0, 0);

		vertexTransformed += xAxis * vertex.x;
		vertexTransformed += yAxis * vertex.y;
		vertexTransformed += translate * vertex.z;

		return vertexTransformed;
	}
}
