using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Sendings : SimpleClasss
	{
		public Sending this[int index]
		{
			get
			{
				return (Sending)base.get_Item(index);
			}
		}

		public Sendings()
		{
			this.NameTable = "Sending";
		}

		public Sending Add()
		{
			Sending sending = new Sending();
			sending.set_Parent(this);
			return sending;
		}

		public Sending item(long ID)
		{
			Sending elementByID;
			try
			{
				elementByID = (Sending)base.GetElementByID(ID);
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
			Sending sending = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDSending");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						sending = new Sending();
						sending.SetState(row);
						base.Add(sending);
					}
					sending = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				sending = null;
				num = 1;
			}
			return num;
		}

		public int Load(int IDCorrespondent, int number)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Sending sending = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idCorrespondent", "numbersending" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", IDCorrespondent.ToString()), string.Concat("=", number.ToString()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDSending");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						sending = new Sending();
						sending.SetState(row);
						base.Add(sending);
					}
					sending = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				sending = null;
				num = 1;
			}
			return num;
		}

		public int Load(int IDCorrespondent)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Sending sending = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idCorrespondent" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", IDCorrespondent.ToString()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDSending");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						sending = new Sending();
						sending.SetState(row);
						base.Add(sending);
					}
					sending = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				sending = null;
				num = 1;
			}
			return num;
		}

		public int Load(Correspondent oCorrespondent)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Sending sending = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "IDCorrespondent" };
				string[] strArrays1 = strArrays;
				strArrays = new string[1];
				long d = oCorrespondent.get_ID();
				strArrays[0] = string.Concat("=", d.ToString());
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDSending");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						sending = new Sending();
						sending.SetState(row);
						base.Add(sending);
					}
					sending = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				sending = null;
				num = 1;
			}
			return num;
		}

		public int Load(Correspondent[] oCorrespondent)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Sending sending = null;
			try
			{
				string str = " in (";
				Correspondent[] correspondentArray = oCorrespondent;
				for (int i = 0; i < (int)correspondentArray.Length; i++)
				{
					long d = correspondentArray[i].get_ID();
					str = string.Concat(str, d.ToString(), ",");
				}
				str = string.Concat(str.Substring(0, str.Length - 1), ")");
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idCorrespondent" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { str };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDCorrespondent,IDSending");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						sending = new Sending();
						sending.SetState(row);
						base.Add(sending);
					}
					sending = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				sending = null;
				num = 1;
			}
			return num;
		}
	}
}