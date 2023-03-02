using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Gmeter : SimpleClass
	{
		private TypeGMeter _TypeGmeter;

		private TypeVerify _TypeVerify;

		private TypeReasonDisconnect _TypeReasonDisconnect;

		private StatusGMeter _StatusGMeter;

		private Gobject _Gobject;

		private Indications _Indications;

		public double BeginValue
		{
			get
			{
				return (double)base.GetValue("BeginValue");
			}
			set
			{
				base.SetValue("BeginValue", value);
			}
		}

		public DateTime DateFabrication
		{
			get
			{
				return (DateTime)base.GetValue("DateFabrication");
			}
			set
			{
				base.SetValue("DateFabrication", value);
			}
		}

		public DateTime DateInstall
		{
			get
			{
				return (DateTime)base.GetValue("DateInstall");
			}
			set
			{
				base.SetValue("DateInstall", value);
			}
		}

		public DateTime DateOnOff
		{
			get
			{
				return (DateTime)base.GetValue("DateOnOff");
			}
			set
			{
				base.SetValue("DateOnOff", value);
			}
		}

		public DateTime DatePlomb
		{
			get
			{
				return (DateTime)base.GetValue("DatePlomb");
			}
			set
			{
				base.SetValue("DatePlomb", value);
			}
		}

		public DateTime DateVerify
		{
			get
			{
				return (DateTime)base.GetValue("DateVerify");
			}
			set
			{
				base.SetValue("DateVerify", value);
			}
		}

		public int IDAgentPlomb
		{
			get
			{
				return (int)base.GetValue("IDAgentPlomb");
			}
			set
			{
				base.SetValue("IDAgentPlomb", value);
			}
		}

		public int IDTypePlombWork
		{
			get
			{
				return (int)base.GetValue("IDTypePlombWork");
			}
			set
			{
				base.SetValue("IDTypePlombWork", value);
			}
		}

		public double IndicationPlomb
		{
			get
			{
				return (double)base.GetValue("IndicationPlomb");
			}
			set
			{
				base.SetValue("IndicationPlomb", value);
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

		public override string Name
		{
			get
			{
				if (this.oTypeGMeter == null)
				{
					return string.Concat(this.SerialNumber, ", ", this.oStatusGMeter.get_Name());
				}
				string[] serialNumber = new string[] { this.SerialNumber, ", ", this.oStatusGMeter.get_Name(), ", ", this.oTypeGMeter.Fullname };
				return string.Concat(serialNumber);
			}
			set
			{
				base.SetValue("SerialNumber", value);
			}
		}

		public Gobject oGobject
		{
			get
			{
				if (this._Gobject == null)
				{
					int value = (int)base.GetValue("IDGobject");
					this._Gobject = new Gobject();
					if (this._Gobject.Load((long)value) != 0)
					{
						this._Gobject = null;
					}
				}
				return this._Gobject;
			}
			set
			{
				this._Gobject = value;
				if (this._Gobject != null)
				{
					base.SetValue("IDGobject", this._Gobject.get_ID());
				}
			}
		}

		public Indications oIndications
		{
			get
			{
				if (this._Indications == null)
				{
					this._Indications = new Indications();
					if (this._Indications.Load(this) != 0)
					{
						this._Indications = null;
					}
				}
				return this._Indications;
			}
			set
			{
				if (value == null)
				{
					this._Indications = value;
				}
			}
		}

		public StatusGMeter oStatusGMeter
		{
			get
			{
				if (this._StatusGMeter == null)
				{
					int value = (int)base.GetValue("IDStatusGMeter");
					this._StatusGMeter = new StatusGMeter();
					if (this._StatusGMeter.Load((long)value) != 0)
					{
						this._StatusGMeter = null;
					}
				}
				return this._StatusGMeter;
			}
			set
			{
				this._StatusGMeter = value;
				if (this._StatusGMeter != null)
				{
					base.SetValue("IDStatusGMeter", this._StatusGMeter.get_ID());
				}
			}
		}

		public TypeGMeter oTypeGMeter
		{
			get
			{
				if (this._TypeGmeter == null)
				{
					int value = (int)base.GetValue("IDTypeGMeter");
					this._TypeGmeter = new TypeGMeter();
					if (this._TypeGmeter.Load((long)value) != 0)
					{
						this._TypeGmeter = null;
					}
				}
				return this._TypeGmeter;
			}
			set
			{
				this._TypeGmeter = value;
				if (this._TypeGmeter != null)
				{
					base.SetValue("IDTypeGMeter", this._TypeGmeter.get_ID());
				}
			}
		}

		public TypeReasonDisconnect oTypeReasonDisconnect
		{
			get
			{
				if (this._TypeReasonDisconnect == null)
				{
					int value = (int)base.GetValue("IDTypeReasonDisconnect");
					this._TypeReasonDisconnect = new TypeReasonDisconnect();
					if (this._TypeReasonDisconnect.Load((long)value) != 0)
					{
						this._TypeReasonDisconnect = null;
					}
				}
				return this._TypeReasonDisconnect;
			}
			set
			{
				this._TypeReasonDisconnect = value;
				if (this._TypeReasonDisconnect != null)
				{
					base.SetValue("IDTypeReasonDisconnect", this._TypeReasonDisconnect.get_ID());
				}
			}
		}

		public TypeVerify oTypeVerify
		{
			get
			{
				if (this._TypeVerify == null)
				{
					int value = (int)base.GetValue("IDTypeVerify");
					this._TypeVerify = new TypeVerify();
					if (this._TypeVerify.Load((long)value) != 0)
					{
						this._TypeVerify = null;
					}
				}
				return this._TypeVerify;
			}
			set
			{
				this._TypeVerify = value;
				if (this._TypeVerify != null)
				{
					base.SetValue("IDTypeVerify", this._TypeVerify.get_ID());
				}
			}
		}

		public string PlombMemo
		{
			get
			{
				return (string)base.GetValue("PlombMemo");
			}
			set
			{
				base.SetValue("PlombMemo", value);
			}
		}

		public string PlombNumber1
		{
			get
			{
				return (string)base.GetValue("PlombNumber1");
			}
			set
			{
				base.SetValue("PlombNumber1", value);
			}
		}

		public string PlombNumber2
		{
			get
			{
				return (string)base.GetValue("PlombNumber2");
			}
			set
			{
				base.SetValue("PlombNumber2", value);
			}
		}

		public string SerialNumber
		{
			get
			{
				return (string)base.GetValue("SerialNumber");
			}
			set
			{
				base.SetValue("SerialNumber", value);
			}
		}

		public Gmeter()
		{
			this.NameTable = "Gmeter";
		}

		public Indication GetCurrentIndication()
		{
			if (this.oIndications == null)
			{
				return null;
			}
			return this._Indications[0];
		}

		public Indication GetPenultimateIndication()
		{
			if (this.oIndications == null || this.oIndications.get_Count() <= 1)
			{
				return null;
			}
			return this._Indications[1];
		}

		public Indications oIndicationss(DateTime vDateBegin, DateTime vDateEnd)
		{
			this._Indications = new Indications();
			if (this._Indications.Load(this, vDateBegin, vDateEnd) != 0)
			{
				this._Indications = null;
			}
			return this._Indications;
		}
	}
}