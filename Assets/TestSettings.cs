using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathU.Matrices;
using LinearInterpolation;
using UnityEngine.UI;


public class TestSettings : MonoBehaviour
{
	[SerializeField] Vector3 CanvasOffset;

	[SerializeField] GameObject guide;

	[SerializeField] GameObject Text;

	[SerializeField] GameObject[] objects;
	[SerializeField][Range(0, 1200)] float angle = 0;

	[SerializeField] int[] box = new int[2];
	[SerializeField] GameObject templateObj;

	GameObject[] canvas;
	//Images
	[SerializeField] Texture2D[] image;

	Color[] Pixels;
	[SerializeField] float Duration = 3f;

	[SerializeField] float drawSpeed = 0.1f;

	int iSwitch = 0;
	float time = 0f;

	int itr = 0;

	[SerializeField] Vector3 cameraStart = new Vector3(35, 35, -1000);
	[SerializeField] Vector3 cameraEnd = new Vector3(35, 35, -80);
	[SerializeField] GameObject viewCamera;

	[SerializeField] bool SlowGenerate = false;
	[SerializeField] bool StaticCamera = false;
	[SerializeField] bool rotates = true;

	Color[] currentCanvas;

	Matrix[] TransformMatrices;

	void MoveObject(GameObject gobject)
	{
		Vector3 Start = new Vector3(0, 0, 0);

		float Radians = (float)Operations.Rad(angle);
		float result = angle / 1200;
		Vector3 End = new Vector3(Radians, Mathf.Sin(Radians), 0);
		Debug.Log(Radians + " " + angle + " " + result);
		gobject.transform.position = LinearInterpolation.LinearInterpolation.Lerp(Start, End, result);

	}

	IEnumerator SlowGen()
	{
		int i = 0;
		for (int x = 0; x < image[0].width; x++)
		{
			for (int y = 0; y < image[0].height; y++)
			{
				Debug.Log(i);
				canvas[i] = Instantiate(templateObj);
				canvas[i].transform.position = new Vector3((x * 10f), (y * 10f), 0);

				i++;
				yield return new WaitForSeconds(Time.deltaTime * Time.deltaTime);
			}
		}

		StartCoroutine(Transition(canvas));
	}



	void Start()
	{
		Time.timeScale = 1;
		canvas = new GameObject[image[0].width * image[0].height];
		Pixels = new Color[image[0].width * image[0].height];

		if (!StaticCamera)
		{
			StartCoroutine(MoveCamera());
		}

		if (templateObj)
		{
			if (SlowGenerate)
			{
				StartCoroutine(SlowGen());
			}
			else
			{
				Generate();
			}
		}
	}

	IEnumerator ChangePixel(GameObject go, int iterator, float t, Texture2D image)
	{
		yield return new WaitForSeconds(Time.deltaTime * drawSpeed);

		int x = iterator / image.height;
		int y = image.width - 1 + (iterator % image.height);

		Color endColour = image.GetPixel(x, y);
		float r = LinearInterpolation.LinearInterpolation.Lerp(currentCanvas[iterator].r, endColour.r, t);
		float g = LinearInterpolation.LinearInterpolation.Lerp(currentCanvas[iterator].g, endColour.g, t);
		float b = LinearInterpolation.LinearInterpolation.Lerp(currentCanvas[iterator].b, endColour.b, t);

		go.GetComponent<MeshRenderer>().material.color = new Color(r, g, b);
	}

	string rM = "";


	Vector3 oldX = Vector3.zero;
	Vector3 oldY = Vector3.zero;
	Vector3 oldZ = Vector3.zero;

	void RotateM(GameObject go, float angle)
	{
		float angleD = angle * Mathf.Deg2Rad;

		// Transform Position
		Matrix mQ = Matrix.Rotation(angleD, "z");

		Matrix vT = new MathU.Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z).ToColumn();
		Matrix vR = mQ * vT;

		rM = mQ.ToString();

		// Transform Rotation
		go.transform.position = vR.ToVector().ToUnity();

