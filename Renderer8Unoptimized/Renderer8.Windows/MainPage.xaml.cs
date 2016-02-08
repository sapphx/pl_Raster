using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

namespace Renderer8
{
    public sealed partial class MainPage : Page
    {
		Device device;
		Mesh[] mesh = new Mesh[] {	new Cylinder(new float3(-1.4f, 0, 0)), new Cylinder(new float3(1.4f,.4f,0), 0.5f, 0.8f), 
									new Cone(new float3(0, 0, 0), new float4(0, 1, 0, 0)), 
									new Cube(new float3(0, 1f, 0), new float4(0, 1, 0, 0), .2f), 
									new Cylinder(new float3(-1.4f, -.5f, 0), 0.5f, 0.8f), new Cylinder(new float3(1.4f, -.5f, 0), 1f, 1f), 
									new Cone(new float3(0, -.5f, 0), new float4(0, 1, 1, 0)) };   
									//new Cone();//new Cube(new float3(), new float4(0,1,0,0));
		Camera camera = new Camera();
		Light light = new Light(new float3(1,0,0), new float3(1, 1, 1), new float3(1, 1, 1), new float3(1, 1, 1), 1);

		public MainPage()
        {
            this.InitializeComponent();
			mesh[3].rotation.W -= 225;
		}

		void CompositionTarget_Rendering ( object sender, object e )
		{

			int before = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;

			device.Clear(new float3(1,1,1) * 255);
			mesh[0].rotation.W += 2;
			mesh[1].rotation.W += 1;
			mesh[2].rotation.W -= 10;
			//mesh[3].rotation.W -= 1;


			device.Render(camera, light, mesh);

			device.Present();

			before = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond - before;
			textBox.Text = (1000.0f / before).ToString() + " ## " + before.ToString() ;
		}

		private void Grid_Loaded ( object sender, RoutedEventArgs e )
		{

		}

		private void Page_Loaded_1 ( object sender, RoutedEventArgs e )
		{
			
		}

		private void Page_Loaded_2 ( object sender, RoutedEventArgs e )
		{
			WriteableBitmap bmp = new WriteableBitmap(640, 480);

			device = new Device(bmp);

			frontBuffer.Source = bmp;

			camera.position = new float3(0, 0, 4.0f);
			camera.target = new float3();
			camera.up = new float3(0,1,0);

			CompositionTarget.Rendering += CompositionTarget_Rendering;
		}

		private void Page_Unloaded ( object sender, RoutedEventArgs e )
		{
			
		}
	}
}
