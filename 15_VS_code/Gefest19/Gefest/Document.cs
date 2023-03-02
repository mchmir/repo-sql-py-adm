using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using WebSecurityDLL;

namespace Gefest
{
	public class Document : SimpleClass
	{
		private Contract _Contract;

		private Period _Period;

		private Batch _Batch;

		private TypeDocument _TypeDocument;

		private SYSUser _UserAdd;

		private SYSUser _UserModify;

		private PDs _PDs;

		private Operations _Operations;

		private Operation _Operation;

		private FactUses _FactUses;

		public DateTime DateAdd
		{
			get
			{
				return (DateTime)base.GetValue("DateAdd");
			}
			set
			{
				base.SetValue("DateAdd", value);
			}
		}

		public DateTime DateModify
		{
			get
			{
				return (DateTime)base.GetValue("DateModify");
			}
			set
			{
				base.SetValue("DateModify", value);
			}
		}

		public double DocumentAmount
		{
			get
			{
				return (double)base.GetValue("DocumentAmount");
			}
			set
			{
				base.SetValue("DocumentAmount", value);
			}
		}

		public DateTime DocumentDate
		{
			get
			{
				return (DateTime)base.GetValue("DocumentDate");
			}
			set
			{
				base.SetValue("DocumentDate", value);
			}
		}

		public string DocumentNumber
		{
			get
			{
				return (string)base.GetValue("DocumentNumber");
			}
			set
			{
				base.SetValue("DocumentNumber", value);
			}
		}

		public int IDModify
		{
			get
			{
				return (int)base.GetValue("IDModify");
			}
			set
			{
				base.SetValue("IDModify", value);
			}
		}

		public int IDUser
		{
			get
			{
				return (int)base.GetValue("IDUser");
			}
			set
			{
				base.SetValue("IDUser", value);
			}
		}

		public string Note
		{
			get
			{
				return (string)base.GetValue("Note");
			}
			set
			{
				base.SetValue("Note", value);
			}
		}

		public Batch oBatch
		{
			get
			{
				if (this._Batch == null)
				{
					int value = (int)base.GetValue("IDBatch");
					this._Batch = new Batch();
					if (this._Batch.Load((long)value) != 0)
					{
						this._Batch = null;
					}
				}
				return this._Batch;
			}
			set
			{
				this._Batch = value;
				if (this._Batch != null)
				{
					base.SetValue("IDBatch", this._Batch.get_ID());
				}
			}
		}

		public Contract oContract
		{
			get
			{
				if (this._Contract == null)
				{
					int value = (int)base.GetValue("IDContract");
					this._Contract = new Contract();
					if (this._Contract.Load((long)value) != 0)
					{
						this._Contract = null;
					}
				}
				return this._Contract;
			}
			set
			{
				this._Contract = value;
				if (this._Contract != null)
				{
					base.SetValue("IDContract", this._Contract.get_ID());
				}
			}
		}

		public FactUses oFactUses
		{
			get
			{
				if (this._FactUses == null)
				{
					this._FactUses = new FactUses();
					if (this._FactUses.Load(this) != 0)
					{
						this._FactUses = null;
					}
				}
				return this._FactUses;
			}
		}

		public Operation oOperation
		{
			get
			{
				if (this._Operation == null)
				{
					int value = (int)base.GetValue("IDOperation");
					this._Operation = new Operation();
					if (this._Operation.Load((long)value) != 0)
					{
						this._Operation = null;
					}
				}
				return this._Operation;
			}
			set
			{
				this._Operation = value;
				if (this._Operation != null)
				{
					base.SetValue("IDOperation", this._Operation.get_ID());
				}
			}
		}

		public Operations oOperations
		{
			get
			{
				if (this._Operations == null)
				{
					this._Operations = new Operations();
					if (this._Operations.Load(this) != 0)
					{
						this._Operations = null;
					}
				}
				return this._Operations;
			}
		}

		public PDs oPDs
		{
			get
			{
				if (this._PDs == null)
				{
					this._PDs = new PDs();
					if (this._PDs.Load(this) != 0)
					{
						this._PDs = null;
					}
				}
				return this._PDs;
			}
			set
			{
				if (value == null)
				{
					this._PDs = value;
				}
			}
		}

