using UnityEngine;

namespace MathU
{
	public class Math
	{
		public static decimal PI = 3.141592653589793238462643383279m;

		public static double Sqrt(double x)
		{
			double y = 0;

			int p, square, c;

			p = 0;
			do
			{
				p++;
				square = (p + 1) * (p + 1);
			}
			while (x > square);

			y = (double)p;
			c = 0;

			while (c < 10)
			{
				y = (x / y + y) / 2;
				if (y * y == x)
					return y;
				c++;
			}
			return y;
		}

		public static double Square(double x)
		{
			return (x * x);
		}

		public static double Rad(double degrees)
		{
			return degrees * ((double)PI / 180);
		}

		public static float Floor(float x)
		{
			return (float)(int)x;
		}
	}
}

