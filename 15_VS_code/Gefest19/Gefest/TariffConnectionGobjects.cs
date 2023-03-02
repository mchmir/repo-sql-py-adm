using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TariffConnectionGobjects : SimpleClasss
	{
		public TariffConnectionGobject this[int index]
		{
			get
			{
				return (TariffConnectionGobject)base.get_Item(index);
			}
		}

		public TariffConnectionGobjects()
		{
			this.NameTable = "TariffConnectionGobject";
		}

		public TariffConnectionGobject Add()
		{
			TariffConnectionGobject tariffConnectionGobject = new TariffConnectionGobject();
			tariffConnectionGobject.Init();
			tariffConnectionGobject.set_Parent(this);
			return tariffConnectionGobject;
		}

		public TariffConnectionGobject item(long ID)
		{
			TariffConnectionGobject elementByID;
			try
			{
				elementByID = (TariffConnectionGobject)base.GetElementByID(ID);
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
			TariffConnectionGobject tariffConnectionGobject = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTariffConnectionGobject desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						tariffConnectionGobject = new TariffConnectionGobject();
						tariffConnectionGobject.SetState(row);
						base.Add(tariffConnectionGobject);
					}
					tariffConnectionGobject = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				tariffConnectionGobject = null;
				num = 1;
			}
			return num;
		}
	}
}