using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Period : SimpleClass
	{
		public DateTime DateBegin
		{
			get
			{
				return (DateTime)base.GetValue("DateBegin");
			}
			set
			{
				base.SetValue("DateBegin", value);
			}
		}

		public DateTime DateEnd
		{
			get
			{
				return (DateTime)base.GetValue("DateEnd");
			}
			set
			{
				base.SetValue("DateEnd", value);
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

		public override string Name
		{
			get
			{
				string str = this.Month.ToString();
				int year = this.Year;
				return string.Concat(str, ".", year.ToString());
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

		public Period()
		{
			this.NameTable = "Period";
		}
	}
}