# Dbarone.Net.Mapper


>## T:Dbarone.Net.Mapper.CustomMapperStrategy`2

 Implements a custom mapper strategy. 

---


>## T:Dbarone.Net.Mapper.IMapperStrategy

 Mapper strategy interface. All mapper strategies must implement this interface. 

---
### M:Dbarone.Net.Mapper.IMapperStrategy.MapTypes(System.Type,System.Type)
 A mapper strategy must implement the MapTypes method. 
| Name | Description      |
| ---- | ---------------- |
| T:   | The source type. |
| U:   | The target type. |

---


>## T:Dbarone.Net.Mapper.NameMapperStrategy

 Default mapper strategy. Maps properties where the names match. 

---
### M:Dbarone.Net.Mapper.NameMapperStrategy.MapTypes(System.Type,System.Type)
 Maps source to target types based on matching property names. 
| Name | Description |
| ---- | ----------- |
| T:   |             |
| U:   |             |

---


>## T:Dbarone.Net.Mapper.ObjectMapper`2

 Maps objects from one type to another. 
|T: |The type to map from.|
|U: |The type to map to.|

---
### M:Dbarone.Net.Mapper.ObjectMapper`2.Create(Dbarone.Net.Mapper.IMapperStrategy)
 Static factory method to create a new mapper instance. 
| Name            | Description                                                                        |
| --------------- | ---------------------------------------------------------------------------------- |
| mapperStrategy: | Optional mapper strategy. If omitted, the default NameMapperStrategy will be used. |

---
### M:Dbarone.Net.Mapper.ObjectMapper`2.MapOne(`0)
 Maps a single object. 
| Name | Description             |
| ---- | ----------------------- |
| obj: | The object to map from. |

---
### M:Dbarone.Net.Mapper.ObjectMapper`2.MapMany(System.Collections.Generic.IEnumerable{`0})
 Maps a collection of objects. 
| Name | Description             |
| ---- | ----------------------- |
| obj: | A collection of object. |

---


>## T:Dbarone.Net.Mapper.PropertyMap

 Defines the mapping link / relationship between properties of two different types. 

---
### P:Dbarone.Net.Mapper.PropertyMap.SourceProperty
 The property in the source type. 

---
### P:Dbarone.Net.Mapper.PropertyMap.TargetProperty
 The property in the target type. 

---
