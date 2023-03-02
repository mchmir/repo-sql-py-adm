using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeFU : SimpleClass
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

		public string Unit
		{
			get
			{
				return (string)base.GetValue("Unit");
			}
			set
			{
				base.SetValue("Unit", value);
			}
		}

		public TypeFU()
		{
			this.NameTable = "TypeFU";
		}
	}
}