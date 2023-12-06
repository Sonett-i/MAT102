using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MAT102.Colliders;

public class Rect2D : MonoBehaviour
{
    MeshFilter meshFilter;
    Vector3[] vertices;

    BoxCollision2D boxCollision;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        vertices = meshFilter.mesh.vertices;

        //boxCollision = new BoxCollision2D(vertices);

    }

    // Update is called once per frame
    void Update()
    {
        boxCollision.UpdateGlobalVertices(this.transform.position);
        Debug.Log(boxCollision.Points(WorldSpace.local) + "\nWORLD\n" + boxCollision.Points(WorldSpace.world));
    }
}
