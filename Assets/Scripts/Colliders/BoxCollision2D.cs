using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MAT102.Colliders
{
	public enum WorldSpace
	{
		local = 0,
		world = 1,
	}
    public class BoxCollision2D
    {
        Vector3[] localVertices = new Vector3[4];
		Vector3 A;
		Vector3 B;
		Vector3 C;
		Vector3 D;


		Vector3 gA;
		Vector3 gB;
		Vector3 gC;
		Vector3 gD;

		public Vector3 position;

		public float width;
		public float height;


		public BoxCollision2D(Vector3[] points, Vector3 position)
		{
			this.A = points[0];
			this.B = points[10];
			this.C = points[120];
			this.D = points[110];

			this.width = A.x - B.x;
			this.height = A.z - D.z;

			this.position = position;
			//Debug.Log($"A {A}\nB {B}\n C {C}\nD {D}\nWidth: {width}\nHeight: {height}");
			UpdateGlobalVertices(position);

		}

		public bool Colliding(BoxCollision2D other)
		{
			bool colliding = (
				this.position.x < other.position.x + other.width &&
				this.position.x + this.width > other.position.x &&
				this.position.z < other.position.z + other.height &&
				this.position.z + this.height > other.position.z
			);
			return colliding;
		}

		public float Collision(BoxCollision2D other)
		{
			float overlapX = Mathf.Max(0, Mathf.Min(this.position.x + this.width, other.position.x + other.width) - Mathf.Max(this.position.x, other.position.x));
			float overlapZ = Mathf.Max(0, Mathf.Min(this.position.z + this.height, other.position.z + other.height) - Mathf.Max(this.position.z, other.position.z));

			float totalOverlap = overlapX * overlapZ;

			float smallestArea = Mathf.Min(this.width * this.height, other.width * other.height);

			float normalizedOverlap = totalOverlap / smallestArea;

			return normalizedOverlap;
		}

		public Vector3 PushDirection(BoxCollision2D other)
		{
			Vector3 thisCenter = new Vector3(this.position.x + this.width / 2, 0, this.position.z + this.height / 2);
			Vector3 otherCenter = new Vector3(other.position.x + other.width / 2, 0, other.position.z + other.height / 2);

			Vector3 pushDir = thisCenter - otherCenter;

			return pushDir.normalized;
		}

		public void UpdateGlobalVertices(Vector3 position)
		{
			gA = A + position;
			gB = B + position;
			gC = C + position;
			gD = D + position;

		}

		public void UpdatePosition(Vector3 pos)
		{
			this.position = pos;
		}

		public Vector3[] GetPoints()
		{
			return new Vector3[] { A, B, C, D };
		}
		public string Points(WorldSpace space)
		{
			string output = "";

			if (space == WorldSpace.local)
			{
				output = $"A: {A} \nB: {B}\nC: {C}\nD: {D}";
			}
			else
			{
				output = $"A: {gA} \nB: {gB}\nC: {gC}\nD: {gD}";
			}

			return output;
		}

		public void Update(Vector3[] points, Vector3 position)
		{
			this.A = points[0];
			this.B = points[10];
			this.C = points[120];
			this.D = points[110];

			this.width = A.x - B.x;
			this.height = A.z - D.z;

			this.position = position;
			UpdateGlobalVertices(position);
		}
	}

}

// https://developer.mozilla.org/en-US/docs/Games/Techniques/2D_collision_detection