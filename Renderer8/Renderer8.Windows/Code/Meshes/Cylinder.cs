using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
	class Cylinder : Mesh
	{
		int		slices; // >= 3
		int		segments; // >= 2
		float	width;
		float	height;

		public	Cylinder () : this(24, 1, 1, 1, new float3(0, 0, 0), new float4(0, 1, 0, 0))
		{

		}

		public Cylinder (float3 position) : this(24, 1, 1, 1, position, new float4(0, 1, 0, 0))
		{

		}

		public Cylinder ( float3 position, float width, float height ) : this(24, 1, width, height, position, new float4(0, 1, 0, 0))
		{

		}

		public  Cylinder (int Slices, int Segments, float Width, float Height, float3 Position, float4 Rotation)
		{
			slices = Slices;
			segments = Segments;
			width = Width;
			height = Height;

			this.position = Position;
			this.rotation = Rotation;
			//this.rotation = new float4(0, .5f, -0.5f, 0);

			vSize = (slices) * (segments + 1) + 2;
			tSize = 2 * (segments + 1) * slices;
			vertices = new Vertex[vSize];
			indices = new int3[tSize];

			VertexProcessor vp = new VertexProcessor();
			float4x4 o2w;

			vp.SetIdentity();
			vp.MultByRot(new float4(0, 1, 0, 360.0f / slices));

			o2w = vp.obj2world;
			float3 rotationPosition = new float3(width, 0, 0);

			vertices[0] = new Vertex(new float3(0, 0, 0), new float3 (0,1,0));
			float heightForSegment = height / (segments);
			for (int i = 1; i <= slices; i++)
			{
				for (int j = 0; j <= segments; j++)
				{
					vertices[i + j*slices] = new Vertex(new float3(rotationPosition.X, j*heightForSegment, rotationPosition.Z), rotationPosition);
				}
				rotationPosition = VertexProcessor.TransformCoordinates(rotationPosition, o2w);
			}
			vertices[vSize - 1] = new Vertex(new float3(0, height, 0), new float3(0, -1, 0));

			int tempI = 0;

			for (int i = 0; i < 2 + 2 * segments; i++)
			{
				//tempI = i / segments;

				for(int j = 1; j <= slices; j++)
				{
					if (i%2 == 0)
					{
						//parzyste
						//indices[i * slices + j - 1] = new int3(0, 0, 0);
						indices[i * slices + j - 1] = new int3(j + i / 2 * slices, j + i/2 * slices - slices, (j - 1)%slices + i / 2 * slices);
						if ((j - 1) % slices == 0) 
							indices[i * slices + j - 1][2] += slices;
					}
					else
					{
						//nieparzyste
						//indices[i * slices + j - 1] = new int3(0, 0, 0);
						indices[i * slices + j - 1] = new int3(j + i / 2 * slices, j + i / 2 * slices + slices, (j + 1) % slices + i / 2 * slices);
						if (((j + 1) % slices + i / 2 * slices) % slices == 0)
							indices[i * slices + j - 1][2] += slices;
					}

					if (i == 0)
					{
						//bot and first
						//indices[i * slices + j - 1] = new int3(0, 0, 0);
						indices[j - 1] = new int3(0, j, (j + 1) % slices);
						if (j == slices - 1) 
						{
							indices[j - 1][2] += slices;
						}
					}
					else if (i == 1 + 2 * segments)
					{
						//top and last
						//indices[i * slices + j - 1] = new int3(0, 0, 0);
						indices[i * slices + j - 1] = new int3(vSize - 1, vSize - 1 - (slices - j), vSize - 1 - (slices - (j + 1) % slices));
						if (j == slices)
						{
							indices[i * slices + j - 1][1] -= slices;
						}
					}
				}

				
			}

		}
	}
}
