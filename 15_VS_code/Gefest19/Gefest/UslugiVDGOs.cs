using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class UslugiVDGOs : SimpleClasss
	{
		public UslugiVDGO this[int index]
		{
			get
			{
				return (UslugiVDGO)base.get_Item(index);
			}
		}

		public UslugiVDGOs()
		{
			this.NameTable = "UslugiVDGO";
		}

		public UslugiVDGO Add()
		{
			UslugiVDGO uslugiVDGO = new UslugiVDGO();
			uslugiVDGO.Init();
			uslugiVDGO.set_Parent(this);
			return uslugiVDGO;
		}

		public UslugiVDGO item(long ID)
		{
			UslugiVDGO elementByID;
			try
			{
				elementByID = (UslugiVDGO)base.GetElementByID(ID);
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
			UslugiVDGO uslugiVDGO = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDUslugiVDGO");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						uslugiVDGO = new UslugiVDGO();
						uslugiVDGO.SetState(row);
						base.Add(uslugiVDGO);
					}
					uslugiVDGO = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				uslugiVDGO = null;
				num = 1;
			}
			return num;
		}
	}
}