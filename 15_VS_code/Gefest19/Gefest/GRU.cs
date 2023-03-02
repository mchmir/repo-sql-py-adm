using System;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class GRU : SimpleClass
	{
		private PGs _PGs;

		private ClassGRU _classgru;

		private Agent _Agent;

		private Address _address;

		public DateTime DateFollowingVerify
		{
			get
			{
				return (DateTime)base.GetValue("DateFollowingVerify");
			}
			set
			{
				base.SetValue("DateFollowingVerify", value);
			}
		}

		public DateTime DateIn
		{
			get
			{
				return (DateTime)base.GetValue("DateIn");
			}
			set
			{
				base.SetValue("DateIn", value);
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

		public string InvNumber
		{
			get
			{
				return (string)base.GetValue("InvNumber");
			}
			set
			{
				base.SetValue("InvNumber", value);
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

		public Address oAddress
		{
			get
			{
				if (this._address == null)
				{
					int value = (int)base.GetValue("IDAddress");
					this._address = new Address();
					if (this._address.Load((long)value) != 0)
					{
						this._address = null;
					}
				}
				return this._address;
			}
			set
			{
				this._address = value;
				if (this._address != null)
				{
					base.SetValue("IDAddress", this._address.get_ID());
				}
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

		public ClassGRU oClassGRU
		{
			get
			{
				if (this._classgru == null)
				{
					int value = (int)base.GetValue("IDClassGRU");
					this._classgru = new ClassGRU();
					if (this._classgru.Load((long)value) != 0)
					{
						this._classgru = null;
					}
				}
				return this._classgru;
			}
			set
			{
				this._classgru = value;
				if (this._classgru != null)
				{
					base.SetValue("IDClassGRU", this._classgru.get_ID());
				}
			}
		}

		public PGs oPGs
		{
			get
			{
				if (this._PGs == null)
				{
					this._PGs = new PGs();
					if (this._PGs.Load() != 0)
					{
						this._PGs = null;
					}
				}
				return this._PGs;
			}
		}

		public GRU()
		{
			this.NameTable = "GRU";
		}

		protected override void Finalize()
		{
			try
			{
				this._PGs = null;
			}
			finally
			{
				base.Finalize();
			}
		}
	}
}