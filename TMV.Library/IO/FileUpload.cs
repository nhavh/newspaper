using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using TMV.Utilities;

namespace TMV.IO
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "FileUploadSoap", Namespace = "http://static.tmvhoanganh.com")]
    public partial class FileUpload : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback WSStartNewFileOperationCompleted;

        private System.Threading.SendOrPostCallback WSWriteFileOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public FileUpload()
        {
            this.Url = Globals.WebServiceFile;
            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public new string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                            && (this.useDefaultCredentialsSetExplicitly == false))
                            && (this.IsLocalFileSystemWebService(value) == false)))
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

        /// <remarks/>
        public event WSStartNewFileCompletedEventHandler WSStartNewFileCompleted;

        /// <remarks/>
        public event WSWriteFileCompletedEventHandler WSWriteFileCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://static.tmvhoanganh.com/WSStartNewFile", RequestNamespace = "http://static.tmvhoanganh.com", ResponseNamespace = "http://static.tmvhoanganh.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string WSStartNewFile(string fileName, string _outputPath)
        {
            object[] results = this.Invoke("WSStartNewFile", new object[] {
                        fileName,
                        _outputPath});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void WSStartNewFileAsync(string fileName, string _outputPath)
        {
            this.WSStartNewFileAsync(fileName, _outputPath, null);
        }

        /// <remarks/>
        public void WSStartNewFileAsync(string fileName, string _outputPath, object userState)
        {
            if ((this.WSStartNewFileOperationCompleted == null))
            {
                this.WSStartNewFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnWSStartNewFileOperationCompleted);
            }
            this.InvokeAsync("WSStartNewFile", new object[] {
                        fileName,
                        _outputPath}, this.WSStartNewFileOperationCompleted, userState);
        }

        private void OnWSStartNewFileOperationCompleted(object arg)
        {
            if ((this.WSStartNewFileCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.WSStartNewFileCompleted(this, new WSStartNewFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://static.tmvhoanganh.com/WSWriteFile", RequestNamespace = "http://static.tmvhoanganh.com", ResponseNamespace = "http://static.tmvhoanganh.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool WSWriteFile([System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] buffer, string fullname)
        {
            object[] results = this.Invoke("WSWriteFile", new object[] {
                        buffer,
                        fullname});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void WSWriteFileAsync(byte[] buffer, string fullname)
        {
            this.WSWriteFileAsync(buffer, fullname, null);
        }

        /// <remarks/>
        public void WSWriteFileAsync(byte[] buffer, string fullname, object userState)
        {
            if ((this.WSWriteFileOperationCompleted == null))
            {
                this.WSWriteFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnWSWriteFileOperationCompleted);
            }
            this.InvokeAsync("WSWriteFile", new object[] {
                        buffer,
                        fullname}, this.WSWriteFileOperationCompleted, userState);
        }

        private void OnWSWriteFileOperationCompleted(object arg)
        {
            if ((this.WSWriteFileCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.WSWriteFileCompleted(this, new WSWriteFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if (((url == null)
                        || (url == string.Empty)))
            {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void WSStartNewFileCompletedEventHandler(object sender, WSStartNewFileCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class WSStartNewFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal WSStartNewFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void WSWriteFileCompletedEventHandler(object sender, WSWriteFileCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class WSWriteFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal WSWriteFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}