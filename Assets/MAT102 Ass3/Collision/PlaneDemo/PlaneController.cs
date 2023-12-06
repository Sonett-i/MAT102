using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MAT102.Colliders;


public class PlaneController : MonoBehaviour
{
    // Text Output
    [SerializeField] Transform parent;
    [SerializeField] GameObject[] TextObjects;
    [SerializeField] GameObject InfoTextObject;

    TMPro.TextMeshProUGUI[] textMeshArray;
    TMPro.TextMeshProUGUI infoText;


    // Collision
    BoxCollision2D boxCollision;

    void UpdateLabels()
	{
        Vector3[] points = boxCollision.GetPoints();

        textMeshArray[3].text = $"A({points[0].x.ToString("0.0")}, {points[0].z.ToString("0.0")})"; //  A
        textMeshArray[2].text = $"D({points[3].x.ToString("0.0")}, {points[3].z.ToString("0.0")})"; //  C
        textMeshArray[1].text = $"C({points[2].x.ToString("0.0")}, {points[2].z.ToString("0.0")})"; //  D
        textMeshArray[0].text = $"B({points[1].x.ToString("0.0")}, {points[1].z.ToString("0.0")})"; //  B

        infoText.text = $"{boxCollision.position}\nWidth: {boxCollision.width}\nHeight: {boxCollision.height}";
    }


    // Start is called before the first frame update
    void Start()
    {
        textMeshArray = new TextMeshProUGUI[]
        {
			TextObjects[0].GetComponent<TextMeshProUGUI>(),
            TextObjects[1].GetComponent<TextMeshProUGUI>(),
            TextObjects[2].GetComponent<TextMeshProUGUI>(),
            TextObjects[3].GetComponent<TextMeshProUGUI>(),
        };

        infoText = InfoTextObject.GetComponent<TextMeshProUGUI>();
        boxCollision = new BoxCollision2D(this.transform.Find("Plane").GetComponent<MeshFilter>().mesh.vertices, this.transform.position);
    }

    void CheckCollision()
	{
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Plane");
        bool colliding = false;

        foreach (GameObject obj in gameObjects)
		{
            colliding = obj.GetComponent<PlaneController>().boxCollision.Colliding(boxCollision);
        }

        if (colliding)
		{
            this.GetComponentInChildren<MeshRenderer>().material.color = new Color(1, 0, 0);
        }
        else
		{
            this.GetComponentInChildren<MeshRenderer>().material.color = new Color(1, 1, 1);
        }
	}
    // Update is called once per frame
    void Update()
    {
        boxCollision.UpdateGlobalVertices(this.transform.position);
        boxCollision.UpdatePosition(this.transform.position);
        boxCollision.Update(this.transform.Find("Plane").GetComponent<MeshFilter>().mesh.vertices, this.transform.position);
        UpdateLabels();
        CheckCollision();
    }
}
