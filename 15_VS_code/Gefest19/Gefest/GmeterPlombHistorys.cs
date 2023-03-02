using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class GmeterPlombHistorys : SimpleClasss
	{
		public GmeterPlombHistory this[int index]
		{
			get
			{
				return (GmeterPlombHistory)base.get_Item(index);
			}
		}

		public GmeterPlombHistorys()
		{
			this.NameTable = "GmeterPlombHistory";
		}

		public GmeterPlombHistory Add()
		{
			GmeterPlombHistory gmeterPlombHistory = new GmeterPlombHistory();
			gmeterPlombHistory.Init();
			gmeterPlombHistory.set_Parent(this);
			return gmeterPlombHistory;
		}

		public GmeterPlombHistory item(long ID)
		{
			GmeterPlombHistory elementByID;
			try
			{
				elementByID = (GmeterPlombHistory)base.GetElementByID(ID);
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

		public int Load(long IDGMeter)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			GmeterPlombHistory gmeterPlombHistory = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idgmeter" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", IDGMeter.ToString()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "dateplomb");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						gmeterPlombHistory = new GmeterPlombHistory();
						gmeterPlombHistory.SetState(row);
						base.Add(gmeterPlombHistory);
					}
					gmeterPlombHistory = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				gmeterPlombHistory = null;
				num = 1;
			}
			return num;
		}
	}
}