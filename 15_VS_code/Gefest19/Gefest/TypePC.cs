using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypePC : SimpleClass
	{
		public TypePC()
		{
			this.NameTable = "TypePC";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}