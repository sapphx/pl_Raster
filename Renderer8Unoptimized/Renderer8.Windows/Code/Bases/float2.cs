using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
	class float2
	{

		private float[] vals;

		#region Constructors

		public  float2 ()
		{
			vals = new float[2] { 0, 0 };
		}

		public  float2 ( float x, float y )
		{
			vals = new float[2] { x, y };
		}

		#endregion

		#region Properties

		public float X { set { vals[0] = value; } get { return vals[0]; } }
		public float Y { set { vals[1] = value; } get { return vals[1]; } }

		#endregion

		#region Operators

		public float this[int i]
		{
			get { return vals[i]; }
			set { vals[i] = value; }
		}

		public override string ToString ()
		{
			return vals[0].ToString() + " " + vals[1].ToString();
		}

		#endregion

	}
}
