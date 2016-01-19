using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
    class Vertex
    {
        public	float3  Position;
        public	float3  Normal;

		#region Constructors

		public	Vertex()
		{
			this.Position	= new float3();
			this.Normal		= new float3();
		}

		public	Vertex(float3 positionOnly)
		{
			this.Position = positionOnly;
			this.Normal = new float3();
		}

		public Vertex(float3 position, float3 normal)
		{
			this.Position = position;
			this.Normal = normal;
		}

		#endregion

    }
}
