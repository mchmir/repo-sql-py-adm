using System;
using WebSecurityDLL;

namespace Gefest
{
	public class AutoDocument : SimpleClass
	{
		private AutoBatch _AutoBatch;

		private Document _Document;

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

		public double DocumentAmount
		{
			get
			{
				return (double)base.GetValue("DocumentAmount");
			}
			set
			{
				base.SetValue("DocumentAmount", value);
			}
		}

		public DateTime DocumentDate
		{
			get
			{
				return (DateTime)base.GetValue("DocumentDate");
			}
			set
			{
				base.SetValue("DocumentDate", value);
			}
		}

		public string FIO
		{
			get
			{
				return (string)base.GetValue("FIO");
			}
			set
			{
				base.SetValue("FIO", value);
			}
		}

		public int IDStatusAutoDocument
		{
			get
			{
				return (int)base.GetValue("IDStatusAutoDocument");
			}
			set
			{
				base.SetValue("IDStatusAutoDocument", value);
			}
		}

		public string Number
		{
			get
			{
				return (string)base.GetValue("Number");
			}
			set
			{
				base.SetValue("Number", value);
			}
		}

		public AutoBatch oAutoBatch
		{
			get
			{
				if (this._AutoBatch == null)
				{
					int value = (int)base.GetValue("IDAutoBatch");
					this._AutoBatch = new AutoBatch();
					if (this._AutoBatch.Load((long)value) != 0)
					{
						this._AutoBatch = null;
					}
				}
				return this._AutoBatch;
			}
			set
			{
				this._AutoBatch = value;
				if (this._AutoBatch != null)
				{
					base.SetValue("IDAutoBatch", this._AutoBatch.get_ID());
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

		public AutoDocument()
		{
			this.NameTable = "AutoDocument";
			base.Init();
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}