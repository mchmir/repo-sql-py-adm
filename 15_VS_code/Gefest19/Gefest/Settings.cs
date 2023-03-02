using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Settings : SimpleClass
	{
		private SYSUser _user;

		private Agent _Agent;

		public int IDAgent
		{
			get
			{
				return (int)base.GetValue("IDAgent");
			}
			set
			{
				base.SetValue("IDAgent", value);
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
				if (this._Agent == null)
				{
					base.SetValue("IDAgent", 0);
					return;
				}
				base.SetValue("IDAgent", this._Agent.get_ID());
			}
		}

		public SYSUser oUser
		{
			get
			{
				if (this._user == null)
				{
					int value = (int)base.GetValue("IDUser");
					this._user = new SYSUser();
					if (this._user.Load((long)value) != 0)
					{
						this._user = null;
					}
				}
				return this._user;
			}
			set
			{
				this._user = value;
				if (this._user != null)
				{
					base.SetValue("IDUser", this._user.get_ID());
				}
			}
		}

		public string ReportPath
		{
			get
			{
				string value = (string)base.GetValue("ReportPath");
				if (!value.EndsWith("\\"))
				{
					value = string.Concat(value, "\\");
				}
				return value;
			}
			set
			{
				base.SetValue("ReportPath", value);
			}
		}

		public bool Startup
		{
			get
			{
				return (bool)base.GetValue("Startup");
			}
			set
			{
				base.SetValue("Startup", value);
			}
		}

		public Settings()
		{
			this.NameTable = "Settings";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}