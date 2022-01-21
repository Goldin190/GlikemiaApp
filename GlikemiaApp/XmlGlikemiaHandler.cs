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
            PomiaryGlikemi pomiar = (PomiaryGlikemi)o;
            /* Przykład reprezentacji XML
             * <Pomiar id="1">
             *      <Data>01.01.1999</Data>
             *      <Cukier>100</Cukier>
             *      <Opis>opis</Opis>
             *      <DodatkoweJI>0</DodatkoweJI>
             * </Pomiar>
            */
            XElement nowyPomiar = new XElement("Pomiar",new XAttribute("id",DeserializeLastObject().id),
                new XElement("Data", pomiar.Get_Date()),
                new XElement("Cukier",pomiar.cukier),
                new XElement("Opis",pomiar.opis),
                new XElement("DodatkoweJI",pomiar.dodatkoweJI));
            return nowyPomiar;
        }
        public void DodajPomiar(XElement pomiar)
        {
            XDocument document = new XDocument();
            document = XDocument.Load(path);
            document.Descendants("Pomiar").Last().AddAfterSelf(pomiar);
            document.Save(path);
        }
        public List<PomiaryGlikemi> DeserializeObjectsAll()
        {
            List<PomiaryGlikemi> pomiary = new List<PomiaryGlikemi>();
            XDocument document = new XDocument();
            document = XDocument.Load(path);
            

            foreach (XElement pomiar in document.Descendants("Pomiar").ToList())
            {
                PomiaryGlikemi pomiarObject = new PomiaryGlikemi();
                int.TryParse(pomiar.Attribute("id").Value, out pomiarObject.id);
                int.TryParse(pomiar.Element("Cukier").Value, out pomiarObject.cukier);
                pomiarObject.opis = pomiar.Element("Opis").Value;
                int.TryParse(pomiar.Element("DodatkoweJI").Value, out pomiarObject.dodatkoweJI);
                pomiarObject.Set_Date(pomiar.Element("Data").Value);

                pomiary.Add(pomiarObject);   
            }
            return pomiary;
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
        public PomiaryGlikemi DeserializeLastObject()
        {
            XDocument document = new XDocument();
            document = XDocument.Load(path);
            PomiaryGlikemi pomiarObject = new PomiaryGlikemi();
            XElement pomiar = document.Descendants("Pomiar").Last();

            int.TryParse(pomiar.Attribute("id").Value, out pomiarObject.id);
            int.TryParse(pomiar.Element("Cukier").Value, out pomiarObject.cukier);
            pomiarObject.opis = pomiar.Element("Opis").Value;
            int.TryParse(pomiar.Element("DodatkoweJI").Value, out pomiarObject.dodatkoweJI);
            pomiarObject.Set_Date(pomiar.Element("Data").Value);

            return pomiarObject;
        }
    }
}
