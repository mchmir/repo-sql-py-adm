using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Indications : SimpleClasss
	{
		private Gmeter _gmeter;

		public Indication this[int index]
		{
			get
			{
				return (Indication)base.get_Item(index);
			}
		}

		public Indications()
		{
			this.NameTable = "Indication";
		}

		public Indication Add()
		{
			Indication indication = new Indication();
			indication.set_Parent(this);
			indication.oGmeter = this._gmeter;
			return indication;
		}

		public Indication item(long ID)
		{
			Indication elementByID;
			try
			{
				elementByID = (Indication)base.GetElementByID(ID);
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

		public int Load(Gmeter oGmeter, DateTime vDateB, DateTime vDateE)
		{
			int num;
			this._gmeter = oGmeter;
			int num1 = 0;
			DataTable dataTable = null;
			Indication indication = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idgmeter", "datedisplay", "datedisplay" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oGmeter.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateB)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateE)) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DateDisplay");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						indication = new Indication();
						indication.SetState(row);
						indication.oGmeter = oGmeter;
						base.Add(indication);
					}
					indication = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				indication = null;
				num = 1;
			}
			return num;
		}

		public int Load(Gmeter oGmeter)
		{
			int num;
			this._gmeter = oGmeter;
			int num1 = 0;
			DataTable dataTable = null;
			Indication indication = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idgmeter" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oGmeter.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DateDisplay");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						indication = new Indication();
						indication.SetState(row);
						indication.oGmeter = oGmeter;
						base.Add(indication);
					}
					indication = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				indication = null;
				num = 1;
			}
			return num;
		}

		public int Load(DateTime vDate, TypeIndication oTypeIndication, Agent oAgent)
		{
			int num;
			long d;
			int num1 = 0;
			DataTable dataTable = null;
			long d1 = (long)0;
			string str = " is null";
			if (oTypeIndication != null)
			{
				d1 = oTypeIndication.get_ID();
			}
			if (oAgent != null)
			{
				d = oAgent.get_ID();
				str = string.Concat("=", d.ToString());
			}
			Indication indication = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "dateadd", "iduser", "idtypeindication", "idagent" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", Tools.ConvertDateFORSQL(vDate)), null, null, null };
				d = SQLConnect.CurrentUser.get_ID();
				strArrays[1] = string.Concat("=", d.ToString());
				strArrays[2] = string.Concat("=", d1);
				strArrays[3] = str;
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idindication");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						indication = new Indication();
						indication.SetState(row);
						base.Add(indication);
					}
					indication = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				indication = null;
				num = 1;
			}
			return num;
		}
	}
}