		Quaternion qX = MathU.Quaternions.Quaternion.Euler(-90, 0, 0);
		Quaternion qY = MathU.Quaternions.Quaternion.Euler(0, 0, 0);
		Quaternion qZ = MathU.Quaternions.Quaternion.Euler(0, 0, angleI);


		Debug.Log($"X: Mine: {qX.ToString()} \n Theirs: {Quaternion.Euler(-90, 0, 0)}");

		Debug.Log($"Z: Mine: {qZ.ToString()} \n Theirs: {Quaternion.Euler(0, 0, angleI)}");

		//Quaternion qX = Quaternion.Euler(-90, 0, 0);
		//Quaternion qY = Quaternion.Euler(0, 0, 0);
		//Quaternion qZ = Quaternion.Euler(0, 0, angleI);

		Quaternion q = qZ * qX * qY;
		go.transform.rotation = q;
	}

	void RotateQ(GameObject go, float angle)
	{

	}

	int angleI = 0;
	int oldAngle = 0;
	string output = "";

	IEnumerator Transition(GameObject[] canvas)
	{
		while (true)
		{
			yield return new WaitForSeconds(0.05f);

			float eulerAngle = angleI - oldAngle * (Mathf.PI / 180);

			if (angleI > 360)
				angleI = 0;

			if (time >= 1.0f)
			{
				time = 0f;
				GetPixelColours(image[iSwitch]);
				iSwitch++;
			}

			if (iSwitch >= image.Length)
			{
				iSwitch = 0;
			}

			time += Time.deltaTime;

			output = angleI.ToString() + "\u00B0";
			Text.GetComponent<Text>().text = $"t = {time.ToString("0.00")}\n" + output + "\n\n" + rM;

			Quaternion rotation = Quaternion.Euler(0, 0, angleI);

			//Rotate(guide, rotation);
			guide.transform.localRotation = rotation;

			for (int i = 0; i < canvas.Length; i++)
			{
				if (rotates)
					RotateM(canvas[i], angleI - oldAngle);

				//StartCoroutine(RotateQ(canvas[i], angleI));

				StartCoroutine(ChangePixel(canvas[i], i, time, image[iSwitch]));
				if (i % 100 == 0) // Pause every 100 iterations.
				{
					yield return new WaitForSeconds(0.01f);
				}
			}
			oldAngle = angleI;
			angleI += 1;

		}
	}
	void GetPixelColours(Texture2D image)
	{
		int i = 0;
		for (int x = 0; x < image.width; x++)
		{
			for (int y = 0; y < image.height; y++)
			{
				currentCanvas[i] = image.GetPixel(x, y);
				i++;
			}
		}

	}
	void Generate()
	{
		int i = 0;
		currentCanvas = new Color[image[0].width * image[0].height];
		TransformMatrices = new Matrix[currentCanvas.Length];
		for (int x = 0; x < image[0].width; x++)
		{
			for (int y = 0; y < image[0].height; y++)
			{
				currentCanvas[i] = new Color(0, 0, 0);
				canvas[i] = Instantiate(templateObj);
				canvas[i].transform.position = new Vector3(CanvasOffset.x + (x * 10f), CanvasOffset.y + (y * 10f), CanvasOffset.z);
				TransformMatrices[i] = Matrix.Identity(3);
				i++;
			}
		}

		StartCoroutine(Transition(canvas));
	}
	IEnumerator MoveCamera()
	{
		if (viewCamera)
		{
			float t = 0.0f;
			Debug.Log(Time.time);
			if (itr > 1)
			{
				itr = 0;
			}

			while (t / Duration <= 1.0f)
			{
				if (itr == 0)
				{
					Camera.main.transform.position = LinearInterpolation.LinearInterpolation.Lerp(cameraStart, cameraEnd, t / Duration);
				}
				else
				{
					Camera.main.transform.position = LinearInterpolation.LinearInterpolation.Lerp(cameraEnd, cameraStart, t / Duration);
				}
				t += Time.deltaTime;
				yield return new WaitForSeconds(Time.deltaTime);
			}
			itr++;
			StartCoroutine(MoveCamera());
		}
	}
}
