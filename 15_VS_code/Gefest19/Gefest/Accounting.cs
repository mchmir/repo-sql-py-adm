using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Accounting : SimpleClass
	{
		public int TypeAccounting
		{
			get
			{
				return (int)base.GetValue("TypeAccounting");
			}
			set
			{
				base.SetValue("TypeAccounting", value);
			}
		}

		public Accounting()
		{
			this.NameTable = "Accounting";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}