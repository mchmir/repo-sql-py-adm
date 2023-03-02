using System;
using WebSecurityDLL;

namespace Gefest
{
	public class UslugiVDGO : SimpleClass
	{
		public override string Name
		{
			get
			{
				return (string)base.GetValue("Name");
			}
			set
			{
				base.SetValue("Name", value);
			}
		}

		public double Value
		{
			get
			{
				return (double)base.GetValue("Value");
			}
			set
			{
				base.SetValue("Value", value);
			}
		}

		public UslugiVDGO()
		{
			this.NameTable = "UslugiVDGO";
		}
	}
}