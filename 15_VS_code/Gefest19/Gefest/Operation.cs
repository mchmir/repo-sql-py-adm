using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Operation : SimpleClass
	{
		private Balance _Balance;

		private Document _Document;

		private TypeOperation _TypeOperation;

		public double AmountOperation
		{
			get
			{
				return (double)base.GetValue("AmountOperation");
			}
			set
			{
				base.SetValue("AmountOperation", value);
			}
		}

		public DateTime DateOperation
		{
			get
			{
				return (DateTime)base.GetValue("DateOperation");
			}
			set
			{
				base.SetValue("DateOperation", value);
			}
		}

		public int NumberOperation
		{
			get
			{
				return (int)base.GetValue("NumberOperation");
			}
			set
			{
				base.SetValue("NumberOperation", value);
			}
		}

		public Balance oBalance
		{
			get
			{
				if (this._Balance == null)
				{
					int value = (int)base.GetValue("IDBalance");
					this._Balance = new Balance();
					if (this._Balance.Load((long)value) != 0)
					{
						this._Balance = null;
					}
				}
				return this._Balance;
			}
			set
			{
				this._Balance = value;
				if (this._Balance != null)
				{
					base.SetValue("IDBalance", this._Balance.get_ID());
				}
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

		public TypeOperation oTypeOperation
		{
			get
			{
				if (this._TypeOperation == null)
				{
					int value = (int)base.GetValue("IDTypeOperation");
					this._TypeOperation = new TypeOperation();
					if (this._TypeOperation.Load((long)value) != 0)
					{
						this._TypeOperation = null;
					}
				}
				return this._TypeOperation;
			}
			set
			{
				this._TypeOperation = value;
				if (this._TypeOperation != null)
				{
					base.SetValue("IDTypeOperation", this._TypeOperation.get_ID());
				}
			}
		}

		public Operation()
		{
			this.NameTable = "Operation";
			base.Init();
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}