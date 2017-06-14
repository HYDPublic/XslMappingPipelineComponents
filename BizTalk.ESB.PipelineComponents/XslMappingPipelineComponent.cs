namespace BizTalk.ESB.PipelineComponents
{
    using System;
    using System.IO;
    using System.Text;
    using System.Drawing;
    using System.Resources;
    using System.Reflection;
    using System.Diagnostics;
    using System.Collections;
    using System.ComponentModel;
    using Microsoft.BizTalk.Message.Interop;
    using Microsoft.BizTalk.Component.Interop;
    using Microsoft.BizTalk.Component;
    using Microsoft.BizTalk.Messaging;
    using BizTalk.ESB.Components.Mapping;
    using System.Xml;
    
    
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("4b31117c-4e08-403d-82f5-372501d414a1")]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    public class XslMappingPipelineComponent : Microsoft.BizTalk.Component.Interop.IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        
        private System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("BizTalk.ESB.PipelineComponents.XslMappingPipelineComponent", Assembly.GetExecutingAssembly());
        
        private string _XPathSourceFileName;
        
        public string XPathSourceFileName
        {
            get
            {
                return _XPathSourceFileName;
            }
            set
            {
                _XPathSourceFileName = value;
            }
        }
        
        private string _XslPath;
        
        public string XslPath
        {
            get
            {
                return _XslPath;
            }
            set
            {
                _XslPath = value;
            }
        }
        
        private bool _EnableValidateNamespace;
        
        public bool EnableValidateNamespace
        {
            get
            {
                return _EnableValidateNamespace;
            }
            set
            {
                _EnableValidateNamespace = value;
            }
        }
        
        private bool _AllowPassThruTransmit;
        
        public bool AllowPassThruTransmit
        {
            get
            {
                return _AllowPassThruTransmit;
            }
            set
            {
                _AllowPassThruTransmit = value;
            }
        }
        
        #region IBaseComponent members
        /// <summary>
        /// Name of the component
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get
            {
                return resourceManager.GetString("COMPONENTNAME", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        
        /// <summary>
        /// Version of the component
        /// </summary>
        [Browsable(false)]
        public string Version
        {
            get
            {
                return resourceManager.GetString("COMPONENTVERSION", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        
        /// <summary>
        /// Description of the component
        /// </summary>
        [Browsable(false)]
        public string Description
        {
            get
            {
                return resourceManager.GetString("COMPONENTDESCRIPTION", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        #endregion
        
        #region IPersistPropertyBag members
        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classid">
        /// Class ID of the component
        /// </param>
        public void GetClassID(out System.Guid classid)
        {
            classid = new System.Guid("4b31117c-4e08-403d-82f5-372501d414a1");
        }
        
        /// <summary>
        /// not implemented
        /// </summary>
        public void InitNew()
        {
        }
        
        /// <summary>
        /// Loads configuration properties for the component
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="errlog">Error status</param>
        public virtual void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, int errlog)
        {
            object val = null;
            val = this.ReadPropertyBag(pb, "XPathSourceFileName");
            if ((val != null))
            {
                this._XPathSourceFileName = ((string)(val));
            }
            val = this.ReadPropertyBag(pb, "XslPath");
            if ((val != null))
            {
                this._XslPath = ((string)(val));
            }
            val = this.ReadPropertyBag(pb, "EnableValidateNamespace");
            if ((val != null))
            {
                this._EnableValidateNamespace = ((bool)(val));
            }
            val = this.ReadPropertyBag(pb, "AllowPassThruTransmit");
            if ((val != null))
            {
                this._AllowPassThruTransmit = ((bool)(val));
            }
        }
        
        /// <summary>
        /// Saves the current component configuration into the property bag
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="fClearDirty">not used</param>
        /// <param name="fSaveAllProperties">not used</param>
        public virtual void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, bool fClearDirty, bool fSaveAllProperties)
        {
            this.WritePropertyBag(pb, "XPathSourceFileName", this.XPathSourceFileName);
            this.WritePropertyBag(pb, "XslPath", this.XslPath);
            this.WritePropertyBag(pb, "EnableValidateNamespace", this.EnableValidateNamespace);
            this.WritePropertyBag(pb, "AllowPassThruTransmit", this.AllowPassThruTransmit);
        }
        
        #region utility functionality
        /// <summary>
        /// Reads property value from property bag
        /// </summary>
        /// <param name="pb">Property bag</param>
        /// <param name="propName">Name of property</param>
        /// <returns>Value of the property</returns>
        private object ReadPropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName)
        {
            object val = null;
            try
            {
                pb.Read(propName, out val, 0);
            }
            catch (System.ArgumentException )
            {
                return val;
            }
            catch (System.Exception e)
            {
                throw new System.ApplicationException(e.Message);
            }
            return val;
        }
        
        /// <summary>
        /// Writes property values into a property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <param name="val">Value of property.</param>
        private void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName, object val)
        {
            try
            {
                pb.Write(propName, ref val);
            }
            catch (System.Exception e)
            {
                throw new System.ApplicationException(e.Message);
            }
        }
        #endregion
        #endregion
        
        #region IComponentUI members
        /// <summary>
        /// Component icon to use in BizTalk Editor
        /// </summary>
        [Browsable(false)]
        public IntPtr Icon
        {
            get
            {
                return ((System.Drawing.Bitmap)(this.resourceManager.GetObject("COMPONENTICON", System.Globalization.CultureInfo.InvariantCulture))).GetHicon();
            }
        }
        
        /// <summary>
        /// The Validate method is called by the BizTalk Editor during the build 
        /// of a BizTalk project.
        /// </summary>
        /// <param name="obj">An Object containing the configuration properties.</param>
        /// <returns>The IEnumerator enables the caller to enumerate through a collection of strings containing error messages. These error messages appear as compiler error messages. To report successful property validation, the method should return an empty enumerator.</returns>
        public System.Collections.IEnumerator Validate(object obj)
        {
            // example implementation:
            // ArrayList errorList = new ArrayList();
            // errorList.Add("This is a compiler error");
            // return errorList.GetEnumerator();
            return null;
        }
        #endregion
        
        #region IComponent members
        /// <summary>
        /// Implements IComponent.Execute method.
        /// </summary>
        /// <param name="pc">Pipeline context</param>
        /// <param name="inmsg">Input message</param>
        /// <returns>Original input message</returns>
        /// <remarks>
        /// IComponent.Execute method is used to initiate
        /// the processing of the message in this pipeline component.
        /// </remarks>
        public Microsoft.BizTalk.Message.Interop.IBaseMessage Execute(Microsoft.BizTalk.Component.Interop.IPipelineContext pc, Microsoft.BizTalk.Message.Interop.IBaseMessage inmsg)
        {
            // 
            // TODO: implement component logic
            // 
            // this way, it's a passthrough pipeline component
            IBaseMessageContext context = inmsg.Context;
            Stream inStream = inmsg.BodyPart.Data;
            XslTransmitHelper transmit = new XslTransmitHelper(this.XslPath, this.XPathSourceFileName, this.EnableValidateNamespace, this.AllowPassThruTransmit);

            if (!string.IsNullOrEmpty(this.XPathSourceFileName)) {
                string sourceFileName = transmit.GetSourceFileName(XmlReader.Create(inStream), this.XPathSourceFileName);
                if (!string.IsNullOrEmpty(sourceFileName)) {
                    context.Write("ReceivedFileName", "http://schemas.microsoft.com/BizTalk/2003/file-properties", sourceFileName);
                    context.Promote("ReceivedFileName", "http://schemas.microsoft.com/BizTalk/2003/file-properties", sourceFileName);
                }
                inStream.Seek(0, SeekOrigin.Begin);
            }
            var outStream = transmit.Transmit(inStream);

            inmsg.BodyPart.Data = outStream;
            pc.ResourceTracker.AddResource(outStream);



            return inmsg;
        }
        #endregion
    }
}