		public Period oPeriod
		{
			get
			{
				if (this._Period == null)
				{
					int value = (int)base.GetValue("IDPeriod");
					this._Period = new Period();
					if (this._Period.Load((long)value) != 0)
					{
						this._Period = null;
					}
				}
				return this._Period;
			}
			set
			{
				this._Period = value;
				if (this._Period != null)
				{
					base.SetValue("IDPeriod", this._Period.get_ID());
				}
			}
		}

		public TypeDocument oTypeDocument
		{
			get
			{
				if (this._TypeDocument == null)
				{
					int value = (int)base.GetValue("IDTypeDocument");
					this._TypeDocument = new TypeDocument();
					if (this._TypeDocument.Load((long)value) != 0)
					{
						this._TypeDocument = null;
					}
				}
				return this._TypeDocument;
			}
			set
			{
				this._TypeDocument = value;
				if (this._TypeDocument != null)
				{
					base.SetValue("IDTypeDocument", this._TypeDocument.get_ID());
				}
			}
		}

		public SYSUser oUserAdd
		{
			get
			{
				if (this._UserAdd == null)
				{
					int value = (int)base.GetValue("IDUser");
					this._UserAdd = new SYSUser();
					if (this._UserAdd.Load((long)value) != 0)
					{
						this._UserAdd = null;
					}
				}
				return this._UserAdd;
			}
			set
			{
				this._UserAdd = value;
				if (this._UserAdd != null)
				{
					base.SetValue("IDUser", this._UserAdd.get_ID());
				}
			}
		}

		public SYSUser oUserModify
		{
			get
			{
				if (this._UserModify == null)
				{
					int value = (int)base.GetValue("IDModify");
					this._UserModify = new SYSUser();
					if (this._UserModify.Load((long)value) != 0)
					{
						this._UserModify = null;
					}
				}
				return this._UserModify;
			}
			set
			{
				this._UserModify = value;
				if (this._UserModify != null)
				{
					base.SetValue("IDModify", this._UserModify.get_ID());
				}
			}
		}

		public Document()
		{
			this.NameTable = "Document";
			base.Init();
		}

		public override int Delete()
		{
			int num;
			int num1 = 0;
			try
			{
				if (this.oPeriod.get_ID() != Depot.CurrentPeriod.get_ID())
				{
					num = 1;
				}
				else if (this.oTypeDocument.get_ID() != (long)22)
				{
					Document document = new Document();
					bool flag = false;
					int count = this.oPDs.get_Count();
					for (int i = 0; i < count; i++)
					{
						PD item = this.oPDs[0];
						if (item.oTypePD.get_ID() == (long)1)
						{
							Indication indication = new Indication();
							indication.Load(Convert.ToInt64(this.GetPD(1).Value));
							num1 = indication.Delete();
						}
						if (item.oTypePD.get_ID() == (long)25)
						{
							document = new Document();
							if (document.Load(Convert.ToInt64(this.GetPD(25).Value)) == 0)
							{
								flag = true;
							}
						}
						if (num1 == 0)
						{
							num1 = this.oPDs.Remove(item.get_ID());
						}
					}
					count = this.oFactUses.get_Count();
					for (int j = 0; j < count; j++)
					{
						FactUse factUse = this.oFactUses[0];
						if (num1 == 0)
						{
							num1 = this.oFactUses.Remove(factUse.get_ID());
						}
					}
					count = this.oOperations.get_Count();
					for (int k = 0; k < count; k++)
					{
						Operation operation = this.oOperations[0];
						if (num1 == 0)
						{
							num1 = this.oOperations.Remove(operation.get_ID());
						}
					}
					if (num1 == 0)
					{
						bool flag1 = false;
						long d = (long)0;
						if (this.oTypeDocument.get_ID() == (long)24 || this.oTypeDocument.get_ID() == (long)1 || this.oTypeDocument.get_ID() == (long)3 || this.oTypeDocument.get_ID() == (long)7 || this.oTypeDocument.get_ID() == (long)8 || this.oTypeDocument.get_ID() == (long)11 || this.oTypeDocument.get_ID() == (long)13)
						{
							flag1 = true;
							d = this.oContract.get_ID();
						}
						if (base.Delete() == 0)
						{
							if (flag1)
							{
								(new Contract()).RecalcBalances(d, Depot.CurrentPeriod.get_ID());
							}
							if (flag)
							{
								document.Delete();
							}
						}
					}
					num = num1;
				}
				else if (this.GetPD(32) == null)
				{
					num = base.Delete();
				}
				else
				{
					Gmeter gmeter = new Gmeter();
					if (gmeter.Load(Convert.ToInt64(this.GetPD(7).Value)) == 0)
					{
						gmeter.DateVerify = Convert.ToDateTime(this.GetNamePD(32));
						gmeter.oTypeVerify = Depot.oTypeVerifys.item((long)1);
						num = (gmeter.Save() == 0 ? base.Delete() : 1);
					}
					else
					{
						num = 1;
					}
				}
			}
			catch (Exception exception)
			{
				num = num1;
			}
			return num;
		}

