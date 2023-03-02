using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Enterprises : SimpleClasss
	{
		public Enterprise this[int index]
		{
			get
			{
				return (Enterprise)base.get_Item(index);
			}
		}

		public Enterprises()
		{
			this.NameTable = "Enterprise";
		}

		public Enterprise Add()
		{
			Enterprise enterprise = new Enterprise();
			enterprise.Init();
			enterprise.set_Parent(this);
			return enterprise;
		}

		public Enterprise item(long ID)
		{
			Enterprise elementByID;
			try
			{
				elementByID = (Enterprise)base.GetElementByID(ID);
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
			Enterprise enterprise = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDEnterprise");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						enterprise = new Enterprise();
						enterprise.SetState(row);
						base.Add(enterprise);
					}
					enterprise = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				enterprise = null;
				num = 1;
			}
			return num;
		}
	}
}