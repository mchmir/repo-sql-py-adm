using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class BalanceReals : SimpleClasss
	{
		private Contract _contract;

		public BalanceReal this[int index]
		{
			get
			{
				return (BalanceReal)base.get_Item(index);
			}
		}

		public BalanceReals()
		{
			this.NameTable = "BalanceReal";
		}

		public BalanceReal Add()
		{
			BalanceReal balanceReal = new BalanceReal();
			balanceReal.Init();
			balanceReal.oContract = this._contract;
			balanceReal.set_Parent(this);
			return balanceReal;
		}

		public BalanceReal item(long ID)
		{
			BalanceReal elementByID;
			try
			{
				elementByID = (BalanceReal)base.GetElementByID(ID);
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
			BalanceReal balanceReal = null;
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
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idbalancereal");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						balanceReal = new BalanceReal();
						balanceReal.SetState(row);
						balanceReal.oContract = this._contract;
						base.Add(balanceReal);
					}
					balanceReal = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				balanceReal = null;
				num = 1;
			}
			return num;
		}
	}
}