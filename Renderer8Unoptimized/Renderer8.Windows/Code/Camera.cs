using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
	class Camera
	{
		public float3 position;
		public float3 target;
		public float3 up;

		public Camera ()
		{
			position = new float3();
			target = new float3();
			up = new float3();
		}

		public	Camera(float3 p, float3 t, float3 u)
		{
			position = p;
			target = t;
			up = u;
		}

	}
}
