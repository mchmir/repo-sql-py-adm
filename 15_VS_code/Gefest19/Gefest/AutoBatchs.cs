using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class AutoBatchs : SimpleClasss
	{
		public AutoBatch this[int index]
		{
			get
			{
				return (AutoBatch)base.get_Item(index);
			}
		}

		public AutoBatchs()
		{
			this.NameTable = "AutoBatch";
		}

		public AutoBatch Add()
		{
			AutoBatch autoBatch = new AutoBatch();
			autoBatch.set_Parent(this);
			return autoBatch;
		}

		public AutoBatch item(long ID)
		{
			AutoBatch elementByID;
			try
			{
				elementByID = (AutoBatch)base.GetElementByID(ID);
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
			AutoBatch autoBatch = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDAutoBatch");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						autoBatch = new AutoBatch();
						autoBatch.SetState(row);
						base.Add(autoBatch);
					}
					autoBatch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				autoBatch = null;
				num = 1;
			}
			return num;
		}

		public int Load(string filename, int _year)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			AutoBatch autoBatch = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "path", "Year(BatchDate)" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("='", filename, "'"), string.Concat("=", _year.ToString()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDAutoBatch");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						autoBatch = new AutoBatch();
						autoBatch.SetState(row);
						base.Add(autoBatch);
					}
					autoBatch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				autoBatch = null;
				num = 1;
			}
			return num;
		}
	}
}