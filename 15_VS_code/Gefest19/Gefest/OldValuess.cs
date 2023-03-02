using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class OldValuess : SimpleClasss
	{
		public OldValues this[int index]
		{
			get
			{
				return (OldValues)base.get_Item(index);
			}
		}

		public OldValuess()
		{
			this.NameTable = "OldValues";
		}

		public OldValues Add()
		{
			OldValues oldValue = new OldValues();
			oldValue.set_Parent(this);
			return oldValue;
		}

		public OldValues item(long ID)
		{
			OldValues elementByID;
			try
			{
				elementByID = (OldValues)base.GetElementByID(ID);
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
			OldValues oldValue = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDOldValues");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						oldValue = new OldValues();
						oldValue.SetState(row);
						base.Add(oldValue);
					}
					oldValue = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				oldValue = null;
				num = 1;
			}
			return num;
		}

		public int Load(string table, string column, long IDObject, Period oPeriod, DateTime vDate)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			OldValues oldValue = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "NameTable", "NameColumn", "IDObject", "IDPeriod", "DateValues" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", table), string.Concat("=", column), string.Concat("=", IDObject.ToString()), null, null };
				long d = oPeriod.get_ID();
				strArrays[3] = string.Concat("=", d.ToString());
				strArrays[4] = string.Concat(">=", Tools.ConvertDateFORSQL(vDate));
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDOldValues");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						oldValue = new OldValues();
						oldValue.SetState(row);
						base.Add(oldValue);
					}
					oldValue = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				oldValue = null;
				num = 1;
			}
			return num;
		}
	}
}