// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/26/2013 9:07:45 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.Business
{
    using CP.NLayer.Models.Entities;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public class DepartmentEditModel : EditModelBase<Department>
    {
    }
}