using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using WebSecurityDLL;

namespace Gefest
{
	public class Contract : SimpleClass
	{
		private TypeContract _TypeContract;

		private TypeInfringements _TypeInfringements;

		private TypeTariff _TypeTariff;

		private Person _Person;

		private PersonalCabinet _PersonalCabinet;

		private Documents _documents;

		private PCs _PCs;

		private Gobjects _Gobjects;

		private Balances _Balances;

		private BalanceReals _BalanceReals;

		public string Account
		{
			get
			{
				return (string)base.GetValue("Account");
			}
			set
			{
				base.SetValue("Account", value);
			}
		}

		public int ChargePeny
		{
			get
			{
				return (int)base.GetValue("ChargePeny");
			}
			set
			{
				base.SetValue("ChargePeny", value);
			}
		}

		public DateTime DateBegin
		{
			get
			{
				return (DateTime)base.GetValue("DateBegin");
			}
			set
			{
				base.SetValue("DateBegin", value);
			}
		}

		public DateTime DateEnd
		{
			get
			{
				return (DateTime)base.GetValue("DateEnd");
			}
			set
			{
				base.SetValue("DateEnd", value);
			}
		}

		public string Memo
		{
			get
			{
				return (string)base.GetValue("Memo");
			}
			set
			{
				base.SetValue("Memo", value);
			}
		}

		public override string Name
		{
			get
			{
				return this.Account;
			}
			set
			{
				this.Account = value;
			}
		}

		public Gobject oActiveGobject
		{
			get
			{
				Gobject gobject = null;
				foreach (Gobject oGobject in this.oGobjects)
				{
					if (oGobject.oStatusGObject.get_ID() != (long)1)
					{
						continue;
					}
					gobject = oGobject;
					break;
				}
				return gobject;
			}
		}

		public BalanceReals oBalanceReals
		{
			get
			{
				if (this._BalanceReals == null)
				{
					this._BalanceReals = new BalanceReals();
					if (this._BalanceReals.Load(this, Depot.CurrentPeriod) != 0)
					{
						this._BalanceReals = null;
					}
				}
				return this._BalanceReals;
			}
		}

		public Balances oBalances
		{
			get
			{
				if (this._Balances == null)
				{
					this._Balances = new Balances();
					if (this._Balances.Load(this, Depot.CurrentPeriod) != 0)
					{
						this._Balances = null;
					}
				}
				return this._Balances;
			}
		}

		public Documents oDocuments
		{
			get
			{
				if (this._documents == null)
				{
					this._documents = new Documents();
					if (this._documents.Load(this) != 0)
					{
						this._documents = null;
					}
				}
				return this._documents;
			}
		}

		public Gobjects oGobjects
		{
			get
			{
				if (this._Gobjects == null)
				{
					this._Gobjects = new Gobjects();
					if (this._Gobjects.Load(this) != 0)
					{
						this._Gobjects = null;
					}
				}
				return this._Gobjects;
			}
		}

		public PCs oPCs
		{
			get
			{
				if (this._PCs == null)
				{
					this._PCs = new PCs();
					if (this._PCs.Load(this) != 0)
					{
						this._PCs = null;
					}
				}
				return this._PCs;
			}
		}

		public Person oPerson
		{
			get
			{
				if (this._Person == null)
				{
					int value = (int)base.GetValue("IDPerson");
					this._Person = new Person();
					if (this._Person.Load((long)value) != 0)
					{
						this._Person = null;
					}
				}
				return this._Person;
			}
			set
			{
				this._Person = value;
				if (this._Person != null)
				{
					base.SetValue("IDPerson", this._Person.get_ID());
				}
			}
		}

		public PersonalCabinet oPersonalCabinet
		{
			get
			{
				if (this._PersonalCabinet == null)
				{
					this._PersonalCabinet = new PersonalCabinet();
					if (base.GetValue("IDPersonalCabinet") != null)
					{
						int value = (int)base.GetValue("IDPersonalCabinet");
						this._PersonalCabinet.Load((long)value);
					}
				}
				return this._PersonalCabinet;
			}
			set
			{
				this._PersonalCabinet = value;
				if (this._PersonalCabinet != null)
				{
					base.SetValue("IDPersonalCabinet", this._PersonalCabinet.get_ID());
				}
			}
		}

		public TypeContract oTypeContract
		{
			get
			{
				if (this._TypeContract == null)
				{
					int value = (int)base.GetValue("IDTypeContract");
					this._TypeContract = new TypeContract();
					if (this._TypeContract.Load((long)value) != 0)
					{
						this._TypeContract = null;
					}
				}
				return this._TypeContract;
			}
			set
			{
				this._TypeContract = value;
				if (this._TypeContract != null)
				{
					base.SetValue("IDTypeContract", this._TypeContract.get_ID());
				}
			}
		}

