using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ServerUtils;

public class Player : MonoBehaviour
{

	[Header("Netcode")]
	[SerializeField] float latencyMS = 400;
	[SerializeField] float networkUpdateIntervalMS = 200;
	[SerializeField] int packetLossPercent = 1;

	[Header("PlayerControls")]
	[SerializeField] float Speed = 3f;

	Server server;

	

	Vector3 CurrentPosition = new Vector3(0, 0, 0);
	Vector3 CurrentVelocity = new Vector3(0, 0, 0);
	float timer = 0;
	bool disconnected = false;
	[SerializeField] float disconnectTimer = 5f;
	IEnumerator NetworkSend()
	{
		if (timer > disconnectTimer)
		{
			disconnected = true;
			Debug.Log("Disconnected");
		}
		yield return new WaitForSeconds(networkUpdateIntervalMS/1000);

		timer += networkUpdateIntervalMS/1000;
		Packet packet = new Packet(latencyMS, this.gameObject, this.transform.position);
		if (!disconnected)
		{
			ServerUtilities.Send(packet, server, packetLossPercent);
		}
		
		StartCoroutine(NetworkSend());
	}

	void HandleMovement()
	{
		CurrentVelocity= new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * Time.deltaTime * Speed;
		this.transform.position += CurrentVelocity;
	}

	private void Start()
	{
		server = GameObject.Find("Server").GetComponent<Server>();
		StartCoroutine(NetworkSend());
	}


	private void Update()
	{
		HandleMovement();
	}
}
