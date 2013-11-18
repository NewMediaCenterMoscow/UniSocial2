﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18331
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.UniSocialService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CollectTask", Namespace="http://schemas.datacontract.org/2004/07/Worker.Model")]
    [System.SerializableAttribute()]
    public partial class CollectTask : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long AllItemsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CollectTaskIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long CounterItemsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Web.UniSocialService.CollectTaskIO InputField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsCompletedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MethodField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Web.UniSocialService.CollectTaskIO OutputField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SocialNetworkField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long AllItems {
            get {
                return this.AllItemsField;
            }
            set {
                if ((this.AllItemsField.Equals(value) != true)) {
                    this.AllItemsField = value;
                    this.RaisePropertyChanged("AllItems");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CollectTaskId {
            get {
                return this.CollectTaskIdField;
            }
            set {
                if ((this.CollectTaskIdField.Equals(value) != true)) {
                    this.CollectTaskIdField = value;
                    this.RaisePropertyChanged("CollectTaskId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long CounterItems {
            get {
                return this.CounterItemsField;
            }
            set {
                if ((this.CounterItemsField.Equals(value) != true)) {
                    this.CounterItemsField = value;
                    this.RaisePropertyChanged("CounterItems");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Web.UniSocialService.CollectTaskIO Input {
            get {
                return this.InputField;
            }
            set {
                if ((object.ReferenceEquals(this.InputField, value) != true)) {
                    this.InputField = value;
                    this.RaisePropertyChanged("Input");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsCompleted {
            get {
                return this.IsCompletedField;
            }
            set {
                if ((this.IsCompletedField.Equals(value) != true)) {
                    this.IsCompletedField = value;
                    this.RaisePropertyChanged("IsCompleted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Method {
            get {
                return this.MethodField;
            }
            set {
                if ((object.ReferenceEquals(this.MethodField, value) != true)) {
                    this.MethodField = value;
                    this.RaisePropertyChanged("Method");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Web.UniSocialService.CollectTaskIO Output {
            get {
                return this.OutputField;
            }
            set {
                if ((object.ReferenceEquals(this.OutputField, value) != true)) {
                    this.OutputField = value;
                    this.RaisePropertyChanged("Output");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SocialNetwork {
            get {
                return this.SocialNetworkField;
            }
            set {
                if ((object.ReferenceEquals(this.SocialNetworkField, value) != true)) {
                    this.SocialNetworkField = value;
                    this.RaisePropertyChanged("SocialNetwork");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CollectTaskIO", Namespace="http://schemas.datacontract.org/2004/07/Worker.Model")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Web.UniSocialService.CollectTaskIODatabase))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Web.UniSocialService.CollectTaskIOFile))]
    public partial class CollectTaskIO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CollectTaskIODatabase", Namespace="http://schemas.datacontract.org/2004/07/Worker.Model")]
    [System.SerializableAttribute()]
    public partial class CollectTaskIODatabase : Web.UniSocialService.CollectTaskIO {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ConnectionStringField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ConnectionString {
            get {
                return this.ConnectionStringField;
            }
            set {
                if ((object.ReferenceEquals(this.ConnectionStringField, value) != true)) {
                    this.ConnectionStringField = value;
                    this.RaisePropertyChanged("ConnectionString");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CollectTaskIOFile", Namespace="http://schemas.datacontract.org/2004/07/Worker.Model")]
    [System.SerializableAttribute()]
    public partial class CollectTaskIOFile : Web.UniSocialService.CollectTaskIO {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FilenameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Filename {
            get {
                return this.FilenameField;
            }
            set {
                if ((object.ReferenceEquals(this.FilenameField, value) != true)) {
                    this.FilenameField = value;
                    this.RaisePropertyChanged("Filename");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UniSocialService.IUniSocial")]
    public interface IUniSocial {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUniSocial/StartNewTask")]
        void StartNewTask(Web.UniSocialService.CollectTask CollectTask);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUniSocial/StartNewTask")]
        System.Threading.Tasks.Task StartNewTaskAsync(Web.UniSocialService.CollectTask CollectTask);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUniSocial/GetTasks", ReplyAction="http://tempuri.org/IUniSocial/GetTasksResponse")]
        System.Collections.Generic.List<Web.UniSocialService.CollectTask> GetTasks();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUniSocial/GetTasks", ReplyAction="http://tempuri.org/IUniSocial/GetTasksResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Web.UniSocialService.CollectTask>> GetTasksAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUniSocial/RemoveTaskFromList")]
        void RemoveTaskFromList(int CollectTaskId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUniSocial/RemoveTaskFromList")]
        System.Threading.Tasks.Task RemoveTaskFromListAsync(int CollectTaskId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUniSocial/CancelTask")]
        void CancelTask(int CollectTaskId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUniSocial/CancelTask")]
        System.Threading.Tasks.Task CancelTaskAsync(int CollectTaskId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUniSocialChannel : Web.UniSocialService.IUniSocial, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UniSocialClient : System.ServiceModel.ClientBase<Web.UniSocialService.IUniSocial>, Web.UniSocialService.IUniSocial {
        
        public UniSocialClient() {
        }
        
        public UniSocialClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UniSocialClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UniSocialClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UniSocialClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void StartNewTask(Web.UniSocialService.CollectTask CollectTask) {
            base.Channel.StartNewTask(CollectTask);
        }
        
        public System.Threading.Tasks.Task StartNewTaskAsync(Web.UniSocialService.CollectTask CollectTask) {
            return base.Channel.StartNewTaskAsync(CollectTask);
        }
        
        public System.Collections.Generic.List<Web.UniSocialService.CollectTask> GetTasks() {
            return base.Channel.GetTasks();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Web.UniSocialService.CollectTask>> GetTasksAsync() {
            return base.Channel.GetTasksAsync();
        }
        
        public void RemoveTaskFromList(int CollectTaskId) {
            base.Channel.RemoveTaskFromList(CollectTaskId);
        }
        
        public System.Threading.Tasks.Task RemoveTaskFromListAsync(int CollectTaskId) {
            return base.Channel.RemoveTaskFromListAsync(CollectTaskId);
        }
        
        public void CancelTask(int CollectTaskId) {
            base.Channel.CancelTask(CollectTaskId);
        }
        
        public System.Threading.Tasks.Task CancelTaskAsync(int CollectTaskId) {
            return base.Channel.CancelTaskAsync(CollectTaskId);
        }
    }
}
