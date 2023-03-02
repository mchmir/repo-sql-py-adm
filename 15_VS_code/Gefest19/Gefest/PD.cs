using System;
using WebSecurityDLL;

namespace Gefest
{
	public class PD : SimpleClass
	{
		private Document _Document;

		private TypePD _TypePD;

		public int IDDocument
		{
			get
			{
				return (int)base.GetValue("IDDocument");
			}
			set
			{
				base.SetValue("IDDocument", value);
			}
		}

		public int IDTypePD
		{
			get
			{
				return (int)base.GetValue("IDTypePD");
			}
			set
			{
				base.SetValue("IDTypePD", value);
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

		public TypePD oTypePD
		{
			get
			{
				if (this._TypePD == null)
				{
					int value = (int)base.GetValue("IDTypePD");
					this._TypePD = new TypePD();
					if (this._TypePD.Load((long)value) != 0)
					{
						this._TypePD = null;
					}
				}
				return this._TypePD;
			}
			set
			{
				this._TypePD = value;
				if (this._TypePD != null)
				{
					base.SetValue("IDTypePD", this._TypePD.get_ID());
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

		public PD()
		{
			this.NameTable = "PD";
			base.Init();
		}
	}
}