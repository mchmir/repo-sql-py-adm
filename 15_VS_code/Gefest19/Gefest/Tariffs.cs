using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Tariffs : SimpleClasss
	{
		public Tariff this[int index]
		{
			get
			{
				return (Tariff)base.get_Item(index);
			}
		}

		public Tariffs()
		{
			this.NameTable = "Tariff";
		}

		public Tariff Add()
		{
			Tariff tariff = new Tariff();
			tariff.Init();
			tariff.set_Parent(this);
			return tariff;
		}

		public Tariff item(long ID)
		{
			Tariff elementByID;
			try
			{
				elementByID = (Tariff)base.GetElementByID(ID);
			}
			catch
			{
				elementByID = null;
			}
			return elementByID;
		}

		public override int Load()
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Tariff tariff = null;
			try
			{
				num1 = Loader.LoadCollection("select distinct case when idperiod<63 then (value*2.85) else value end as value from tariff order by value asc", ref dataTable, "");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						tariff = new Tariff();
						tariff.SetState(row);
						base.Add(tariff);
					}
					tariff = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				tariff = null;
				num = 1;
			}
			return num;
		}

		public int Load(Period oPeriod)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Tariff tariff = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idPeriod" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oPeriod.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idTypeTariff");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						tariff = new Tariff();
						tariff.SetState(row);
						tariff.oPeriod = oPeriod;
						base.Add(tariff);
					}
					tariff = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				tariff = null;
				num = 1;
			}
			return num;
		}

		public int Load(TypeTariff oTypeTariff)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Tariff tariff = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idTypeTariff" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oTypeTariff.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idPeriod");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						tariff = new Tariff();
						tariff.SetState(row);
						tariff.oTypeTariff = oTypeTariff;
						base.Add(tariff);
					}
					tariff = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				tariff = null;
				num = 1;
			}
			return num;
		}
	}
}