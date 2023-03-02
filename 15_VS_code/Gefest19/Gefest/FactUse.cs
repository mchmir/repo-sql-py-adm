using System;
using WebSecurityDLL;

namespace Gefest
{
	public class FactUse : SimpleClass
	{
		private Indication _Indication;

		private Period _Period;

		private Gobject _GObject;

		private Document _Document;

		private TypeFU _TypeFU;

		private Operation _Operation;

		public double FactAmount
		{
			get
			{
				return (double)base.GetValue("FactAmount");
			}
			set
			{
				base.SetValue("FactAmount", value);
			}
		}

		public Document oDocument
		{
			get
			{
				if (this._Document == null)
				{
					int value = (int)base.GetValue("IDDocument");
					this._Document = new Document();
					if (this._Document.Load((long)value) != 0)
					{
						this._Document = null;
					}
				}
				return this._Document;
			}
			set
			{
				this._Document = value;
				if (this._Document != null)
				{
					base.SetValue("IDDocument", this._Document.get_ID());
				}
			}
		}

		public Gobject oGobject
		{
			get
			{
				if (this._GObject == null)
				{
					int value = (int)base.GetValue("IDGobject");
					this._GObject = new Gobject();
					if (this._GObject.Load((long)value) != 0)
					{
						this._GObject = null;
					}
				}
				return this._GObject;
			}
			set
			{
				this._GObject = value;
				if (this._GObject != null)
				{
					base.SetValue("IDGobject", this._GObject.get_ID());
				}
			}
		}

		public Indication oIndication
		{
			get
			{
				if (this._Indication == null)
				{
					int value = (int)base.GetValue("IDIndication");
					this._Indication = new Indication();
					if (this._Indication.Load((long)value) != 0)
					{
						this._Indication = null;
					}
				}
				return this._Indication;
			}
			set
			{
				this._Indication = value;
				if (this._Indication != null)
				{
					base.SetValue("IDIndication", this._Indication.get_ID());
				}
			}
		}

		public Operation oOperation
		{
			get
			{
				if (this._Operation == null)
				{
					int value = (int)base.GetValue("IDOperation");
					this._Operation = new Operation();
					if (this._Operation.Load((long)value) != 0)
					{
						this._Operation = null;
					}
				}
				return this._Operation;
			}
			set
			{
				this._Operation = value;
				if (this._Operation != null)
				{
					base.SetValue("IDOperation", this._Operation.get_ID());
				}
			}
		}

		public Period oPeriod
		{
			get
			{
				if (this._Period == null)
				{
					int value = (int)base.GetValue("IDPeriod");
					this._Period = new Period();
					if (this._Period.Load((long)value) != 0)
					{
						this._Period = null;
					}
				}
				return this._Period;
			}
			set
			{
				this._Period = value;
				if (this._Period != null)
				{
					base.SetValue("IDPeriod", this._Period.get_ID());
				}
			}
		}

		public TypeFU oTypeFU
		{
			get
			{
				if (this._TypeFU == null)
				{
					int value = (int)base.GetValue("IDTypeFU");
					this._TypeFU = new TypeFU();
					if (this._TypeFU.Load((long)value) != 0)
					{
						this._TypeFU = null;
					}
				}
				return this._TypeFU;
			}
			set
			{
				this._TypeFU = value;
				if (this._TypeFU != null)
				{
					base.SetValue("IDTypeFU", this._TypeFU.get_ID());
				}
			}
		}

		public FactUse()
		{
			this.NameTable = "FactUse";
			base.Init();
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}