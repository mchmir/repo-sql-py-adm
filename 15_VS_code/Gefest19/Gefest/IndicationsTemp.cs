using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class IndicationsTemp : SimpleClasss
	{
		private Gmeter _gmeter;

		public IndicationTemp this[int index]
		{
			get
			{
				return (IndicationTemp)base.get_Item(index);
			}
		}

		public IndicationsTemp()
		{
			this.NameTable = "IndicationTemp";
		}

		public IndicationTemp Add()
		{
			IndicationTemp indicationTemp = new IndicationTemp();
			indicationTemp.set_Parent(this);
			indicationTemp.oGmeter = this._gmeter;
			return indicationTemp;
		}

		public IndicationTemp item(long ID)
		{
			IndicationTemp elementByID;
			try
			{
				elementByID = (IndicationTemp)base.GetElementByID(ID);
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
			IndicationTemp indicationTemp = null;
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
						indicationTemp = new IndicationTemp();
						indicationTemp.SetState(row);
						indicationTemp.oGmeter = oGmeter;
						base.Add(indicationTemp);
					}
					indicationTemp = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				indicationTemp = null;
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
			IndicationTemp indicationTemp = null;
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
						indicationTemp = new IndicationTemp();
						indicationTemp.SetState(row);
						indicationTemp.oGmeter = oGmeter;
						base.Add(indicationTemp);
					}
					indicationTemp = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				indicationTemp = null;
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
			IndicationTemp indicationTemp = null;
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
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idindicationtemp");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						indicationTemp = new IndicationTemp();
						indicationTemp.SetState(row);
						base.Add(indicationTemp);
					}
					indicationTemp = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				indicationTemp = null;
				num = 1;
			}
			return num;
		}

		public int Load(DateTime vDate)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			IndicationTemp indicationTemp = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "dateadd" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", Tools.ConvertDateFORSQL(vDate)) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idindicationtemp");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						indicationTemp = new IndicationTemp();
						indicationTemp.SetState(row);
						base.Add(indicationTemp);
					}
					indicationTemp = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				indicationTemp = null;
				num = 1;
			}
			return num;
		}
	}
}