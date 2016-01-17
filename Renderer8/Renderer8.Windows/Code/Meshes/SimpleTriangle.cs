using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer.Meshes
{
	class SimpleTriangle : Mesh
	{
		public	SimpleTriangle() 
		{
			vSize = 3;
			tSize = 1;
			vertices	= new Vertex[3] { new Vertex(new float3(-.5f, 0f, 0f)), new Vertex(new float3(0f, .5f, 0f)), new Vertex(new float3(.5f, 0f, 0f)) };
			indices		= new int3[1] { new int3(0,1,2) };
		}
	}
}
