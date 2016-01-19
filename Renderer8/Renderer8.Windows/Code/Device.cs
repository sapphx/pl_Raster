using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Drawing.Imaging;
//using System.Windows.Media.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Renderer8
{
	class Device
	{
		private	byte[]	backBuffer;
		private WriteableBitmap bmp;

		private VertexProcessor vp;

		#region Constructors

		public	Device(WriteableBitmap bmp)
		{
			this.bmp = bmp;
			backBuffer = new byte[bmp.PixelWidth * bmp.PixelHeight * 4];
			vp = new VertexProcessor();
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
			using (var stream = bmp.PixelBuffer.AsStream())
			{
				// writing our byte[] back buffer into our WriteableBitmap stream
				stream.Write(backBuffer, 0, backBuffer.Length);
			}
			// request a redraw of the entire bitmap
			bmp.Invalidate();
		}

		public void PutPixel(int x, int y, byte4 color)
		{
			var index = (x + y * bmp.PixelWidth) * 4;

			backBuffer[index]		= (byte)(color.b * 255);
			backBuffer[index + 1]	= (byte)(color.g * 255);
			backBuffer[index + 2]	= (byte)(color.r * 255);
			backBuffer[index + 3]	= (byte)(color.a * 255);
		}

		public float2 Project ( float3 coord, float4x4 transMat )
		{
			
			float3 point = VertexProcessor.TransformCoordinates(coord, transMat);

			return new float2(point.X * bmp.PixelWidth + bmp.PixelWidth * 0.5f, -point.Y * bmp.PixelHeight + bmp.PixelHeight * 0.5f);
		}

		public void DrawPoint ( float2 point )
		{
			// Clipping what's visible on screen
			if (point.X >= 0 && point.Y >= 0 && point.X < bmp.PixelWidth && point.Y < bmp.PixelHeight)
			{
				// Drawing a yellow point
				PutPixel((int)point.X, (int)point.Y, new float4(1.0f, 1.0f, 0.0f, 1.0f).ToByte4());
			}
		}

		public void DrawBline ( float2 point0, float2 point1 )
		{
			int x0 = (int)point0.X;
			int y0 = (int)point0.Y;
			int x1 = (int)point1.X;
			int y1 = (int)point1.Y;

			var dx = Math.Abs(x1 - x0);
			var dy = Math.Abs(y1 - y0);
			var sx = (x0 < x1) ? 1 : -1;
			var sy = (y0 < y1) ? 1 : -1;
			var err = dx - dy;

			while (true)
			{
				DrawPoint(new float2(x0, y0));

				if ((x0 == x1) && (y0 == y1)) break;
				var e2 = 2 * err;
				if (e2 > -dy) { err -= dy; x0 += sx; }
				if (e2 < dx) { err += dx; y0 += sy; }
			}
		}

		public void Render ( Camera camera, params Mesh[] meshes )
		{
			
			vp.SetLookAt(camera.position, camera.target, camera.up);
			vp.SetPerspective(90f, (float)bmp.PixelWidth / bmp.PixelHeight, 0.01f, 1.0f);
			//vp.SetPerspective(0.78f, (float)bmp.PixelWidth / bmp.PixelHeight, 0.01f, 1.0f);
			//var viewMatrix = Matrix.LookAtLH(camera.Position, camera.Target, Vector3.UnitY);
			//var projectionMatrix = Matrix.PerspectiveFovRH(0.78f,
			//											   (float)bmp.PixelWidth / bmp.PixelHeight,
			//											   0.01f, 1.0f);

			foreach (Mesh mesh in meshes)
			{

				vp.SetIdentity();
				//vp.MultByRot(1, new float3(0, 1, 0));
				vp.MultByRot(mesh.rotation);
				//

				//var worldMatrix = Matrix.RotationYawPitchRoll(mesh.Rotation.Y,
				//											  mesh.Rotation.X, mesh.Rotation.Z) *
				//				  Matrix.Translation(mesh.Position);

				float4x4 transformMatrix = vp.obj2world * vp.world2view * vp.view2proj;
				// worldMatrix * viewMatrix * projectionMatrix;

				//foreach (Vertex vertex in mesh.vertices)
				//{
				//	// First, we project the 3D coordinates into the 2D space
				//	float2 point = Project(vertex.Position, transformMatrix);
				//	// Then we can draw on screen
				//	DrawPoint(point);
				//}

				foreach (int3 face in mesh.indices)
				{
					float2 pixelA = Project(mesh.vertices[face.A].Position, transformMatrix);
					float2 pixelB = Project(mesh.vertices[face.B].Position, transformMatrix);
					float2 pixelC = Project(mesh.vertices[face.C].Position, transformMatrix);

					DrawBline(pixelA, pixelB);
					DrawBline(pixelB, pixelC);
					DrawBline(pixelC, pixelA);
				}
			}
		}

		#endregion
	}
}
