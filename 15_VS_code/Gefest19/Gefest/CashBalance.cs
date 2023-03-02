using System;
using WebSecurityDLL;

namespace Gefest
{
	public class CashBalance : SimpleClass
	{
		private Agent _Cashier;

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

		public DateTime DateCash
		{
			get
			{
				return (DateTime)base.GetValue("DateCash");
			}
			set
			{
				base.SetValue("DateCash", value);
			}
		}

		public Agent oCashier
		{
			get
			{
				if (this._Cashier == null)
				{
					int value = (int)base.GetValue("IDCashier");
					this._Cashier = new Agent();
					if (this._Cashier.Load((long)value) != 0)
					{
						this._Cashier = null;
					}
				}
				return this._Cashier;
			}
			set
			{
				this._Cashier = value;
				if (this._Cashier != null)
				{
					base.SetValue("IDCashier", this._Cashier.get_ID());
				}
			}
		}

		public CashBalance()
		{
			this.NameTable = "CashBalance";
			base.Init();
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}