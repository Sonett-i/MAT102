using UnityEngine;
using System;

namespace MathU.Matrices
{
	public struct Matrix
	{
		public int rows { get; set; }
		public int cols { get; set; }

		public double[,] data { get; set; }

		public Matrix(int rows, int cols)
		{
			this.rows = rows;
			this.cols = cols;
			this.data = new double[rows, cols];
		}

		public Matrix(Quaternion quat)
		{
			this = Matrix.Identity(4);
			this[0, 0] -= 2 * (quat.y * quat.y) - 2 * (quat.z * quat.z);
			this[0, 1] = 2 * (quat.x * quat.y) + 2 * (quat.w * quat.z);
			this[0, 2] = 2 * (quat.x * quat.z) - 2 * (quat.w * quat.y);

			this[1, 0] = 2 * (quat.x * quat.y) - 2 * (quat.w * quat.z);
			this[1, 1] -= 2 * (quat.x * quat.x) - 2 * (quat.z * quat.z);
			this[1, 2] = 2 * (quat.y * quat.z) + 2 * (quat.w * quat.x);

			this[2, 0] = 2 * (quat.x * quat.z) + 2 * (quat.w * quat.y);
			this[2, 1] = 2 * (quat.y * quat.z) - 2 * (quat.w * quat.x);
			this[2, 2] -= 2 * (quat.x * quat.x) - 2 * (quat.y * quat.y);


		}

		public override string ToString()
		{
			string output = string.Empty;

			for (int x = 0; x < this.rows; x++)
			{
				for (int y = 0; y < this.cols; y++)
				{
					output += $"{this.data[x, y].ToString("0.000")}, ";
				}
				output += "\n";
			}

			return output;
		}

		// Indexer to access elements using the [int, int] syntax
		public double this[int row, int col]
		{
			get { return data[row, col]; }
			set { data[row, col] = value; }
		}

		public static Matrix operator *(Matrix A, Matrix B)
		{
			if (A.cols == B.rows)
			{
				Matrix result = new Matrix(A.rows, B.cols);
				for (int i = 0; i < A.rows; i++)
				{
					for (int j = 0; j < B.cols; j++)
					{
						double sum = 0;
						for (int k = 0; k < A.cols; k++)
						{
							sum += A.data[i, k] * B.data[k, j];
						}
						result.data[i, j] = sum;
					}
				}

				return result;
			}
			else // Abort if mutiplication not possible.
			{
				// Multiplying a vector of n dimensions
				if (A.cols == 1 && A.rows == B.rows && A.cols == B.cols)
				{
					Matrix result = new Matrix(A.rows, 1);
					for (int i = 0; i < A.rows; i++)
					{
						result.data[i, 0] = A.data[i, 0] * B.data[i, 0];
					}
					return result;
				}
				else
				{
					throw new Exception("Cannot multiply");
				}
			}
		}

		public static Matrix operator *(Matrix A, Vector2 right)
		{
			Matrix rightM = new Matrix(2, 1);
			rightM.data[0, 0] = right.x;
			rightM.data[1, 0] = right.y;
			return (A * rightM);
		}

		public static Matrix operator *(double scalar, Matrix m)
		{
			Matrix result = new Matrix(m.rows, m.cols);
			for (int i = 0; i < m.rows; i++)
			{
				for (int j = 0; j < m.cols; j++)
				{
					result.data[i, j] = scalar * m.data[i, j];
				}
			}

			return result;
		}

		public static Matrix operator *(Matrix m, double scalar)
		{
			Matrix result = new Matrix(m.rows, m.cols);
			for (int i = 0; i < m.rows; i++)
			{
				for (int j = 0; j < m.cols; j++)
				{
					result.data[i, j] = scalar * m.data[i, j];
				}
			}

			return result;
		}

		public static Matrix Transform(Vector2 left, Matrix right)
		{
			Matrix leftM = new Matrix(2, 1);
			leftM.data[0, 0] = left.x;
			leftM.data[1, 0] = left.y;

			Matrix result = new Matrix(2, right.rows);

			for (int col = 0; col < right.cols; col++)
			{
				for (int row = 0; row < right.rows; row++)
				{
					double l = leftM.data[row, 0];
					double r = right.data[row, col];
					result.data[col, row] = l * r;


				}
			}

			return result;
		}

