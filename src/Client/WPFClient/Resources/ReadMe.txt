Add DesignTimeResources.xaml in the project Properties folder as a Page to support WPF design time styles.

<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/DesignTimeResources.xaml" />
    </ResourceDictionary.MergedDictionaries>
</ResourceDictionary>