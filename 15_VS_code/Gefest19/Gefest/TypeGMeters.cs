using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeGMeters : SimpleClasss
	{
		public TypeGMeter this[int index]
		{
			get
			{
				return (TypeGMeter)base.get_Item(index);
			}
		}

		public TypeGMeters()
		{
			this.NameTable = "TypeGMeter";
		}

		public TypeGMeter Add()
		{
			TypeGMeter typeGMeter = new TypeGMeter();
			typeGMeter.Init();
			typeGMeter.set_Parent(this);
			return typeGMeter;
		}

		public TypeGMeter item(long ID)
		{
			TypeGMeter elementByID;
			try
			{
				elementByID = (TypeGMeter)base.GetElementByID(ID);
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
			TypeGMeter typeGMeter = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeGMeter desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeGMeter = new TypeGMeter();
						typeGMeter.SetState(row);
						base.Add(typeGMeter);
					}
					typeGMeter = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeGMeter = null;
				num = 1;
			}
			return num;
		}
	}
}