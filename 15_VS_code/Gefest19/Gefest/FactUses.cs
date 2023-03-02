using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class FactUses : SimpleClasss
	{
		private Gobject _gobject;

		private Indication _indication;

		public FactUse this[int index]
		{
			get
			{
				return (FactUse)base.get_Item(index);
			}
		}

		public FactUses()
		{
			this.NameTable = "FactUse";
		}

		public FactUse Add()
		{
			FactUse factUse = new FactUse();
			factUse.set_Parent(this);
			factUse.oGobject = this._gobject;
			factUse.oIndication = this._indication;
			return factUse;
		}

		public FactUse item(long ID)
		{
			FactUse elementByID;
			try
			{
				elementByID = (FactUse)base.GetElementByID(ID);
			}
			catch
			{
				elementByID = null;
			}
			return elementByID;
		}

		public override int Load()
		{
			return 1;
		}

		public int Load(Gobject oGobject)
		{
			int num;
			this._gobject = oGobject;
			int num1 = 0;
			DataTable dataTable = null;
			FactUse factUse = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idGobject" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oGobject.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDPeriod, IDFactUse");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						factUse = new FactUse();
						factUse.SetState(row);
						factUse.oGobject = oGobject;
						base.Add(factUse);
					}
					factUse = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				factUse = null;
				num = 1;
			}
			return num;
		}

		public int Load(Gobject oGobject, long IDPeriod1, long IDPeriod2)
		{
			int num;
			this._gobject = oGobject;
			int num1 = 0;
			DataTable dataTable = null;
			FactUse factUse = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idGobject", "IDPeriod", "IDPeriod" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oGobject.get_ID()), string.Concat(">=", IDPeriod1), string.Concat("<=", IDPeriod2) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDPeriod, IDFactUse");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						factUse = new FactUse();
						factUse.SetState(row);
						factUse.oGobject = oGobject;
						base.Add(factUse);
					}
					factUse = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				factUse = null;
				num = 1;
			}
			return num;
		}

		public int Load(Indication oIndication)
		{
			int num;
			this._indication = oIndication;
			int num1 = 0;
			DataTable dataTable = null;
			FactUse factUse = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idIndication" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oIndication.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDPeriod, IDFactUse");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						factUse = new FactUse();
						factUse.SetState(row);
						factUse.oIndication = oIndication;
						base.Add(factUse);
					}
					factUse = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				factUse = null;
				num = 1;
			}
			return num;
		}

		public int Load(Document oDocument)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			FactUse factUse = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "iddocument" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oDocument.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDPeriod, IDFactUse");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						factUse = new FactUse();
						factUse.SetState(row);
						base.Add(factUse);
					}
					factUse = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				factUse = null;
				num = 1;
			}
			return num;
		}
	}
}