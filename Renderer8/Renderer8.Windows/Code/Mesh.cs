using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer
{
    class Mesh
    {
        public	int         vSize;
        public	int         tSize;
        public	Vertex[]    vertices;
        public	int3[]      indices;
		
		#region Constructor

		public	Mesh()
		{
			vertices = new Vertex[0];
			indices = new int3[0];
			vSize = 0;
			tSize = 0;
		}

		public	Mesh(Vertex[] v, int3[] i) 
		{
			vertices = new Vertex[0]; 
			v.CopyTo(vertices, 0);
			
			indices = new int3[0];
			i.CopyTo(indices, 0);

			vSize = v.Length;
			tSize = i.Length;
		}

		#endregion

		#region Functions

			public	void	draw(Rasterizer rasterizer, VertexProcessor vProcesor);
			
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