		public bool DocumentCarryPay(long IDPeriod, long IDContract, DateTime DocumentDate, double DocumentAmount, long IDDocument, ref bool blErr)
		{
			bool flag;
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
				SqlParameter sqlParameter2 = new SqlParameter("@DocumentDate", SqlDbType.DateTime)
				{
					Direction = ParameterDirection.Input,
					Value = DocumentDate
				};
				SqlParameter sqlParameter3 = new SqlParameter("@DocumentAmount", SqlDbType.Money)
				{
					Direction = ParameterDirection.Input,
					Value = DocumentAmount
				};
				SqlParameter sqlParameter4 = new SqlParameter("@IDDocument", SqlDbType.Int)
				{
					Direction = ParameterDirection.InputOutput,
					Value = IDDocument
				};
				SqlParameter sqlParameter5 = new SqlParameter("@blErr", SqlDbType.Bit)
				{
					Direction = ParameterDirection.Output,
					Value = 0
				};
				SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1, sqlParameter2, sqlParameter3, sqlParameter4, sqlParameter5 };
				Saver.ExecuteProcedure("DocumentCarryPay", sqlParameterArray);
				IDDocument = Convert.ToInt64(sqlParameter4.Value);
				blErr = !Convert.ToBoolean(sqlParameter5.Value);
				flag = blErr;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		public bool DocumentPay(int IsJuridical, long IDPeriod, long IDBatch, long IDContract, long IDTypeDocument, DateTime DocumentDate, double DocumentAmount, double Display, bool UpdateBatch, long IDUser, ref long IDDocument, ref long IDIndication, ref double FactAmount, ref string DocumentNumber, ref string Premech, ref bool blErr, ref double AmountVDGO)
		{
			bool flag;
			try
			{
				SqlParameter sqlParameter = new SqlParameter("@IDPeriod", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDPeriod
				};
				SqlParameter sqlParameter1 = new SqlParameter("@IDBatch", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDBatch
				};
				SqlParameter sqlParameter2 = new SqlParameter("@IDContract", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDContract
				};
				SqlParameter sqlParameter3 = new SqlParameter("@IDTypeDocument", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDTypeDocument
				};
				SqlParameter sqlParameter4 = new SqlParameter("@DocumentDate", SqlDbType.DateTime)
				{
					Direction = ParameterDirection.Input,
					Value = DocumentDate
				};
				SqlParameter sqlParameter5 = new SqlParameter("@DocumentAmount", SqlDbType.Money)
				{
					Direction = ParameterDirection.Input,
					Value = DocumentAmount
				};
				SqlParameter sqlParameter6 = new SqlParameter("@Display", SqlDbType.Float)
				{
					Direction = ParameterDirection.Input,
					Value = Display
				};
				SqlParameter sqlParameter7 = new SqlParameter("@UpdateBatch", SqlDbType.Bit)
				{
					Direction = ParameterDirection.Input,
					Value = (UpdateBatch ? 1 : 0)
				};
				SqlParameter sqlParameter8 = new SqlParameter("@IDUser", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDUser
				};
				SqlParameter sqlParameter9 = new SqlParameter("@IDDocument", SqlDbType.Int)
				{
					Direction = ParameterDirection.InputOutput,
					Value = IDDocument
				};
				SqlParameter sqlParameter10 = new SqlParameter("@IDIndication", SqlDbType.Int)
				{
					Direction = ParameterDirection.InputOutput,
					Value = IDIndication
				};
				SqlParameter sqlParameter11 = new SqlParameter("@FactAmount", SqlDbType.Float)
				{
					Direction = ParameterDirection.InputOutput,
					Value = FactAmount
				};
				SqlParameter sqlParameter12 = new SqlParameter("@DocumentNumber", SqlDbType.VarChar)
				{
					Direction = ParameterDirection.InputOutput,
					Value = DocumentNumber
				};
				SqlParameter sqlParameter13 = new SqlParameter("@Premech", SqlDbType.VarChar)
				{
					Direction = ParameterDirection.InputOutput,
					Value = Premech
				};
				SqlParameter sqlParameter14 = new SqlParameter("@blErr", SqlDbType.Bit)
				{
					Direction = ParameterDirection.Output,
					Value = 0
				};
				SqlParameter sqlParameter15 = new SqlParameter("@AmountVDGO", SqlDbType.Float)
				{
					Direction = ParameterDirection.Output,
					Value = 0
				};
				SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1, sqlParameter2, sqlParameter3, sqlParameter4, sqlParameter5, sqlParameter6, sqlParameter7, sqlParameter8, sqlParameter9, sqlParameter10, sqlParameter11, sqlParameter12, sqlParameter13, sqlParameter14, sqlParameter15 };
				Saver.ExecuteProcedure("DocumentPay", sqlParameterArray);
				IDDocument = Convert.ToInt64(sqlParameter9.Value);
				IDIndication = Convert.ToInt64(sqlParameter10.Value);
				FactAmount = Convert.ToDouble(sqlParameter11.Value);
				DocumentNumber = Convert.ToString(sqlParameter12.Value);
				Premech = Convert.ToString(sqlParameter13.Value);
				blErr = !Convert.ToBoolean(sqlParameter14.Value);
				AmountVDGO = Convert.ToDouble(sqlParameter15.Value);
				flag = blErr;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		public bool DocumentPayVDGO(int IsJuridical, long IDPeriod, long IDBatch, long IDContract, long IDTypeDocument, DateTime DocumentDate, double DocumentAmount, double Display, bool UpdateBatch, long IDUser, ref long IDDocument, ref long IDIndication, ref double FactAmount, ref string DocumentNumber, ref string Premech, ref bool blErr)
		{
			bool flag;
			try
			{
				SqlParameter sqlParameter = new SqlParameter("@IDPeriod", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDPeriod
				};
				SqlParameter sqlParameter1 = new SqlParameter("@IDBatch", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDBatch
				};
				SqlParameter sqlParameter2 = new SqlParameter("@IDContract", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDContract
				};
				SqlParameter sqlParameter3 = new SqlParameter("@IDTypeDocument", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDTypeDocument
				};
				SqlParameter sqlParameter4 = new SqlParameter("@DocumentDate", SqlDbType.DateTime)
				{
					Direction = ParameterDirection.Input,
					Value = DocumentDate
				};
				SqlParameter sqlParameter5 = new SqlParameter("@DocumentAmount", SqlDbType.Money)
				{
					Direction = ParameterDirection.Input,
					Value = DocumentAmount
				};
				SqlParameter sqlParameter6 = new SqlParameter("@Display", SqlDbType.Float)
				{
					Direction = ParameterDirection.Input,
					Value = Display
				};
				SqlParameter sqlParameter7 = new SqlParameter("@UpdateBatch", SqlDbType.Bit)
				{
					Direction = ParameterDirection.Input,
					Value = (UpdateBatch ? 1 : 0)
				};
				SqlParameter sqlParameter8 = new SqlParameter("@IDUser", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDUser
				};
				SqlParameter sqlParameter9 = new SqlParameter("@IDDocument", SqlDbType.Int)
				{
					Direction = ParameterDirection.InputOutput,
					Value = IDDocument
				};
				SqlParameter sqlParameter10 = new SqlParameter("@IDIndication", SqlDbType.Int)
				{
					Direction = ParameterDirection.InputOutput,
					Value = IDIndication
				};
				SqlParameter sqlParameter11 = new SqlParameter("@FactAmount", SqlDbType.Float)
				{
					Direction = ParameterDirection.InputOutput,
					Value = FactAmount
				};
				SqlParameter sqlParameter12 = new SqlParameter("@DocumentNumber", SqlDbType.VarChar)
				{
					Direction = ParameterDirection.InputOutput,
					Value = DocumentNumber
				};
				SqlParameter sqlParameter13 = new SqlParameter("@Premech", SqlDbType.VarChar)
				{
					Direction = ParameterDirection.InputOutput,
					Value = Premech
				};
				SqlParameter sqlParameter14 = new SqlParameter("@blErr", SqlDbType.Bit)
				{
					Direction = ParameterDirection.Output,
					Value = 0
				};
				SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1, sqlParameter2, sqlParameter3, sqlParameter4, sqlParameter5, sqlParameter6, sqlParameter7, sqlParameter8, sqlParameter9, sqlParameter10, sqlParameter11, sqlParameter12, sqlParameter13, sqlParameter14 };
				Saver.ExecuteProcedure("DocumentPayVDGO", sqlParameterArray);
				IDDocument = Convert.ToInt64(sqlParameter9.Value);
				IDIndication = Convert.ToInt64(sqlParameter10.Value);
				FactAmount = Convert.ToDouble(sqlParameter11.Value);
				DocumentNumber = Convert.ToString(sqlParameter12.Value);
				Premech = Convert.ToString(sqlParameter13.Value);
				blErr = !Convert.ToBoolean(sqlParameter14.Value);
				flag = blErr;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		public FactUse GetFactUseByType(TypeFU oTypeFU)
		{
			FactUse factUse;
			IEnumerator enumerator = this.oFactUses.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					FactUse current = (FactUse)enumerator.Current;
					if (current.oTypeFU.get_ID() != oTypeFU.get_ID())
					{
						continue;
					}
					factUse = current;
					return factUse;
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
			return factUse;
		}

		public Indication GetIndication(Gobject oGobject)
		{
			Indication indication;
			IEnumerator enumerator = this.oFactUses.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					FactUse current = (FactUse)enumerator.Current;
					if (current.oGobject.get_ID() != oGobject.get_ID())
					{
						continue;
					}
					indication = current.oIndication;
					return indication;
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
			return indication;
		}

		public string GetNamePD(int IdTypePD)
		{
			string value;
			IEnumerator enumerator = this.oPDs.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					PD current = (PD)enumerator.Current;
					if (current.oTypePD.get_ID() != (long)IdTypePD)
					{
						continue;
					}
					value = current.Value;
					return value;
				}
				return "";
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return value;
		}

		public Operation GetOperation(Balance oBalance, TypeOperation oTypeOperation)
		{
			Operation operation;
			IEnumerator enumerator = this.oOperations.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Operation current = (Operation)enumerator.Current;
					if (current.oBalance.get_ID() != oBalance.get_ID() || current.oTypeOperation.get_ID() != oTypeOperation.get_ID())
					{
						continue;
					}
					operation = current;
					return operation;
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
			return operation;
		}

		public PD GetPD(int IdTypePD)
		{
			PD pD;
			IEnumerator enumerator = this.oPDs.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					PD current = (PD)enumerator.Current;
					if (current.oTypePD.get_ID() != (long)IdTypePD)
					{
						continue;
					}
					pD = current;
					return pD;
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
			return pD;
		}

		public bool SaveGrafikForCreditUsl(long IDDocument, ref bool blErr)
		{
			bool flag;
			try
			{
				SqlParameter sqlParameter = new SqlParameter("@IDDocument", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDDocument
				};
				SqlParameter sqlParameter1 = new SqlParameter("@blErr", SqlDbType.Bit)
				{
					Direction = ParameterDirection.Output,
					Value = 0
				};
				SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1 };
				Saver.ExecuteProcedure("spSaveGrafikForCreditUsl", sqlParameterArray);
				IDDocument = Convert.ToInt64(sqlParameter.Value);
				blErr = !Convert.ToBoolean(sqlParameter1.Value);
				flag = blErr;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		public bool UndertakingJuridikal(long IDPeriod, long IDContract, ref long IDDocument, double AmountDocument, double GosPoshlina, ref bool blErr)
		{
			bool flag;
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
				SqlParameter sqlParameter2 = new SqlParameter("@IDDocument", SqlDbType.Int)
				{
					Direction = ParameterDirection.InputOutput,
					Value = IDDocument
				};
				SqlParameter sqlParameter3 = new SqlParameter("@AmountDocument", SqlDbType.Float)
				{
					Direction = ParameterDirection.Input,
					Value = AmountDocument
				};
				SqlParameter sqlParameter4 = new SqlParameter("@GosPoshlina", SqlDbType.Float)
				{
					Direction = ParameterDirection.Input,
					Value = GosPoshlina
				};
				SqlParameter sqlParameter5 = new SqlParameter("@blErr", SqlDbType.Bit)
				{
					Direction = ParameterDirection.Output,
					Value = 0
				};
				SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1, sqlParameter2, sqlParameter3, sqlParameter4, sqlParameter5 };
				Saver.ExecuteProcedure("spUndertakingJuridikal", sqlParameterArray);
				IDDocument = Convert.ToInt64(sqlParameter2.Value);
				blErr = !Convert.ToBoolean(sqlParameter5.Value);
				flag = blErr;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}
	}
}