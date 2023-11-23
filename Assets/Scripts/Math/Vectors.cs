using System;
using MathU.Matrices;

namespace MathU
{
	public struct Vector3
	{
		public double x;
		public double y;
		public double z;

		public Vector3(double _x, double _y, double _z)
		{
			x = _x;
			y = _y;
			z = _z;
		}

		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3 operator /(Vector3 a, float divisor)
		{
			if (divisor == 0)
			{
				return new Vector3(0, 0, 0);
			}
			return new Vector3(a.x / divisor, a.y / divisor, a.z / divisor);
		}

		public static Vector3 operator *(Vector3 a, float scalar)
		{
			return new Vector3(a.x * scalar, a.y * scalar, a.z * scalar);
		}

		public float Magnitude()
		{
			return (float)Operations.Sqrt((x * x) + (y * y) + (z * z));
		}

		public static Vector3 Distance(Vector3 Origin, Vector3 Destination)
		{
			return new Vector3(Destination.x - Origin.x, Destination.y - Origin.y, Destination.z - Origin.z);
		}

		public Vector3 Normalized()
		{
			return new Vector3(this.x / this.Magnitude(), this.y / this.Magnitude(), this.z / this.Magnitude());
		}

		public static double Dot(Vector3 left, Vector3 right)
		{
			double result = 0;

			result += left.x * right.x;
			result += left.y * right.y;
			result += left.z * right.z;

			return result;
		}
		public static Vector3 Cross(Vector3 left, Vector3 right)
		{
			Vector3 result = new Vector3();
			result.x = (left.y * right.z) - (left.z * right.y);
			result.y = (left.z * right.x) - (left.x * right.z);
			result.z = (left.x * right.y) - (left.y * right.x);

			return result;
		}

		public Matrix ToColumn()
		{
			Matrix columnVector = new Matrix(3, 1);
			columnVector.data[0, 0] = this.x;
			columnVector.data[1, 0] = this.y;
			columnVector.data[2, 0] = this.z;

			return columnVector;
		}

		public Matrix ToRow()
		{
			Matrix columnVector = new Matrix(1, 3);
			columnVector.data[0, 0] = this.x;
			columnVector.data[0, 1] = this.y;
			columnVector.data[0, 2] = this.z;

			return columnVector;
		}

		public Vector3 Unit()
		{
			Vector3 UnitV = new Vector3();

			UnitV.x = this.x / Magnitude();
			UnitV.y = this.y / Magnitude();
			UnitV.z = this.z / Magnitude();

			return UnitV;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>String representation of Vector3 x y z</returns>
		public override string ToString()
		{
			return new string($"{x}, {y}, {z}");
		}
	}
}
