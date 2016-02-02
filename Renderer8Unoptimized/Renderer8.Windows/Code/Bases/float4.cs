using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
	class float4
	{

		private float[] vals;

		#region Constructors

		public  float4 ()
		{
			vals = new float[4] { 0, 0, 0, 1 };
		}

		public  float4 ( float x, float y, float z, float w )
		{
			vals = new float[4] { x, y, z, w };
		}

		public float4 ( float3 f )
		{
			vals = new float[4] { f.X, f.Y, f.Z, 1 };
		}

		#endregion

		#region Properties

		public float X { set { vals[0] = value; } get { return vals[0]; } }
		public float Y { set { vals[1] = value; } get { return vals[1]; } }
		public float Z { set { vals[2] = value; } get { return vals[2]; } }
		public float W { set { vals[3] = value; } get { return vals[3]; } }

		public float R { set { vals[0] = value; } get { return vals[0]; } }
		public float G { set { vals[1] = value; } get { return vals[1]; } }
		public float B { set { vals[2] = value; } get { return vals[2]; } }
		public float A { set { vals[3] = value; } get { return vals[3]; } }

		#endregion

		#region Operators

		public float this[int i]
		{
			get { return vals[i]; }
			set { vals[i] = value; }
		}

		public override string ToString ()
		{
			return vals[0].ToString() + " " + vals[1].ToString() + " " + vals[2].ToString() + " " + vals[3].ToString();
		}

		//float4

		public static float4 operator - ( float4 a, float4 b )
		{
			return new float4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
		}

		public static float4 operator + ( float4 a, float4 b )
		{
			return new float4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
		}

		public static float4 operator * ( float4 a, float4 b )
		{
			return new float4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
		}

		public static float4 operator / ( float4 a, float4 b )
		{
			return new float4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
		}

		//floats

		public static float4 operator - ( float4 a, float b )
		{
			return new float4(a.X - b, a.Y - b, a.Z - b, a.W - b);
		}

		public static float4 operator + ( float4 a, float b )
		{
			return new float4(a.X + b, a.Y + b, a.Z + b, a.W + b);
		}

		public static float4 operator * ( float4 a, float b )
		{
			return new float4(a.X * b, a.Y * b, a.Z * b, a.W * b);
		}

		public static float4 operator / ( float4 a, float b )
		{
			return new float4(a.X / b, a.Y / b, a.Z / b, a.W / b);
		}

		public static float4 operator - ( float b, float4 a )
		{
			return new float4(a.X - b, a.Y - b, a.Z - b, a.W - b);
		}

		public static float4 operator + ( float b, float4 a )
		{
			return new float4(a.X + b, a.Y + b, a.Z + b, a.W + b);
		}

		public static float4 operator * ( float b, float4 a )
		{
			return new float4(a.X * b, a.Y * b, a.Z * b, a.W * b);
		}

		public static float4 operator / ( float b, float4 a )
		{
			return new float4(a.X / b, a.Y / b, a.Z / b, a.W / b);
		}


		#endregion

		#region Functions

		public void Saturate ()
		{
			vals[0] = (vals[0] < 0) ? 0 : ((vals[0] > 1) ? 1 : vals[0]);
			vals[1] = (vals[1] < 0) ? 0 : ((vals[1] > 1) ? 1 : vals[1]);
			vals[2] = (vals[2] < 0) ? 0 : ((vals[2] > 1) ? 1 : vals[2]);
			vals[3] = (vals[3] < 0) ? 0 : ((vals[3] > 1) ? 1 : vals[3]);
		}

		public float Dot ( float4 v )
		{
			return (vals[0] * v.X + vals[1] * v.Y + vals[2] * v.Z);
		}

		public float4 Lerp ( float4 v, float t )
		{
			return new float4(vals[0] + t * (v.X - vals[0]), vals[1] + t * (v.Y - vals[1]), vals[2] + t * (v.Z - vals[2]), vals[3] + t * (v.W - vals[3]));
		}

		public byte4 ToByte4 ()
		{
			return new byte4(this);
		}

		#endregion

	}
}
