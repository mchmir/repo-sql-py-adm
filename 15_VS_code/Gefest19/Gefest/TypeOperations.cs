using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeOperations : SimpleClasss
	{
		public TypeOperation this[int index]
		{
			get
			{
				return (TypeOperation)base.get_Item(index);
			}
		}

		public TypeOperations()
		{
			this.NameTable = "TypeOperation";
		}

		public TypeOperation Add()
		{
			TypeOperation typeOperation = new TypeOperation();
			typeOperation.Init();
			typeOperation.set_Parent(this);
			return typeOperation;
		}

		public TypeOperation item(long ID)
		{
			TypeOperation elementByID;
			try
			{
				elementByID = (TypeOperation)base.GetElementByID(ID);
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
			TypeOperation typeOperation = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeOperation");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeOperation = new TypeOperation();
						typeOperation.SetState(row);
						base.Add(typeOperation);
					}
					typeOperation = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeOperation = null;
				num = 1;
			}
			return num;
		}
	}
}