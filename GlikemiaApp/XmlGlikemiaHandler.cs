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
            XDocument document = new XDocument();
            document = XDocument.Load(path);
            PomiaryGlikemi pomiarObject = new PomiaryGlikemi();
            foreach(XElement pomiar in document.Descendants("Pomiar"))
            {
                if(pomiar.Attribute("id").Value == id.ToString())
                {
                    int.TryParse(pomiar.Attribute("id").Value, out pomiarObject.id);
                    int.TryParse(pomiar.Element("Cukier").Value, out pomiarObject.cukier);
                    pomiarObject.opis = pomiar.Element("Opis").Value;
                    int.TryParse(pomiar.Element("DodatkoweJI").Value, out pomiarObject.dodatkoweJI);
                    pomiarObject.Set_Date(pomiar.Element("Data").Value);

                    return pomiarObject;
                }
            }

            return new PomiaryGlikemi();
        }
    }
}
