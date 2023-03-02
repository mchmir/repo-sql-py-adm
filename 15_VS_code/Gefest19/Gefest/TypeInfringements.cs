using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeInfringements : SimpleClass
	{
		public TypeInfringements()
		{
			this.NameTable = "TypeInfringements";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}