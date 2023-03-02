using System;
using WebSecurityDLL;

namespace Gefest
{
	public class AutoBatch : SimpleClass
	{
		private Batch _Batch;

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

		public string Path
		{
			get
			{
				return (string)base.GetValue("Path");
			}
			set
			{
				base.SetValue("Path", value);
			}
		}

		public AutoBatch()
		{
			this.NameTable = "AutoBatch";
			base.Init();
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}