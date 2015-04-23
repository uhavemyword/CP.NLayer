// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 5/24/2014 2:05:34 AM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using Microsoft.Practices.Prism.Events;

    public class DialogResultEvent : CompositePresentationEvent<DialogResultEventPayload>
    {
    }

    public class DialogResultEventPayload
    {
        public string Subject { get; set; }
        public bool Result { get; set; }
        public object Body { get; set; }
    }
}