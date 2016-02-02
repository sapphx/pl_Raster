using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
	class byte4
	{
		public	byte r;
		public	byte g;
		public	byte b;
		public	byte a;
		
		public byte4()
		{
			r = g = b = a = 1;
		}

		public byte4(byte value4all)
		{
			r = g = b = a = value4all;
		}

		public byte4 ( byte x, byte y, byte z, byte w )
		{
			r = x;
			g = y;
			b = z;
			a = w;
		}

		public byte4 ( int x, int y, int z, int w )
		{
			r = (byte) x;
			g = (byte) y;
			b = (byte) z;
			a = (byte) w;
		}

		public byte4 (float3 colour)
		{
			r = (byte) colour.R;
			g = (byte) colour.G;
			b = (byte) colour.B;
			a = 1;
		}

		public byte4 ( float4 colour )
		{
			r = (byte)colour.R;
			g = (byte)colour.G;
			b = (byte)colour.B;
			a = (byte)colour.A;
		}

		public static byte4 operator * ( byte4 a, byte4 b )
		{
			return new byte4(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}

		public static byte4 operator * ( byte4 a, byte b )
		{
			return new byte4(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		public static byte4 operator * ( byte4 a, float b )
		{
			return new byte4((byte) (a.r * b), (byte) (a.g * b), (byte) (a.b * b), (byte) (a.a * b));
		}

		public static byte4 operator - ( byte4 a, byte4 b )
		{
			return new byte4(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
		}

		public static byte4 operator + ( byte4 a, byte4 b )
		{
			return new byte4(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
		}
	}
}
