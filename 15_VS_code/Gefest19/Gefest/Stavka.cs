using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Stavka : SimpleClass
	{
		public DateTime Date
		{
			get
			{
				return (DateTime)base.GetValue("Date");
			}
			set
			{
				base.SetValue("Date", value);
			}
		}

		public Stavka()
		{
			this.NameTable = "Stavka";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}