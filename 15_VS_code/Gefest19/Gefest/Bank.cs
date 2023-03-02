using System;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class Bank : SimpleClass
	{
		private Place _Place;

		public string MFO
		{
			get
			{
				return (string)base.GetValue("MFO");
			}
			set
			{
				base.SetValue("MFO", value);
			}
		}

		public override string Name
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

		public Place oPlace
		{
			get
			{
				if (this._Place == null)
				{
					int value = (int)base.GetValue("IDPlace");
					this._Place = new Place();
					if (this._Place.Load((long)value) != 0)
					{
						this._Place = null;
					}
				}
				return this._Place;
			}
			set
			{
				this._Place = value;
				if (this._Place != null)
				{
					base.SetValue("IDPlace", this._Place.get_ID());
				}
			}
		}

		public Bank()
		{
			this.NameTable = "Bank";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}