using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TariffDisconnectionGobjects : SimpleClasss
	{
		public TariffDisconnectionGobject this[int index]
		{
			get
			{
				return (TariffDisconnectionGobject)base.get_Item(index);
			}
		}

		public TariffDisconnectionGobjects()
		{
			this.NameTable = "TariffDisconnectionGobject";
		}

		public TariffDisconnectionGobject Add()
		{
			TariffDisconnectionGobject tariffDisconnectionGobject = new TariffDisconnectionGobject();
			tariffDisconnectionGobject.Init();
			tariffDisconnectionGobject.set_Parent(this);
			return tariffDisconnectionGobject;
		}

		public TariffDisconnectionGobject item(long ID)
		{
			TariffDisconnectionGobject elementByID;
			try
			{
				elementByID = (TariffDisconnectionGobject)base.GetElementByID(ID);
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
			TariffDisconnectionGobject tariffDisconnectionGobject = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTariffDisconnectionGobject desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						tariffDisconnectionGobject = new TariffDisconnectionGobject();
						tariffDisconnectionGobject.SetState(row);
						base.Add(tariffDisconnectionGobject);
					}
					tariffDisconnectionGobject = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				tariffDisconnectionGobject = null;
				num = 1;
			}
			return num;
		}
	}
}