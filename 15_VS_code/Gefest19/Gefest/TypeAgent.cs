using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeAgent : SimpleClass
	{
		public TypeAgent()
		{
			this.NameTable = "TypeAgent";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}