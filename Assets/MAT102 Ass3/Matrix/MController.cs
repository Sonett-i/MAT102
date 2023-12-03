using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathU.Matrices;

public class MController : MonoBehaviour
{
    [SerializeField] Vector3 rotation;
    Vector3 oldRotation;
    Matrix rotationM = new Matrix(3, 3);

    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;

    [SerializeField] bool trigger;
    
	[SerializeField] List<GameObject> gameObjects = new List<GameObject>();


    void init()
	{
        oldRotation = rotation;

	}

    void Rotate()
	{
        float nX = x - oldRotation.x;
        float nY = y - oldRotation.y;
        float nZ = z - oldRotation.z;

        float rX = (float)MathU.Math.Rad(nX);
        float rY = (float)MathU.Math.Rad(nY);
        float rZ = (float)MathU.Math.Rad(nZ);

        Quaternion qX = new Quaternion(1, 0, 0, rX);
        Quaternion qY = new Quaternion(0, 1, 0, rY);
        Quaternion qZ = new Quaternion(0, 0, 1, rZ);

        Quaternion qL = qZ * qY * qX;

        Quaternion inverseQL = Quaternion.Inverse(qL);

        Debug.Log($"{qX}\n{qY}\n{qZ}\n\n{qL}");

        Matrix mQ = Matrix.Rotate(inverseQL);

        for (int i = 0; i < gameObjects.Count; i++)
        {
            GameObject go = gameObjects[i];
            MathU.Vector3 vec = new MathU.Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
            Matrix vT = vec.ToColumn();
            Matrix vR = mQ * vT;

            go.transform.position = vR.ToVector().ToUnity();
        }

        oldRotation = new Vector3(x, y, z);
    }

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        //rot();
        Rotate();

        if (trigger == true)
		{
            trigger = false;

            Rotate();
		}
    }

}
