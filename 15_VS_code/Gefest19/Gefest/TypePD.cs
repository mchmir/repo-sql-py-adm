using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypePD : SimpleClass
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

		public TypePD()
		{
			this.NameTable = "TypePD";
		}
	}
}