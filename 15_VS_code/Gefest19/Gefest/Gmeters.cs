using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Gmeters : SimpleClasss
	{
		private Gobject _gobject;

		public Gmeter this[int index]
		{
			get
			{
				return (Gmeter)base.get_Item(index);
			}
		}

		public Gmeters()
		{
			this.NameTable = "Gmeter";
		}

		public Gmeter Add()
		{
			Gmeter gmeter = new Gmeter();
			gmeter.Init();
			gmeter.oGobject = this._gobject;
			gmeter.set_Parent(this);
			return gmeter;
		}

		public Gmeter item(long ID)
		{
			Gmeter elementByID;
			try
			{
				elementByID = (Gmeter)base.GetElementByID(ID);
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

		public int Load(Gobject oGobject, StatusGMeter oStatusGMeter)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Gmeter gmeter = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idGobject", "idStatusGMeter" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oGobject.get_ID()), string.Concat("=", oStatusGMeter.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idgmeter");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						gmeter = new Gmeter();
						gmeter.SetState(row);
						base.Add(gmeter);
					}
					gmeter = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				gmeter = null;
				num = 1;
			}
			return num;
		}

		public int Load(Gobject oGobject)
		{
			int num;
			this._gobject = oGobject;
			int num1 = 0;
			DataTable dataTable = null;
			Gmeter gmeter = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idGobject" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oGobject.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idstatusgmeter desc, idgmeter");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						gmeter = new Gmeter();
						gmeter.SetState(row);
						gmeter.oGobject = this._gobject;
						base.Add(gmeter);
					}
					gmeter = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				gmeter = null;
				num = 1;
			}
			return num;
		}

		public int Load(string sernumber, long idtypegmeter)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Gmeter gmeter = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "serialnumber", "idtypegmeter" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("='", sernumber, "'"), string.Concat("=", idtypegmeter.ToString()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idgmeter");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						gmeter = new Gmeter();
						gmeter.SetState(row);
						base.Add(gmeter);
					}
					gmeter = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				gmeter = null;
				num = 1;
			}
			return num;
		}
	}
}