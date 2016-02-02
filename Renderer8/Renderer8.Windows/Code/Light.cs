using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
	class Light
	{
		public	float3	Position; 
		public	float3	Ambient;
		public	float3	Diffuse;
		public	float3	Specular;
		public	float	Shininess;

		#region Constructors

			public  Light() {}

			public	Light(float3 position, float3 ambient, float3 diffuse, float3 specular, float shininess) 
			{
				this.Position = position;
				this.Ambient = ambient;
				this.Diffuse = diffuse;
				this.Specular = specular;
				this.Shininess = shininess;
			}

		#endregion

		#region Functions

			public	virtual	float3	Calculate(Fragment fragment, VertexProcessor vertexProcessor)
			{
				return new float3();
			}

		#endregion
	}
}
