using System;
using WebSecurityDLL;

namespace Gefest
{
	public class PG : SimpleClass
	{
		private TypePG _typepg;

		private Period _Period;

		public bool IsDistr
		{
			get
			{
				return (bool)base.GetValue("IsDistr");
			}
			set
			{
				base.SetValue("IsDistr", value);
			}
		}

		public string Norma
		{
			get
			{
				return (string)base.GetValue("Norma");
			}
			set
			{
				base.SetValue("Norma", value);
			}
		}

		public TypePG oTypePG
		{
			get
			{
				if (this._typepg == null)
				{
					int value = (int)base.GetValue("IDTypePG");
					this._typepg = new TypePG();
					if (this._typepg.Load((long)value) != 0)
					{
						this._typepg = null;
					}
				}
				return this._typepg;
			}
			set
			{
				this._typepg = value;
				if (this._typepg != null)
				{
					base.SetValue("IDTypePG", this._typepg.get_ID());
				}
			}
		}

		public string Value
		{
			get
			{
				return (string)base.GetValue("Value");
			}
			set
			{
				base.SetValue("Value", value);
			}
		}

		public PG()
		{
			this.NameTable = "PG";
		}
	}
}