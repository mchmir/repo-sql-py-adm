using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmCashRepTerminal : Form
	{
		private Label label1;

		private DateTimePicker dtpDate;

		private Button bOK;

		private Button bCancel;

		private Label label2;

		private NumericUpDown numBatchAmount;

		private Agents _agents;

		private Button bCashRep;

		private System.ComponentModel.Container components = null;

		public frmCashRepTerminal()
		{
			this.InitializeComponent();
		}

		private void bCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void bCashRep_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					int num = 125;
					this.Cursor = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 3 };
					string[] str = new string[] { "@IDCashier", "@DateB" };
					string[] strArrays = str;
					str = new string[] { num.ToString(), null };
					str[1] = this.dtpDate.Value.ToString();
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repCashRepTerminal.rpt");
					Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = "",
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
					System.Windows.Forms.Cursor.Current = Cursors.Default;
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					System.Windows.Forms.Cursor.Current = Cursors.Default;
					MessageBox.Show(exception.Message);
				}
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
		}

		private void bOK_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					if (this.numBatchAmount.Value <= new decimal(0))
					{
						MessageBox.Show("Не указана сумма инкассации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else if (MessageBox.Show("Будет произведена инскассация и печать РКО. Продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
					{
						this._agents = new Agents();
						this._agents.Load(Depot.oTypeAgents.item((long)2));
						Batchs batch = new Batchs();
						batch.Load(this.dtpDate.Value, this._agents.item((long)125), Depot.oTypeBatchs.item((long)3));
						if (batch.get_Count() <= 0)
						{
							Batch num = new Batch()
							{
								oTypePay = Depot.oTypePays.item((long)1),
								oPeriod = Depot.CurrentPeriod,
								oCashier = this._agents.item((long)125)
							};
							num.set_Name("1010@В центральную кассу");
							num.BatchCount = 1;
							num.BatchAmount = Convert.ToDouble(this.numBatchAmount.Value);
							num.BatchDate = this.dtpDate.Value;
							num.oTypeBatch = Depot.oTypeBatchs.item((long)3);
							num.oStatusBatch = Depot.oStatusBatchs.item((long)2);
							num.Note = "";
							if (num.Save() != 0)
							{
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								this.PrintRKO(num);
							}
						}
						else
						{
							return;
						}
					}
					else
					{
						return;
					}
				}
				catch
				{
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.dtpDate = new DateTimePicker();
			this.bOK = new Button();
			this.bCancel = new Button();
			this.label2 = new Label();
			this.numBatchAmount = new NumericUpDown();
			this.bCashRep = new Button();
			((ISupportInitialize)this.numBatchAmount).BeginInit();
			base.SuspendLayout();
			this.label1.Location = new Point(8, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Дата:";
			this.dtpDate.Location = new Point(48, 8);
			this.dtpDate.Name = "dtpDate";
			this.dtpDate.Size = new System.Drawing.Size(176, 20);
			this.dtpDate.TabIndex = 1;
			this.bOK.Location = new Point(8, 64);
			this.bOK.Name = "bOK";
			this.bOK.Size = new System.Drawing.Size(216, 23);
			this.bOK.TabIndex = 2;
			this.bOK.Text = "Инкассация и печать РКО";
			this.bOK.Click += new EventHandler(this.bOK_Click);
			this.bCancel.Location = new Point(8, 128);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(216, 23);
			this.bCancel.TabIndex = 3;
			this.bCancel.Text = "Отмена";
			this.bCancel.Click += new EventHandler(this.bCancel_Click);
			this.label2.Location = new Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Сумма:";
			this.numBatchAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numBatchAmount.DecimalPlaces = 2;
			this.numBatchAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.numBatchAmount.Location = new Point(56, 37);
			NumericUpDown num = this.numBatchAmount;
			int[] numArray = new int[] { 99999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numBatchAmount.Name = "numBatchAmount";
			this.numBatchAmount.Size = new System.Drawing.Size(168, 22);
			this.numBatchAmount.TabIndex = 6;
			this.bCashRep.Location = new Point(8, 96);
			this.bCashRep.Name = "bCashRep";
			this.bCashRep.Size = new System.Drawing.Size(216, 23);
			this.bCashRep.TabIndex = 7;
			this.bCashRep.Text = "Кассовый отчет";
			this.bCashRep.Click += new EventHandler(this.bCashRep_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(232, 158);
			base.Controls.Add(this.bCashRep);
			base.Controls.Add(this.numBatchAmount);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.bCancel);
			base.Controls.Add(this.bOK);
			base.Controls.Add(this.dtpDate);
			base.Controls.Add(this.label1);
			base.Name = "frmCashRepTerminal";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Кассовый отчет по терминалу";
			((ISupportInitialize)this.numBatchAmount).EndInit();
			base.ResumeLayout(false);
		}

		public bool PrintRKO(Batch oBatch)
		{
			bool flag = false;
			try
			{
				string str = "";
				str = string.Concat("Сдано в центральную кассу через ", oBatch.oCashier.get_Name().ToString());
				string str1 = "";
				string str2 = Tools.ConvertCurencyInString(oBatch.BatchAmount);
				char[] chr = new char[] { Convert.ToChar("@") };
				string[] strArrays = oBatch.get_Name().Split(chr);
				if ((int)strArrays.Length == 2)
				{
					str1 = strArrays[0];
				}
				string str3 = "";
				str3 = "Внутреннее перемещение";
				int day = oBatch.BatchDate.Day;
				day.ToString();
				day = oBatch.BatchDate.Month;
				day.ToString();
				day = oBatch.BatchDate.Year;
				day.ToString();
				oBatch.BatchAmount.ToString();
				oBatch.oCashier.get_Name();
				day = oBatch.BatchDate.Day;
				string str4 = day.ToString();
				day = oBatch.BatchDate.Month;
				string str5 = day.ToString();
				day = oBatch.BatchDate.Year;
				string str6 = day.ToString();
				string[] strArrays1 = new string[] { string.Concat("1|", str1, "|85|670|0"), string.Concat("3|", str, "|103|646|0"), string.Concat("3|", str3, "|103|624|0"), null, null, null, null, null };
				double batchAmount = oBatch.BatchAmount;
				strArrays1[3] = string.Concat("1|", batchAmount.ToString(), "|235|670|0");
				strArrays1[4] = string.Concat("3|", str2, "|100|598|0");
				strArrays1[5] = string.Concat("3|", str2, "|92|531|0");
				string[] strArrays2 = new string[] { "1|", str4, ".", str5, ".", str6, "г.|510|733|0" };
				strArrays1[6] = string.Concat(strArrays2);
				strArrays1[7] = string.Concat("1|", oBatch.oCashier.get_Name(), "|358|453|0");
				string[] strArrays3 = strArrays1;
				Tools.ShowPdfDocument(string.Concat("Report_", str.ToString(), ".pdf"), string.Concat(Depot.oSettings.ReportPath.Trim(), "Templates\\"), "РКО.pdf", strArrays3);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка формирования отчета - PКО! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}
	}
}