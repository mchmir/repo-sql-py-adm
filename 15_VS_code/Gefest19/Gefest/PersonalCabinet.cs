using System;
using WebSecurityDLL;

namespace Gefest
{
	public class PersonalCabinet : SimpleClass
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

		public long IDContract
		{
			get
			{
				return (long)base.GetValue("IDContract");
			}
			set
			{
				base.SetValue("IDContract", value);
			}
		}

		public string PCEmail
		{
			get
			{
				return (string)base.GetValue("PCEmail");
			}
			set
			{
				base.SetValue("PCEmail", value);
			}
		}

		public int PCNeedChgPwd
		{
			get
			{
				return (int)base.GetValue("PCNeedChgPwd");
			}
			set
			{
				base.SetValue("PCNeedChgPwd", value);
			}
		}

		public string PCPass
		{
			get
			{
				return (string)base.GetValue("PCPass");
			}
			set
			{
				base.SetValue("PCPass", value);
			}
		}

		public string PCPassword
		{
			get
			{
				return (string)base.GetValue("PCPassword");
			}
			set
			{
				base.SetValue("PCPassword", value);
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

		public PersonalCabinet()
		{
			this.NameTable = "PersonalCabinet";
			this.Init();
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}