using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class PrintLogs : SimpleClasss
	{
		public PrintLog this[int index]
		{
			get
			{
				return (PrintLog)base.get_Item(index);
			}
		}

		public PrintLogs()
		{
			this.NameTable = "PrintLog";
		}

		public PrintLog Add()
		{
			PrintLog printLog = new PrintLog();
			printLog.Init();
			printLog.set_Parent(this);
			return printLog;
		}

		public PrintLog item(long ID)
		{
			PrintLog elementByID;
			try
			{
				elementByID = (PrintLog)base.GetElementByID(ID);
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
			PrintLog printLog = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDPrintLog");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						printLog = new PrintLog();
						printLog.SetState(row);
						base.Add(printLog);
					}
					printLog = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				printLog = null;
				num = 1;
			}
			return num;
		}

		public int Load(DateTime vDate)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			PrintLog printLog = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "DatePrintLog" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=DatePrintLog(", Tools.ConvertDateFORSQL(vDate), ")") };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						printLog = new PrintLog();
						printLog.SetState(row);
						base.Add(printLog);
					}
					printLog = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				printLog = null;
				num = 1;
			}
			return num;
		}
	}
}