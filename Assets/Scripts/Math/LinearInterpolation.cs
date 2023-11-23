using UnityEngine;

namespace LinearInterpolation
{
	public class LinearInterpolation
	{
		/*	Linear Interpolation
		 * 
		 * x = a + (b-a) * t
		 * 
		 * where: 
		 *	a = start
		 *	b = finish
		 *	t = percentage value between 0 -> 1
		 * 
		 */

		public static float Lerp(float Start, float Finish, float t)
		{
			return Start + (Finish - Start) * t;
		}

		// Overload: Vector3
		public static Vector3 Lerp(Vector3 Start, Vector3 Finish, float t)
		{
			return new Vector3(
				Lerp(Start.x, Finish.x, t),
				Lerp(Start.y, Finish.y, t),
				Lerp(Start.z, Finish.z, t));
		}

		public static float Extrapolate(float Start, float Finish, float t)
		{
			float dir = Finish - Start;

			return Finish + (dir * t);
		}

		// Overload: Vector3
		public static Vector3 Extrapolate(Vector3 Start, Vector3 Finish, float t)
		{
			return new Vector3(
				Extrapolate(Start.x, Finish.x, t),
				Extrapolate(Start.y, Finish.y, t),
				Extrapolate(Start.z, Finish.z, t));
		}
	}
}