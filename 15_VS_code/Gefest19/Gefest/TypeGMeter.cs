using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeGMeter : SimpleClass
	{
		public double ClassAccuracy
		{
			get
			{
				return (double)base.GetValue("ClassAccuracy");
			}
			set
			{
				base.SetValue("ClassAccuracy", value);
			}
		}

		public int CountDigital
		{
			get
			{
				return (int)base.GetValue("CountDigital");
			}
			set
			{
				base.SetValue("CountDigital", value);
			}
		}

		public string Fullname
		{
			get
			{
				string name = base.get_Name();
				double classAccuracy = this.ClassAccuracy;
				return string.Concat(name, ", ", classAccuracy.ToString());
			}
		}

		public string Memo
		{
			get
			{
				return (string)base.GetValue("Memo");
			}
			set
			{
				base.SetValue("Memo", value);
			}
		}

		public int ServiceLife
		{
			get
			{
				return (int)base.GetValue("ServiceLife");
			}
			set
			{
				base.SetValue("ServiceLife", value);
			}
		}

		public TypeGMeter()
		{
			this.NameTable = "TypeGMeter";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}