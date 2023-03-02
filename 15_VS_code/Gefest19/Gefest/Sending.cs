using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Sending : SimpleClass
	{
		private Correspondent _Correspondent;

		public DateTime DateSending
		{
			get
			{
				return (DateTime)base.GetValue("DateSending");
			}
			set
			{
				base.SetValue("DateSending", value);
			}
		}

		public int IDCorrespondent
		{
			get
			{
				return (int)base.GetValue("IDCorrespondent");
			}
			set
			{
				base.SetValue("IDCorrespondent", value);
			}
		}

		public int NumberSending
		{
			get
			{
				return (int)base.GetValue("NumberSending");
			}
			set
			{
				base.SetValue("NumberSending", value);
			}
		}

		public Correspondent oCorrespondent
		{
			get
			{
				if (this._Correspondent == null)
				{
					int value = (int)base.GetValue("IDCorrespondent");
					this._Correspondent = new Correspondent();
					if (this._Correspondent.Load((long)value) != 0)
					{
						this._Correspondent = null;
					}
				}
				return this._Correspondent;
			}
			set
			{
				this._Correspondent = value;
				if (this._Correspondent != null)
				{
					base.SetValue("IDCorrespondent", this._Correspondent.get_ID());
				}
			}
		}

		public Sending()
		{
			this.NameTable = "Sending";
			base.Init();
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}