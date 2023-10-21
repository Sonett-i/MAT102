using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServerUtils
{
	public class ServerUtilities
	{
		public static float Latency(float latency, float NetworkFuzz) // returns in ms
		{
			latency += Random.Range(0.0f, NetworkFuzz);
			return latency;
		}

		public static void Send(Packet packet, Server receiver, int PacketLoss)
		{
			if (Random.Range(0,100) <= PacketLoss)
			{
				//Debug.Log("Packet Lost");
			}
			else
			{
				receiver.Receive(packet, Latency(packet.LatencyS, 0.1f));
			}
		}
	}

	public class Packet
	{
		public float LatencyS;
		public GameObject Sender;
		public Vector3 positionData;
		public Vector3 velocityData;

		public Packet(float latencyMS, GameObject sender, Vector3 positionData)
		{
			LatencyS = latencyMS/1000;
			Sender = sender;
			this.positionData = positionData;
		}
	}

	public class NetworkClass
	{
		public static double GetNetworkTime()
		{
			return System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
		}
	}
}

