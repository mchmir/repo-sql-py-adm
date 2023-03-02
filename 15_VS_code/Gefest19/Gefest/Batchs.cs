using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Batchs : SimpleClasss
	{
		public Batch this[int index]
		{
			get
			{
				return (Batch)base.get_Item(index);
			}
		}

		public Batchs()
		{
			this.NameTable = "Batch";
		}

		public Batch Add()
		{
			Batch batch = new Batch();
			batch.set_Parent(this);
			return batch;
		}

		public Batch item(long ID)
		{
			Batch elementByID;
			try
			{
				elementByID = (Batch)base.GetElementByID(ID);
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

		public int Load(Period oPeriod)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Batch batch = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idPeriod" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oPeriod.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "BatchDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						batch = new Batch();
						batch.SetState(row);
						base.Add(batch);
					}
					batch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				batch = null;
				num = 1;
			}
			return num;
		}

		public int Load(Period oPeriod, DateTime vDateBegin, DateTime vDateEnd)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Batch batch = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idPeriod", "BatchDate", "BatchDate" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oPeriod.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "BatchDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						batch = new Batch();
						batch.SetState(row);
						base.Add(batch);
					}
					batch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				batch = null;
				num = 1;
			}
			return num;
		}

		public int Load(DateTime vDateBegin, DateTime vDateEnd)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Batch batch = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "BatchDate", "BatchDate" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "BatchDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						batch = new Batch();
						batch.SetState(row);
						base.Add(batch);
					}
					batch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				batch = null;
				num = 1;
			}
			return num;
		}

		public int Load(DateTime vDateBegin, DateTime vDateEnd, int idagent, int idtypepay, int idstatusbatch, int idtypebatch, double summa)
		{
			int num;
			string[] strArrays;
			int num1 = 0;
			DataTable dataTable = null;
			Batch batch = null;
			try
			{
				string[] strArrays1 = new string[] { string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)), null, null, null, null, null };
				if (idagent != 0)
				{
					strArrays1[2] = string.Concat("=", idagent.ToString());
				}
				else
				{
					strArrays1[2] = "!=0";
				}
				if (idtypepay != 0)
				{
					strArrays1[3] = string.Concat("=", idtypepay.ToString());
				}
				else
				{
					strArrays1[3] = "!=0";
				}
				if (idstatusbatch != 0)
				{
					strArrays1[4] = string.Concat("=", idstatusbatch.ToString());
				}
				else
				{
					strArrays1[4] = "!=0";
				}
				if (idtypebatch != 0)
				{
					strArrays1[5] = string.Concat("=", idtypebatch.ToString());
				}
				else
				{
					strArrays1[5] = "!=0";
				}
				if (summa == 0)
				{
					string nameTable = this.NameTable;
					strArrays = new string[] { "BatchDate", "BatchDate", "isnull(iddispatcher,1)", "idtypepay", "idstatusbatch", "idtypebatch" };
					string[] strArrays2 = strArrays;
					strArrays = new string[] { strArrays1[0], strArrays1[1], strArrays1[2], strArrays1[3], strArrays1[4], strArrays1[5] };
					num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays2, strArrays, "BatchDate");
				}
				else
				{
					strArrays1[6] = string.Concat("=", summa.ToString());
					string str = this.NameTable;
					strArrays = new string[] { "BatchDate", "BatchDate", "isnull(iddispatcher,1)", "idtypepay", "idstatusbatch", "idtypebatch", "batchamount" };
					num1 = Loader.LoadCollection(str, ref dataTable, strArrays, strArrays1, "BatchDate");
				}
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						batch = new Batch();
						batch.SetState(row);
						base.Add(batch);
					}
					batch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				batch = null;
				num = 1;
			}
			return num;
		}

		public int Load(DateTime vDate, Agent oCashier, TypeBatch oTypeBatch)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Batch batch = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "BatchDate", "idcashier", "idtypebatch" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", Tools.ConvertDateFORSQL(vDate)), null, null };
				long d = oCashier.get_ID();
				strArrays[1] = string.Concat("=", d.ToString());
				d = oTypeBatch.get_ID();
				strArrays[2] = string.Concat("=", d.ToString());
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "BatchDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						batch = new Batch();
						batch.SetState(row);
						base.Add(batch);
					}
					batch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				batch = null;
				num = 1;
			}
			return num;
		}

		public int Load(DateTime vDate, Agent oCashier, TypeBatch[] oTypeBatch)
		{
			int num;
			long d;
			int num1 = 0;
			DataTable dataTable = null;
			Batch batch = null;
			try
			{
				string str = " in (";
				TypeBatch[] typeBatchArray = oTypeBatch;
				for (int i = 0; i < (int)typeBatchArray.Length; i++)
				{
					d = typeBatchArray[i].get_ID();
					str = string.Concat(str, d.ToString(), ",");
				}
				str = string.Concat(str.Substring(0, str.Length - 1), ")");
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "BatchDate", "idcashier", "idtypebatch" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", Tools.ConvertDateFORSQL(vDate)), null, null };
				d = oCashier.get_ID();
				strArrays[1] = string.Concat("=", d.ToString());
				strArrays[2] = str;
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "BatchDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						batch = new Batch();
						batch.SetState(row);
						base.Add(batch);
					}
					batch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				batch = null;
				num = 1;
			}
			return num;
		}

		public int Load(StatusBatch oStatusBatch, TypePay oTypePay, TypeBatch oTypeBatch, Agent oCashier)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Batch batch = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idstatusbatch", "idtypepay", "idtypebatch", "idcashier" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oStatusBatch.get_ID()), string.Concat("=", oTypePay.get_ID()), string.Concat("=", oTypeBatch.get_ID()), string.Concat("=", oCashier.get_ID(), " and IDDispatcher is null") };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "BatchDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						batch = new Batch();
						batch.SetState(row);
						base.Add(batch);
					}
					batch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				batch = null;
				num = 1;
			}
			return num;
		}

		public int Load(StatusBatch oStatusBatch, TypePay oTypePay, TypeBatch oTypeBatch, Agent oCashier, int idDispatcher)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Batch batch = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idstatusbatch", "idtypepay", "idtypebatch", "idcashier", "idDispatcher" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oStatusBatch.get_ID()), string.Concat("=", oTypePay.get_ID()), string.Concat("=", oTypeBatch.get_ID()), string.Concat("=", oCashier.get_ID()), string.Concat("=", idDispatcher) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "BatchDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						batch = new Batch();
						batch.SetState(row);
						base.Add(batch);
					}
					batch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				batch = null;
				num = 1;
			}
			return num;
		}

		public int Load(DateTime vDate, StatusBatch oStatusBatch, TypePay oTypePay, TypeBatch oTypeBatch, Agent oCashier)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Batch batch = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "batchdate", "idstatusbatch", "idtypepay", "idtypebatch", "idcashier" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", Tools.ConvertDateFORSQL(vDate)), string.Concat("=", oStatusBatch.get_ID()), string.Concat("=", oTypePay.get_ID()), string.Concat("=", oTypeBatch.get_ID()), string.Concat("=", oCashier.get_ID(), " and IDDispatcher is null") };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "BatchDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						batch = new Batch();
						batch.SetState(row);
						base.Add(batch);
					}
					batch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				batch = null;
				num = 1;
			}
			return num;
		}

		public int Load(DateTime vDate, Agent oCashier)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Batch batch = null;
			try
			{
				string[] str = new string[] { "select * from batch with (nolock) where dbo.DateOnly(batchdate) =", Tools.ConvertDateFORSQL(vDate), " and IDCashier=", null, null };
				str[3] = oCashier.get_ID().ToString();
				str[4] = "  and idtypebatch<>3";
				string str1 = string.Concat(str);
				num1 = Loader.LoadCollection(str1, ref dataTable, "");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						batch = new Batch();
						batch.SetState(row);
						base.Add(batch);
					}
					batch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				batch = null;
				num = 1;
			}
			return num;
		}

		public int LoadByAgent(DateTime vDate, Agent oCashier, TypeBatch oTypeBatch, TypePay oTypePay)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Batch batch = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "batchdate", "idtypepay", "idtypebatch", "idcashier" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", Tools.ConvertDateFORSQL(vDate)), string.Concat("=", oTypePay.get_ID()), string.Concat("=", oTypeBatch.get_ID()), string.Concat("=", oCashier.get_ID(), " and IDDispatcher is not null") };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "BatchDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						batch = new Batch();
						batch.SetState(row);
						base.Add(batch);
					}
					batch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				batch = null;
				num = 1;
			}
			return num;
		}
	}
}