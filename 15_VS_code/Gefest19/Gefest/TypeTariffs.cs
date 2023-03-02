using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeTariffs : SimpleClasss
	{
		public TypeTariff this[int index]
		{
			get
			{
				return (TypeTariff)base.get_Item(index);
			}
		}

		public TypeTariffs()
		{
			this.NameTable = "TypeTariff";
		}

		public TypeTariff Add()
		{
			TypeTariff typeTariff = new TypeTariff();
			typeTariff.Init();
			typeTariff.set_Parent(this);
			return typeTariff;
		}

		public TypeTariff item(long ID)
		{
			TypeTariff elementByID;
			try
			{
				elementByID = (TypeTariff)base.GetElementByID(ID);
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
			TypeTariff typeTariff = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeTariff");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeTariff = new TypeTariff();
						typeTariff.SetState(row);
						base.Add(typeTariff);
					}
					typeTariff = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeTariff = null;
				num = 1;
			}
			return num;
		}
	}
}