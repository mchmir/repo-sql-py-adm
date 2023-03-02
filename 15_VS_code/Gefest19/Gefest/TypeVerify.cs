using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeVerify : SimpleClass
	{
		public TypeVerify()
		{
			this.NameTable = "TypeVerify";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}