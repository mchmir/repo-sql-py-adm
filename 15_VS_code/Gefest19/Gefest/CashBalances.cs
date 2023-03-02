using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class CashBalances : SimpleClasss
	{
		public CashBalance this[int index]
		{
			get
			{
				return (CashBalance)base.get_Item(index);
			}
		}

		public CashBalances()
		{
			this.NameTable = "CashBalance";
		}

		public CashBalance Add()
		{
			CashBalance cashBalance = new CashBalance();
			cashBalance.set_Parent(this);
			return cashBalance;
		}

		public CashBalance item(long ID)
		{
			CashBalance elementByID;
			try
			{
				elementByID = (CashBalance)base.GetElementByID(ID);
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
			CashBalance cashBalance = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDCashBalance");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						cashBalance = new CashBalance();
						cashBalance.SetState(row);
						base.Add(cashBalance);
					}
					cashBalance = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				cashBalance = null;
				num = 1;
			}
			return num;
		}

		public int Load(DateTime vDate, Agent oCashier)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			CashBalance cashBalance = null;
			try
			{
				string[] str = new string[] { "select top 1 * from CashBalance with (nolock) where DateCash<=", Tools.ConvertDateFORSQL(vDate), " and IDCashier=", null, null };
				str[3] = oCashier.get_ID().ToString();
				str[4] = " order by DateCash desc";
				string str1 = string.Concat(str);
				num1 = Loader.LoadCollection(str1, ref dataTable, "");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						cashBalance = new CashBalance();
						cashBalance.SetState(row);
						base.Add(cashBalance);
					}
					cashBalance = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				cashBalance = null;
				num = 1;
			}
			return num;
		}
	}
}