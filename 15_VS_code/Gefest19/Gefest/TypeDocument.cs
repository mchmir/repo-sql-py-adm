using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeDocument : SimpleClass
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

		public TypeDocument()
		{
			this.NameTable = "TypeDocument";
		}
	}
}