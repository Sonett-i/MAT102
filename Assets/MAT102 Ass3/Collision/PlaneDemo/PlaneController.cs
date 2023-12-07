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
    public BoxCollision2D boxCollision;

    public bool colliding = false;

    public Vector3 position;
    public float speed = 1;

    GameObject player;
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

        //player = GameObject.FindWithTag("Player");
    }
    
    void CheckCollision()
	{
        GameObject[] colGos = GameObject.FindGameObjectsWithTag("Plane");

        for (int i = 0; i < colGos.Length; i++)
        {
            colliding = colGos[i].GetComponent<PlaneController>().boxCollision.Colliding(boxCollision);
            float overlapAmount = boxCollision.Collision(colGos[i].GetComponent<PlaneController>().boxCollision);


            if (colliding)
            {
                colGos[i].GetComponent<PlaneController>().colliding = true;
                colGos[i].GetComponent<PlaneController>().position -= boxCollision.PushDirection(colGos[i].GetComponent<PlaneController>().boxCollision);
                colGos[i].GetComponent<PlaneController>().speed = overlapAmount;
                Debug.Log($"Overlap: {overlapAmount}");
            }
            else
            {
                colGos[i].GetComponent<PlaneController>().colliding = false;
            }
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

	void FixedUpdate()
	{
        this.transform.position += position * (Time.deltaTime * speed); 
	}
}
