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
		public	float3	PreTransformValue;
		public	byte4	Color;

		#region Constructors

		public	Vertex()
		{
			this.Position	= new float3();
			this.Normal		= new float3();
			this.Color		= new byte4();
			this.PreTransformValue = new float3();
		}

		public	Vertex(float3 positionOnly)
		{
			this.Position = positionOnly;
			this.Normal = new float3();
			this.PreTransformValue = new float3();
			this.Color = new byte4();
		}

		public Vertex(float3 position, float3 normal)
		{
			this.Position = position;
			this.Normal = normal;
			this.PreTransformValue = new float3();
			this.Color = new byte4();
		}

		public Vertex ( float3 position, float3 normal, float3 pretransformValue )
		{
			this.Position = position;
			this.Normal = normal;
			this.PreTransformValue = pretransformValue;
			this.Color = new byte4();
		}

		#endregion

	}
}
