using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathU.Matrices;
public class MatrixController : MonoBehaviour
{
	public Transform v1T, v2T, v3T, v4T;
	public Vector3 v1, v2, v3, v4;
	LineRenderer v1LR, v2LR, v3LR, v4LR;

	[Header("Controls")]
	public Vector3 xAxis;
	public Vector3 yAxis;
	public Vector3 trans;
	public Vector3 rotationAngle;

	[Header("Output")]
	public Vector3 v1Rot = Vector3.zero;
	public Vector3 v2Rot = Vector3.zero;
	public Vector3 v3Rot = Vector3.zero;
	public Vector3 v4Rot = Vector3.zero;


	public Vector3 v1Trans = new Vector3(0, 0, 0);
	public Vector3 v2Trans = new Vector3(0, 0, 0);
	public Vector3 v3Trans = new Vector3(0, 0, 0);
	public Vector3 v4Trans = new Vector3(0,0,0);

	private void Start()
	{
		v1 = v1T.transform.position;
		v2 = v2T.transform.position;
		v3 = v3T.transform.position;
		v4 = v4T.transform.position;

		// Homogeneous coords
		v1.z = 1;
		v2.z = 1;
		v3.z = 1;
		v4.z = 1;

		v1LR = v1T.GetComponent<LineRenderer>();
		v2LR = v2T.GetComponent<LineRenderer>();
		v3LR = v3T.GetComponent<LineRenderer>();
		v4LR = v4T.GetComponent<LineRenderer>();

		xAxis = new Vector3(1, 0, 0);
		yAxis = new Vector3(0, 1, 0);
		trans = new Vector3(0, 0, 1);
	}

	private void Update()
	{
		v1Rot = Rotate(v1, rotationAngle);
		v2Rot = Rotate(v2, rotationAngle);
		v3Rot = Rotate(v3, rotationAngle);
		v4Rot = Rotate(v4, rotationAngle);


		v1Trans = Transform(v1Rot);
		v2Trans = Transform(v2Rot);
		v3Trans = Transform(v3Rot);
		v4Trans = Transform(v4Rot);


		v1LR.SetPosition(0, v1Trans);
		v1LR.SetPosition(1, v2Trans);

		v2LR.SetPosition(0, v2Trans);
		v2LR.SetPosition(1, v3Trans);

		v3LR.SetPosition(0, v3Trans);
		v3LR.SetPosition(1, v4Trans);

		v4LR.SetPosition(0, v4Trans);
		v4LR.SetPosition(1, v1Trans);

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

		//vertexRotated = rX *(rY * (rZ * vertex));

		return vertexRotated;
	}

}
