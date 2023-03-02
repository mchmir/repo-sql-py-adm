using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Accountings : SimpleClasss
	{
		public Accounting this[int index]
		{
			get
			{
				return (Accounting)base.get_Item(index);
			}
		}

		public Accountings()
		{
			this.NameTable = "Accounting";
		}

		public Accounting Add()
		{
			Accounting accounting = new Accounting();
			accounting.Init();
			accounting.set_Parent(this);
			return accounting;
		}

		public Accounting item(long ID)
		{
			Accounting elementByID;
			try
			{
				elementByID = (Accounting)base.GetElementByID(ID);
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
			Accounting accounting = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDAccounting desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						accounting = new Accounting();
						accounting.SetState(row);
						base.Add(accounting);
					}
					accounting = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				accounting = null;
				num = 1;
			}
			return num;
		}
	}
}