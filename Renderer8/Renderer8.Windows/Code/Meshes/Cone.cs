using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
	class Cone : Mesh
	{
		int		slices; // >=3
		float	width;
		float	height;

		public	Cone()
		{
			slices = 6;
			width = 1;
			height = 1;

			this.position = new float3(0,0,0);
			this.rotation = new float4(0,.5f,-0.5f,0);

			vSize = slices+2;
			tSize = 2*slices;
			vertices = new Vertex[vSize];
			indices = new int3[tSize];

			VertexProcessor vp = new VertexProcessor();
			float4x4 o2w;

			vp.SetIdentity();
			vp.MultByRot(new float4(0,1,0, 360.0f / slices ));

			o2w = vp.obj2world;
			float3 rotationPosition = new float3(width, 0, 0);

			vertices[0] = new Vertex(new float3(0, 0, 0));
			for (int i = 1; i < vSize - 1; i++)
			{
				vertices[i] = new Vertex(rotationPosition);
				rotationPosition = VertexProcessor.TransformCoordinates(rotationPosition, o2w); ///rotationPosition * o2w;
			}
			vertices[vSize - 1] = new Vertex(new float3(0, height, 0));

			indices[0] = new int3(0, slices, 1);
			indices[tSize - 1] = new int3(vSize - 1, slices, 1);
			for (int i = 1; i < tSize-1; i++)
			{
				if (i < slices)
				{
					indices[i] = new int3(0, i, i + 1);
				}
				else
				{
					indices[i] = new int3(vSize - 1, i - slices + 1, i - slices + 2);
				}
			}

		}

		//public  Cone ( float3 position, float4 rotation)
		//{
		//	this.position = position;
		//	this.rotation = rotation;
		//}
	}
}
