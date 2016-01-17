using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
//using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Renderer
{
	class Device
	{
		private	byte[]	backBuffer;
		private WriteableBitmap bmp;

		#region Constructors

		public	Device(WriteableBitmap bmp)
		{
			this.bmp = bmp;
			backBuffer = new byte[bmp.PixelWidth * bmp.PixelHeight * 4];
		}

		#endregion

		#region Functions

		public	void	Clear( float3 color )
		{
			byte4	clearColorValues = new byte4(color);
			for (var index = 0; index < backBuffer.Length; index += 4)
			{
				backBuffer[index]		= clearColorValues.b;
				backBuffer[index + 1]	= clearColorValues.g;
				backBuffer[index + 2]	= clearColorValues.r;
				backBuffer[index + 3]	= clearColorValues.a;
			}
		}

		public void Present()
		{
			bmp.d	

			using (var stream = bmp.PixelBuffer.AsStream())
			{
				// writing our byte[] back buffer into our WriteableBitmap stream
				stream.Write(backBuffer, 0, backBuffer.Length);
			}
			// request a redraw of the entire bitmap
			bmp.Invalidate();
		}

# endregions
	}
}
