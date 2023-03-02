using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class GroupPersons : SimpleClasss
	{
		public GroupPerson this[int index]
		{
			get
			{
				return (GroupPerson)base.get_Item(index);
			}
		}

		public GroupPersons()
		{
			this.NameTable = "GroupPerson";
		}

		public GroupPerson Add()
		{
			GroupPerson groupPerson = new GroupPerson();
			groupPerson.Init();
			groupPerson.set_Parent(this);
			return groupPerson;
		}

		public GroupPerson item(long ID)
		{
			GroupPerson elementByID;
			try
			{
				elementByID = (GroupPerson)base.GetElementByID(ID);
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
			GroupPerson groupPerson = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDGroupPerson");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						groupPerson = new GroupPerson();
						groupPerson.SetState(row);
						base.Add(groupPerson);
					}
					groupPerson = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				groupPerson = null;
				num = 1;
			}
			return num;
		}
	}
}