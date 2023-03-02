using System;
using WebSecurityDLL;

namespace Gefest
{
	public class ClassGRU : SimpleClass
	{
		private GRUs _GRUs;

		public int IDParent
		{
			get
			{
				return (int)base.GetValue("IDParent");
			}
			set
			{
				base.SetValue("IDParent", value);
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

		public GRUs oGRUs
		{
			get
			{
				if (this._GRUs == null)
				{
					this._GRUs = new GRUs();
					if (this._GRUs.Load(this) != 0)
					{
						this._GRUs = null;
					}
				}
				return this._GRUs;
			}
		}

		public ClassGRU()
		{
			this.NameTable = "ClassGRU";
		}

		protected override void Finalize()
		{
			try
			{
				this._GRUs = null;
			}
			finally
			{
				base.Finalize();
			}
		}
	}
}