		public TypeInfringements oTypeInfringements
		{
			get
			{
				if (this._TypeInfringements == null)
				{
					int value = (int)base.GetValue("IDTypeInfringements");
					this._TypeInfringements = new TypeInfringements();
					if (this._TypeInfringements.Load((long)value) != 0)
					{
						this._TypeInfringements = null;
					}
				}
				return this._TypeInfringements;
			}
			set
			{
				this._TypeInfringements = value;
				if (this._TypeInfringements != null)
				{
					base.SetValue("IDTypeInfringements", this._TypeInfringements.get_ID());
				}
			}
		}

		public TypeTariff oTypeTariff
		{
			get
			{
				if (this._TypeTariff == null)
				{
					int value = (int)base.GetValue("IDTypeTariff");
					this._TypeTariff = new TypeTariff();
					if (this._TypeTariff.Load((long)value) != 0)
					{
						this._TypeTariff = null;
					}
				}
				return this._TypeTariff;
			}
			set
			{
				this._TypeTariff = value;
				if (this._TypeTariff != null)
				{
					base.SetValue("IDTypeTariff", this._TypeTariff.get_ID());
				}
			}
		}

		public string PCEmail
		{
			get
			{
				return (string)base.GetValue("PCEmail");
			}
			set
			{
				base.SetValue("PCEmail", value);
			}
		}

		public string PCPass
		{
			get
			{
				return (string)base.GetValue("PCPass");
			}
			set
			{
				base.SetValue("PCPass", value);
			}
		}

		public string PCPassword
		{
			get
			{
				return (string)base.GetValue("PCPassword");
			}
			set
			{
				base.SetValue("PCPassword", value);
			}
		}

		public int PrintChetIzvehen
		{
			get
			{
				return (int)base.GetValue("PrintChetIzvehen");
			}
			set
			{
				base.SetValue("PrintChetIzvehen", value);
			}
		}

		public string StastusName
		{
			get
			{
				switch (this.Status)
				{
					case 0:
					{
						return "Не определен";
					}
					case 1:
					{
						return "Активен";
					}
					case 2:
					{
						return "Закрыт";
					}
				}
				return "";
			}
		}

		public int Status
		{
			get
			{
				return (int)base.GetValue("Status");
			}
			set
			{
				base.SetValue("Status", value);
			}
		}

		public Contract()
		{
			this.NameTable = "Contract";
			this.Init();
			this.DateBegin = DateTime.Today;
			this.DateEnd = new DateTime(3000, 1, 1);
		}

		public double CurrentBalance()
		{
			double amountBalance = 0;
			foreach (Balance oBalance in this.oBalances)
			{
				amountBalance += oBalance.AmountBalance;
			}
			return amountBalance;
		}

		public Balance CurrentBalance(Accounting oAccounting)
		{
			Balance balance;
			IEnumerator enumerator = this.oBalances.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Balance current = (Balance)enumerator.Current;
					if (current.oAccounting.get_ID() != oAccounting.get_ID())
					{
						continue;
					}
					balance = current;
					return balance;
				}
				return null;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return balance;
		}

		public double CurrentBalanceReal()
		{
			double amountBalance = 0;
			foreach (BalanceReal oBalanceReal in this.oBalanceReals)
			{
				amountBalance += oBalanceReal.AmountBalance;
			}
			return amountBalance;
		}

		public BalanceReal CurrentBalanceReal(Accounting oAccounting)
		{
			BalanceReal balanceReal;
			IEnumerator enumerator = this.oBalanceReals.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					BalanceReal current = (BalanceReal)enumerator.Current;
					if (current.oAccounting.get_ID() != oAccounting.get_ID())
					{
						continue;
					}
					balanceReal = current;
					return balanceReal;
				}
				return null;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return balanceReal;
		}

		protected override void Finalize()
		{
			base.Finalize();
		}

		public Documents oDocumentss(DateTime vDateBegin, DateTime vDateEnd)
		{
			this._documents = new Documents();
			if (this._documents.Load(this, vDateBegin, vDateEnd) != 0)
			{
				this._documents = null;
			}
			return this._documents;
		}

		public void RecalcBalances(long IDContract, long IDPeriod)
		{
			try
			{
				SqlParameter sqlParameter = new SqlParameter("@IDPeriod", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDPeriod
				};
				SqlParameter sqlParameter1 = new SqlParameter("@IDContract", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDContract
				};
				SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter1, sqlParameter };
				Saver.ExecuteProcedure("spRecalcBalances", sqlParameterArray);
			}
			catch
			{
			}
		}

		public enum eStatus
		{
			NotDefined,
			Active,
			Closed
		}
	}
}