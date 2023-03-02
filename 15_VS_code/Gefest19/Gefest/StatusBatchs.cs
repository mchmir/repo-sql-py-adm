using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class StatusBatchs : SimpleClasss
	{
		public StatusBatch this[int index]
		{
			get
			{
				return (StatusBatch)base.get_Item(index);
			}
		}

		public StatusBatchs()
		{
			this.NameTable = "StatusBatch";
		}

		public StatusBatch Add()
		{
			StatusBatch statusBatch = new StatusBatch();
			statusBatch.Init();
			statusBatch.set_Parent(this);
			return statusBatch;
		}

		public StatusBatch item(long ID)
		{
			StatusBatch elementByID;
			try
			{
				elementByID = (StatusBatch)base.GetElementByID(ID);
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
			StatusBatch statusBatch = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDStatusBatch");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						statusBatch = new StatusBatch();
						statusBatch.SetState(row);
						base.Add(statusBatch);
					}
					statusBatch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				statusBatch = null;
				num = 1;
			}
			return num;
		}
	}
}