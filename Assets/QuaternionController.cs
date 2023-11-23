using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathU;
public class QuaternionController : MonoBehaviour
{
	[Header("Quaternion")]
    [SerializeField] float w = 0f;
    [SerializeField] float i = 0f;
    [SerializeField] float j = 0f;
    [SerializeField] float k = 0f;

    Quaternions.Quaternion quaternion;
    Quaternions.Quaternion Q = new Quaternions.Quaternion(1, 0, 2, 0);

    // Start is called before the first frame update
    void Start()
    {
        quaternion = new Quaternions.Quaternion(w, i, j, k);
    }

    // Update is called once per frame
    void Update()
    {
        quaternion = new Quaternions.Quaternion(w, i, j, k);
        Debug.Log(quaternion.ToString() + " + " + Q.ToString() + " = " + (quaternion+Q).ToString());
    }

}
