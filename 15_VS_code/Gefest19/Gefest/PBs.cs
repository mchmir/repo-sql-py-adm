using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class PBs : SimpleClasss
	{
		public PB this[int index]
		{
			get
			{
				return (PB)base.get_Item(index);
			}
		}

		public PBs()
		{
			this.NameTable = "PB";
		}

		public PB Add()
		{
			PB pB = new PB();
			pB.Init();
			pB.set_Parent(this);
			return pB;
		}

		public PB item(long ID)
		{
			PB elementByID;
			try
			{
				elementByID = (PB)base.GetElementByID(ID);
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
			PB pB = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDPB");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						pB = new PB();
						pB.SetState(row);
						base.Add(pB);
					}
					pB = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				pB = null;
				num = 1;
			}
			return num;
		}

		public int Load(Document oDoc)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			PB pB = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idDocument" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oDoc.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDPB");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						pB = new PB();
						pB.SetState(row);
						base.Add(pB);
					}
					pB = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				pB = null;
				num = 1;
			}
			return num;
		}
	}
}