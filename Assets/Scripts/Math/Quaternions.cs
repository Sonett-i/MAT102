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


		}
	}
}