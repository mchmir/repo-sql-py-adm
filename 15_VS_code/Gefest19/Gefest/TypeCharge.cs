using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeCharge : SimpleClass
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

		public TypeCharge()
		{
			this.NameTable = "TypeCharge";
		}
	}
}