using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer
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

		public byte4(float3 colour)
		{
			r = (byte) colour.R;
			g = (byte) colour.G;
			b = (byte) colour.B;
			a = 1;
		}

	}
}
