using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
	class DirectionalLight : Light
	{
		
		#region Constructors

			public  DirectionalLight() : base() { }

			public DirectionalLight(float3 position, float3 ambient, float3 diffuse, float3 specular, float shininess) : base(position, ambient, diffuse, specular, shininess) { }

		#endregion

		public	override	float3	Calculate(Fragment fragment, VertexProcessor vertexProcessor)
		{
			return	new float3();
			//TODO
		}
	}
}
