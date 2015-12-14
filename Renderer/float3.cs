using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer
{
	class float3
	{

		private float[] vals;

		#region Constructors

		public	float3()
		{
			vals = new float[3] { 0, 0, 0 };
		}

		public	float3(float x, float y, float z)
		{
			vals = new float[3] { x, y, z};
		}

		#endregion

		#region Properties

		public	float	X { set { vals[0] = value; } get { return vals[0]; } }
		public	float	Y { set { vals[1] = value; } get { return vals[1]; } }
		public	float	Z { set { vals[2] = value; } get { return vals[2]; } }

		public	float	R { set { vals[0] = value; } get { return vals[0]; } }
		public	float	G { set { vals[1] = value; } get { return vals[1]; } }
		public	float	B { set { vals[2] = value; } get { return vals[2]; } }

		#endregion

		#region Operators

		public	float	this[int i]
		{
			get { return vals[i]; }
			set { vals[i] = value; }
		}

		public	override	string	ToString ()
		{
			return vals[0].ToString() + " " + vals[1].ToString() + " " + vals[2].ToString();
		}

		//float3

		public	static	float3	operator - (float3 a, float3 b)
		{
			return new float3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public	static	float3	operator + (float3 a, float3 b)
		{
			return new float3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public	static	float3	operator * (float3 a, float3 b)
		{
			return new float3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
		}

		public	static	float3	operator / (float3 a, float3 b)
		{
			return new float3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
		}

		//floats

		public	static	float3	operator - ( float3 a, float b )
		{
			return new float3(a.X - b, a.Y - b, a.Z - b);
		}

		public	static	float3	operator + ( float3 a, float b )
		{
			return new float3(a.X + b, a.Y + b, a.Z + b);
		}

		public	static	float3	operator * ( float3 a, float b )
		{
			return new float3(a.X * b, a.Y * b, a.Z * b);
		}

		public	static	float3	operator / ( float3 a, float b )
		{
			return new float3(a.X / b, a.Y / b, a.Z / b);
		}

		public	static	float3	operator - ( float b, float3 a )
		{
			return new float3(a.X - b, a.Y - b, a.Z - b);
		}

		public	static	float3	operator + ( float b, float3 a )
		{
			return new float3(a.X + b, a.Y + b, a.Z + b);
		}

		public	static	float3	operator * ( float b, float3 a )
		{
			return new float3(a.X * b, a.Y * b, a.Z * b);
		}

		public	static	float3	operator / ( float b, float3 a )
		{
			return new float3(a.X / b, a.Y / b, a.Z / b);
		}


		#endregion

		#region Functions

		public	void	Saturate()
		{
			vals[0] = (vals[0] < 0) ? 0 : ((vals[0] > 1) ? 1 : vals[0]);
			vals[1] = (vals[1] < 0) ? 0 : ((vals[1] > 1) ? 1 : vals[1]);
			vals[2] = (vals[2] < 0) ? 0 : ((vals[2] > 1) ? 1 : vals[2]);
		}

		public	float	Dot (float3 v)
		{
			return (vals[0] * v.X + vals[1] * v.Y + vals[2] * v.Z);
		}

		public	float3	Cross (float3 v)
		{
			return	new float3(vals[1] * v.Z - vals[2] * v.Y, vals[2] * v.X - vals[0] * v.Z, vals[0] * v.Y - vals[1] * v.X);
		}

		public	float	Length ()
		{
			return (float) Math.Sqrt(vals[0] * vals[0] + vals[1] * vals[1] + vals[2] * vals[2]);
		}

		public	float LenghtSquared ()
		{
			return (vals[0] * vals[0] + vals[1] * vals[1] + vals[2] * vals[2]);
		}

		public void Normalize ()
		{
			float n = Length();
			if (n != 0)
			{
				vals[0] = vals[0] / n;
				vals[1] = vals[1] / n;
				vals[2] = vals[2] / n;
			}

		}

		public	float3	NormalizeProduct ()
		{
			float n = Length();
			if (n != 0)
			{
				vals[0] = vals[0] / n;
				vals[1] = vals[1] / n;
				vals[2] = vals[2] / n;
			}
			return new float3(vals[0], vals[1], vals[2]);
		}

		public	float3	Reflect ( float3 normal )
		{
			return this - (2 * this.Dot(normal) * normal);
		}

		public float3 Lerp ( float3 v, float t )
		{
			return new float3(vals[0] + t * (v.X - vals[0]), vals[1] + t * (v.Y - vals[1]), vals[2] + t * (v.Z - vals[2]));
		}

		public	float3	Negate()
		{
			vals[0] = -vals[0];
			vals[1] = -vals[1];
			vals[2] = -vals[2];
			return this;
		}

		#endregion


	}
}
