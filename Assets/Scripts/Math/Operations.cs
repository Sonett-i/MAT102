using System;
public abstract class Operations
{
	float Sqrt(float number)
	{
		return 0;
	}

	public static float Floor(float x)
	{
		return (float)(int)x;
	}

	public static double Square(double x)
	{
		return (x * x);
	}

	public static double Pow(double x, int pow)
	{
		double y = x;
		int i = 1;

		while (i++ < pow)
		{
			y *= x;
		}
		return y;
	}

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

	public static double Rad(double x)
	{
		return x * (Math.PI / 180);
	}
}