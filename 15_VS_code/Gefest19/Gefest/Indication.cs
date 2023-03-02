using System;
using System.Collections;
using WebSecurityDLL;

namespace Gefest
{
	public class Indication : SimpleClass
	{
		private Gmeter _Gmeter;

		private TypeIndication _TypeIndication;

		private Agent _Agent;

		private SYSUser _UserAdd;

		private SYSUser _UserModify;

		private FactUses _factuses;

		public DateTime DateAdd
		{
			get
			{
				return (DateTime)base.GetValue("DateAdd");
			}
			set
			{
				base.SetValue("DateAdd", value);
			}
		}

		public DateTime Datedisplay
		{
			get
			{
				return (DateTime)base.GetValue("Datedisplay");
			}
			set
			{
				base.SetValue("Datedisplay", value);
			}
		}

		public DateTime DateModify
		{
			get
			{
				return (DateTime)base.GetValue("DateModify");
			}
			set
			{
				base.SetValue("DateModify", value);
			}
		}

		public double Display
		{
			get
			{
				return (double)base.GetValue("Display");
			}
			set
			{
				base.SetValue("Display", value);
			}
		}

		public int IDModify
		{
			get
			{
				return (int)base.GetValue("IDModify");
			}
			set
			{
				base.SetValue("IDModify", value);
			}
		}

		public int IDUser
		{
			get
			{
				return (int)base.GetValue("IDUser");
			}
			set
			{
				base.SetValue("IDUser", value);
			}
		}

		public Agent oAgent
		{
			get
			{
				if (this._Agent == null)
				{
					int value = (int)base.GetValue("IDAgent");
					this._Agent = new Agent();
					if (this._Agent.Load((long)value) != 0)
					{
						this._Agent = null;
					}
				}
				return this._Agent;
			}
			set
			{
				this._Agent = value;
				if (this._Agent != null)
				{
					base.SetValue("IDAgent", this._Agent.get_ID());
				}
			}
		}

		public FactUses oFactUses
		{
			get
			{
				if (this._factuses == null)
				{
					this._factuses = new FactUses();
					if (this._factuses.Load(this) > 0)
					{
						this._factuses = null;
					}
				}
				return this._factuses;
			}
			set
			{
				if (value == null)
				{
					this._factuses = value;
				}
			}
		}

		public Gmeter oGmeter
		{
			get
			{
				if (this._Gmeter == null)
				{
					int value = (int)base.GetValue("IDGmeter");
					this._Gmeter = new Gmeter();
					if (this._Gmeter.Load((long)value) != 0)
					{
						this._Gmeter = null;
					}
				}
				return this._Gmeter;
			}
			set
			{
				this._Gmeter = value;
				if (this._Gmeter != null)
				{
					base.SetValue("IDGmeter", this._Gmeter.get_ID());
				}
			}
		}

		public TypeIndication oTypeIndication
		{
			get
			{
				if (this._TypeIndication == null)
				{
					int value = (int)base.GetValue("IDTypeIndication");
					this._TypeIndication = new TypeIndication();
					if (this._TypeIndication.Load((long)value) != 0)
					{
						this._TypeIndication = null;
					}
				}
				return this._TypeIndication;
			}
			set
			{
				this._TypeIndication = value;
				if (this._TypeIndication != null)
				{
					base.SetValue("IDTypeIndication", this._TypeIndication.get_ID());
				}
			}
		}

		public SYSUser oUserAdd
		{
			get
			{
				if (this._UserAdd == null)
				{
					int value = (int)base.GetValue("IDUser");
					this._UserAdd = new SYSUser();
					if (this._UserAdd.Load((long)value) != 0)
					{
						this._UserAdd = null;
					}
				}
				return this._UserAdd;
			}
			set
			{
				this._UserAdd = value;
				if (this._UserAdd != null)
				{
					base.SetValue("IDUser", this._UserAdd.get_ID());
				}
			}
		}

