using CP.NLayer.Client.WpfClient.Common;
using CP.NLayer.Resources.UI;
using CP.NLayer.Service.Contracts;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using Telerik.Windows.Controls.Map;
using entities = CP.NLayer.Models.Entities;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.UserMap
{
    public static class MapUtilities
    {
        public static void SelectShape(MapShape shape)
        {
            var color = (Color)Application.Current.FindResource("SelectedShapeColor");
            shape.Stroke = new SolidColorBrush(color);
            shape.StrokeThickness = 2;
        }

        public static void UnSelectShape(MapShape shape)
        {
            shape.StrokeThickness = 0;
        }

        public static void ColorShape(MapShape shape, Color color)
        {
            shape.Fill = new SolidColorBrush(color);
        }

        public static MapShape CreateUserShape(ShapeData data)
        {
            var shape = new MapRectangle()
            {
                Width = data.Width,
                Height = data.Height,
                Location = new Location(data.Latitude, data.Longitude)
            };

            var user = data.Tag as entities.User;
            if (user != null)
            {
                var propertySet = new ExtendedPropertySet();
                propertySet.RegisterProperty("Placemark.Name", "Name", typeof(string), string.Empty); // KML file defines Placemark.Name, we create a same property too.
                var extendedData = new ExtendedData(propertySet);
                extendedData.SetValue("Placemark.Name", user.UserName);

                shape.ExtendedData = extendedData;
                shape.Tag = user;
            }

            //set CaptionTemplate
            shape.CaptionTemplate = Application.Current.FindResource("ShapeCaptionTemplate") as DataTemplate; //In the template, we use ExtendedData["Placemark.Name"] as caption

            return shape;
        }

        public static List<ShapeData> LoadUserShapeData()
        {
            var result = new List<ShapeData>();
            var service = ServiceLocator.Current.GetInstance<IUserService>();
            var users = service.GetAll();
            foreach (var user in users)
            {
                var data = Deserialize(user.MapData);
                data.Tag = user;
                result.Add(data);
            }
            return result;
        }

        public static void SaveUserShapeData(List<ShapeData> list)
        {
            var service = ServiceLocator.Current.GetInstance<IUserService>();
            foreach (var item in list)
            {
                var user = item.Tag as entities.User;
                if (user != null)
                {
                    string mapData = Serialize(item);
                    if (user.MapData != mapData)
                    {
                        user.MapData = mapData;
                        service.Update(user);
                    }
                }
            }

            ServiceLocator.Current.GetInstance<IInteractionService>().ShowMessage(UiResources.Message, UiResources.SavedSuccessfully, 2);
        }

        public static string Serialize(ShapeData mapData)
        {
            string result = null;
            try
            {
                using (var stringWriter = new StringWriter())
                {
                    var serializer = new XmlSerializer(typeof(ShapeData));
                    serializer.Serialize(stringWriter, mapData);
                    result = stringWriter.ToString();
                }
            }
            catch
            {
            }
            return result;
        }

        public static ShapeData Deserialize(string stringData)
        {
            var result = new ShapeData();
            try
            {
                using (var stringWriter = new StringReader(stringData))
                {
                    var serializer = new XmlSerializer(typeof(ShapeData));
                    result = (ShapeData)serializer.Deserialize(stringWriter);
                }
            }
            catch
            {
            }
            return result;
        }
    }
}