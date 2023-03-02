using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class PersonalCabinetRequests : SimpleClasss
	{
		public PersonalCabinetRequest this[int index]
		{
			get
			{
				return (PersonalCabinetRequest)base.get_Item(index);
			}
		}

		public PersonalCabinetRequests()
		{
			this.NameTable = "PersonalCabinetRequest";
		}

		public PersonalCabinetRequest Add()
		{
			PersonalCabinetRequest personalCabinetRequest = new PersonalCabinetRequest();
			personalCabinetRequest.set_Parent(this);
			return personalCabinetRequest;
		}

		public PersonalCabinetRequest item(long ID)
		{
			PersonalCabinetRequest elementByID;
			try
			{
				elementByID = (PersonalCabinetRequest)base.GetElementByID(ID);
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

		public int Load(DateTime vDateBegin, DateTime vDateEnd)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			PersonalCabinetRequest personalCabinetRequest = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "DateRequest", "DateRequest" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat(">=", Tools.ConvertDateFORSQL(vDateBegin)), string.Concat("<=", Tools.ConvertDateFORSQL(vDateEnd)) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "DateRequest");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						personalCabinetRequest = new PersonalCabinetRequest();
						personalCabinetRequest.SetState(row);
						base.Add(personalCabinetRequest);
					}
					personalCabinetRequest = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				personalCabinetRequest = null;
				num = 1;
			}
			return num;
		}
	}
}