		public SYSUser oUserModify
		{
			get
			{
				if (this._UserModify == null)
				{
					int value = (int)base.GetValue("IDModify");
					this._UserModify = new SYSUser();
					if (this._UserModify.Load((long)value) != 0)
					{
						this._UserModify = null;
					}
				}
				return this._UserModify;
			}
			set
			{
				this._UserModify = value;
				if (this._UserModify != null)
				{
					base.SetValue("IDModify", this._UserModify.get_ID());
				}
			}
		}

		public string Premech
		{
			get
			{
				return (string)base.GetValue("Premech");
			}
			set
			{
				base.SetValue("Premech", value);
			}
		}

		public Indication()
		{
			this.NameTable = "Indication";
			base.Init();
		}

		public string CalcFactUse()
		{
			string str;
			string message = "";
			try
			{
				double display = -1;
				double num = 0;
				Indication item = null;
				if (base.get_isNew())
				{
					this.oGmeter.oIndications = null;
					if (this.Datedisplay <= this.oGmeter.oIndications[0].Datedisplay)
					{
						str = "Дата новых показаний должна быть больше предыдущих!";
						return str;
					}
					else if (this.oGmeter.oIndications.get_Count() > 0)
					{
						item = this.oGmeter.oIndications[0];
					}
				}
				else if (this.get_ID() != this.oGmeter.oIndications[0].get_ID())
				{
					str = "Редактировать можно только последние показания!";
					return str;
				}
				else if (this.oFactUses.get_Count() > 0 && this.oFactUses[0].oPeriod.get_ID() != Depot.CurrentPeriod.get_ID())
				{
					str = "Редактировать можно только показания текущего периода!";
					return str;
				}
				else if (this.oGmeter.oIndications.get_Count() > 1)
				{
					item = this.oGmeter.oIndications[1];
				}
				if (item != null)
				{
					if (this.Datedisplay > item.Datedisplay)
					{
						display = item.Display;
						if (this.Display >= display)
						{
							num = this.Display - display;
						}
						else
						{
							str = "Попытка сохранения отрицательного потребления!";
							return str;
						}
					}
					else
					{
						str = "Дата новых показаний должна быть больше предыдущих!";
						return str;
					}
				}
				message = num.ToString();
				return message;
			}
			catch (Exception exception)
			{
				message = exception.Message;
				return message;
			}
			return str;
		}

		public override int Delete()
		{
			if (this.Datedisplay < Depot.CurrentPeriod.DateBegin)
			{
				return 1;
			}
			foreach (FactUse oFactUse in this.oGmeter.oGobject.oFactUses)
			{
				if (oFactUse.oIndication == null || oFactUse.oIndication.get_ID() != this.get_ID())
				{
					continue;
				}
				this.oGmeter.oGobject.oFactUses.Remove(oFactUse.get_ID());
			}
			return base.Delete();
		}

		public override int Save()
		{
			FactUse currentPeriod;
			int num;
			try
			{
				if (this.Datedisplay >= Depot.CurrentPeriod.DateBegin)
				{
					currentPeriod = (this.oFactUses.get_Count() <= 0 ? this.oGmeter.oGobject.oFactUses.Add() : this.oFactUses[0]);
					currentPeriod.oPeriod = Depot.CurrentPeriod;
					currentPeriod.oTypeFU = Depot.oTypeFUs[0];
					currentPeriod.FactAmount = Convert.ToDouble(this.CalcFactUse());
					int num1 = base.Save();
					if (num1 <= 0)
					{
						currentPeriod.oIndication = this;
						num1 = currentPeriod.Save();
					}
					num = num1;
				}
				else
				{
					num = 1;
				}
			}
			catch
			{
				num = 1;
			}
			return num;
		}

		public int Save(bool NeedCalc)
		{
			int num = 1;
			if (this.Datedisplay < Depot.CurrentPeriod.DateBegin)
			{
				return 1;
			}
			num = (!NeedCalc ? base.Save() : this.Save());
			return num;
		}
	}
}