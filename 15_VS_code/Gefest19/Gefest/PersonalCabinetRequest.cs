using System;
using WebSecurityDLL;

namespace Gefest
{
	public class PersonalCabinetRequest : SimpleClass
	{
		public string Account
		{
			get
			{
				return (string)base.GetValue("Account");
			}
			set
			{
				base.SetValue("Account", value);
			}
		}

		public string AdresHome
		{
			get
			{
				return (string)base.GetValue("AdresHome");
			}
			set
			{
				base.SetValue("AdresHome", value);
			}
		}

		public DateTime DateRequest
		{
			get
			{
				return (DateTime)base.GetValue("DateRequest");
			}
		}

		public string Email
		{
			get
			{
				return (string)base.GetValue("Email");
			}
			set
			{
				base.SetValue("Email", value);
			}
		}

		public string FIO
		{
			get
			{
				return (string)base.GetValue("FIO");
			}
			set
			{
				base.SetValue("FIO", value);
			}
		}

		public int NeedSendMessage
		{
			get
			{
				return (int)base.GetValue("NeedSendMessage");
			}
			set
			{
				base.SetValue("NeedSendMessage", value);
			}
		}

		public string Phone
		{
			get
			{
				return (string)base.GetValue("Phone");
			}
			set
			{
				base.SetValue("Phone", value);
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

		public PersonalCabinetRequest()
		{
			this.NameTable = "PersonalCabinetRequest";
			base.Init();
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}