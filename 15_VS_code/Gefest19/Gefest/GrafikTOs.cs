using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class GrafikTOs : SimpleClasss
	{
		private GrafikTO _GrafikTO;

		public GrafikTO this[int index]
		{
			get
			{
				return (GrafikTO)base.get_Item(index);
			}
		}

		public GrafikTOs()
		{
			this.NameTable = "GrafikTO";
		}

		public GrafikTO Add()
		{
			GrafikTO grafikTO = new GrafikTO();
			grafikTO.Init();
			grafikTO.set_Parent(this);
			return grafikTO;
		}

		public GrafikTO item(long ID)
		{
			GrafikTO elementByID;
			try
			{
				elementByID = (GrafikTO)base.GetElementByID(ID);
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

		public int Load(Contract oCt, int year)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			GrafikTO grafikTO = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idcontract", "year" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oCt.get_ID()), string.Concat("=", year) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "Month");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						grafikTO = new GrafikTO();
						grafikTO.SetState(row);
						base.Add(grafikTO);
					}
					grafikTO = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				grafikTO = null;
				num = 1;
			}
			return num;
		}
	}
}