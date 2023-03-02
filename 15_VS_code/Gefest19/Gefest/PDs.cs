using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class PDs : SimpleClasss
	{
		public PD this[int index]
		{
			get
			{
				return (PD)base.get_Item(index);
			}
		}

		public PDs()
		{
			this.NameTable = "PD";
		}

		public PD Add()
		{
			PD pD = new PD();
			pD.set_Parent(this);
			return pD;
		}

		public PD item(long ID)
		{
			PD elementByID;
			try
			{
				elementByID = (PD)base.GetElementByID(ID);
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
			PD pD = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDPD");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						pD = new PD();
						pD.SetState(row);
						base.Add(pD);
					}
					pD = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				pD = null;
				num = 1;
			}
			return num;
		}

		public int Load(Document oDoc)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			PD pD = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idDocument" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oDoc.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDPD");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						pD = new PD();
						pD.SetState(row);
						base.Add(pD);
					}
					pD = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				pD = null;
				num = 1;
			}
			return num;
		}
	}
}