using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer
{
	class VertexProcessor
	{

		public	float4x4	obj2world;
		public	float4x4	world2view;
		public	float4x4	view2proj;

		#region Functions

		public	void	SetPerspective(float fovy, float aspect, float near, float far)
		{
			fovy *= MathMisc.fPI * MathMisc.i360;
			float	f = (float) Math.Cos(fovy) / (float)Math.Sin(fovy);
			float nmf = 1 / (near - far);
			float4x4 world2view = new float4x4( new float4(f / aspect, 0, 0, 0),
												new float4(0, f, 0, 0),
												new float4(0, 0, (near + far) * nmf, -1),
												new float4(0, 0, 2 * near * far * nmf, 0));
        }

		public	void	SetLookAt(float3 eye, float3 center, float3 up)
		{
			float3	f = center - eye;
			f.Normalize();
			up.Normalize();
			float3	s = f.Cross(up);
			float3	u = s.Cross(f);
			float4x4 world2view = new float4x4(	new float4(s[0], u[0], -f[0], 0),
												new float4(s[1], u[1], -f[1], 0),
												new float4(s[2], u[2], -f[2], 0),
												new float4(-eye[0], -eye[1], -eye[2], 1));
		}

		public	void	SetIdentity()
		{
			float4x4 obj2world = new float4x4(	new float4(1, 0, 0, 0),
												new float4(0, 1, 0, 0),
												new float4(0, 0, 1, 0),
												new float4(0, 0, 0, 1));
		}

		public	void	MultByTrans(float3 v)
        {
			float4x4 m = new float4x4(	new float4(1, 0, 0, 0),
										new float4(0, 1, 0, 0),
										new float4(0, 0, 1, 0),
										new float4(v.X, v.Y, v.Z, 1));
			obj2world = m * obj2world;
		}

		public	void	MultByScale(float3 s)
		{
			float4x4 m = new float4x4(	new float4(	s.X,	0,		0,		0),
										new float4(	0,		s.Y,	0,		0),
										new float4(	0,		0,		s.Z,	0),
										new float4(	0,		0,		0,		1));
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

		#endregion

	}
}
