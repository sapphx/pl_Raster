using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
	class int2
	{
		private int[] vals;

		#region Constructors

			public  int2 ()
			{
				vals = new int[2] { 0, 0 };
			}

			public  int2 ( int x, int y )
			{
				vals = new int[2] { x, y };
			}

		#endregion

		#region Properties

		public int X { set { vals[0] = value; } get { return vals[0]; } }
		public int Y { set { vals[1] = value; } get { return vals[1]; } }

		#endregion

		#region Operators

		public int this[int i]
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
