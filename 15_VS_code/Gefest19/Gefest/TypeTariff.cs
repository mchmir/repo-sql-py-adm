using System;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeTariff : SimpleClass
	{
		private Tariffs _tariffs;

		public string FullName
		{
			get
			{
				string name = this.get_Name();
				double lastValue = this.LastValue;
				return string.Concat(name, ", ", lastValue.ToString());
			}
		}

		public double LastValue
		{
			get
			{
				if (this._tariffs == null)
				{
					this._tariffs = new Tariffs();
					if (this._tariffs.Load(this) != 0)
					{
						this._tariffs = null;
					}
				}
				if (this._tariffs == null)
				{
					return 0;
				}
				return this._tariffs[0].Value;
			}
		}

		public TypeTariff()
		{
			this.NameTable = "TypeTariff";
		}
	}
}