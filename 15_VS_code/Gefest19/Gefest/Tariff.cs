using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Tariff : SimpleClass
	{
		private Period _Period;

		private TypeTariff _typetariff;

		public override string Name
		{
			get
			{
				return this.Value.ToString();
			}
			set
			{
				this.Value = Convert.ToDouble(value);
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

		public TypeTariff oTypeTariff
		{
			get
			{
				if (this._typetariff == null)
				{
					int value = (int)base.GetValue("IDTypeTariff");
					this._typetariff = new TypeTariff();
					if (this._typetariff.Load((long)value) != 0)
					{
						this._typetariff = null;
					}
				}
				return this._typetariff;
			}
			set
			{
				this._typetariff = value;
				if (this._typetariff != null)
				{
					base.SetValue("IDTypeTariff", this._typetariff.get_ID());
				}
			}
		}

		public double Value
		{
			get
			{
				return (double)base.GetValue("Value");
			}
			set
			{
				base.SetValue("Value", value);
			}
		}

		public Tariff()
		{
			this.NameTable = "Tariff";
		}
	}
}