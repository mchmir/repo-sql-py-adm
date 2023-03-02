using System;
using WebSecurityDLL;

namespace Gefest
{
	public class IndicationTemp : SimpleClass
	{
		private Gmeter _Gmeter;

		private TypeIndication _TypeIndication;

		private Agent _Agent;

		private SYSUser _UserAdd;

		private SYSUser _UserModify;

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

		public int State
		{
			get
			{
				return (int)base.GetValue("State");
			}
			set
			{
				base.SetValue("State", value);
			}
		}

		public IndicationTemp()
		{
			this.NameTable = "IndicationTemp";
			base.Init();
		}

		public override int Delete()
		{
			return base.Delete();
		}

		public override int Save()
		{
			int num;
			try
			{
				num = base.Save();
			}
			catch
			{
				num = 1;
			}
			return num;
		}
	}
}