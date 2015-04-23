namespace CP.NLayer.Client.WpfClient.Modules.Dashboard
{
    using CP.NLayer.Models.Entities;
    using Microsoft.Practices.Prism.Events;
    using System.Collections.Generic;

    public class SelectedUsersChangedEvent : CompositePresentationEvent<List<User>>
    {
    }
}