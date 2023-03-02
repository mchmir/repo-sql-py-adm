using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypePays : SimpleClasss
	{
		public TypePay this[int index]
		{
			get
			{
				return (TypePay)base.get_Item(index);
			}
		}

		public TypePays()
		{
			this.NameTable = "TypePay";
		}

		public TypePay Add()
		{
			TypePay typePay = new TypePay();
			typePay.Init();
			typePay.set_Parent(this);
			return typePay;
		}

		public TypePay item(long ID)
		{
			TypePay elementByID;
			try
			{
				elementByID = (TypePay)base.GetElementByID(ID);
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
			TypePay typePay = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypePay");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typePay = new TypePay();
						typePay.SetState(row);
						base.Add(typePay);
					}
					typePay = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typePay = null;
				num = 1;
			}
			return num;
		}
	}
}