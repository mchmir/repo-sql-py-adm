using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class Depot
	{
		public static frmMain _main;

		private static Periods _Periods;

		private static TypeDocuments _TypeDocuments;

		private static TypeBatchs _TypeBatchs;

		private static TypePays _TypePays;

		private static TypeOperations _TypeOperations;

		private static TypeAgents _TypeAgents;

		private static TypeEnds _TypeEnds;

		private static TypePDs _TypePDs;

		private static TypeFUs _TypeFUs;

		private static TypeIndications _TypeIndications;

		private static StatusBatchs _StatusBatchs;

		private static StatusGObjects _StatusGObjects;

		private static StatusGMeters _StatusGMeters;

		private static Accountings _Accountings;

		private static Settings _settings;

		private static TypeGMeters _typegmeters;

		private static TypeVerifys _TypeVerifys;

		private static Correspondents _Correspondents;

		private static Sending _Sending;

		private static TypeActions _TypeActions;

		private static Settings1 _settings1;

		public static Period CurrentPeriod
		{
			get
			{
				if (Depot.oPeriods == null || Depot.oPeriods.get_Count() <= 0)
				{
					return null;
				}
				return Depot.oPeriods[0];
			}
		}

		public static Accountings oAccountings
		{
			get
			{
				if (Depot._Accountings == null)
				{
					Depot._Accountings = new Accountings();
					if (Depot._Accountings.Load() != 0)
					{
						Depot._Accountings = null;
					}
				}
				return Depot._Accountings;
			}
		}

		public static Correspondents oCorrespondents
		{
			get
			{
				if (Depot._Correspondents == null)
				{
					Depot._Correspondents = new Correspondents();
					if (Depot._Correspondents.Load() != 0)
					{
						Depot._Correspondents = null;
					}
				}
				return Depot._Correspondents;
			}
		}

		public static Periods oPeriods
		{
			get
			{
				if (Depot._Periods == null)
				{
					Depot._Periods = new Periods();
					if (Depot._Periods.Load() != 0)
					{
						Depot._Periods = null;
					}
				}
				return Depot._Periods;
			}
			set
			{
				if (value == null)
				{
					Depot._Periods = null;
				}
			}
		}

		public static Sending oSending
		{
			get
			{
				if (Depot.oSettings1 == null)
				{
					Depot._Sending = null;
					return Depot._Sending;
				}
				if (Depot._Sending == null)
				{
					Sendings sending = new Sendings();
					if (sending.Load(Depot.oSettings1.oCorrespondent) == 0)
					{
						Depot._Sending = sending[0];
					}
					else
					{
						Depot._Sending = null;
					}
					if (Depot._Sending == null)
					{
						Depot._Sending = new Sending()
						{
							oCorrespondent = Depot.oSettings1.oCorrespondent,
							DateSending = DateTime.Today.Date,
							NumberSending = 1
						};
						if (Depot._Sending.Save() != 0)
						{
							Depot._Sending = null;
						}
					}
				}
				return Depot._Sending;
			}
			set
			{
				Depot._Sending = value;
			}
		}

		public static Settings oSettings
		{
			get
			{
				if (Depot._settings == null)
				{
					Settingss settingss = new Settingss();
					if (settingss.Load(SQLConnect.CurrentUser) == 0)
					{
						Depot._settings = settingss[0];
						if (Depot._settings == null)
						{
							Depot._settings = settingss.Add();
							Depot._settings.oUser = SQLConnect.CurrentUser;
							Depot._settings.oAgent = null;
							Depot._settings.Startup = false;
							Depot._settings.Save();
						}
					}
					else
					{
						Depot._settings = null;
					}
				}
				return Depot._settings;
			}
		}

		public static Settings1 oSettings1
		{
			get
			{
				if (Depot._settings1 == null)
				{
					Settings1s settings1 = new Settings1s();
					if (settings1.Load() == 0)
					{
						Depot._settings1 = settings1[0];
					}
					else
					{
						Depot._settings1 = null;
					}
				}
				return Depot._settings1;
			}
		}

		public static StatusBatchs oStatusBatchs
		{
			get
			{
				if (Depot._StatusBatchs == null)
				{
					Depot._StatusBatchs = new StatusBatchs();
					if (Depot._StatusBatchs.Load() != 0)
					{
						Depot._StatusBatchs = null;
					}
				}
				return Depot._StatusBatchs;
			}
		}

		public static StatusGMeters oStatusGMeters
		{
			get
			{
				if (Depot._StatusGMeters == null)
				{
					Depot._StatusGMeters = new StatusGMeters();
					if (Depot._StatusGMeters.Load() != 0)
					{
						Depot._StatusGMeters = null;
					}
				}
				return Depot._StatusGMeters;
			}
		}

		public static StatusGObjects oStatusGObjects
		{
			get
			{
				if (Depot._StatusGObjects == null)
				{
					Depot._StatusGObjects = new StatusGObjects();
					if (Depot._StatusGObjects.Load() != 0)
					{
						Depot._StatusGObjects = null;
					}
				}
				return Depot._StatusGObjects;
			}
		}

		public static TypeActions oTypeActions
		{
			get
			{
				if (Depot._TypeActions == null)
				{
					Depot._TypeActions = new TypeActions();
					if (Depot._TypeActions.Load() != 0)
					{
						Depot._TypeActions = null;
					}
				}
				return Depot._TypeActions;
			}
		}

		public static TypeAgents oTypeAgents
		{
			get
			{
				if (Depot._TypeAgents == null)
				{
					Depot._TypeAgents = new TypeAgents();
					if (Depot._TypeAgents.Load() != 0)
					{
						Depot._TypeAgents = null;
					}
				}
				return Depot._TypeAgents;
			}
		}

		public static TypeBatchs oTypeBatchs
		{
			get
			{
				if (Depot._TypeBatchs == null)
				{
					Depot._TypeBatchs = new TypeBatchs();
					if (Depot._TypeBatchs.Load() != 0)
					{
						Depot._TypeBatchs = null;
					}
				}
				return Depot._TypeBatchs;
			}
		}

		public static TypeDocuments oTypeDocuments
		{
			get
			{
				if (Depot._TypeDocuments == null)
				{
					Depot._TypeDocuments = new TypeDocuments();
					if (Depot._TypeDocuments.Load() != 0)
					{
						Depot._TypeDocuments = null;
					}
				}
				return Depot._TypeDocuments;
			}
		}

		public static TypeEnds oTypeEnds
		{
			get
			{
				if (Depot._TypeEnds == null)
				{
					Depot._TypeEnds = new TypeEnds();
					if (Depot._TypeEnds.Load() != 0)
					{
						Depot._TypeEnds = null;
					}
				}
				return Depot._TypeEnds;
			}
		}

		public static TypeFUs oTypeFUs
		{
			get
			{
				if (Depot._TypeFUs == null)
				{
					Depot._TypeFUs = new TypeFUs();
					if (Depot._TypeFUs.Load() != 0)
					{
						Depot._TypeFUs = null;
					}
				}
				return Depot._TypeFUs;
			}
		}

		public static TypeGMeters oTypeGMeters
		{
			get
			{
				if (Depot._typegmeters == null)
				{
					Depot._typegmeters = new TypeGMeters();
					if (Depot._typegmeters.Load() != 0)
					{
						Depot._typegmeters = null;
					}
				}
				return Depot._typegmeters;
			}
		}

		public static TypeIndications oTypeIndications
		{
			get
			{
				if (Depot._TypeIndications == null)
				{
					Depot._TypeIndications = new TypeIndications();
					if (Depot._TypeIndications.Load() != 0)
					{
						Depot._TypeIndications = null;
					}
				}
				return Depot._TypeIndications;
			}
		}

		public static TypeOperations oTypeOperations
		{
			get
			{
				if (Depot._TypeOperations == null)
				{
					Depot._TypeOperations = new TypeOperations();
					if (Depot._TypeOperations.Load() != 0)
					{
						Depot._TypeOperations = null;
					}
				}
				return Depot._TypeOperations;
			}
		}

		public static TypePays oTypePays
		{
			get
			{
				if (Depot._TypePays == null)
				{
					Depot._TypePays = new TypePays();
					if (Depot._TypePays.Load() != 0)
					{
						Depot._TypePays = null;
					}
				}
				return Depot._TypePays;
			}
		}

		public static TypePDs oTypePDs
		{
			get
			{
				if (Depot._TypePDs == null)
				{
					Depot._TypePDs = new TypePDs();
					if (Depot._TypePDs.Load() != 0)
					{
						Depot._TypePDs = null;
					}
				}
				return Depot._TypePDs;
			}
		}

		public static TypeVerifys oTypeVerifys
		{
			get
			{
				if (Depot._TypeVerifys == null)
				{
					Depot._TypeVerifys = new TypeVerifys();
					if (Depot._TypeVerifys.Load() != 0)
					{
						Depot._TypeVerifys = null;
					}
				}
				return Depot._TypeVerifys;
			}
		}

		public static string[] status
		{
			get
			{
				string[] text = new string[] { Depot._main.sbpStatus.Text, Depot._main.sbpStatus2.Text };
				return text;
			}
			set
			{
				Depot._main.sbpStatus.Text = value[0];
				Depot._main.sbpStatus2.Text = value[1];
			}
		}

		public Depot()
		{
		}

		public static void eNumGoups()
		{
			Saver.ExecuteProcedure("speNumGroup", null);
		}

		public static void spRecalcCash(long IDCashier, DateTime vDate)
		{
			try
			{
				SqlParameter sqlParameter = new SqlParameter("@IDCashier", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDCashier
				};
				SqlParameter sqlParameter1 = new SqlParameter("@Date", SqlDbType.DateTime)
				{
					Direction = ParameterDirection.Input,
					Value = vDate
				};
				SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1 };
				Saver.ExecuteProcedure("spRecalcCashBalance", sqlParameterArray);
			}
			catch (Exception exception)
			{
			}
		}
	}
}