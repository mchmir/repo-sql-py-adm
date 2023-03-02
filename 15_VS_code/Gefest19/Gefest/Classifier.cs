using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Classifier : SimpleClass
	{
		public int IDParent
		{
			get
			{
				return (int)base.GetValue("IDParent");
			}
			set
			{
				base.SetValue("IDParent", value);
			}
		}

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

		public Classifier()
		{
			this.NameTable = "Classifier";
		}
	}
}