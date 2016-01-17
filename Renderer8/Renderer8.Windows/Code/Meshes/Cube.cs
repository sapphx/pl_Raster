using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer.Meshes
{
	class Cube : Mesh
	{

		public	Cube()
		{
			vSize = 8;
			tSize = 12;
			
			vertices =	new Vertex[8];
			vertices[0] = new Vertex(new float3(	-1,	1,	1	));
			vertices[1] = new Vertex(new float3(	1,	1,	1	));
			vertices[2] = new Vertex(new float3(	-1,	-1,	1	));
			vertices[3] = new Vertex(new float3(	1,	-1,	1	));
			vertices[4] = new Vertex(new float3(	-1,	1,	-1	));
			vertices[5] = new Vertex(new float3(	1,	1,	-1	));
			vertices[6] = new Vertex(new float3(	-1,	-1,	-1	));
			vertices[7] = new Vertex(new float3(	1,	-1,	-1	));
			
			indices =	new int3[12];
			indices[0]	= new int3(2, 3, 0);
			indices[1]	= new int3(1, 0, 3);
			indices[2]	= new int3(0, 1, 4);
			indices[3]	= new int3(5, 4, 1);
			indices[4]	= new int3(6, 2, 4);
			indices[5]	= new int3(0, 4, 2);
			indices[6]	= new int3(3, 2, 7);
			indices[7]	= new int3(6, 7, 2);
			indices[8]	= new int3(0, 1, 4);
			indices[9]	= new int3(5, 4, 1);
			indices[10]	= new int3(5, 7, 4);
			indices[11]	= new int3(6, 4, 7);

		}

		public	Cube(float size)
		{
			vertices =	new Vertex[8];
			vertices[0] = new Vertex(new float3(	-size,	size,	size	));
			vertices[1] = new Vertex(new float3(	size,	size,	size	));
			vertices[2] = new Vertex(new float3(	-size,	-size,	size	));
			vertices[3] = new Vertex(new float3(	size,	-size,	size	));
			vertices[4] = new Vertex(new float3(	-size,	size,	-size	));
			vertices[5] = new Vertex(new float3(	size,	size,	-size	));
			vertices[6] = new Vertex(new float3(	-size,	-size,	-size	));
			vertices[7] = new Vertex(new float3(	size,	-size,	-size	));

			indices =	new int3[12];
			indices[0]	= new int3(2, 3, 0);
			indices[1]	= new int3(1, 0, 3);
			indices[2]	= new int3(0, 1, 4);
			indices[3]	= new int3(5, 4, 1);
			indices[4]	= new int3(6, 2, 4);
			indices[5]	= new int3(0, 4, 2);
			indices[6]	= new int3(3, 2, 7);
			indices[7]	= new int3(6, 7, 2);
			indices[8]	= new int3(0, 1, 4);
			indices[9]	= new int3(5, 4, 1);
			indices[10]	= new int3(5, 7, 4);
			indices[11]	= new int3(6, 4, 7);
		}
	}
}
