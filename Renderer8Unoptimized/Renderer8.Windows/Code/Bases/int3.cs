using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer8
{
	class int3
	{

		private int[] vals;

		#region Constructors

		public  int3 ()
		{
			vals = new int[3] { 0, 0, 0 };
		}

		public  int3 ( int x, int y, int z )
		{
			vals = new int[3] { x, y, z };
		}

		#endregion

		#region Properties

		public int A { set { vals[0] = value; } get { return vals[0]; } }
		public int B { set { vals[1] = value; } get { return vals[1]; } }
		public int C { set { vals[2] = value; } get { return vals[2]; } }

		#endregion

		#region Operators

		public int this[int i]
		{
			get { return vals[i]; }
			set { vals[i] = value; }
		}

		public override string ToString ()
		{
			return vals[0].ToString() + " " + vals[1].ToString() + " " + vals[2].ToString();
		}

		#endregion

	}
}
