using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
	class float4x4
	{

		private float4[] vals;

		#region Constructors

		public  float4x4 ()
		{
			vals = new float4[4] { new float4(), new float4 (), new float4 (), new float4 () };
		}

		public float4x4 ( float4 x, float4 y, float4 z, float4 w )
		{
			vals = new float4[4] { x, y, z, w };
		}

		#endregion

		#region Operators

		public float4 this[int i]
		{
			get { return vals[i]; }
			set { vals[i] = value; }
		}

		public	override	string	ToString ()
		{
			return vals[0].ToString() + '\n' + vals[1].ToString() + '\n' + vals[2].ToString() + '\n' + vals[3].ToString();
		}

		public	string	ToString ( string split)
		{
			return vals[0].ToString() + split + vals[1].ToString() + split + vals[2].ToString() + split + vals[3].ToString();
		}

		//float4

		public static float4x4 operator - ( float4x4 a, float4x4 b )
		{
			return new float4x4(a[0] - b[0], a[1] - b[1], a[2] - b[2], a[3] - b[3]);
		}

		public static float4x4 operator + ( float4x4 a, float4x4 b )
		{
			return new float4x4(a[0] + b[0], a[1] + b[1], a[2] + b[2], a[3] + b[3]);
		}

		public static float4x4 operator * ( float4x4 a, float4x4 b )
		{
			float4x4 c = new float4x4();

			c[0][0] = a[0][0] * b[0][0] + a[0][1] * b[1][0] + a[0][2] * b[2][0] + a[0][3] * b[3][0];
			c[0][1] = a[0][0] * b[0][1] + a[0][1] * b[1][1] + a[0][2] * b[2][1] + a[0][3] * b[3][1];
			c[0][2] = a[0][0] * b[0][2] + a[0][1] * b[1][2] + a[0][2] * b[2][2] + a[0][3] * b[3][2];
			c[0][3] = a[0][0] * b[0][3] + a[0][1] * b[1][3] + a[0][2] * b[2][3] + a[0][3] * b[3][3];

			c[1][0] = a[1][0] * b[0][0] + a[1][1] * b[1][0] + a[1][2] * b[2][0] + a[1][3] * b[3][0];
			c[1][1] = a[1][0] * b[0][1] + a[1][1] * b[1][1] + a[1][2] * b[2][1] + a[1][3] * b[3][1];
			c[1][2] = a[1][0] * b[0][2] + a[1][1] * b[1][2] + a[1][2] * b[2][2] + a[1][3] * b[3][2];
			c[1][3] = a[1][0] * b[0][3] + a[1][1] * b[1][3] + a[1][2] * b[2][3] + a[1][3] * b[3][3];

			c[2][0] = a[2][0] * b[0][0] + a[2][1] * b[1][0] + a[2][2] * b[2][0] + a[2][3] * b[3][0];
			c[2][1] = a[2][0] * b[0][1] + a[2][1] * b[1][1] + a[2][2] * b[2][1] + a[2][3] * b[3][1];
			c[2][2] = a[2][0] * b[0][2] + a[2][1] * b[1][2] + a[2][2] * b[2][2] + a[2][3] * b[3][2];
			c[2][3] = a[2][0] * b[0][3] + a[2][1] * b[1][3] + a[2][2] * b[2][3] + a[2][3] * b[3][3];

			c[3][0] = a[3][0] * b[0][0] + a[3][1] * b[1][0] + a[3][2] * b[2][0] + a[3][3] * b[3][0];
			c[3][1] = a[3][0] * b[0][1] + a[3][1] * b[1][1] + a[3][2] * b[2][1] + a[3][3] * b[3][1];
			c[3][2] = a[3][0] * b[0][2] + a[3][1] * b[1][2] + a[3][2] * b[2][2] + a[3][3] * b[3][2];
			c[3][3] = a[3][0] * b[0][3] + a[3][1] * b[1][3] + a[3][2] * b[2][3] + a[3][3] * b[3][3];

			return c;
		}

		//floats

		public static float4x4 operator * ( float4x4 a, float b )
		{
			return new float4x4(a[0] * b, a[1] * b, a[2] * b, a[3] * b);
		}

		public static float4x4 operator / ( float4x4 a, float b )
		{
			return new float4x4(a[0] / b, a[1] / b, a[2] / b, a[3] / b);
		}

		#endregion

		#region Functions

		public float4x4 Transpose ()
		{
			return  new float4x4(	new	float4(vals[0][0],	vals[1][0], vals[2][0], vals[3][0]),
									new	float4(vals[0][1],	vals[1][1], vals[2][1], vals[3][1]),
									new	float4(vals[0][2],	vals[1][2], vals[2][2], vals[3][2]),
									new	float4(vals[0][3],	vals[1][3], vals[2][3], vals[3][3]));
		}

		#endregion

	}
}
