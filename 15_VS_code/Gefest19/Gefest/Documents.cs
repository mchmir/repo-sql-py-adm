using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Documents : SimpleClasss
	{
		private Batch _batch;

		private Contract _contract;

		public Document this[int index]
		{
			get
			{
				return (Document)base.get_Item(index);
			}
		}

		public Documents()
		{
			this.NameTable = "Document";
		}

		public Document Add()
		{
			Document document = new Document()
			{
				oBatch = this._batch,
				oContract = this._contract
			};
			document.set_Parent(this);
			return document;
		}

		public Document item(long ID)
		{
			Document elementByID;
			try
			{
				elementByID = (Document)base.GetElementByID(ID);
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

		public int Load(TypeDocument oTypeDoc)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Document document = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idTypeDocument" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oTypeDoc.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DocumentDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}

		public int Load(Contract oContract)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			this._contract = oContract;
			Document document = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idContract" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oContract.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DocumentDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						document.oContract = this._contract;
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}

		public int Load(Contract oContract, DateTime vDateBegin, DateTime vDateEnd)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			this._contract = oContract;
			Document document = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idContract", "DocumentDate", "DocumentDate" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oContract.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DocumentDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						document.oContract = this._contract;
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}

		public int Load(Contract oContract, TypeDocument oTypeDoc)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			this._contract = oContract;
			Document document = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idContract", "idTypeDocument" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oContract.get_ID()), string.Concat("=", oTypeDoc.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DocumentDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						document.oContract = this._contract;
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}

		public int Load(Period oPeriod)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Document document = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idPeriod" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oPeriod.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DocumentDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}

		public int Load(Batch oBatch)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			this._batch = oBatch;
			Document document = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idBatch" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oBatch.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IdDocument");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						document.oBatch = this._batch;
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}

		public int Load(TypeDocument oTypeDoc, DateTime vDateBegin, DateTime vDateEnd)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Document document = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idTypeDocument", "DocumentDate", "DocumentDate" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oTypeDoc.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DocumentDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}

		public int Load(TypeDocument oTypeDoc, DateTime vDateBegin, DateTime vDateEnd, int idstatus)
		{
			int num;
			string[] strArrays;
			int num1 = 0;
			DataTable dataTable = null;
			Document document = null;
			try
			{
				if (idstatus != 0)
				{
					if (oTypeDoc.get_ID() == (long)13)
					{
						string nameTable = this.NameTable;
						strArrays = new string[] { "idTypeDocument", "DocumentDate", "DocumentDate", "dbo.fGetStatusLegalDoc(iddocument)" };
						string[] strArrays1 = strArrays;
						strArrays = new string[] { string.Concat("=", oTypeDoc.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)), string.Concat("=", idstatus.ToString()) };
						num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DocumentDate");
					}
					if (oTypeDoc.get_ID() == (long)10)
					{
						string str = this.NameTable;
						strArrays = new string[] { "idTypeDocument", "DocumentDate", "DocumentDate", "dbo.fGetStatusClaimDoc(iddocument)" };
						string[] strArrays2 = strArrays;
						strArrays = new string[] { string.Concat("=", oTypeDoc.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)), string.Concat("=", idstatus.ToString()) };
						num1 = Loader.LoadCollection(str, ref dataTable, strArrays2, strArrays, "DocumentDate");
					}
					if (oTypeDoc.get_ID() == (long)22)
					{
						string nameTable1 = this.NameTable;
						strArrays = new string[] { "idTypeDocument", "DocumentDate", "DocumentDate", "dbo.fGetValuePD(iddocument,30)" };
						string[] strArrays3 = strArrays;
						strArrays = new string[] { string.Concat("=", oTypeDoc.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)), string.Concat("=", idstatus.ToString()) };
						num1 = Loader.LoadCollection(nameTable1, ref dataTable, strArrays3, strArrays, "DocumentDate");
					}
				}
				else
				{
					string str1 = this.NameTable;
					strArrays = new string[] { "idTypeDocument", "DocumentDate", "DocumentDate" };
					string[] strArrays4 = strArrays;
					strArrays = new string[] { string.Concat("=", oTypeDoc.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)) };
					num1 = Loader.LoadCollection(str1, ref dataTable, strArrays4, strArrays, "DocumentDate");
				}
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}

		public int Load(TypeDocument oTypeDoc, DateTime vDateBegin, DateTime vDateEnd, int idstatus, int idagent)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Document document = null;
			try
			{
				string[] strArrays = new string[2];
				if (idstatus != 0)
				{
					strArrays[0] = string.Concat("=", idstatus.ToString());
				}
				else
				{
					strArrays[0] = "!=0";
				}
				if (idagent != 0)
				{
					strArrays[1] = string.Concat("=", idagent.ToString());
				}
				else
				{
					strArrays[1] = "!=0";
				}
				if (oTypeDoc.get_ID() == (long)22)
				{
					string nameTable = this.NameTable;
					string[] strArrays1 = new string[] { "idTypeDocument", "DocumentDate", "DocumentDate", "dbo.fGetValuePD(iddocument,30)", "dbo.fGetValuePD(iddocument,16)" };
					string[] strArrays2 = strArrays1;
					strArrays1 = new string[] { string.Concat("=", oTypeDoc.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)), strArrays[0], strArrays[1] };
					num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays2, strArrays1, "DocumentDate");
				}
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}

		public int Load(TypeDocument oTypeDoc, DateTime vDate, long idgmeter)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Document document = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idTypeDocument", "DocumentDate", "dbo.fGetValuePD(iddocument,7)" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oTypeDoc.get_ID()), string.Concat("=", Tools.ConvertDateFORSQL(vDate)), string.Concat("=", idgmeter.ToString()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DocumentDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}

		public int Load(TypeDocument oTypeDoc, long idgmeter)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Document document = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idTypeDocument", "dbo.fGetValuePD(iddocument,7)" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oTypeDoc.get_ID()), string.Concat("=", idgmeter.ToString()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DocumentDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}

		public int Load(TypeDocument oTypeDoc, DateTime vDateBegin, DateTime vDateEnd, double summa, int choice)
		{
			int num;
			string[] strArrays;
			int num1 = 0;
			DataTable dataTable = null;
			Document document = null;
			try
			{
				switch (choice)
				{
					case 0:
					{
						string nameTable = this.NameTable;
						strArrays = new string[] { "idTypeDocument", "DocumentDate", "DocumentDate", "documentamount", "dbo.fGetTypePayment(iddocument)" };
						string[] strArrays1 = strArrays;
						strArrays = new string[] { string.Concat("=", oTypeDoc.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)), string.Concat("=", summa.ToString()), "=0" };
						num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DocumentDate");
						break;
					}
					case 1:
					{
						string str = this.NameTable;
						strArrays = new string[] { "idTypeDocument", "DocumentDate", "DocumentDate", "documentamount", "dbo.fGetTypePayment(iddocument)" };
						string[] strArrays2 = strArrays;
						strArrays = new string[] { string.Concat("=", oTypeDoc.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)), string.Concat("=", summa.ToString()), "=1" };
						num1 = Loader.LoadCollection(str, ref dataTable, strArrays2, strArrays, "DocumentDate");
						break;
					}
					case 2:
					{
						string nameTable1 = this.NameTable;
						strArrays = new string[] { "idTypeDocument", "DocumentDate", "DocumentDate", "documentamount" };
						string[] strArrays3 = strArrays;
						strArrays = new string[] { string.Concat("=", oTypeDoc.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)), string.Concat("=", summa.ToString()) };
						num1 = Loader.LoadCollection(nameTable1, ref dataTable, strArrays3, strArrays, "DocumentDate");
						break;
					}
					default:
					{
						string str1 = this.NameTable;
						strArrays = new string[] { "idTypeDocument", "DocumentDate", "DocumentDate", "documentamount" };
						string[] strArrays4 = strArrays;
						strArrays = new string[] { string.Concat("=", oTypeDoc.get_ID()), string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)), string.Concat("=", summa.ToString()) };
						num1 = Loader.LoadCollection(str1, ref dataTable, strArrays4, strArrays, "DocumentDate");
						break;
					}
				}
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}

		public int Load(DateTime vDateBegin, DateTime vDateEnd)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Document document = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "DocumentDate", "DocumentDate" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DocumentDate");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						document = new Document();
						document.SetState(row);
						base.Add(document);
					}
					document = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				document = null;
				num = 1;
			}
			return num;
		}
	}
}