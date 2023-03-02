using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeEnd : SimpleClass
	{
		public TypeEnd()
		{
			this.NameTable = "TypeEnd";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}