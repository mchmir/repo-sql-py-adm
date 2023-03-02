using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeGobject : SimpleClass
	{
		public string Name
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

		public TypeGobject()
		{
			this.NameTable = "TypeGobject";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}