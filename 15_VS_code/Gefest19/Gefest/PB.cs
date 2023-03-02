using System;
using WebSecurityDLL;

namespace Gefest
{
	public class PB : SimpleClass
	{
		private Batch _Batch;

		private TypePB _TypePB;

		public int IDBatch
		{
			get
			{
				return (int)base.GetValue("IDBatch");
			}
			set
			{
				base.SetValue("IDBatch", value);
			}
		}

		public int IDTypePB
		{
			get
			{
				return (int)base.GetValue("IDTypePB");
			}
			set
			{
				base.SetValue("IDTypePB", value);
			}
		}

		public override string Name
		{
			get
			{
				return (string)base.GetValue("Value");
			}
			set
			{
				base.SetValue("Value", value);
			}
		}

		public Batch oBatch
		{
			get
			{
				if (this._Batch == null)
				{
					int value = (int)base.GetValue("IDBatch");
					this._Batch = new Batch();
					if (this._Batch.Load((long)value) != 0)
					{
						this._Batch = null;
					}
				}
				return this._Batch;
			}
			set
			{
				this._Batch = value;
				if (this._Batch != null)
				{
					base.SetValue("IDBatch", this._Batch.get_ID());
				}
			}
		}

		public TypePB oTypePB
		{
			get
			{
				if (this._TypePB == null)
				{
					int value = (int)base.GetValue("IDTypePB");
					this._TypePB = new TypePB();
					if (this._TypePB.Load((long)value) != 0)
					{
						this._TypePB = null;
					}
				}
				return this._TypePB;
			}
			set
			{
				this._TypePB = value;
				if (this._TypePB != null)
				{
					base.SetValue("IDTypePB", this._TypePB.get_ID());
				}
			}
		}

		public string Value
		{
			get
			{
				return (string)base.GetValue("Value");
			}
			set
			{
				base.SetValue("Value", value);
			}
		}

		public PB()
		{
			this.NameTable = "PB";
		}
	}
}