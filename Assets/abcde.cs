using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abcde : MonoBehaviour
{
    RectTransform rT;
    public Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        rT = this.GetComponent<RectTransform>();
        pos = rT.position;
    }

    // Update is called once per frame
    void Update()
    {
        rT.position = pos;
    }
}
