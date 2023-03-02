using System;
using WebSecurityDLL;

namespace Gefest
{
	public class PC : SimpleClass
	{
		private Period _Period;

		private Contract _Contract;

		private TypePC _TypePC;

		public int IDContract
		{
			get
			{
				return (int)base.GetValue("IDContract");
			}
			set
			{
				base.SetValue("IDContract", value);
			}
		}

		public int IDPeriod
		{
			get
			{
				return (int)base.GetValue("IDPeriod");
			}
			set
			{
				base.SetValue("IDPeriod", value);
			}
		}

		public int IDTypePC
		{
			get
			{
				return (int)base.GetValue("IDTypePC");
			}
			set
			{
				base.SetValue("IDTypePC", value);
			}
		}

		public override string Name
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

		public Contract oContract
		{
			get
			{
				if (this._Contract == null)
				{
					int value = (int)base.GetValue("IDContract");
					this._Contract = new Contract();
					if (this._Contract.Load((long)value) != 0)
					{
						this._Contract = null;
					}
				}
				return this._Contract;
			}
			set
			{
				this._Contract = value;
				if (this._Contract != null)
				{
					base.SetValue("IDContract", this._Contract.get_ID());
				}
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

		public TypePC oTypePC
		{
			get
			{
				if (this._TypePC == null)
				{
					int value = (int)base.GetValue("IDTypePC");
					this._TypePC = new TypePC();
					if (this._TypePC.Load((long)value) != 0)
					{
						this._TypePC = null;
					}
				}
				return this._TypePC;
			}
			set
			{
				this._TypePC = value;
				if (this._TypePC != null)
				{
					base.SetValue("IDTypePC", this._TypePC.get_ID());
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

		public PC()
		{
			this.NameTable = "PC";
		}
	}
}