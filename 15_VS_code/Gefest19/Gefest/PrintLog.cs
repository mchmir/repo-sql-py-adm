using System;
using WebSecurityDLL;

namespace Gefest
{
	public class PrintLog : SimpleClass
	{
		public DateTime DatePrintLog
		{
			get
			{
				return (DateTime)base.GetValue("DatePrintLog");
			}
			set
			{
				base.SetValue("DatePrintLog", value);
			}
		}

		public int IDUser
		{
			get
			{
				return (int)base.GetValue("IDUser");
			}
			set
			{
				base.SetValue("IDUser", value);
			}
		}

		public string Note
		{
			get
			{
				return (string)base.GetValue("Note");
			}
			set
			{
				base.SetValue("Note", value);
			}
		}

		public int TypePrintLog
		{
			get
			{
				return (int)base.GetValue("TypePrintLog");
			}
			set
			{
				base.SetValue("TypePrintLog", value);
			}
		}

		public PrintLog()
		{
			this.NameTable = "PrintLog";
			base.Init();
		}
	}
}