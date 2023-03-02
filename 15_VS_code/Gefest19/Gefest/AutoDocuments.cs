using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class AutoDocuments : SimpleClasss
	{
		public AutoDocument this[int index]
		{
			get
			{
				return (AutoDocument)base.get_Item(index);
			}
		}

		public AutoDocuments()
		{
			this.NameTable = "AutoDocument";
		}

		public AutoDocument Add()
		{
			AutoDocument autoDocument = new AutoDocument();
			autoDocument.set_Parent(this);
			return autoDocument;
		}

		public AutoDocument item(long ID)
		{
			AutoDocument elementByID;
			try
			{
				elementByID = (AutoDocument)base.GetElementByID(ID);
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
			AutoDocument autoDocument = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDAutoDocument");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						autoDocument = new AutoDocument();
						autoDocument.SetState(row);
						base.Add(autoDocument);
					}
					autoDocument = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				autoDocument = null;
				num = 1;
			}
			return num;
		}

		public int Load(AutoBatch oAutoBatch, string DocumentNumber)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			AutoDocument autoDocument = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idautobatch", "number" };
				string[] strArrays1 = strArrays;
				strArrays = new string[2];
				long d = oAutoBatch.get_ID();
				strArrays[0] = string.Concat("=", d.ToString());
				strArrays[1] = string.Concat("=", DocumentNumber);
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDAutoDocument");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						autoDocument = new AutoDocument();
						autoDocument.SetState(row);
						base.Add(autoDocument);
					}
					autoDocument = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				autoDocument = null;
				num = 1;
			}
			return num;
		}
	}
}