		public static Matrix operator +(Matrix left, Matrix right)
		{
			if (left.rows == right.rows && left.cols == right.cols)
			{
				Matrix result = new Matrix(left.rows, left.cols);

				for (int i = 0; i < left.rows; i++)
				{
					for (int j = 0; j < left.cols; j++)
					{
						result.data[i, j] = left.data[i, j] + right.data[i, j]; ;
					}
				}
				return result;
			}
			else
			{
				throw new Exception("Cannot add");
			}
		}
		public static Matrix operator -(Matrix left, Matrix right)
		{
			if (left.rows == right.rows && left.cols == right.cols)
			{
				Matrix result = new Matrix(left.rows, left.cols);

				for (int i = 0; i < left.rows; i++)
				{
					for (int j = 0; j < left.cols; j++)
					{
						result.data[i, j] = left.data[i, j] - right.data[i, j];
					}
				}
				return result;
			}
			else
			{
				throw new Exception("Cannot add");
			}
		}

		public static double Dot(Matrix left, Matrix right)
		{
			double result = 0;
			if (left.cols == 1 && left.rows == right.rows && left.cols == right.cols)
			{
				for (int i = 0; i < left.rows; i++)
				{
					result = result + (left.data[i, 0] * right.data[i, 0]);
				}
			}


			return result;
		}

		public double Magnitude()
		{
			double sum = 0;

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					sum += (this.data[i, j] * this.data[i, j]);
				}
			}

