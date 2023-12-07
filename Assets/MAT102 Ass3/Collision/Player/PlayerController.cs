using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MAT102.Colliders;

public class PlayerController : MonoBehaviour
{
    Vector3 position;

    [SerializeField] float moveSpeed = 3f;
    public BoxCollision2D boxCollision;

    bool colliding = false;
    // Start is called before the first frame update
    void Start()
    {

        boxCollision = new BoxCollision2D(this.GetComponent<MeshFilter>().mesh.vertices, this.transform.position);
        Debug.Log(boxCollision.Points(WorldSpace.local));
    }

    void HandleMovement()
	{
        position.x = Input.GetAxisRaw("Horizontal");
        position.z = Input.GetAxisRaw("Vertical");
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
                Debug.Log($"Overlap: {overlapAmount}");
            }
            else
			{
                colGos[i].GetComponent<PlaneController>().colliding = false;
			}
        }

    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        boxCollision.UpdateGlobalVertices(this.transform.position);
        boxCollision.UpdatePosition(this.transform.position);
        boxCollision.Update(this.GetComponent<MeshFilter>().mesh.vertices, this.transform.position);

        CheckCollision();
    }

	void FixedUpdate()
	{
        this.transform.position = position * Time.deltaTime * moveSpeed;
    }
}
