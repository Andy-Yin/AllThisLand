//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//
//     对此文件的更改可能导致不正确的行为，并在以下条件下丢失:
//     代码重新生成。
// </auto-generated>
//------------------------------------------------------------------------------

namespace U9Service
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ReOrder", Namespace="http://schemas.datacontract.org/2004/07/LWS.JFAPP")]
    public partial class ReOrder : object
    {
        
        private string DisReasonField;
        
        private string PersonField;
        
        private string SendStateField;
        
        private string SheedIdField;
        
        private int StepField;
        
        private int TypeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DisReason
        {
            get
            {
                return this.DisReasonField;
            }
            set
            {
                this.DisReasonField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person
        {
            get
            {
                return this.PersonField;
            }
            set
            {
                this.PersonField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SendState
        {
            get
            {
                return this.SendStateField;
            }
            set
            {
                this.SendStateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SheedId
        {
            get
            {
                return this.SheedIdField;
            }
            set
            {
                this.SheedIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Step
        {
            get
            {
                return this.StepField;
            }
            set
            {
                this.StepField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Type
        {
            get
            {
                return this.TypeField;
            }
            set
            {
                this.TypeField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MaInfo", Namespace="http://schemas.datacontract.org/2004/07/LWS.JFAPP")]
    public partial class MaInfo : object
    {
        
        private string DetailsIDField;
        
        private string MeasureCountField;
        
        private string MeasureInfoField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DetailsID
        {
            get
            {
                return this.DetailsIDField;
            }
            set
            {
                this.DetailsIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MeasureCount
        {
            get
            {
                return this.MeasureCountField;
            }
            set
            {
                this.MeasureCountField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MeasureInfo
        {
            get
            {
                return this.MeasureInfoField;
            }
            set
            {
                this.MeasureInfoField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="U9Service.IJF")]
    public interface IJF
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/GetJFCategory", ReplyAction="http://tempuri.org/IJF/GetJFCategoryResponse")]
        System.Threading.Tasks.Task<string> GetJFCategoryAsync(string Source, string TimeSign, string key, string org, string[] code, string parentCode, string level);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/GetJFItemMaster", ReplyAction="http://tempuri.org/IJF/GetJFItemMasterResponse")]
        System.Threading.Tasks.Task<string> GetJFItemMasterAsync(string Source, string TimeSign, string key, string org, string NameCode, string TopCode, int page, int limit);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/GetProject", ReplyAction="http://tempuri.org/IJF/GetProjectResponse")]
        System.Threading.Tasks.Task<string> GetProjectAsync(string Source, string TimeSign, string key, string SheetID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/GetProjectConfirmInfoByNo", ReplyAction="http://tempuri.org/IJF/GetProjectConfirmInfoByNoResponse")]
        System.Threading.Tasks.Task<string> GetProjectConfirmInfoByNoAsync(string Source, string TimeSign, string key, string SheetID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/JFSendProStatus", ReplyAction="http://tempuri.org/IJF/JFSendProStatusResponse")]
        System.Threading.Tasks.Task<string> JFSendProStatusAsync(string Source, string TimeSign, string key, U9Service.ReOrder Orinfor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/GetMateriel", ReplyAction="http://tempuri.org/IJF/GetMaterielResponse")]
        System.Threading.Tasks.Task<string> GetMaterielAsync(string Source, string TimeSign, string key, string SheetID, string[] ClassCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/JFSendProMeasureStatus", ReplyAction="http://tempuri.org/IJF/JFSendProMeasureStatusResponse")]
        System.Threading.Tasks.Task<string> JFSendProMeasureStatusAsync(string Source, string TimeSign, string key, U9Service.MaInfo[] MaDetails);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/SetPlaceOrder2", ReplyAction="http://tempuri.org/IJF/SetPlaceOrder2Response")]
        System.Threading.Tasks.Task<string> SetPlaceOrder2Async(string Source, string TimeSign, string key, string[] DetailID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/GetJFAPStatus", ReplyAction="http://tempuri.org/IJF/GetJFAPStatusResponse")]
        System.Threading.Tasks.Task<string> GetJFAPStatusAsync(string Source, string TimeSign, string key, string ProjectCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/SetMaterialApply2", ReplyAction="http://tempuri.org/IJF/SetMaterialApply2Response")]
        System.Threading.Tasks.Task<string> SetMaterialApply2Async(string Source, string TimeSign, string key, string[] DetailID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/SetConfirmationGoods", ReplyAction="http://tempuri.org/IJF/SetConfirmationGoodsResponse")]
        System.Threading.Tasks.Task<string> SetConfirmationGoodsAsync(string Source, string TimeSign, string key, string[] DetailsIDList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/SetInstallGoods", ReplyAction="http://tempuri.org/IJF/SetInstallGoodsResponse")]
        System.Threading.Tasks.Task<string> SetInstallGoodsAsync(string Source, string TimeSign, string key, string[] DetailsIDList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJF/GetAccessoriese", ReplyAction="http://tempuri.org/IJF/GetAccessorieseResponse")]
        System.Threading.Tasks.Task<string> GetAccessorieseAsync(string Source, string TimeSign, string key, string OrgId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface IJFChannel : U9Service.IJF, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class JFClient : System.ServiceModel.ClientBase<U9Service.IJF>, U9Service.IJF
    {
        
        /// <summary>
        /// 实现此分部方法，配置服务终结点。
        /// </summary>
        /// <param name="serviceEndpoint">要配置的终结点</param>
        /// <param name="clientCredentials">客户端凭据</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public JFClient() : 
                base(JFClient.GetDefaultBinding(), JFClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IJF.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public JFClient(EndpointConfiguration endpointConfiguration) : 
                base(JFClient.GetBindingForEndpoint(endpointConfiguration), JFClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public JFClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(JFClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public JFClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(JFClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public JFClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> GetJFCategoryAsync(string Source, string TimeSign, string key, string org, string[] code, string parentCode, string level)
        {
            return base.Channel.GetJFCategoryAsync(Source, TimeSign, key, org, code, parentCode, level);
        }
        
        public System.Threading.Tasks.Task<string> GetJFItemMasterAsync(string Source, string TimeSign, string key, string org, string NameCode, string TopCode, int page, int limit)
        {
            return base.Channel.GetJFItemMasterAsync(Source, TimeSign, key, org, NameCode, TopCode, page, limit);
        }
        
        public System.Threading.Tasks.Task<string> GetProjectAsync(string Source, string TimeSign, string key, string SheetID)
        {
            return base.Channel.GetProjectAsync(Source, TimeSign, key, SheetID);
        }
        
        public System.Threading.Tasks.Task<string> GetProjectConfirmInfoByNoAsync(string Source, string TimeSign, string key, string SheetID)
        {
            return base.Channel.GetProjectConfirmInfoByNoAsync(Source, TimeSign, key, SheetID);
        }
        
        public System.Threading.Tasks.Task<string> JFSendProStatusAsync(string Source, string TimeSign, string key, U9Service.ReOrder Orinfor)
        {
            return base.Channel.JFSendProStatusAsync(Source, TimeSign, key, Orinfor);
        }
        
        public System.Threading.Tasks.Task<string> GetMaterielAsync(string Source, string TimeSign, string key, string SheetID, string[] ClassCode)
        {
            return base.Channel.GetMaterielAsync(Source, TimeSign, key, SheetID, ClassCode);
        }
        
        public System.Threading.Tasks.Task<string> JFSendProMeasureStatusAsync(string Source, string TimeSign, string key, U9Service.MaInfo[] MaDetails)
        {
            return base.Channel.JFSendProMeasureStatusAsync(Source, TimeSign, key, MaDetails);
        }
        
        public System.Threading.Tasks.Task<string> SetPlaceOrder2Async(string Source, string TimeSign, string key, string[] DetailID)
        {
            return base.Channel.SetPlaceOrder2Async(Source, TimeSign, key, DetailID);
        }
        
        public System.Threading.Tasks.Task<string> GetJFAPStatusAsync(string Source, string TimeSign, string key, string ProjectCode)
        {
            return base.Channel.GetJFAPStatusAsync(Source, TimeSign, key, ProjectCode);
        }
        
        public System.Threading.Tasks.Task<string> SetMaterialApply2Async(string Source, string TimeSign, string key, string[] DetailID)
        {
            return base.Channel.SetMaterialApply2Async(Source, TimeSign, key, DetailID);
        }
        
        public System.Threading.Tasks.Task<string> SetConfirmationGoodsAsync(string Source, string TimeSign, string key, string[] DetailsIDList)
        {
            return base.Channel.SetConfirmationGoodsAsync(Source, TimeSign, key, DetailsIDList);
        }
        
        public System.Threading.Tasks.Task<string> SetInstallGoodsAsync(string Source, string TimeSign, string key, string[] DetailsIDList)
        {
            return base.Channel.SetInstallGoodsAsync(Source, TimeSign, key, DetailsIDList);
        }
        
        public System.Threading.Tasks.Task<string> GetAccessorieseAsync(string Source, string TimeSign, string key, string OrgId)
        {
            return base.Channel.GetAccessorieseAsync(Source, TimeSign, key, OrgId);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IJF))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IJF))
            {
                return new System.ServiceModel.EndpointAddress("http://lws_test.lohas-deco.com/JFAPP/JF.svc");
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return JFClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IJF);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return JFClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IJF);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IJF,
        }
    }
}
