using System.Collections.Generic;
using System.Windows.Media;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard
{
    public class UsersColorModel
    {
        public string Subject { get; set; }
        public Dictionary<CP.NLayer.Models.Entities.User, Color> UsersColorDic { get; set; }
    }
}