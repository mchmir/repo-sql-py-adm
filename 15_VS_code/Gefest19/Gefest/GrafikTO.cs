using System;
using WebSecurityDLL;

namespace Gefest
{
	public class GrafikTO : SimpleClass
	{
		public double Amount
		{
			get
			{
				return (double)base.GetValue("Amount");
			}
			set
			{
				base.SetValue("Amount", value);
			}
		}

		public int IDContract
		{
			get
			{
				return (int)base.GetValue("IDContract");
			}
			set
			{
				base.SetValue("IDContract", value);
			}
		}

		public int Month
		{
			get
			{
				return (int)base.GetValue("Month");
			}
			set
			{
				base.SetValue("Month", value);
			}
		}

		public int Year
		{
			get
			{
				return (int)base.GetValue("Year");
			}
			set
			{
				base.SetValue("Year", value);
			}
		}

		public GrafikTO()
		{
			this.NameTable = "GrafikTO";
			base.Init();
		}
	}
}