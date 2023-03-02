using System;
using System.Collections;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class Gobject : SimpleClass
	{
		private TypeGobject _TypeGobject;

		private StatusGObject _StatusGobject;

		private Contract _Contract;

		private Address _address;

		private GRU _GRU;

		private FactUses _FactUses;

		private Gmeters _Gmeters;

		public int CountLives
		{
			get
			{
				return (int)base.GetValue("CountLives");
			}
			set
			{
				base.SetValue("CountLives", value);
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

		public string Name
		{
			get
			{
				return (string)base.GetValue("Name");
			}
			set
			{
				base.SetValue("Name", value);
			}
		}

		public Gmeter oActiveGmeter
		{
			get
			{
				Gmeter gmeter = null;
				foreach (Gmeter oGmeter in this.oGmeters)
				{
					if (oGmeter.oStatusGMeter.get_ID() != (long)1)
					{
						continue;
					}
					gmeter = oGmeter;
					break;
				}
				return gmeter;
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

		public Contract oContract
		{
			get
			{
				if (this._Contract == null)
				{
					int value = (int)base.GetValue("IDContract");
					this._Contract = new Contract();
					if (this._Contract.Load((long)value) != 0)
					{
						this._Contract = null;
					}
				}
				return this._Contract;
			}
			set
			{
				this._Contract = value;
				if (this._Contract != null)
				{
					base.SetValue("IDContract", this._Contract.get_ID());
				}
			}
		}

		public FactUses oFactUses
		{
			get
			{
				if (this._FactUses == null)
				{
					this._FactUses = new FactUses();
					if (this._FactUses.Load(this) != 0)
					{
						this._FactUses = null;
					}
				}
				return this._FactUses;
			}
		}

		public Gmeters oGmeters
		{
			get
			{
				if (this._Gmeters == null)
				{
					this._Gmeters = new Gmeters();
					if (this._Gmeters.Load(this) != 0)
					{
						this._Gmeters = null;
					}
				}
				return this._Gmeters;
			}
		}

		public GRU oGRU
		{
			get
			{
				if (this._GRU == null)
				{
					int value = (int)base.GetValue("IDGRU");
					this._GRU = new GRU();
					if (this._GRU.Load((long)value) != 0)
					{
						this._GRU = null;
					}
				}
				return this._GRU;
			}
			set
			{
				this._GRU = value;
				if (this._GRU == null)
				{
					base.SetValue("IDGRU", 0);
					return;
				}
				base.SetValue("IDGRU", this._GRU.get_ID());
			}
		}

		public StatusGObject oStatusGObject
		{
			get
			{
				if (this._StatusGobject == null)
				{
					int value = (int)base.GetValue("IDStatusGObject");
					this._StatusGobject = new StatusGObject();
					if (this._StatusGobject.Load((long)value) != 0)
					{
						this._StatusGobject = null;
					}
				}
				return this._StatusGobject;
			}
			set
			{
				this._StatusGobject = value;
				if (this._StatusGobject != null)
				{
					base.SetValue("IDStatusGobject", this._StatusGobject.get_ID());
				}
			}
		}

		public TypeGobject oTypeGobject
		{
			get
			{
				if (this._TypeGobject == null)
				{
					int value = (int)base.GetValue("IDTypeGobject");
					this._TypeGobject = new TypeGobject();
					if (this._TypeGobject.Load((long)value) != 0)
					{
						this._TypeGobject = null;
					}
				}
				return this._TypeGobject;
			}
			set
			{
				this._TypeGobject = value;
				if (this._TypeGobject != null)
				{
					base.SetValue("IDTypeGobject", this._TypeGobject.get_ID());
				}
			}
		}

		public Gobject()
		{
			this.NameTable = "Gobject";
		}

		public Gmeter GetActiveGMeter()
		{
			Gmeter gmeter;
			IEnumerator enumerator = this.oGmeters.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Gmeter current = (Gmeter)enumerator.Current;
					if (current.oStatusGMeter.get_ID() != (long)1)
					{
						continue;
					}
					gmeter = current;
					return gmeter;
				}
				return null;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return gmeter;
		}
	}
}