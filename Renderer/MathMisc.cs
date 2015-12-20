using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer
{

	class MathMisc
	{
		#region Constants 

		public	static	readonly	float	i360 = (float) (1.0 / 360.0);
		public	static	readonly	float	i180 = (float) (1.0 / 180.0);
		
		public	static	readonly	float	fPI  = (float)Math.PI;
		public	static	readonly	float	f2PI = (float) (Math.PI*2);

		public	static	readonly	float	fPId180 = (float) (Math.PI / 180.0);
		public	static	readonly	float	fPId360 = (float) (Math.PI / 360.0);

		#endregion

		#region Functions

		public static float Saturate(float f)
			{
				return (f < 0) ? 0 : ((f > 1) ? 1 : f);
			}

		#endregion
	}

}