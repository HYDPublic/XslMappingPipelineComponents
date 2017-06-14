using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.XPath;
using System.Xml;
using Microsoft.BizTalk.Streaming;
using System.Xml.Xsl;

namespace BizTalk.ESB.Components.Mapping
{
    public class XslTransmitHelper
    {
        public XslTransmitHelper(string xslpath,string sourcefilenamexpath,bool enablevaildate,bool allowpassthru)
        {
            this._AllowPassThruTransmit = allowpassthru;
            this._EnableValidateNamespace = enablevaildate;
            this._XPathSourceFileName = sourcefilenamexpath;
            this._XslPath = xslpath;

        }
        #region properties
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
        #endregion
        public Stream Transmit(Stream inStream) {
            XmlReader xmlreader = XmlReader.Create(inStream);
            XmlReader xslReader = XmlReader.Create(this.XslPath);

            if (this.EnableValidateNamespace) {
                var sourceNamepsaces = GetNamespaces(xmlreader);
                var xslNamesapces = GetNamespaces(xslReader);
                foreach (var snamesapce in sourceNamepsaces.Values) {
                    if (!xslNamesapces.Values.Contains(snamesapce)) {

                        if (!this.AllowPassThruTransmit)
                        {

                            throw new Exception("Xsl mapping找不到匹配的原XmlDocument{" + snamesapce + "}");

                        }
                        else {
                            inStream.Seek(0, SeekOrigin.Begin);
                            return inStream;
                        }


                    }
                }
            }

            XslCompiledTransform xsl = new XslCompiledTransform();
            XsltSettings setting = new XsltSettings(true, true);
            setting.EnableDocumentFunction = true;
            setting.EnableScript = true;
          
            var outStream = new MemoryStream();
            xslReader.MoveToFirstAttribute();
            xsl.Load(XmlReader.Create(this.XslPath), setting, null);
            inStream.Seek(0, SeekOrigin.Begin);
            xsl.Transform(XmlReader.Create(inStream), null, outStream);
            outStream.Seek(0, SeekOrigin.Begin);
            return outStream;


        }

        public class StringCompareItemComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                var result = x == y;
                return result;
            }

            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }

        public string GetSourceFileName(XmlReader xmlreader, string xpath) {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlreader);
                return doc.SelectSingleNode(xpath) != null ? doc.SelectSingleNode(xpath).InnerText : "";
       

        }
        public IDictionary<string, string> GetNamespaces(XmlReader xmlreader)
        {
            XPathDocument x = new XPathDocument(xmlreader);
            XPathNavigator foo = x.CreateNavigator();
            foo.MoveToFollowing(XPathNodeType.Element);
            IDictionary<string, string> whatever = foo.GetNamespacesInScope(XmlNamespaceScope.All);
            return whatever;
        }
         
    }
}
