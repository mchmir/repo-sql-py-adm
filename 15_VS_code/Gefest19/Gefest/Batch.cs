using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Batch : SimpleClass
	{
		private TypePay _TypePay;

		private Period _Period;

		private Agent _Dispatcher;

		private Agent _Cashier;

		private TypeBatch _TypeBatch;

		private StatusBatch _StatusBatch;

		private Documents _Documents;

		public double BatchAmount
		{
			get
			{
				return (double)base.GetValue("BatchAmount");
			}
			set
			{
				base.SetValue("BatchAmount", value);
			}
		}

		public int BatchCount
		{
			get
			{
				return (int)base.GetValue("BatchCount");
			}
			set
			{
				base.SetValue("BatchCount", value);
			}
		}

		public DateTime BatchDate
		{
			get
			{
				return (DateTime)base.GetValue("BatchDate");
			}
			set
			{
				base.SetValue("BatchDate", value);
			}
		}

		public string Note
		{
			get
			{
				return (string)base.GetValue("Note");
			}
			set
			{
				base.SetValue("Note", value);
			}
		}

		public string NumberBatch
		{
			get
			{
				return (string)base.GetValue("NumberBatch");
			}
			set
			{
				base.SetValue("NumberBatch", value);
			}
		}

		public Agent oCashier
		{
			get
			{
				if (this._Cashier == null)
				{
					int value = (int)base.GetValue("IDCashier");
					this._Cashier = new Agent();
					if (this._Cashier.Load((long)value) != 0)
					{
						this._Cashier = null;
					}
				}
				return this._Cashier;
			}
			set
			{
				this._Cashier = value;
				if (this._Cashier != null)
				{
					base.SetValue("IDCashier", this._Cashier.get_ID());
				}
			}
		}

		public Agent oDispatcher
		{
			get
			{
				if (this._Dispatcher == null)
				{
					int value = (int)base.GetValue("IDDispatcher");
					this._Dispatcher = new Agent();
					if (this._Dispatcher.Load((long)value) != 0)
					{
						this._Dispatcher = null;
					}
				}
				return this._Dispatcher;
			}
			set
			{
				this._Dispatcher = value;
				if (this._Dispatcher != null)
				{
					base.SetValue("IDDispatcher", this._Dispatcher.get_ID());
				}
			}
		}

		public Documents oDocuments
		{
			get
			{
				if (this._Documents == null)
				{
					this._Documents = new Documents();
					if (this._Documents.Load(this) != 0)
					{
						this._Documents = null;
					}
				}
				return this._Documents;
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

		public StatusBatch oStatusBatch
		{
			get
			{
				if (this._StatusBatch == null)
				{
					int value = (int)base.GetValue("IDStatusBatch");
					this._StatusBatch = new StatusBatch();
					if (this._StatusBatch.Load((long)value) != 0)
					{
						this._StatusBatch = null;
					}
				}
				return this._StatusBatch;
			}
			set
			{
				this._StatusBatch = value;
				if (this._StatusBatch != null)
				{
					base.SetValue("IDStatusBatch", this._StatusBatch.get_ID());
				}
			}
		}

		public TypeBatch oTypeBatch
		{
			get
			{
				if (this._TypeBatch == null)
				{
					int value = (int)base.GetValue("IDTypeBatch");
					this._TypeBatch = new TypeBatch();
					if (this._TypeBatch.Load((long)value) != 0)
					{
						this._TypeBatch = null;
					}
				}
				return this._TypeBatch;
			}
			set
			{
				this._TypeBatch = value;
				if (this._TypeBatch != null)
				{
					base.SetValue("IDTypeBatch", this._TypeBatch.get_ID());
				}
			}
		}

		public TypePay oTypePay
		{
			get
			{
				if (this._TypePay == null)
				{
					int value = (int)base.GetValue("IDTypePay");
					this._TypePay = new TypePay();
					if (this._TypePay.Load((long)value) != 0)
					{
						this._TypePay = null;
					}
				}
				return this._TypePay;
			}
			set
			{
				this._TypePay = value;
				if (this._TypePay != null)
				{
					base.SetValue("IDTypePay", this._TypePay.get_ID());
				}
			}
		}

		public Batch()
		{
			this.NameTable = "Batch";
			base.Init();
		}
	}
}