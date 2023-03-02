using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeContract : SimpleClass
	{
		public TypeContract()
		{
			this.NameTable = "TypeContract";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}