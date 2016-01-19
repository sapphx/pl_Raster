using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
	class VertexProcessor
	{

		public float4x4 obj2world;
		public float4x4 world2view;
		public float4x4 view2proj;

		public VertexProcessor ()
		{
			obj2world = new float4x4();
			world2view = new float4x4();
			view2proj = new float4x4();
		}

		#region Functions

		public void SetPerspective ( float fovy, float aspect, float near, float far )
		{
			fovy *= MathMisc.fPI * MathMisc.i360;
			float f = (float)Math.Cos(fovy) / (float)Math.Sin(fovy);
			float nmf = 1 / (near - far);
			view2proj = new float4x4(new float4(f / aspect, 0, 0, 0),
												new float4(0, f, 0, 0),
												new float4(0, 0, (near + far) * nmf, -1),
												new float4(0, 0, 2 * near * far * nmf, 0));
		}

		public void SetLookAt ( float3 eye, float3 center, float3 up )
		{
			float3 f = center - eye;
			f.Normalize();
			up.Normalize();
			float3 s = f.Cross(up);
			float3 u = s.Cross(f);
			world2view = new float4x4(new float4(s[0], u[0], -f[0], 0),
										new float4(s[1], u[1], -f[1], 0),
										new float4(s[2], u[2], -f[2], 0),
										new float4(-eye[0], -eye[1], -eye[2], 1));
		}

		public void SetIdentity ()
		{
			obj2world = new float4x4(new float4(1, 0, 0, 0),
												new float4(0, 1, 0, 0),
												new float4(0, 0, 1, 0),
												new float4(0, 0, 0, 1));
		}

		public void MultByTrans ( float3 v )
		{
			float4x4 m = new float4x4(new float4(1, 0, 0, 0),
										new float4(0, 1, 0, 0),
										new float4(0, 0, 1, 0),
										new float4(v.X, v.Y, v.Z, 1));
			obj2world = m * obj2world;
		}

		public void MultByScale ( float3 s )
		{
			float4x4 m = new float4x4(new float4(s.X, 0, 0, 0),
										new float4(0, s.Y, 0, 0),
										new float4(0, 0, s.Z, 0),
										new float4(0, 0, 0, 1));
			obj2world = m * obj2world;
		}

		public void MultByRot ( float4 v4 )
		{
			float s = (float)Math.Sin(v4.W * MathMisc.fPId180);
			float c = (float)Math.Cos(v4.W * MathMisc.fPId180);
			float i1mC = 1.0f - c;
			float3 v = new float3(v4.X, v4.Y, v4.Z);
			v.Normalize();
			float4x4 m = new float4x4(new float4(v.X * v.X * i1mC + c, v.Y * v.X * i1mC + v.Z * s, v.X * v.Z * i1mC - v.Y * s, 0),
										new float4(v.X * v.Y * i1mC - v.Z * s, v.Y * v.Y * i1mC + c, v.Y * v.Z * i1mC + v.X * s, 0),
										new float4(v.X * v.Z * i1mC + v.Y * s, v.Y * v.Z * i1mC - v.X * s, v.Z * v.Z * i1mC + c, 0),
										new float4(0, 0, 0, 1));
			obj2world = m * obj2world;
		}

		public	void	MultByRot(float a, float3 v)
		{
			float s = (float) Math.Sin(a * MathMisc.fPId180);
			float c = (float) Math.Cos(a * MathMisc.fPId180);
			float i1mC = 1.0f - c;
			v.Normalize();
			float4x4 m = new float4x4 (	new float4 (v.X * v.X * i1mC + c, v.Y * v.X * i1mC + v.Z * s, v.X * v.Z * i1mC - v.Y * s, 0),
										new float4 (v.X * v.Y * i1mC - v.Z * s, v.Y * v.Y * i1mC + c, v.Y * v.Z * i1mC + v.X * s, 0),
										new float4 (v.X * v.Z * i1mC + v.Y * s, v.Y * v.Z * i1mC - v.X * s, v.Z * v.Z * i1mC + c, 0),
										new float4 (0, 0, 0, 1) );
			obj2world = m * obj2world;
		}

		public static float3 TransformCoordinates ( float3 vector, float4x4 transformation )  
		{
			float4 c = new float4();

			c.X =			(vector.X * transformation[0][0]) + (vector.Y * transformation[1][0]) + (vector.Z * transformation[2][0])	+ transformation[3][0];
			c.Y =			(vector.X * transformation[0][1]) + (vector.Y * transformation[1][1]) + (vector.Z * transformation[2][1])	+ transformation[3][1];
			c.Z =			(vector.X * transformation[0][2]) + (vector.Y * transformation[1][2]) + (vector.Z * transformation[2][2])	+ transformation[3][2];
			c.W = 1.0f /	((vector.X * transformation[0][3]) + (vector.Y * transformation[1][3]) + (vector.Z * transformation[2][3])	+ transformation[3][3]);

            return new float3( c.X * c.W, c.Y * c.W, c.Z * c.W);
        }

	#endregion

}
}
