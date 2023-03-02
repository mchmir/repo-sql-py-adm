using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Settings1 : SimpleClass
	{
		private Correspondent _Correspondent;

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
				if (this._Correspondent == null)
				{
					base.SetValue("IDCorrespondent", 0);
					return;
				}
				base.SetValue("IDCorrespondent", this._Correspondent.get_ID());
			}
		}

		public Settings1()
		{
			this.NameTable = "Settings1";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}