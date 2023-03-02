using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeCharges : SimpleClasss
	{
		public TypeCharge this[int index]
		{
			get
			{
				return (TypeCharge)base.get_Item(index);
			}
		}

		public TypeCharges()
		{
			this.NameTable = "TypeCharge";
		}

		public TypeCharge Add()
		{
			TypeCharge typeCharge = new TypeCharge();
			typeCharge.Init();
			typeCharge.set_Parent(this);
			return typeCharge;
		}

		public TypeCharge item(long ID)
		{
			TypeCharge elementByID;
			try
			{
				elementByID = (TypeCharge)base.GetElementByID(ID);
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
			TypeCharge typeCharge = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeCharge");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeCharge = new TypeCharge();
						typeCharge.SetState(row);
						base.Add(typeCharge);
					}
					typeCharge = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeCharge = null;
				num = 1;
			}
			return num;
		}
	}
}