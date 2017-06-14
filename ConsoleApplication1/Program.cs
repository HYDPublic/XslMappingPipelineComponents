using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using BizTalk.ESB.Components.Mapping;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var stream = new FileStream(@"C:\Users\Administrator\AppData\Local\Temp\2\_SchemaData\WNCB2B.CALLOFF_output.xml", FileMode.OpenOrCreate);
            var h = new XslTransmitHelper(@"D:\B2BPlatForm\XSL\WNC\CALLOFF.xsl","",true,true);
            
            var stream1 = h.Transmit(stream);

            var buff = new byte[stream1.Length];
            stream1.Read(buff, 0, buff.Length);
            FileStream fs = new FileStream("d:\\ddx.xml", FileMode.Create);
            fs.Write(buff,0,buff.Length);
            fs.Close();


            GetNamespace();
        }

        static void GetNamespace() {
            //XmlDocument doc = new XmlDocument();
            //doc.Load(@"D:\B2BPlatForm\XSL\Compal_IQC\CompalIQC_GR_OUT.xsl");
            XPathDocument x = new XPathDocument(@"D:\a.xml");
            XPathNavigator foo = x.CreateNavigator();
            foo.MoveToFollowing(XPathNodeType.Element);
            IDictionary<string, string> whatever = foo.GetNamespacesInScope(XmlNamespaceScope.All);
            foreach (var item in whatever) { 
            
            }
        }
    }
}