			return Math.Sqrt(sum);
		}

		public Matrix Unit()
		{
			Matrix result = new Matrix(rows, cols);

			double mag = this.Magnitude();

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					result.data[i, j] = this.data[i, j] / mag;
				}
			}
			return result;
		}

		public static Matrix Identity(int n)
		{
			Matrix result = new Matrix(n, n);

			for (int i = 0; i < result.rows; i++)
			{
				for (int j = 0; j < result.cols; j++)
				{
					if (i == j)
					{
						result.data[i, j] = 1;
					}
					else
					{
						result.data[i, j] = 0;
					}
				}
			}
			return result;
		}

		public Matrix Transpose()
		{
			Matrix transposed = new Matrix(this.cols, this.rows);
			for (int row = 0; row < this.rows; row++)
			{
				for (int col = 0; col < this.cols; col++)
				{
					transposed.data[col, row] = this.data[row, col];
				}
			}

			return transposed;
		}

		public double Determinant()
		{
			return Determinant(this);
		}
		/* |a	b|
		 * |c	d|
		 * det = ad - bc
		 * 
		 */
		public double Determinant(Matrix m)
		{
			double d = 0;
			if (m.rows == 2 && m.cols == 2)
			{
				d = (m.data[0, 0] * m.data[1, 1]) - (m.data[0, 1] * m.data[1, 0]);
				return d;
			}
			else
			{
				for (int col = 0; col < m.cols; col++)
				{
					int sign = -1;
					if (col % 2 == 0)
					{
						sign = 1;
					}

					d += sign * m.data[0, col] * Determinant(m.SubMatrix(0, col));
				}
			}
			return d;
		}

		public Matrix SubMatrix(int row, int col)
		{
			Matrix result = new Matrix(rows - 1, cols - 1);

			int r = 0;
			for (int i = 0; i < rows; i++)
			{
				int k = 0;

				if (i == row)
				{
					i++;
				}
				if (i == this.rows) continue;

				for (int j = 0; j < this.cols; j++)
				{
					if (j == col)
					{
						j++;
					}

					if (j == this.cols) continue;

					result.data[r, k] = this.data[i, j];
					k++;
				}
				r++;
			}

			return result;
		}

		public static Matrix Rotate(UnityEngine.Vector3 vector)
		{
			Matrix xR = Rotation(vector.x, "x");
			Matrix yR = Rotation(vector.y, "y");
			Matrix zR = Rotation(vector.z, "z");


			return zR * yR * xR;

		}

		public static Matrix Rotate(Quaternion q)
		{
			// Precalculate coordinate products
			float x = q.x * 2.0F;
			float y = q.y * 2.0F;
			float z = q.z * 2.0F;
			float xx = q.x * x;
			float yy = q.y * y;
			float zz = q.z * z;
			float xy = q.x * y;
			float xz = q.x * z;
			float yz = q.y * z;
			float wx = q.w * x;
			float wy = q.w * y;
			float wz = q.w * z;

			// Calculate 3x3 matrix from orthonormal basis
			Matrix res = new Matrix(4, 4);

			res[0, 0] = 1.0f - (yy + zz); 
			res[0, 1] = xy + wz;
			res[0, 2] = xz - wy;
			res[0, 3] = 0f;

			//m.m01 = xy - wz; m.m11 = 1.0f - (xx + zz); m.m21 = yz + wx; m.m31 = 0.0F;
			res[1, 0] = xy - wz;
			res[1, 1] = 1.0f - (xx + zz);
			res[1, 2] = yz + wx;
			res[1, 3] = 0.0f;

			//m.m02 = xz + wy; m.m12 = yz - wx; m.m22 = 1.0f - (xx + yy); m.m32 = 0.0F;
			res[2, 0] = xz + wy;
			res[2, 1] = yz - wx;
			res[2, 2] = 1.0f - (xx + yy);
			res[2, 3] = 0;

			//m.m03 = 0.0F; m.m13 = 0.0F; m.m23 = 0.0F; m.m33 = 1.0F;
			res[3, 0] = 0;
			res[3, 1] = 0;
			res[3, 2] = 0;
			res[3, 3] = 1.0f;

			return res;
		}

		public static Matrix Rotation(float angle, string axis)
		{
			Matrix result = Matrix.Identity(3);

			if (axis == "x")
			{
				result.data[1, 1] = MathF.Cos(angle);
				result.data[1, 2] = -MathF.Sin(angle);
				result.data[2, 1] = MathF.Sin(angle);
				result.data[2, 2] = MathF.Cos(angle);
			}
			else if (axis == "y")
			{
				result.data[0, 0] = MathF.Cos(angle);
				result.data[0, 2] = MathF.Sin(angle);
				result.data[2, 0] = -MathF.Sin(angle);
				result.data[2, 2] = MathF.Cos(angle);
			}
			else if (axis == "z")
			{
				result.data[0, 0] = MathF.Cos(angle);
				result.data[0, 1] = -MathF.Sin(angle);
				result.data[1, 0] = MathF.Sin(angle);
				result.data[1, 1] = MathF.Cos(angle);
			}

			return result;
		}

		public Matrix Minors()
		{
			Matrix result = new Matrix(this.rows, this.cols);

			for (int i = 0; i < this.rows; i++)
			{
				for (int j = 0; j < this.cols; j++)
				{
					result.data[i, j] = Determinant(SubMatrix(i, j));
				}
			}
			return result;
		}

		public Matrix Cofactor()
		{
			Matrix result = new Matrix(this.rows, this.cols);
			int sign = -1;
			for (int i = 0; i < this.rows; i++)
			{
				for (int j = 0; j < this.cols; j++)
				{
					sign = (sign > 0) ? -1 : 1;
					result.data[i, j] = sign * this.data[i, j];
				}
			}

			return result;
		}

		public Matrix Inverse()
		{
			double d = this.Determinant();
			Matrix inverse = (1 / d) * this.Minors().Cofactor().Transpose();
			return inverse;
		}


		public static Matrix Project(Vector3 n, float scale)
		{
			Matrix P = Matrix.Identity(3);

			P[0, 0] += (scale - 1) * (n.x * n.x);
			P[0, 1] += (scale - 1) * (n.x * n.y);
			P[0, 2] += (scale - 1) * (n.x * n.z);

			P[1, 0] += (scale - 1) * (n.x * n.y);
			P[1, 1] += (scale - 1) * (n.y * n.y);
			P[1, 2] += (scale - 1) * (n.y * n.z);

			P[2, 0] += (scale - 1) * (n.x * n.z);
			P[2, 1] += (scale - 1) * (n.y * n.z);
			P[2, 2] += (scale - 1) * (n.z * n.z);

			return P;
		}

		public static Matrix Reflect(Vector3 n)
		{
			Matrix P = Matrix.Identity(3);

			P[0, 0] += (-1 - 1) * (n.x * n.x);
			P[0, 1] += (-1 - 1) * (n.x * n.y);
			P[0, 2] += (-1 - 1) * (n.x * n.z);

			P[1, 0] += (-1 - 1) * (n.x * n.y);
			P[1, 1] += (-1 - 1) * (n.y * n.y);
			P[1, 2] += (-1 - 1) * (n.y * n.z);

			P[2, 0] += (-1 - 1) * (n.x * n.z);
			P[2, 1] += (-1 - 1) * (n.y * n.z);
			P[2, 2] += (-1 - 1) * (n.z * n.z);

			return P;
		}

		public static Matrix Translate(Vector3 v)
		{
			Matrix result = Matrix.Identity(3);

			result[0, 0] = v.x;
			result[1, 1] = v.y;
			result[2, 2] = v.z;

			return result;
		}
		public Vector3 ToVector()
		{
			return new Vector3((float)this[0, 0], (float)this[1, 0], (float)this[2, 0]);
		}
	}
}
