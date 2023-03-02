using PayNavigator.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace PayNavigator.SynchServ
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "4.0.30319.17929")]
	[WebServiceBinding(Name="SynchServSoap", Namespace="http://tempuri.org/")]
	[XmlInclude(typeof(object[]))]
	public class SynchServ : SoapHttpClientProtocol
	{
		private SendOrPostCallback _PortalVersionOperationCompleted;

		private SendOrPostCallback _GetConsamerInfoOperationCompleted;

		private SendOrPostCallback _ResiverPaymentsOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;

		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				if ((!this.IsLocalFileSystemWebService(base.Url) || this.useDefaultCredentialsSetExplicitly ? false : !this.IsLocalFileSystemWebService(value)))
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

		public SynchServ()
		{
			this.Url = Settings.Default.PayNavigator_SynchServ_SynchServ;
			if (!this.IsLocalFileSystemWebService(this.Url))
			{
				this.useDefaultCredentialsSetExplicitly = true;
			}
			else
			{
				this.UseDefaultCredentials = true;
				this.useDefaultCredentialsSetExplicitly = false;
			}
		}

		[SoapDocumentMethod("http://tempuri.org/_GetConsamerInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public object[] _GetConsamerInfo(string LS)
		{
			object[] lS = new object[] { LS };
			return (object[])base.Invoke("_GetConsamerInfo", lS)[0];
		}

		public void _GetConsamerInfoAsync(string LS)
		{
			this._GetConsamerInfoAsync(LS, null);
		}

		public void _GetConsamerInfoAsync(string LS, object userState)
		{
			if (this._GetConsamerInfoOperationCompleted == null)
			{
				this._GetConsamerInfoOperationCompleted = new SendOrPostCallback(this.On_GetConsamerInfoOperationCompleted);
			}
			object[] lS = new object[] { LS };
			base.InvokeAsync("_GetConsamerInfo", lS, this._GetConsamerInfoOperationCompleted, userState);
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

		[SoapDocumentMethod("http://tempuri.org/_ResiverPayments", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public bool _ResiverPayments(object[] Payments)
		{
			object[] payments = new object[] { Payments };
			return (bool)base.Invoke("_ResiverPayments", payments)[0];
		}

		public void _ResiverPaymentsAsync(object[] Payments)
		{
			this._ResiverPaymentsAsync(Payments, null);
		}

		public void _ResiverPaymentsAsync(object[] Payments, object userState)
		{
			if (this._ResiverPaymentsOperationCompleted == null)
			{
				this._ResiverPaymentsOperationCompleted = new SendOrPostCallback(this.On_ResiverPaymentsOperationCompleted);
			}
			object[] payments = new object[] { Payments };
			base.InvokeAsync("_ResiverPayments", payments, this._ResiverPaymentsOperationCompleted, userState);
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			bool flag;
			if ((url == null ? false : !(url == string.Empty)))
			{
				System.Uri uri = new System.Uri(url);
				flag = ((uri.Port < 1024 ? true : string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) != 0) ? false : true);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private void On_GetConsamerInfoOperationCompleted(object arg)
		{
			if (this._GetConsamerInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._GetConsamerInfoCompleted(this, new _GetConsamerInfoCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
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

		private void On_ResiverPaymentsOperationCompleted(object arg)
		{
			if (this._ResiverPaymentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this._ResiverPaymentsCompleted(this, new _ResiverPaymentsCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		public event _GetConsamerInfoCompletedEventHandler _GetConsamerInfoCompleted;

		public event _PortalVersionCompletedEventHandler _PortalVersionCompleted;

		public event _ResiverPaymentsCompletedEventHandler _ResiverPaymentsCompleted;
	}
}