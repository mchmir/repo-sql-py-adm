using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class StatusGMeters : SimpleClasss
	{
		public StatusGMeter this[int index]
		{
			get
			{
				return (StatusGMeter)base.get_Item(index);
			}
		}

		public StatusGMeters()
		{
			this.NameTable = "StatusGMeter";
		}

		public StatusGMeter Add()
		{
			StatusGMeter statusGMeter = new StatusGMeter();
			statusGMeter.Init();
			statusGMeter.set_Parent(this);
			return statusGMeter;
		}

		public StatusGMeter item(long ID)
		{
			StatusGMeter elementByID;
			try
			{
				elementByID = (StatusGMeter)base.GetElementByID(ID);
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
			StatusGMeter statusGMeter = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDStatusGMeter");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						statusGMeter = new StatusGMeter();
						statusGMeter.SetState(row);
						base.Add(statusGMeter);
					}
					statusGMeter = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				statusGMeter = null;
				num = 1;
			}
			return num;
		}
	}
}