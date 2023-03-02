using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TariffDisconnectionGobjectStakan : SimpleClass
	{
		private Period _Period;

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

		public TariffDisconnectionGobjectStakan()
		{
			this.NameTable = "TariffDisconnectionGobjectStakan";
		}
	}
}