// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/21/2013 10:28:58 PM
// ------------------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace CP.NLayer.Models.Business
{
    [DataContract(IsReference = true)]
    public class CheckListItem
    {
        public bool Checked { get; set; }
        public string Text { get; set; }
        public object Value { get; set; }
        public string Description { get; set; }
    }
}