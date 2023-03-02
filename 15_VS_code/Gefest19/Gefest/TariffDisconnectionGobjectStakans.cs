using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TariffDisconnectionGobjectStakans : SimpleClasss
	{
		public TariffDisconnectionGobjectStakan this[int index]
		{
			get
			{
				return (TariffDisconnectionGobjectStakan)base.get_Item(index);
			}
		}

		public TariffDisconnectionGobjectStakans()
		{
			this.NameTable = "TariffDisconnectionGobjectStakan";
		}

		public TariffDisconnectionGobjectStakan Add()
		{
			TariffDisconnectionGobjectStakan tariffDisconnectionGobjectStakan = new TariffDisconnectionGobjectStakan();
			tariffDisconnectionGobjectStakan.Init();
			tariffDisconnectionGobjectStakan.set_Parent(this);
			return tariffDisconnectionGobjectStakan;
		}

		public TariffDisconnectionGobjectStakan item(long ID)
		{
			TariffDisconnectionGobjectStakan elementByID;
			try
			{
				elementByID = (TariffDisconnectionGobjectStakan)base.GetElementByID(ID);
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
			TariffDisconnectionGobjectStakan tariffDisconnectionGobjectStakan = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTariffDisconnectionGobjectStakan desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						tariffDisconnectionGobjectStakan = new TariffDisconnectionGobjectStakan();
						tariffDisconnectionGobjectStakan.SetState(row);
						base.Add(tariffDisconnectionGobjectStakan);
					}
					tariffDisconnectionGobjectStakan = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				tariffDisconnectionGobjectStakan = null;
				num = 1;
			}
			return num;
		}
	}
}