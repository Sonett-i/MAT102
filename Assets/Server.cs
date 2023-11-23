using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServerUtils;
using LinearInterpolation;

public class Server : MonoBehaviour
{
	[Header("Server")]
	int T = 0;
	Vector3 oldPosition;
	Vector3 networkPosition;
	Vector3 networkVelocity;
	Vector3 VelocityVector;
	double lastUpdateTime = 0;
	float lastLatency = 1f;
	float timer = 0f;

	[Header("Netcode")]
	[SerializeField] float ExtrapolationFactor = 1f;
	[SerializeField] bool isRemote = false;
	[SerializeField] Server remote;
	[SerializeField] float latencyMS = 400;
	[SerializeField] float networkUpdateIntervalMS = 200;
	[SerializeField] float remoteSpeed = 1;

	public void Receive(Packet packet, float Latency)
	{
		oldPosition = networkPosition;
		StartCoroutine(Listen(packet, Latency));
	}

	IEnumerator Listen(Packet packet, float Latency)
	{
		yield return new WaitForSeconds(packet.LatencyS);
		lastUpdateTime = NetworkClass.GetNetworkTime();
		lastLatency = Latency;
		UpdatePositions(packet);
		StartCoroutine(Timer());
	}
	void UpdatePositions(Packet packet)
	{

		oldPosition = networkPosition;
		networkPosition = packet.positionData;
		VelocityVector = networkPosition - oldPosition;
		if (T == 0)
		{
			T += 1;
			this.transform.position = networkPosition;
		}
	}

	IEnumerator Broadcast()
	{
		yield return new WaitForSeconds(networkUpdateIntervalMS / 1000);
		Packet packet = new Packet(latencyMS, this.gameObject, this.transform.position);
		ServerUtilities.Send(packet, GameObject.Find("Remote").GetComponent<Server>(), 1);
		StartCoroutine(Broadcast());
	}

	IEnumerator Timer()
	{
		timer = 0;
		while (timer < lastLatency)
		{
			timer += Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}


	void HandleMovement()
	{
		Vector3 estimatedPosition = LinearInterpolation.LinearInterpolation.Extrapolate(oldPosition, networkPosition + VelocityVector, (1 + lastLatency) * ExtrapolationFactor);
		transform.position = LinearInterpolation.LinearInterpolation.Lerp(oldPosition, estimatedPosition, Time.deltaTime * remoteSpeed);
	}

	private void FixedUpdate()
	{
		HandleMovement();
	}

	private void Start()
	{
		if (!isRemote)
		{
			StartCoroutine(Broadcast());
		}
		else
		{
			remoteSpeed = 2f;
		}
	}


}

//