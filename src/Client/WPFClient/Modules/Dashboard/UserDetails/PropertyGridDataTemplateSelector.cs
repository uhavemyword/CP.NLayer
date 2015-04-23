using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.Data.PropertyGrid;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.UserDetails
{
    public class PropertyGridDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item as PropertyDefinition != null && (item as PropertyDefinition).SourceProperty.PropertyType == typeof(decimal))
            {
                return DecimalPropertyDataTemplate;
            }
            return null;
        }

        public DataTemplate DecimalPropertyDataTemplate { get; set; }
    }
}