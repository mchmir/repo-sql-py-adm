using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Banks : SimpleClasss
	{
		public Bank this[int index]
		{
			get
			{
				return (Bank)base.get_Item(index);
			}
		}

		public Banks()
		{
			this.NameTable = "Bank";
		}

		public Bank Add()
		{
			Bank bank = new Bank();
			bank.Init();
			bank.set_Parent(this);
			return bank;
		}

		public Bank item(long ID)
		{
			Bank elementByID;
			try
			{
				elementByID = (Bank)base.GetElementByID(ID);
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
			Bank bank = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDBank");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						bank = new Bank();
						bank.SetState(row);
						base.Add(bank);
					}
					bank = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				bank = null;
				num = 1;
			}
			return num;
		}
	}
}