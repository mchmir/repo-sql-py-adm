using System;
using WebSecurityDLL;

namespace Gefest
{
	public class BalanceReal : SimpleClass
	{
		private Accounting _Accounting;

		private Period _Period;

		private Contract _Contract;

		public double AmountBalance
		{
			get
			{
				return (double)base.GetValue("AmountBalance");
			}
			set
			{
				base.SetValue("AmountBalance", value);
			}
		}

		public double AmountCharge
		{
			get
			{
				return (double)base.GetValue("AmountCharge");
			}
			set
			{
				base.SetValue("AmountCharge", value);
			}
		}

		public double AmountPay
		{
			get
			{
				return (double)base.GetValue("AmountPay");
			}
			set
			{
				base.SetValue("AmountPay", value);
			}
		}

		public Accounting oAccounting
		{
			get
			{
				if (this._Accounting == null)
				{
					int value = (int)base.GetValue("IDAccounting");
					this._Accounting = new Accounting();
					if (this._Accounting.Load((long)value) != 0)
					{
						this._Accounting = null;
					}
				}
				return this._Accounting;
			}
			set
			{
				this._Accounting = value;
				if (this._Accounting != null)
				{
					base.SetValue("IDAccounting", this._Accounting.get_ID());
				}
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

		public BalanceReal()
		{
			this.NameTable = "BalanceReal";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}