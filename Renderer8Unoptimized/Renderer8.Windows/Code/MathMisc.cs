using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
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

		public	static	readonly	float	f2PId180 = (float) (Math.PI * 2 / 180.0);
		public	static	readonly	float	f2PId360 = (float) (Math.PI * 2 / 360.0);
		#endregion

		#region Functions

			public static float Saturate(float f)
			{
				return (f < 0) ? 0 : ((f > 1) ? 1 : f);
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float Clamp ( float value, float min = 0, float max = 1 )
			{
				return Math.Max(min, Math.Min(value, max));
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float Interpolate ( float min, float max, float gradient )
			{
				return min + (max - min) * Clamp(gradient);
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float3 Interpolate ( float3 min, float3 max, float gradient )
			{
				return min + (max - min) * Clamp(gradient);
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static byte4 Interpolate ( byte4 min, byte4 max, float gradient )
			{
				return min + (max - min) * Clamp(gradient);
			}

		#endregion
	}

}