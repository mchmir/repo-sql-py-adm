using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class StatusGObjects : SimpleClasss
	{
		public StatusGObject this[int index]
		{
			get
			{
				return (StatusGObject)base.get_Item(index);
			}
		}

		public StatusGObjects()
		{
			this.NameTable = "StatusGObject";
		}

		public StatusGObject Add()
		{
			StatusGObject statusGObject = new StatusGObject();
			statusGObject.Init();
			statusGObject.set_Parent(this);
			return statusGObject;
		}

		public StatusGObject item(long ID)
		{
			StatusGObject elementByID;
			try
			{
				elementByID = (StatusGObject)base.GetElementByID(ID);
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
			StatusGObject statusGObject = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDStatusGObject");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						statusGObject = new StatusGObject();
						statusGObject.SetState(row);
						base.Add(statusGObject);
					}
					statusGObject = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				statusGObject = null;
				num = 1;
			}
			return num;
		}
	}
}