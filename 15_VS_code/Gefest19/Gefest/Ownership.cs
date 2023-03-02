using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Ownership : SimpleClass
	{
		public int Level
		{
			get
			{
				return (int)base.GetValue("Level");
			}
			set
			{
				base.SetValue("Level", value);
			}
		}

		public Ownership()
		{
			this.NameTable = "Ownership";
		}
	}
}