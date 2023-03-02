using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Classifiers : SimpleClasss
	{
		private int _level;

		public Classifier this[int index]
		{
			get
			{
				return (Classifier)base.get_Item(index);
			}
		}

		public Classifiers()
		{
			this.NameTable = "Classifier";
		}

		public Classifier Add()
		{
			Classifier classifier = new Classifier();
			classifier.Init();
			classifier.Level = this._level;
			classifier.set_Parent(this);
			return classifier;
		}

		public Classifier item(long ID)
		{
			Classifier elementByID;
			try
			{
				elementByID = (Classifier)base.GetElementByID(ID);
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

		public int Load(int Level)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			this._level = Level;
			Classifier classifier = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "Level" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", Level.ToString()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "Name");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						classifier = new Classifier();
						classifier.SetState(row);
						base.Add(classifier);
					}
					classifier = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				classifier = null;
				num = 1;
			}
			return num;
		}
	}
}