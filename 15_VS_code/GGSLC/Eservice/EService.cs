using GGSLC.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace GGSLC.Eservice
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "4.6.1055.0")]
	[WebServiceBinding(Name="EServiceSoap", Namespace="http://tempuri.org/")]
	[XmlInclude(typeof(object[]))]
	public class EService : SoapHttpClientProtocol
	{
		private SendOrPostCallback _TerminalStatusOperationCompleted;

		private SendOrPostCallback _ConsumerSearchOperationCompleted;

		private SendOrPostCallback _ConsumerSearchLiteOperationCompleted;

		private SendOrPostCallback _ConsumerPayOperationCompleted;

		private SendOrPostCallback _ServerCommandGetOperationCompleted;

		private SendOrPostCallback _ResultExecServerComandOperationCompleted;

		private SendOrPostCallback _ServerCommandSaveOperationCompleted;

		private SendOrPostCallback _UpdateFilesOperationCompleted;

		private SendOrPostCallback _PortalVersionOperationCompleted;

		private SendOrPostCallback _PaymentSynchOperationCompleted;

		private SendOrPostCallback _PaymentSynchCheckOperationCompleted;

		private SendOrPostCallback _KomLoadCollectionOperationCompleted;

		private SendOrPostCallback _PortalTimeOperationCompleted;

		private SendOrPostCallback PCLoginOperationCompleted;

		private SendOrPostCallback PCGetDataOperationCompleted;

		private SendOrPostCallback PCGetDatainArrayOperationCompleted;

		private SendOrPostCallback PCChabgePWDOperationCompleted;

		private SendOrPostCallback PCSaveRequestOperationCompleted;

		private SendOrPostCallback PCLostPWDOperationCompleted;

		private SendOrPostCallback PCSaveCommentOperationCompleted;

		private SendOrPostCallback PCSaveIndOperationCompleted;

		private SendOrPostCallback PCGetDataPayinArrayOperationCompleted;

		private SendOrPostCallback PCGetDataFAQinArrayOperationCompleted;

		private SendOrPostCallback PCGetConsamerIndicationsinArrayOperationCompleted;

		private SendOrPostCallback PCGetConsamerFactUseinArrayOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;

		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
				{
					base.UseDefaultCredentials = false;
				}
				base.Url = value;
			}
		}

		public new bool UseDefaultCredentials
		{
			get
			{
				return base.UseDefaultCredentials;
			}
			set
			{
				base.UseDefaultCredentials = value;
				this.useDefaultCredentialsSetExplicitly = true;
			}
		}

		public EService()
		{
			this.Url = Settings.Default.GGSLC_Eservice_EService;
			if (!this.IsLocalFileSystemWebService(this.Url))
			{
				this.useDefaultCredentialsSetExplicitly = true;
				return;
			}
			this.UseDefaultCredentials = true;
			this.useDefaultCredentialsSetExplicitly = false;
		}

		[SoapDocumentMethod("http://tempuri.org/_ConsumerPay", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public object[] _ConsumerPay(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (object[])base.Invoke("_ConsumerPay", objArray)[0];
		}

		public void _ConsumerPayAsync(string param1, string param2, string param3, string param4)
		{
			this._ConsumerPayAsync(param1, param2, param3, param4, null);
		}

		public void _ConsumerPayAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this._ConsumerPayOperationCompleted == null)
			{
				this._ConsumerPayOperationCompleted = new SendOrPostCallback(this.On_ConsumerPayOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("_ConsumerPay", objArray, this._ConsumerPayOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/_ConsumerSearch", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public object[] _ConsumerSearch(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (object[])base.Invoke("_ConsumerSearch", objArray)[0];
		}

		public void _ConsumerSearchAsync(string param1, string param2, string param3, string param4)
		{
			this._ConsumerSearchAsync(param1, param2, param3, param4, null);
		}

		public void _ConsumerSearchAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this._ConsumerSearchOperationCompleted == null)
			{
				this._ConsumerSearchOperationCompleted = new SendOrPostCallback(this.On_ConsumerSearchOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("_ConsumerSearch", objArray, this._ConsumerSearchOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/_ConsumerSearchLite", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public string _ConsumerSearchLite(string param1)
		{
			object[] objArray = new object[] { param1 };
			return (string)base.Invoke("_ConsumerSearchLite", objArray)[0];
		}

		public void _ConsumerSearchLiteAsync(string param1)
		{
			this._ConsumerSearchLiteAsync(param1, null);
		}

		public void _ConsumerSearchLiteAsync(string param1, object userState)
		{
			if (this._ConsumerSearchLiteOperationCompleted == null)
			{
				this._ConsumerSearchLiteOperationCompleted = new SendOrPostCallback(this.On_ConsumerSearchLiteOperationCompleted);
			}
			object[] objArray = new object[] { param1 };
			base.InvokeAsync("_ConsumerSearchLite", objArray, this._ConsumerSearchLiteOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/_KomLoadCollection", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public object[] _KomLoadCollection(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (object[])base.Invoke("_KomLoadCollection", objArray)[0];
		}

		public void _KomLoadCollectionAsync(string param1, string param2, string param3, string param4)
		{
			this._KomLoadCollectionAsync(param1, param2, param3, param4, null);
		}

		public void _KomLoadCollectionAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this._KomLoadCollectionOperationCompleted == null)
			{
				this._KomLoadCollectionOperationCompleted = new SendOrPostCallback(this.On_KomLoadCollectionOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("_KomLoadCollection", objArray, this._KomLoadCollectionOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/_PaymentSynch", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public object[] _PaymentSynch(string param1, string param2)
		{
			object[] objArray = new object[] { param1, param2 };
			return (object[])base.Invoke("_PaymentSynch", objArray)[0];
		}

		public void _PaymentSynchAsync(string param1, string param2)
		{
			this._PaymentSynchAsync(param1, param2, null);
		}

		public void _PaymentSynchAsync(string param1, string param2, object userState)
		{
			if (this._PaymentSynchOperationCompleted == null)
			{
				this._PaymentSynchOperationCompleted = new SendOrPostCallback(this.On_PaymentSynchOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2 };
			base.InvokeAsync("_PaymentSynch", objArray, this._PaymentSynchOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/_PaymentSynchCheck", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public bool _PaymentSynchCheck(string param1, string param2)
		{
			object[] objArray = new object[] { param1, param2 };
			return (bool)base.Invoke("_PaymentSynchCheck", objArray)[0];
		}

		public void _PaymentSynchCheckAsync(string param1, string param2)
		{
			this._PaymentSynchCheckAsync(param1, param2, null);
		}

		public void _PaymentSynchCheckAsync(string param1, string param2, object userState)
		{
			if (this._PaymentSynchCheckOperationCompleted == null)
			{
				this._PaymentSynchCheckOperationCompleted = new SendOrPostCallback(this.On_PaymentSynchCheckOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2 };
			base.InvokeAsync("_PaymentSynchCheck", objArray, this._PaymentSynchCheckOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/_PortalTime", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public string _PortalTime()
		{
			object[] objArray = base.Invoke("_PortalTime", new object[0]);
			return (string)objArray[0];
		}

		public void _PortalTimeAsync()
		{
			this._PortalTimeAsync(null);
		}

		public void _PortalTimeAsync(object userState)
		{
			if (this._PortalTimeOperationCompleted == null)
			{
				this._PortalTimeOperationCompleted = new SendOrPostCallback(this.On_PortalTimeOperationCompleted);
			}
			base.InvokeAsync("_PortalTime", new object[0], this._PortalTimeOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/_PortalVersion", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public string _PortalVersion()
		{
			object[] objArray = base.Invoke("_PortalVersion", new object[0]);
			return (string)objArray[0];
		}

		public void _PortalVersionAsync()
		{
			this._PortalVersionAsync(null);
		}

		public void _PortalVersionAsync(object userState)
		{
			if (this._PortalVersionOperationCompleted == null)
			{
				this._PortalVersionOperationCompleted = new SendOrPostCallback(this.On_PortalVersionOperationCompleted);
			}
			base.InvokeAsync("_PortalVersion", new object[0], this._PortalVersionOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/_ResultExecServerComand", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public void _ResultExecServerComand(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.Invoke("_ResultExecServerComand", objArray);
		}

		public void _ResultExecServerComandAsync(string param1, string param2, string param3, string param4)
		{
			this._ResultExecServerComandAsync(param1, param2, param3, param4, null);
		}

		public void _ResultExecServerComandAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this._ResultExecServerComandOperationCompleted == null)
			{
				this._ResultExecServerComandOperationCompleted = new SendOrPostCallback(this.On_ResultExecServerComandOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("_ResultExecServerComand", objArray, this._ResultExecServerComandOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/_ServerCommandGet", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public object[] _ServerCommandGet(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (object[])base.Invoke("_ServerCommandGet", objArray)[0];
		}

		public void _ServerCommandGetAsync(string param1, string param2, string param3, string param4)
		{
			this._ServerCommandGetAsync(param1, param2, param3, param4, null);
		}

		public void _ServerCommandGetAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this._ServerCommandGetOperationCompleted == null)
			{
				this._ServerCommandGetOperationCompleted = new SendOrPostCallback(this.On_ServerCommandGetOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("_ServerCommandGet", objArray, this._ServerCommandGetOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/_ServerCommandSave", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public bool _ServerCommandSave(string param1, string param2)
		{
			object[] objArray = new object[] { param1, param2 };
			return (bool)base.Invoke("_ServerCommandSave", objArray)[0];
		}

		public void _ServerCommandSaveAsync(string param1, string param2)
		{
			this._ServerCommandSaveAsync(param1, param2, null);
		}

		public void _ServerCommandSaveAsync(string param1, string param2, object userState)
		{
			if (this._ServerCommandSaveOperationCompleted == null)
			{
				this._ServerCommandSaveOperationCompleted = new SendOrPostCallback(this.On_ServerCommandSaveOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2 };
			base.InvokeAsync("_ServerCommandSave", objArray, this._ServerCommandSaveOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/_TerminalStatus", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public bool _TerminalStatus(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (bool)base.Invoke("_TerminalStatus", objArray)[0];
		}

		public void _TerminalStatusAsync(string param1, string param2, string param3, string param4)
		{
			this._TerminalStatusAsync(param1, param2, param3, param4, null);
		}

		public void _TerminalStatusAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this._TerminalStatusOperationCompleted == null)
			{
				this._TerminalStatusOperationCompleted = new SendOrPostCallback(this.On_TerminalStatusOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("_TerminalStatus", objArray, this._TerminalStatusOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/_UpdateFiles", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public object[] _UpdateFiles(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (object[])base.Invoke("_UpdateFiles", objArray)[0];
		}

		public void _UpdateFilesAsync(string param1, string param2, string param3, string param4)
		{
			this._UpdateFilesAsync(param1, param2, param3, param4, null);
		}

		public void _UpdateFilesAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this._UpdateFilesOperationCompleted == null)
			{
				this._UpdateFilesOperationCompleted = new SendOrPostCallback(this.On_UpdateFilesOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("_UpdateFiles", objArray, this._UpdateFilesOperationCompleted, userState);
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			if (url == null || url == string.Empty)
			{
				return false;
			}
			System.Uri uri = new System.Uri(url);
			if (uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			return false;
		}

		private void On_ConsumerPayOperationCompleted(object arg)
		{
			if (this._ConsumerPayCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._ConsumerPayCompleted(this, new _ConsumerPayCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void On_ConsumerSearchLiteOperationCompleted(object arg)
		{
			if (this._ConsumerSearchLiteCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._ConsumerSearchLiteCompleted(this, new _ConsumerSearchLiteCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void On_ConsumerSearchOperationCompleted(object arg)
		{
			if (this._ConsumerSearchCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._ConsumerSearchCompleted(this, new _ConsumerSearchCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void On_KomLoadCollectionOperationCompleted(object arg)
		{
			if (this._KomLoadCollectionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._KomLoadCollectionCompleted(this, new _KomLoadCollectionCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void On_PaymentSynchCheckOperationCompleted(object arg)
		{
			if (this._PaymentSynchCheckCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._PaymentSynchCheckCompleted(this, new _PaymentSynchCheckCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void On_PaymentSynchOperationCompleted(object arg)
		{
			if (this._PaymentSynchCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._PaymentSynchCompleted(this, new _PaymentSynchCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void On_PortalTimeOperationCompleted(object arg)
		{
			if (this._PortalTimeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._PortalTimeCompleted(this, new _PortalTimeCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void On_PortalVersionOperationCompleted(object arg)
		{
			if (this._PortalVersionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._PortalVersionCompleted(this, new _PortalVersionCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void On_ResultExecServerComandOperationCompleted(object arg)
		{
			if (this._ResultExecServerComandCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._ResultExecServerComandCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void On_ServerCommandGetOperationCompleted(object arg)
		{
			if (this._ServerCommandGetCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._ServerCommandGetCompleted(this, new _ServerCommandGetCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void On_ServerCommandSaveOperationCompleted(object arg)
		{
			if (this._ServerCommandSaveCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._ServerCommandSaveCompleted(this, new _ServerCommandSaveCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void On_TerminalStatusOperationCompleted(object arg)
		{
			if (this._TerminalStatusCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._TerminalStatusCompleted(this, new _TerminalStatusCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void On_UpdateFilesOperationCompleted(object arg)
		{
			if (this._UpdateFilesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._UpdateFilesCompleted(this, new _UpdateFilesCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnPCChabgePWDOperationCompleted(object arg)
		{
			if (this.PCChabgePWDCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.PCChabgePWDCompleted(this, new PCChabgePWDCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnPCGetConsamerFactUseinArrayOperationCompleted(object arg)
		{
			if (this.PCGetConsamerFactUseinArrayCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.PCGetConsamerFactUseinArrayCompleted(this, new PCGetConsamerFactUseinArrayCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnPCGetConsamerIndicationsinArrayOperationCompleted(object arg)
		{
			if (this.PCGetConsamerIndicationsinArrayCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.PCGetConsamerIndicationsinArrayCompleted(this, new PCGetConsamerIndicationsinArrayCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnPCGetDataFAQinArrayOperationCompleted(object arg)
		{
			if (this.PCGetDataFAQinArrayCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.PCGetDataFAQinArrayCompleted(this, new PCGetDataFAQinArrayCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnPCGetDatainArrayOperationCompleted(object arg)
		{
			if (this.PCGetDatainArrayCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.PCGetDatainArrayCompleted(this, new PCGetDatainArrayCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnPCGetDataOperationCompleted(object arg)
		{
			if (this.PCGetDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.PCGetDataCompleted(this, new PCGetDataCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnPCGetDataPayinArrayOperationCompleted(object arg)
		{
			if (this.PCGetDataPayinArrayCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.PCGetDataPayinArrayCompleted(this, new PCGetDataPayinArrayCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnPCLoginOperationCompleted(object arg)
		{
			if (this.PCLoginCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.PCLoginCompleted(this, new PCLoginCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnPCLostPWDOperationCompleted(object arg)
		{
			if (this.PCLostPWDCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.PCLostPWDCompleted(this, new PCLostPWDCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnPCSaveCommentOperationCompleted(object arg)
		{
			if (this.PCSaveCommentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.PCSaveCommentCompleted(this, new PCSaveCommentCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnPCSaveIndOperationCompleted(object arg)
		{
			if (this.PCSaveIndCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.PCSaveIndCompleted(this, new PCSaveIndCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnPCSaveRequestOperationCompleted(object arg)
		{
			if (this.PCSaveRequestCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.PCSaveRequestCompleted(this, new PCSaveRequestCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/PCChabgePWD", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public string PCChabgePWD(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (string)base.Invoke("PCChabgePWD", objArray)[0];
		}

		public void PCChabgePWDAsync(string param1, string param2, string param3, string param4)
		{
			this.PCChabgePWDAsync(param1, param2, param3, param4, null);
		}

		public void PCChabgePWDAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this.PCChabgePWDOperationCompleted == null)
			{
				this.PCChabgePWDOperationCompleted = new SendOrPostCallback(this.OnPCChabgePWDOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("PCChabgePWD", objArray, this.PCChabgePWDOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/PCGetConsamerFactUseinArray", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public object[] PCGetConsamerFactUseinArray(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (object[])base.Invoke("PCGetConsamerFactUseinArray", objArray)[0];
		}

		public void PCGetConsamerFactUseinArrayAsync(string param1, string param2, string param3, string param4)
		{
			this.PCGetConsamerFactUseinArrayAsync(param1, param2, param3, param4, null);
		}

		public void PCGetConsamerFactUseinArrayAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this.PCGetConsamerFactUseinArrayOperationCompleted == null)
			{
				this.PCGetConsamerFactUseinArrayOperationCompleted = new SendOrPostCallback(this.OnPCGetConsamerFactUseinArrayOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("PCGetConsamerFactUseinArray", objArray, this.PCGetConsamerFactUseinArrayOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/PCGetConsamerIndicationsinArray", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public object[] PCGetConsamerIndicationsinArray(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (object[])base.Invoke("PCGetConsamerIndicationsinArray", objArray)[0];
		}

		public void PCGetConsamerIndicationsinArrayAsync(string param1, string param2, string param3, string param4)
		{
			this.PCGetConsamerIndicationsinArrayAsync(param1, param2, param3, param4, null);
		}

		public void PCGetConsamerIndicationsinArrayAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this.PCGetConsamerIndicationsinArrayOperationCompleted == null)
			{
				this.PCGetConsamerIndicationsinArrayOperationCompleted = new SendOrPostCallback(this.OnPCGetConsamerIndicationsinArrayOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("PCGetConsamerIndicationsinArray", objArray, this.PCGetConsamerIndicationsinArrayOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/PCGetData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public string PCGetData(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (string)base.Invoke("PCGetData", objArray)[0];
		}

		public void PCGetDataAsync(string param1, string param2, string param3, string param4)
		{
			this.PCGetDataAsync(param1, param2, param3, param4, null);
		}

		public void PCGetDataAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this.PCGetDataOperationCompleted == null)
			{
				this.PCGetDataOperationCompleted = new SendOrPostCallback(this.OnPCGetDataOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("PCGetData", objArray, this.PCGetDataOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/PCGetDataFAQinArray", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public object[] PCGetDataFAQinArray(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (object[])base.Invoke("PCGetDataFAQinArray", objArray)[0];
		}

		public void PCGetDataFAQinArrayAsync(string param1, string param2, string param3, string param4)
		{
			this.PCGetDataFAQinArrayAsync(param1, param2, param3, param4, null);
		}

		public void PCGetDataFAQinArrayAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this.PCGetDataFAQinArrayOperationCompleted == null)
			{
				this.PCGetDataFAQinArrayOperationCompleted = new SendOrPostCallback(this.OnPCGetDataFAQinArrayOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("PCGetDataFAQinArray", objArray, this.PCGetDataFAQinArrayOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/PCGetDatainArray", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public object[] PCGetDatainArray(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (object[])base.Invoke("PCGetDatainArray", objArray)[0];
		}

		public void PCGetDatainArrayAsync(string param1, string param2, string param3, string param4)
		{
			this.PCGetDatainArrayAsync(param1, param2, param3, param4, null);
		}

		public void PCGetDatainArrayAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this.PCGetDatainArrayOperationCompleted == null)
			{
				this.PCGetDatainArrayOperationCompleted = new SendOrPostCallback(this.OnPCGetDatainArrayOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("PCGetDatainArray", objArray, this.PCGetDatainArrayOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/PCGetDataPayinArray", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public object[] PCGetDataPayinArray(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (object[])base.Invoke("PCGetDataPayinArray", objArray)[0];
		}

		public void PCGetDataPayinArrayAsync(string param1, string param2, string param3, string param4)
		{
			this.PCGetDataPayinArrayAsync(param1, param2, param3, param4, null);
		}

		public void PCGetDataPayinArrayAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this.PCGetDataPayinArrayOperationCompleted == null)
			{
				this.PCGetDataPayinArrayOperationCompleted = new SendOrPostCallback(this.OnPCGetDataPayinArrayOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("PCGetDataPayinArray", objArray, this.PCGetDataPayinArrayOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/PCLogin", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public string PCLogin(string param1, string param2)
		{
			object[] objArray = new object[] { param1, param2 };
			return (string)base.Invoke("PCLogin", objArray)[0];
		}

		public void PCLoginAsync(string param1, string param2)
		{
			this.PCLoginAsync(param1, param2, null);
		}

		public void PCLoginAsync(string param1, string param2, object userState)
		{
			if (this.PCLoginOperationCompleted == null)
			{
				this.PCLoginOperationCompleted = new SendOrPostCallback(this.OnPCLoginOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2 };
			base.InvokeAsync("PCLogin", objArray, this.PCLoginOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/PCLostPWD", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public string PCLostPWD(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (string)base.Invoke("PCLostPWD", objArray)[0];
		}

		public void PCLostPWDAsync(string param1, string param2, string param3, string param4)
		{
			this.PCLostPWDAsync(param1, param2, param3, param4, null);
		}

		public void PCLostPWDAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this.PCLostPWDOperationCompleted == null)
			{
				this.PCLostPWDOperationCompleted = new SendOrPostCallback(this.OnPCLostPWDOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("PCLostPWD", objArray, this.PCLostPWDOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/PCSaveComment", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public string PCSaveComment(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (string)base.Invoke("PCSaveComment", objArray)[0];
		}

		public void PCSaveCommentAsync(string param1, string param2, string param3, string param4)
		{
			this.PCSaveCommentAsync(param1, param2, param3, param4, null);
		}

		public void PCSaveCommentAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this.PCSaveCommentOperationCompleted == null)
			{
				this.PCSaveCommentOperationCompleted = new SendOrPostCallback(this.OnPCSaveCommentOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("PCSaveComment", objArray, this.PCSaveCommentOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/PCSaveInd", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public string PCSaveInd(string param1, string param2, string param3, string param4)
		{
			object[] objArray = new object[] { param1, param2, param3, param4 };
			return (string)base.Invoke("PCSaveInd", objArray)[0];
		}

		public void PCSaveIndAsync(string param1, string param2, string param3, string param4)
		{
			this.PCSaveIndAsync(param1, param2, param3, param4, null);
		}

		public void PCSaveIndAsync(string param1, string param2, string param3, string param4, object userState)
		{
			if (this.PCSaveIndOperationCompleted == null)
			{
				this.PCSaveIndOperationCompleted = new SendOrPostCallback(this.OnPCSaveIndOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4 };
			base.InvokeAsync("PCSaveInd", objArray, this.PCSaveIndOperationCompleted, userState);
		}

		[SoapDocumentMethod("http://tempuri.org/PCSaveRequest", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public string PCSaveRequest(string param1, string param2, string param3, string param4, string param5)
		{
			object[] objArray = new object[] { param1, param2, param3, param4, param5 };
			return (string)base.Invoke("PCSaveRequest", objArray)[0];
		}

		public void PCSaveRequestAsync(string param1, string param2, string param3, string param4, string param5)
		{
			this.PCSaveRequestAsync(param1, param2, param3, param4, param5, null);
		}

		public void PCSaveRequestAsync(string param1, string param2, string param3, string param4, string param5, object userState)
		{
			if (this.PCSaveRequestOperationCompleted == null)
			{
				this.PCSaveRequestOperationCompleted = new SendOrPostCallback(this.OnPCSaveRequestOperationCompleted);
			}
			object[] objArray = new object[] { param1, param2, param3, param4, param5 };
			base.InvokeAsync("PCSaveRequest", objArray, this.PCSaveRequestOperationCompleted, userState);
		}

		public event _ConsumerPayCompletedEventHandler _ConsumerPayCompleted;

		public event _ConsumerSearchCompletedEventHandler _ConsumerSearchCompleted;

		public event _ConsumerSearchLiteCompletedEventHandler _ConsumerSearchLiteCompleted;

		public event _KomLoadCollectionCompletedEventHandler _KomLoadCollectionCompleted;

		public event _PaymentSynchCheckCompletedEventHandler _PaymentSynchCheckCompleted;

		public event _PaymentSynchCompletedEventHandler _PaymentSynchCompleted;

		public event _PortalTimeCompletedEventHandler _PortalTimeCompleted;

		public event _PortalVersionCompletedEventHandler _PortalVersionCompleted;

		public event _ResultExecServerComandCompletedEventHandler _ResultExecServerComandCompleted;

		public event _ServerCommandGetCompletedEventHandler _ServerCommandGetCompleted;

		public event _ServerCommandSaveCompletedEventHandler _ServerCommandSaveCompleted;

		public event _TerminalStatusCompletedEventHandler _TerminalStatusCompleted;

		public event _UpdateFilesCompletedEventHandler _UpdateFilesCompleted;

		public event PCChabgePWDCompletedEventHandler PCChabgePWDCompleted;

		public event PCGetConsamerFactUseinArrayCompletedEventHandler PCGetConsamerFactUseinArrayCompleted;

		public event PCGetConsamerIndicationsinArrayCompletedEventHandler PCGetConsamerIndicationsinArrayCompleted;

		public event PCGetDataCompletedEventHandler PCGetDataCompleted;

		public event PCGetDataFAQinArrayCompletedEventHandler PCGetDataFAQinArrayCompleted;

		public event PCGetDatainArrayCompletedEventHandler PCGetDatainArrayCompleted;

		public event PCGetDataPayinArrayCompletedEventHandler PCGetDataPayinArrayCompleted;

		public event PCLoginCompletedEventHandler PCLoginCompleted;

		public event PCLostPWDCompletedEventHandler PCLostPWDCompleted;

		public event PCSaveCommentCompletedEventHandler PCSaveCommentCompleted;

		public event PCSaveIndCompletedEventHandler PCSaveIndCompleted;

		public event PCSaveRequestCompletedEventHandler PCSaveRequestCompleted;
	}
}