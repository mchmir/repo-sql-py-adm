using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Periods : SimpleClasss
	{
		public Period this[int index]
		{
			get
			{
				return (Period)base.get_Item(index);
			}
		}

		public Periods()
		{
			this.NameTable = "Period";
		}

		public Period Add()
		{
			Period period = new Period();
			period.Init();
			period.set_Parent(this);
			return period;
		}

		public Period item(long ID)
		{
			Period elementByID;
			try
			{
				elementByID = (Period)base.GetElementByID(ID);
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
			Period period = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDPeriod");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						period = new Period();
						period.SetState(row);
						base.Add(period);
					}
					period = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				period = null;
				num = 1;
			}
			return num;
		}

		public int Load(DateTime vDate)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Period period = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "Month", "Year" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=Month(", Tools.ConvertDateFORSQL(vDate), ")"), string.Concat(" =Year(", Tools.ConvertDateFORSQL(vDate), ")") };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						period = new Period();
						period.SetState(row);
						base.Add(period);
					}
					period = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				period = null;
				num = 1;
			}
			return num;
		}
	}
}