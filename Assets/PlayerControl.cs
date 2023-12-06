using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] GameObject playerCamera;

    Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        position = this.transform.position;
    }

    void HandleMovement()
	{
        float x = Input.GetAxisRaw("Horizontal");
        position = new Vector3(x, 0, 1);
	}
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

	void FixedUpdate()
	{
		this.transform.position += position * (Time.deltaTime * speed);
        position.x = 0;
        playerCamera.transform.position += position * (Time.deltaTime * speed);
	}
}
