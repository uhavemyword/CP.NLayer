using System.Xml.Serialization;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.UserMap
{
    public class ShapeData
    {
        public ShapeData()
        {
            this.Width = 80;
            this.Height = 35;
        }

        [XmlIgnoreAttribute]
        public object Tag { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        //Only support Rectangle for now.
        public double Width { get; set; }
        public double Height { get; set; }
    }
}