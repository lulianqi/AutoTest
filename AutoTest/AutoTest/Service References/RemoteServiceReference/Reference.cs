﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoTest.RemoteServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RemoteRunnerInfo", Namespace="http://schemas.datacontract.org/2004/07/RemoteService.MyService")]
    [System.SerializableAttribute()]
    public partial class RemoteRunnerInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private AutoTest.RemoteServiceReference.RunnerState[] RunnerStateListField;
        
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
        public AutoTest.RemoteServiceReference.RunnerState[] RunnerStateList {
            get {
                return this.RunnerStateListField;
            }
            set {
                if ((object.ReferenceEquals(this.RunnerStateListField, value) != true)) {
                    this.RunnerStateListField = value;
                    this.RaisePropertyChanged("RunnerStateList");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="RunnerState", Namespace="http://schemas.datacontract.org/2004/07/RemoteService.MyService")]
    [System.SerializableAttribute()]
    public partial class RunnerState : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CellResultField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NowCellField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RunDetailsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RunnerIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RunnerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.KeyValuePair<int, int>[] RunnerProgressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TimeField;
        
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
        public string CellResult {
            get {
                return this.CellResultField;
            }
            set {
                if ((object.ReferenceEquals(this.CellResultField, value) != true)) {
                    this.CellResultField = value;
                    this.RaisePropertyChanged("CellResult");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NowCell {
            get {
                return this.NowCellField;
            }
            set {
                if ((object.ReferenceEquals(this.NowCellField, value) != true)) {
                    this.NowCellField = value;
                    this.RaisePropertyChanged("NowCell");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RunDetails {
            get {
                return this.RunDetailsField;
            }
            set {
                if ((object.ReferenceEquals(this.RunDetailsField, value) != true)) {
                    this.RunDetailsField = value;
                    this.RaisePropertyChanged("RunDetails");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RunnerID {
            get {
                return this.RunnerIDField;
            }
            set {
                if ((this.RunnerIDField.Equals(value) != true)) {
                    this.RunnerIDField = value;
                    this.RaisePropertyChanged("RunnerID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RunnerName {
            get {
                return this.RunnerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.RunnerNameField, value) != true)) {
                    this.RunnerNameField = value;
                    this.RaisePropertyChanged("RunnerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.KeyValuePair<int, int>[] RunnerProgress {
            get {
                return this.RunnerProgressField;
            }
            set {
                if ((object.ReferenceEquals(this.RunnerProgressField, value) != true)) {
                    this.RunnerProgressField = value;
                    this.RaisePropertyChanged("RunnerProgress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string State {
            get {
                return this.StateField;
            }
            set {
                if ((object.ReferenceEquals(this.StateField, value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Time {
            get {
                return this.TimeField;
            }
            set {
                if ((object.ReferenceEquals(this.TimeField, value) != true)) {
                    this.TimeField = value;
                    this.RaisePropertyChanged("Time");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RemoteServiceReference.IExecuteService", CallbackContract=typeof(AutoTest.RemoteServiceReference.IExecuteServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IExecuteService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IExecuteService/ExecuteServiceBeat")]
        void ExecuteServiceBeat();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExecuteService/GetAllRunnerSate", ReplyAction="http://tempuri.org/IExecuteService/GetAllRunnerSateResponse")]
        AutoTest.RemoteServiceReference.RemoteRunnerInfo GetAllRunnerSate();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExecuteService/StartRunner", ReplyAction="http://tempuri.org/IExecuteService/StartRunnerResponse")]
        void StartRunner(int[] runnerList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExecuteService/PauseRunner", ReplyAction="http://tempuri.org/IExecuteService/PauseRunnerResponse")]
        void PauseRunner(int[] runnerList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExecuteService/StopRunner", ReplyAction="http://tempuri.org/IExecuteService/StopRunnerResponse")]
        void StopRunner(int[] runnerList);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IExecuteServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IExecuteService/ReportState")]
        void ReportState(AutoTest.RemoteServiceReference.RemoteRunnerInfo remoteRunnerInfo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IExecuteServiceChannel : AutoTest.RemoteServiceReference.IExecuteService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ExecuteServiceClient : System.ServiceModel.DuplexClientBase<AutoTest.RemoteServiceReference.IExecuteService>, AutoTest.RemoteServiceReference.IExecuteService {
        
        public ExecuteServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ExecuteServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ExecuteServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ExecuteServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ExecuteServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void ExecuteServiceBeat() {
            base.Channel.ExecuteServiceBeat();
        }
        
        public AutoTest.RemoteServiceReference.RemoteRunnerInfo GetAllRunnerSate() {
            return base.Channel.GetAllRunnerSate();
        }
        
        public void StartRunner(int[] runnerList) {
            base.Channel.StartRunner(runnerList);
        }
        
        public void PauseRunner(int[] runnerList) {
            base.Channel.PauseRunner(runnerList);
        }
        
        public void StopRunner(int[] runnerList) {
            base.Channel.StopRunner(runnerList);
        }
    }
}
