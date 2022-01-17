using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace GlikemiaApp
{
    class XmlGlikemiaHandler:IXmlHandler
    {
        private string path;

        public XmlGlikemiaHandler()
        {
            path = @"XML\Pomiary.xml";
        }

        public XElement SerializeObject(object o)
        {
            return new XElement("Pomiar");
        }
        public object DeserializeObject(int id)
        {
            return new object();
        }
    }
}
