The project is for hosting data models.

Knowledge:
[DataContract] attribute is not necessary for WCF service from .NET 3.5 SP1. 
However, Is is important to set IsReference to true([DataContract(IsReference = true)]) because we can have cycles in the object tree, 
and missing this flag will lead to indefinite recursions while serializing.(http://blog.rsuter.com/?p=286)