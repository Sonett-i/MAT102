using MathU.Matrices;
using UnityEngine;

namespace MathU
{
	public class Quaternions
	{

		public class Quaternion
		{
			
			public float i;
			public float j;
			public float k;
			public float w;


			public Quaternion(float i, float j, float k, float w)
			{
				
				this.i = i;
				this.j = j;
				this.k = k;
				this.w = w;
			}

			public Quaternion(float value)
			{

				this.i = value;
				this.j = value;
				this.k = value;
				this.w = value;
			}

			public Quaternion(UnityEngine.Quaternion q)
			{
				this.i = q.x;
				this.j = q.y;
				this.k = q.z;
				this.w = q.w;
			}

			public static Quaternion Identity
			{
				get { return new Quaternion(1, 0, 0, 0); }
			}
			public bool IsIdentity
			{
				get { return i == 0f && j == 0f && k == 0f && w == 1f; }
			}

			public static Quaternion operator +(Quaternion left, Quaternion right)
			{
				Quaternion result = new Quaternion(0, 0, 0, 0);

				result.i = left.i + right.i;
				result.j = left.j + right.j;
				result.k = left.k + right.k;
				result.w = left.w + right.w;

				return result;
			}

			public static Quaternion operator -(Quaternion left, Quaternion right)
			{
				Quaternion result = new Quaternion(0, 0, 0, 0);

				result.i = left.i + right.i;
				result.j = left.j + right.j;
				result.k = left.k + right.k;
				result.w = left.w + right.w;

				return result;
			}

			public Quaternion Conjugate()
			{
				return new Quaternion(-i, -j, -k, w);
			}

			public Quaternion Invert()
			{
				Quaternion result = new Quaternion(0);

				float lengthSq = this.LengthSquared();

				if (lengthSq != 0)
				{
					lengthSq = 1.0f / lengthSq;
					result.i = -i * lengthSq;
					result.j = -j * lengthSq;
					result.k = -k * lengthSq;
					result.w = w * lengthSq;
				}

				return result;
			}

			public float Dot(Quaternion left, Quaternion right)
			{
				float result = (left.i * right.i) + (left.j * right.j) + (left.k * right.k) + (left.w * right.w);
				return result;
			}
			public float LengthSquared()
			{
				return (i * i) + (j * j) + (k * k) + (w * w);
			}

			public float Norm()
			{
				return (float)Math.Sqrt((this.i*this.i)+(this.j*this.j)+(this.k*this.k)+(this.w * this.w));
			}

			public override string ToString()
			{
				return new string($"{w} {i} {j} {k}");
			}

			public Matrix ToColumn()
			{
				Matrix res = new Matrix(4, 1);

				res[0, 0] = this.i;
				res[1, 0] = this.j;
				res[2, 0] = this.k;
				res[3, 0] = this.w;

				return res;
			}

			public static UnityEngine.Quaternion Euler(float x, float y, float z)
			{
				x = (x * Mathf.Deg2Rad) / 2;
				y = (y * Mathf.Deg2Rad) / 2;
				z = (z * Mathf.Deg2Rad) / 2;

				float c1 = Mathf.Cos(x); //C1
				float s1 = Mathf.Sin(x); //S1

				float c2 = Mathf.Cos(y); //C2
				float s2 = Mathf.Sin(y); //S2

				float c3 = Mathf.Cos(z); //C3
				float s3 = Mathf.Sin(z); //S3

				/*
				 * x = s1 s2 c3 +c1 c2 s3
				 * y = s1 c2 c3 + c1 s2 s3
				 * z = c1 s2 c3 - s1 c2 s3
				 * w = c1 c2 c3 - s1 s2 s3
				 */

				// Permuted due to unity cardinal system.

				UnityEngine.Quaternion result = new UnityEngine.Quaternion(0,0,0,0);

				result.x = c1 * c2 * c3 + s1 * s2 * s3;
				result.y = c1 * c2 * s3 - s1 * s2 * c3;
				result.z = c1 * s2 * c3 + s1 * c2 * s3;
				result.w = s1 * c2 * c3 - c1 * s2 * s3;

				return result;
			}

		}
	}
}
