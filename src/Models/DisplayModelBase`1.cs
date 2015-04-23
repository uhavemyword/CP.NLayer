// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 5/17/2014 3:16:55 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models
{
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public abstract class DisplayModelBase<T> : DisplayModelBase
    {
        [DataMember]
        public T Target { get; set; }
    }
}