using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Stavkas : SimpleClasss
	{
		public Stavka this[int index]
		{
			get
			{
				return (Stavka)base.get_Item(index);
			}
		}

		public Stavkas()
		{
			this.NameTable = "Stavka";
		}

		public Stavka Add()
		{
			Stavka stavka = new Stavka();
			stavka.Init();
			stavka.set_Parent(this);
			return stavka;
		}

		public Stavka item(long ID)
		{
			Stavka elementByID;
			try
			{
				elementByID = (Stavka)base.GetElementByID(ID);
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
			Stavka stavka = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "Name desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						stavka = new Stavka();
						stavka.SetState(row);
						base.Add(stavka);
					}
					stavka = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				stavka = null;
				num = 1;
			}
			return num;
		}
	}
}