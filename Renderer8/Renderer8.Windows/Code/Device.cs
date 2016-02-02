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
		private readonly float[] depthBuffer;
		private WriteableBitmap bmp;

		int pixelWidth;
		int pixelHeight;

		private VertexProcessor vp;

		Light	light1;
		byte4[] Colours;

		#region Constructors

		public	Device(WriteableBitmap bmp)
		{
			this.bmp = bmp;
			pixelWidth = bmp.PixelWidth;
			pixelHeight = bmp.PixelHeight;
			backBuffer = new byte[pixelWidth * pixelHeight * 4];
			depthBuffer = new float[pixelWidth * pixelHeight];
			vp = new VertexProcessor();
			Colours = new byte4[] { new byte4(255, 0, 0, 0), new byte4(0, 255, 0, 0), new byte4(0, 0, 255, 0), new byte4(255, 255, 0, 0), new byte4(0, 255, 255, 0), new byte4(255, 255, 255, 0), new byte4(127, 127, 127, 0) };
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

			for (var index = 0; index < depthBuffer.Length; index++)
			{
				depthBuffer[index] = float.MaxValue;
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

		public void PutPixel(int x, int y, float z, byte4 color)
		{
			var index = (x + y * pixelWidth);

			//backBuffer[index]		= (byte)(color.b * 255);
			//backBuffer[index + 1]	= (byte)(color.g * 255);
			//backBuffer[index + 2]	= (byte)(color.r * 255);
			//backBuffer[index + 3]	= (byte)(color.a * 255);

			if (depthBuffer[index] < z)
			{
				return; // Discard
			}

			depthBuffer[index] = z;

			index *= 4;

			backBuffer[index]		= color.b;
			backBuffer[index + 1]	= color.g;
			backBuffer[index + 2]	= color.r;
			backBuffer[index + 3]	= color.a;
		}

		public float3 Project ( float3 coord, float4x4 transMat )
		{
			
			float3 point = VertexProcessor.TransformCoordinates(coord, transMat);

			return new float3(point.X * pixelWidth + pixelWidth * 0.5f, -point.Y * pixelHeight + pixelHeight * 0.5f, point.Z);
		}

		public void DrawPoint ( float3 point, float4 color )
		{
			// Clipping what's visible on screen
			if (point.X >= 0 && point.Y >= 0 && point.X < pixelWidth && point.Y < pixelHeight)
			{

				PutPixel((int)point.X, (int)point.Y, point.Z, (color * 255).ToByte4());//new float4(1.0f, 1.0f, 0.0f, 1.0f).ToByte4());
			}
		}

		public void DrawPoint ( int2 point, float z, byte4 color )
		{
			// Clipping what's visible on screen
			if (point.X >= 0 && point.Y >= 0 && point.X < pixelWidth && point.Y < pixelHeight)
			{

				PutPixel(point.X, point.Y, z, (color * 255));//.ToByte4());//new float4(1.0f, 1.0f, 0.0f, 1.0f).ToByte4());
																	   //new byte4((byte) (z * 120), 0, 0, 1));//
			}
		}

		public void DrawPoint ( int pX, int pY, float z, byte4 color )
		{
			// Clipping what's visible on screen
			if (pX >= 0 && pY >= 0 && pX < pixelWidth && pY < pixelHeight)
			{

				PutPixel(pX, pY, z, color);//.ToByte4());//new float4(1.0f, 1.0f, 0.0f, 1.0f).ToByte4());
															 //new byte4((byte) (z * 120), 0, 0, 1));//
			}
		}

		//	public void DrawBline ( float2 point0, float2 point1, float4 color )
		//	{
		//		int x0 = (int)point0.X;
		//		int y0 = (int)point0.Y;
		//		int x1 = (int)point1.X;
		//		int y1 = (int)point1.Y;
		//
		//		var dx = Math.Abs(x1 - x0);
		//		var dy = Math.Abs(y1 - y0);
		//		var sx = (x0 < x1) ? 1 : -1;
		//		var sy = (y0 < y1) ? 1 : -1;
		//		var err = dx - dy;
		//
		//		while (true)
		//		{
		//			DrawPoint(new float2(x0, y0), color);
		//
		//			if ((x0 == x1) && (y0 == y1)) break;
		//			var e2 = 2 * err;
		//			if (e2 > -dy) { err -= dy; x0 += sx; }
		//			if (e2 < dx) { err += dx; y0 += sy; }
		//		}
		//	}

		void ProcessScanLine ( int y, Vertex pa, Vertex pb, Vertex pc, Vertex pd, byte4 color, float nDotL )
		{
			float gradient1 = pa.Position.Y != pb.Position.Y ? (y - pa.Position.Y) / (pb.Position.Y - pa.Position.Y) : 1;
			float gradient2 = pc.Position.Y != pd.Position.Y ? (y - pc.Position.Y) / (pd.Position.Y - pc.Position.Y) : 1;

			int sx = (int) MathMisc.Interpolate(pa.Position.X, pb.Position.X, gradient1);
			int ex = (int) MathMisc.Interpolate(pc.Position.X, pd.Position.X, gradient2);

			float z1 = MathMisc.Interpolate(pa.Position.Z, pb.Position.Z, gradient1);
			float z2 = MathMisc.Interpolate(pc.Position.Z, pd.Position.Z, gradient2);

			byte4 n1 = MathMisc.Interpolate(pa.Color, pb.Color, gradient1);
			byte4 n2 = MathMisc.Interpolate(pc.Color, pd.Color, gradient2);

			//float normalGradient = 1 / (float)ex;

			float valueForInterpolation = 0;
			byte4 colorTemp = new byte4();
			//colorTemp = color * nDotL;

			// drawing a line from left (sx) to right (ex) 
			for (int x = sx; x < ex; x++)
			{
				//DrawPoint(new int2(x, y), MathMisc.Interpolate(z1, z2, (x - sx) / (float)(ex - sx)), color);
				valueForInterpolation = (x - sx) / (float)(ex - sx);
				///////colorTemp = (MathMisc.Interpolate(n1, n2, valueForInterpolation).NormalizeProduct() * 255).ToByte4();
				colorTemp = MathMisc.Interpolate(n1, n2, valueForInterpolation);
				//colorTemp = color * Math.Max(0, MathMisc.Interpolate(n1, n2, valueForInterpolation).NormalizeProduct().Dot((light1.Position).NormalizeProduct()));
				DrawPoint(x, y, MathMisc.Interpolate(z1, z2, valueForInterpolation), colorTemp );
				//color * Math.Max(0, MathMisc.Interpolate(n1, n2, valueForInterpolation).NormalizeProduct().Dot((light1.Position).NormalizeProduct())));
			}
		}

		public void DrawTriangle ( Vertex vv1, Vertex vv2, Vertex vv3, byte4 color )
		{
			float3 p1 = vv1.Position;
			float3 p2 = vv2.Position;
			float3 p3 = vv3.Position;

			Vertex v1 = vv1;
			Vertex v2 = vv2;
			Vertex v3 = vv3;

			if (p1.Y > p2.Y)
			{
				float3 temp = p2;
				p2 = p1;
				p1 = temp;

				Vertex tempVert = v2;
				v2 = v1;
				v1 = tempVert;
			}

			if (p2.Y > p3.Y)
			{
				float3 temp = p2;
				p2 = p3;
				p3 = temp;

				Vertex tempVert = v2;
				v2 = v3;
				v3 = tempVert;
			}

			if (p1.Y > p2.Y)
			{
				float3 temp = p2;
				p2 = p1;
				p1 = temp;

				Vertex tempVert = v2;
				v2 = v1;
				v1 = tempVert;
			}

			// Compute normal

			float3 vnFace = (v1.Normal + v2.Normal + v3.Normal) / 3;
			//vnFace = (v2.PreTransformValue - v1.PreTransformValue).Cross(v3.PreTransformValue - v1.PreTransformValue);
			float3 centerPoint = (v1.PreTransformValue + v2.PreTransformValue + v3.PreTransformValue) / 3;

			//color = (vnFace * 255).ToByte4();

			float ndotl = Math.Max(0, vnFace.NormalizeProduct().Dot((light1.Position).NormalizeProduct()));//ComputeNDotL(centerPoint, vnFace, lightPos);

			v1.Color = color * Math.Max(0, v1.Normal.NormalizeProduct().Dot((light1.Position).NormalizeProduct()));
			v2.Color = color * Math.Max(0, v2.Normal.NormalizeProduct().Dot((light1.Position).NormalizeProduct()));
			v3.Color = color * Math.Max(0, v3.Normal.NormalizeProduct().Dot((light1.Position).NormalizeProduct()));

			// Scanline

			float dP1P2, dP1P3;

			if (p2.Y - p1.Y > 0)
				dP1P2 = (p2.X - p1.X) / (p2.Y - p1.Y);
			else
				dP1P2 = 0;

			if (p3.Y - p1.Y > 0)
				dP1P3 = (p3.X - p1.X) / (p3.Y - p1.Y);
			else
				dP1P3 = 0;

			if (dP1P2 > dP1P3)
			{
				for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
				{
					if (y < p2.Y)
					{
						ProcessScanLine(y, v1, v3, v1, v2, color, ndotl);
					}
					else
					{
						ProcessScanLine(y, v1, v3, v2, v3, color, ndotl);
					}
				}
			}
			else
			{
				for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
				{
					if (y < p2.Y)
					{
						ProcessScanLine(y, v1, v2, v1, v3, color, ndotl);
					}
					else
					{
						ProcessScanLine(y, v2, v3, v1, v3, color, ndotl);
					}
				}
			}
		}

		public void Render ( Camera camera, Light light, params Mesh[] meshes )
		{

			light1 = light;

			vp.SetLookAt(camera.position, camera.target, camera.up);
			vp.SetPerspective(90f, (float)pixelWidth / pixelHeight, 0.01f, 1.0f);
			//vp.SetPerspective(0.78f, (float)pixelWidth / pixelHeight, 0.01f, 1.0f);
			//var viewMatrix = Matrix.LookAtLH(camera.Position, camera.Target, Vector3.UnitY);
			//var projectionMatrix = Matrix.PerspectiveFovRH(0.78f,
			//											   (float)pixelWidth / pixelHeight,
			//											   0.01f, 1.0f);

			int iterator = 0;

			foreach (Mesh mesh in meshes)
			{
				//iterator = 0;
				vp.SetIdentity();
				vp.MultByTrans(mesh.position);
				vp.MultByRot(mesh.rotation);

				float4x4 transformMatrix = vp.obj2world * vp.world2view * vp.view2proj;
				float4x4 worldTransform = vp.obj2world;

				vp.SetIdentity();
				vp.MultByRot(mesh.rotation);

				float4x4 normalTransform = vp.obj2world;

				//foreach (Vertex vertex in mesh.vertices)
				//{
				//	// First, we project the 3D coordinates into the 2D space
				//	float3 point = Project(vertex.Position, transformMatrix);
				//	// Then we can draw on screen
				//	DrawPoint(point, new float4(1.0f, 1.0f, 0.0f, 1.0f));
				//}

				//foreach (int3 face in mesh.indices)
				//{
				//	float3 pixelA = Project(mesh.vertices[face.A].Position, transformMatrix);
				//	float3 pixelB = Project(mesh.vertices[face.B].Position, transformMatrix);
				//	float3 pixelC = Project(mesh.vertices[face.C].Position, transformMatrix);
				//
				//	DrawBline(pixelA, pixelB);
				//	DrawBline(pixelB, pixelC);
				//	DrawBline(pixelC, pixelA);
				//}


				foreach (int3 face in mesh.indices)
				{
					float3 pixelA = Project(mesh.vertices[face.A].Position, transformMatrix);
					float3 pixelB = Project(mesh.vertices[face.B].Position, transformMatrix);
					float3 pixelC = Project(mesh.vertices[face.C].Position, transformMatrix);

					//float3 normalA = mesh.vertices[face.A].Normal;///Project(mesh.vertices[face.A].Normal, vp.obj2world);
					//float3 normalB = mesh.vertices[face.B].Normal;///Project(mesh.vertices[face.B].Normal, vp.obj2world);
					//float3 normalC = mesh.vertices[face.C].Normal;///Project(mesh.vertices[face.C].Normal, vp.obj2world);

					float3 normalA = VertexProcessor.TransformCoordinates(mesh.vertices[face.A].Normal, normalTransform);
					float3 normalB = VertexProcessor.TransformCoordinates(mesh.vertices[face.B].Normal, normalTransform);
					float3 normalC = VertexProcessor.TransformCoordinates(mesh.vertices[face.C].Normal, normalTransform);
					
					float3 worldPosA = Project(mesh.vertices[face.A].Position, worldTransform);
					float3 worldPosB = Project(mesh.vertices[face.B].Position, worldTransform);
					float3 worldPosC = Project(mesh.vertices[face.C].Position, worldTransform);

					DrawTriangle(new Vertex(pixelA, normalA, worldPosA), new Vertex(pixelB, normalB, worldPosB), new Vertex(pixelC, normalC, worldPosC), Colours[iterator]);//new byte4(255, 0, 0, 255));// (new byte4(iterator%2, 0,0,1)) * 255);
					
				}
				iterator++;
			}
		}

		#endregion
	}
}
