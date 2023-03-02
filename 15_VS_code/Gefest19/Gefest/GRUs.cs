using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class GRUs : SimpleClasss
	{
		public GRU this[int index]
		{
			get
			{
				return (GRU)base.get_Item(index);
			}
		}

		public GRUs()
		{
			this.NameTable = "GRU";
		}

		public GRU Add()
		{
			GRU gRU = new GRU();
			gRU.Init();
			gRU.set_Parent(this);
			return gRU;
		}

		public GRU item(long ID)
		{
			return (GRU)base.GetElementByID(ID);
		}

		public override int Load()
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			GRU gRU = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "invnumber desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						gRU = new GRU();
						gRU.SetState(row);
						base.Add(gRU);
					}
					gRU = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				gRU = null;
				num = 1;
			}
			return num;
		}

		public int Load(ClassGRU oClassGRU)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			GRU gRU = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "IdClassGRU" };
				string[] strArrays1 = strArrays;
				strArrays = new string[1];
				long d = oClassGRU.get_ID();
				strArrays[0] = string.Concat("=", d.ToString());
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "invnumber desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						gRU = new GRU();
						gRU.SetState(row);
						base.Add(gRU);
					}
					gRU = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				gRU = null;
				num = 1;
			}
			return num;
		}
	}
}