using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypePB : SimpleClass
	{
		public TypePB()
		{
			this.NameTable = "TypePB";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}