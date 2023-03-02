using System;
using WebSecurityDLL;

namespace Gefest
{
	public class OldValues : SimpleClass
	{
		private Period _Period;

		public DateTime DateValues
		{
			get
			{
				return (DateTime)base.GetValue("DateValues");
			}
			set
			{
				base.SetValue("DateValues", value);
			}
		}

		public long IDObject
		{
			get
			{
				return (long)((int)base.GetValue("IdObject"));
			}
			set
			{
				base.SetValue("IdObject", value);
			}
		}

		public string NameColumn
		{
			get
			{
				return (string)base.GetValue("NameColumn");
			}
			set
			{
				base.SetValue("NameColumn", value);
			}
		}

		public string NameTable
		{
			get
			{
				return (string)base.GetValue("NameTable");
			}
			set
			{
				base.SetValue("NameTable", value);
			}
		}

		public Period oPeriod
		{
			get
			{
				if (this._Period == null)
				{
					int value = (int)base.GetValue("IDPeriod");
					this._Period = new Period();
					if (this._Period.Load((long)value) != 0)
					{
						this._Period = null;
					}
				}
				return this._Period;
			}
			set
			{
				this._Period = value;
				if (this._Period != null)
				{
					base.SetValue("IDPeriod", this._Period.get_ID());
				}
			}
		}

		public OldValues()
		{
			this.NameTable = "OldValues";
			base.Init();
		}
	}
}