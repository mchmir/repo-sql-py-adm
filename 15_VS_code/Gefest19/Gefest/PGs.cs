using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class PGs : SimpleClasss
	{
		public PG this[int index]
		{
			get
			{
				return (PG)base.get_Item(index);
			}
		}

		public PGs()
		{
			this.NameTable = "PG";
		}

		public PG Add()
		{
			PG pG = new PG();
			pG.Init();
			pG.set_Parent(this);
			return pG;
		}

		public PG item(long ID)
		{
			PG elementByID;
			try
			{
				elementByID = (PG)base.GetElementByID(ID);
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
			PG pG = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDPG");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						pG = new PG();
						pG.SetState(row);
						base.Add(pG);
					}
					pG = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				pG = null;
				num = 1;
			}
			return num;
		}
	}
}