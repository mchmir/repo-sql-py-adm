using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Balances : SimpleClasss
	{
		private Contract _contract;

		public Balance this[int index]
		{
			get
			{
				return (Balance)base.get_Item(index);
			}
		}

		public Balances()
		{
			this.NameTable = "Balance";
		}

		public Balance Add()
		{
			Balance balance = new Balance();
			balance.Init();
			balance.oContract = this._contract;
			balance.set_Parent(this);
			return balance;
		}

		public Balance item(long ID)
		{
			Balance elementByID;
			try
			{
				elementByID = (Balance)base.GetElementByID(ID);
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

		public int Load(Contract oContract, Period oPeriod)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			this._contract = oContract;
			Balance balance = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idContract", "IDPeriod" };
				string[] strArrays1 = strArrays;
				strArrays = new string[2];
				long d = oContract.get_ID();
				strArrays[0] = string.Concat("=", d.ToString());
				d = oPeriod.get_ID();
				strArrays[1] = string.Concat("=", d.ToString());
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idbalance");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						balance = new Balance();
						balance.SetState(row);
						balance.oContract = this._contract;
						base.Add(balance);
					}
					balance = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				balance = null;
				num = 1;
			}
			return num;
		}
	}
}