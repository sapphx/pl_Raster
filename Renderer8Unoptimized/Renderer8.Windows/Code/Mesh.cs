using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
    class Mesh
    {
        public	int         vSize;
        public	int         tSize;
		public	float3		position;
		public	float4		rotation;
        public	Vertex[]    vertices;
        public	int3[]      indices;
		
		#region Constructor

		public	Mesh()
		{
			vertices = new Vertex[0];
			indices = new int3[0];
			vSize = 0;
			tSize = 0;
			position = new float3();
			rotation = new float4();
		}

		public	Mesh(Vertex[] v, int3[] i) 
		{
			position = new float3();
			rotation = new float4();

			vertices = new Vertex[0]; 
			v.CopyTo(vertices, 0);
			
			indices = new int3[0];
			i.CopyTo(indices, 0);

			vSize = v.Length;
			tSize = i.Length;
		}

		public Mesh ( Vertex[] v, int3[] i, float3 pos, float4 rot )
		{
			position = pos;
			rotation = rot;

			vertices = new Vertex[0];
			v.CopyTo(vertices, 0);

			indices = new int3[0];
			i.CopyTo(indices, 0);

			vSize = v.Length;
			tSize = i.Length;
		}

		#endregion

		#region Functions

			public	void	draw(Rasterizer rasterizer, VertexProcessor vProcesor)
			{

			}
			
			public	void	makeNormals()
			{
				for (int i = 0; i < vSize; i++)
				{
					vertices[i].Normal = new float3();
				}

				for (int i = 0; i < tSize; i++)
				{
					float3 n =	(
									(vertices[indices[i].C].Position - vertices[indices[i].A].Position)
									.Cross
									(vertices[indices[i].B].Position - vertices[indices[i].A].Position)
								)
								.NormalizeProduct();
					vertices[indices[i].A].Normal += n;
					vertices[indices[i].B].Normal += n;
					vertices[indices[i].C].Normal += n;
				}

				for (int i = 0; i < vSize; i++)
				{
					vertices[i].Normal.Normalize();
				}
			}

		#endregion
    }
}
