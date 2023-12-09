<a id='top'></a>
# Assembly: Dbarone.Net.Mapper
## Contents
- [MapperIgnoreAttribute](#dbaronenetmappermapperignoreattribute)
- [BuildMember](#dbaronenetmapperbuildmember)
  - [MemberName](#dbaronenetmapperbuildmembermembername)
  - [DataType](#dbaronenetmapperbuildmemberdatatype)
  - [InternalMemberName](#dbaronenetmapperbuildmemberinternalmembername)
  - [Ignore](#dbaronenetmapperbuildmemberignore)
  - [Getter](#dbaronenetmapperbuildmembergetter)
  - [Setter](#dbaronenetmapperbuildmembersetter)
  - [IsCalculation](#dbaronenetmapperbuildmemberiscalculation)
  - [Calculation](#dbaronenetmapperbuildmembercalculation)
- [BuildType](#dbaronenetmapperbuildtype)
  - [Type](#dbaronenetmapperbuildtypetype)
  - [Options](#dbaronenetmapperbuildtypeoptions)
  - [Members](#dbaronenetmapperbuildtypemembers)
  - [MemberResolver](#dbaronenetmapperbuildtypememberresolver)
  - [GetMemberRule](#dbaronenetmapperbuildtypegetmemberrule(systemlinqexpressionsexpression))
  - [MemberFilterRule](#dbaronenetmapperbuildtypememberfilterrule)
  - [IsNullable](#dbaronenetmapperbuildtypeisnullable)
  - [NullableUnderlyingType](#dbaronenetmapperbuildtypenullableunderlyingtype)
  - [IsEnumerable](#dbaronenetmapperbuildtypeisenumerable)
  - [EnumerableElementType](#dbaronenetmapperbuildtypeenumerableelementtype)
  - [IsGenericType](#dbaronenetmapperbuildtypeisgenerictype)
  - [isOpenGeneric](#dbaronenetmapperbuildtypeisopengeneric)
- [MapperBuilder](#dbaronenetmappermapperbuilder)
  - [Configuration](#dbaronenetmappermapperbuilderconfiguration)
  - [BuildTypes](#dbaronenetmappermapperbuilderbuildtypes)
  - [OnCreateOperator](#dbaronenetmappermapperbuilderoncreateoperator)
  - [#ctor](#dbaronenetmappermapperbuilder#ctor(dbaronenetmappermapperconfiguration))
  - [GetMapper](#dbaronenetmappermapperbuildergetmapper(dbaronenetmappersourcetarget,dbaronenetmappermapperoperator))
  - [Build](#dbaronenetmappermapperbuilderbuild)
  - [GetBuildTypeFor](#dbaronenetmappermapperbuildergetbuildtypefor(systemtype))
  - [GetCreatorFor](#dbaronenetmappermapperbuildergetcreatorfor(systemtype))
  - [AddDynamicMembers](#dbaronenetmappermapperbuilderadddynamicmembers(systemtype,systemstring,systemobject))
- [AssignableMapperOperator](#dbaronenetmapperassignablemapperoperator)
  - [#ctor](#dbaronenetmapperassignablemapperoperator#ctor(dbaronenetmappermapperbuilder,dbaronenetmapperbuildtype,dbaronenetmapperbuildtype,dbaronenetmappermapperoperator))
  - [Priority](#dbaronenetmapperassignablemapperoperatorpriority)
  - [CanMap](#dbaronenetmapperassignablemapperoperatorcanmap)
  - [GetMap](#dbaronenetmapperassignablemapperoperatorgetmap)
- [ConverterMapperOperator](#dbaronenetmapperconvertermapperoperator)
  - [#ctor](#dbaronenetmapperconvertermapperoperator#ctor(dbaronenetmappermapperbuilder,dbaronenetmapperbuildtype,dbaronenetmapperbuildtype,dbaronenetmappermapperoperator))
  - [Priority](#dbaronenetmapperconvertermapperoperatorpriority)
  - [CanMap](#dbaronenetmapperconvertermapperoperatorcanmap)
  - [GetMap](#dbaronenetmapperconvertermapperoperatorgetmap)
- [ConvertibleMapperOperator](#dbaronenetmapperconvertiblemapperoperator)
  - [#ctor](#dbaronenetmapperconvertiblemapperoperator#ctor(dbaronenetmappermapperbuilder,dbaronenetmapperbuildtype,dbaronenetmapperbuildtype,dbaronenetmappermapperoperator))
  - [Priority](#dbaronenetmapperconvertiblemapperoperatorpriority)
  - [CanMap](#dbaronenetmapperconvertiblemapperoperatorcanmap)
  - [GetMap](#dbaronenetmapperconvertiblemapperoperatorgetmap)
- [EnumerableMapperOperator](#dbaronenetmapperenumerablemapperoperator)
  - [#ctor](#dbaronenetmapperenumerablemapperoperator#ctor(dbaronenetmappermapperbuilder,dbaronenetmapperbuildtype,dbaronenetmapperbuildtype,dbaronenetmappermapperoperator))
  - [GetChildren](#dbaronenetmapperenumerablemapperoperatorgetchildren)
  - [Priority](#dbaronenetmapperenumerablemapperoperatorpriority)
  - [CanMap](#dbaronenetmapperenumerablemapperoperatorcanmap)
  - [GetMap](#dbaronenetmapperenumerablemapperoperatorgetmap)
- [MapperOperator](#dbaronenetmappermapperoperator)
  - [Builder](#dbaronenetmappermapperoperatorbuilder)
  - [From](#dbaronenetmappermapperoperatorfrom)
  - [To](#dbaronenetmappermapperoperatorto)
  - [Parent](#dbaronenetmappermapperoperatorparent)
  - [#ctor](#dbaronenetmappermapperoperator#ctor(dbaronenetmappermapperbuilder,dbaronenetmapperbuildtype,dbaronenetmapperbuildtype,dbaronenetmappermapperoperator))
  - [Priority](#dbaronenetmappermapperoperatorpriority)
  - [CanMap](#dbaronenetmappermapperoperatorcanmap)
  - [GetMap](#dbaronenetmappermapperoperatorgetmap)
  - [GetChildren](#dbaronenetmappermapperoperatorgetchildren)
  - [Children](#dbaronenetmappermapperoperatorchildren)
  - [Create](#dbaronenetmappermapperoperatorcreate(dbaronenetmappermapperbuilder,dbaronenetmapperbuildtype,dbaronenetmapperbuildtype,dbaronenetmappermapperoperator,createoperatordelegate))
  - [ToMapperOperatorInfo](#dbaronenetmappermapperoperatortomapperoperatorinfo)
  - [GetPath](#dbaronenetmappermapperoperatorgetpath)
- [MapperOperatorInfo](#dbaronenetmappermapperoperatorinfo)
  - [Path](#dbaronenetmappermapperoperatorinfopath)
  - [MapperOperator](#dbaronenetmappermapperoperatorinfomapperoperator)
  - [FromType](#dbaronenetmappermapperoperatorinfofromtype)
  - [FromMemberResolver](#dbaronenetmappermapperoperatorinfofrommemberresolver)
  - [ToType](#dbaronenetmappermapperoperatorinfototype)
  - [ToMemberResolver](#dbaronenetmappermapperoperatorinfotomemberresolver)
  - [#ctor](#dbaronenetmappermapperoperatorinfo#ctor(systemstring,systemstring,systemstring,systemstring,systemstring,systemstring))
  - [ToString](#dbaronenetmappermapperoperatorinfotostring)
- [SourceTarget](#dbaronenetmappersourcetarget)
  - [Source](#dbaronenetmappersourcetargetsource)
  - [Target](#dbaronenetmappersourcetargettarget)
  - [#ctor](#dbaronenetmappersourcetarget#ctor(systemtype,systemtype))
  - [GetHashCode](#dbaronenetmappersourcetargetgethashcode)
  - [Equals](#dbaronenetmappersourcetargetequals(systemobject))
- [SourceTargetPath](#dbaronenetmappersourcetargetpath)
  - [SourceTarget](#dbaronenetmappersourcetargetpathsourcetarget)
  - [Path](#dbaronenetmappersourcetargetpathpath)
  - [#ctor](#dbaronenetmappersourcetargetpath#ctor(dbaronenetmappersourcetarget,systemstring))
  - [GetHashCode](#dbaronenetmappersourcetargetpathgethashcode)
  - [Equals](#dbaronenetmappersourcetargetpathequals(systemobject))
- [SourceTargetPathRules](#dbaronenetmappersourcetargetpathrules)
  - [SourceTarget](#dbaronenetmappersourcetargetpathrulessourcetarget)
  - [Path](#dbaronenetmappersourcetargetpathrulespath)
  - [Maps](#dbaronenetmappersourcetargetpathrulesmaps)
- [Config](#dbaronenetmapperconfig)
  - [AutoRegisterTypes](#dbaronenetmapperconfigautoregistertypes)
  - [Resolvers](#dbaronenetmapperconfigresolvers)
  - [Types](#dbaronenetmapperconfigtypes)
  - [Calculations](#dbaronenetmapperconfigcalculations)
  - [MemberInclusions](#dbaronenetmapperconfigmemberinclusions)
  - [Converters](#dbaronenetmapperconfigconverters)
  - [MemberFilters](#dbaronenetmapperconfigmemberfilters)
  - [MemberRenames](#dbaronenetmapperconfigmemberrenames)
- [ConfigCalculation](#dbaronenetmapperconfigcalculation)
  - [SourceType](#dbaronenetmapperconfigcalculationsourcetype)
  - [MemberName](#dbaronenetmapperconfigcalculationmembername)
  - [MemberType](#dbaronenetmapperconfigcalculationmembertype)
  - [Calculation](#dbaronenetmapperconfigcalculationcalculation)
- [ConfigMemberInclusion](#dbaronenetmapperconfigmemberinclusion)
  - [Type](#dbaronenetmapperconfigmemberinclusiontype)
  - [Member](#dbaronenetmapperconfigmemberinclusionmember)
  - [IncludeExclude](#dbaronenetmapperconfigmemberinclusionincludeexclude)
- [ConfigMemberRename](#dbaronenetmapperconfigmemberrename)
  - [Type](#dbaronenetmapperconfigmemberrenametype)
  - [MemberName](#dbaronenetmapperconfigmemberrenamemembername)
  - [InternalMemberName](#dbaronenetmapperconfigmemberrenameinternalmembername)
- [ConfigType](#dbaronenetmapperconfigtype)
  - [Type](#dbaronenetmapperconfigtypetype)
  - [Options](#dbaronenetmapperconfigtypeoptions)
- [MapperConfiguration](#dbaronenetmappermapperconfiguration)
  - [SetAutoRegisterTypes](#dbaronenetmappermapperconfigurationsetautoregistertypes(systemboolean))
  - [RegisterResolvers](#dbaronenetmappermapperconfigurationregisterresolvers``1)
  - [RegisterResolver](#dbaronenetmappermapperconfigurationregisterresolver(systemtype))
  - [RegisterResolver](#dbaronenetmappermapperconfigurationregisterresolver(dbaronenetmapperimemberresolver))
  - [RegisterTypes](#dbaronenetmappermapperconfigurationregistertypes(systemtype[],dbaronenetmappermapperoptions))
  - [RegisterType](#dbaronenetmappermapperconfigurationregistertype``1(dbaronenetmappermapperoptions))
  - [RegisterType](#dbaronenetmappermapperconfigurationregistertype(systemtype,dbaronenetmappermapperoptions))
  - [RegisterCalculation](#dbaronenetmappermapperconfigurationregistercalculation``2(systemstring,systemfunc{``0,``1}))
  - [Exclude](#dbaronenetmappermapperconfigurationexclude``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}}))
  - [Exclude](#dbaronenetmappermapperconfigurationexclude(systemtype,systemstring[]))
  - [Include](#dbaronenetmappermapperconfigurationinclude``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}}))
  - [Include](#dbaronenetmappermapperconfigurationinclude(systemtype,systemstring[]))
  - [SetMemberFilterRule](#dbaronenetmappermapperconfigurationsetmemberfilterrule``1(dbaronenetmappermemberfilterdelegate))
  - [SetMemberFilterRule](#dbaronenetmappermapperconfigurationsetmemberfilterrule(systemtype,dbaronenetmappermemberfilterdelegate))
  - [RegisterConverter](#dbaronenetmappermapperconfigurationregisterconverter``2(systemfunc{``0,``1}))
  - [Rename](#dbaronenetmappermapperconfigurationrename``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemstring))
  - [Rename](#dbaronenetmappermapperconfigurationrename(systemtype,systemstring,systemstring))
- [MapperOptions](#dbaronenetmappermapperoptions)
  - [IncludeFields](#dbaronenetmappermapperoptionsincludefields)
  - [IncludePrivateMembers](#dbaronenetmappermapperoptionsincludeprivatemembers)
  - [MemberRenameStrategy](#dbaronenetmappermapperoptionsmemberrenamestrategy)
  - [EndPointValidation](#dbaronenetmappermapperoptionsendpointvalidation)
  - [MemberFilterRule](#dbaronenetmappermapperoptionsmemberfilterrule)
- [MemberFilterDelegate](#dbaronenetmappermemberfilterdelegate)
- [CreateInstance](#dbaronenetmappercreateinstance)
- [Getter](#dbaronenetmappergetter)
- [Setter](#dbaronenetmappersetter)
- [ClassMemberResolver](#dbaronenetmapperclassmemberresolver)
  - [GetTypeMembers](#dbaronenetmapperclassmemberresolvergettypemembers(systemtype,dbaronenetmappermapperoptions))
  - [DeferBuild](#dbaronenetmapperclassmemberresolverdeferbuild)
  - [CreateInstance](#dbaronenetmapperclassmemberresolvercreateinstance(systemtype,systemobject[]))
  - [GetGetter](#dbaronenetmapperclassmemberresolvergetgetter(systemtype,systemstring,dbaronenetmappermapperoptions))
  - [GetSetter](#dbaronenetmapperclassmemberresolvergetsetter(systemtype,systemstring,dbaronenetmappermapperoptions))
  - [GetMemberType](#dbaronenetmapperclassmemberresolvergetmembertype(systemtype,systemstring,dbaronenetmappermapperoptions))
  - [GetInstanceMembers](#dbaronenetmapperclassmemberresolvergetinstancemembers(systemobject))
  - [CanResolveMembersForType](#dbaronenetmapperclassmemberresolvercanresolvemembersfortype(systemtype))
- [DictionaryMemberResolver](#dbaronenetmapperdictionarymemberresolver)
  - [DeferBuild](#dbaronenetmapperdictionarymemberresolverdeferbuild)
  - [GetGetter](#dbaronenetmapperdictionarymemberresolvergetgetter(systemtype,systemstring,dbaronenetmappermapperoptions))
  - [GetSetter](#dbaronenetmapperdictionarymemberresolvergetsetter(systemtype,systemstring,dbaronenetmappermapperoptions))
  - [CreateInstance](#dbaronenetmapperdictionarymemberresolvercreateinstance(systemtype,systemobject[]))
  - [GetMemberType](#dbaronenetmapperdictionarymemberresolvergetmembertype(systemtype,systemstring,dbaronenetmappermapperoptions))
  - [GetInstanceMembers](#dbaronenetmapperdictionarymemberresolvergetinstancemembers(systemobject))
  - [GetTypeMembers](#dbaronenetmapperdictionarymemberresolvergettypemembers(systemtype,dbaronenetmappermapperoptions))
  - [CanResolveMembersForType](#dbaronenetmapperdictionarymemberresolvercanresolvemembersfortype(systemtype))
- [IMemberResolver](#dbaronenetmapperimemberresolver)
  - [GetGetter](#dbaronenetmapperimemberresolvergetgetter(systemtype,systemstring,dbaronenetmappermapperoptions))
  - [GetSetter](#dbaronenetmapperimemberresolvergetsetter(systemtype,systemstring,dbaronenetmappermapperoptions))
  - [CreateInstance](#dbaronenetmapperimemberresolvercreateinstance(systemtype,systemobject[]))
  - [GetTypeMembers](#dbaronenetmapperimemberresolvergettypemembers(systemtype,dbaronenetmappermapperoptions))
  - [GetInstanceMembers](#dbaronenetmapperimemberresolvergetinstancemembers(systemobject))
  - [GetMemberType](#dbaronenetmapperimemberresolvergetmembertype(systemtype,systemstring,dbaronenetmappermapperoptions))
  - [DeferBuild](#dbaronenetmapperimemberresolverdeferbuild)
  - [HasMembers](#dbaronenetmapperimemberresolverhasmembers)
  - [CanResolveMembersForType](#dbaronenetmapperimemberresolvercanresolvemembersfortype(systemtype))
- [InterfaceMemberResolver](#dbaronenetmapperinterfacememberresolver)
  - [CreateInstance](#dbaronenetmapperinterfacememberresolvercreateinstance(systemtype,systemobject[]))
  - [CanResolveMembersForType](#dbaronenetmapperinterfacememberresolvercanresolvemembersfortype(systemtype))
- [StructMemberResolver](#dbaronenetmapperstructmemberresolver)
  - [CreateInstance](#dbaronenetmapperstructmemberresolvercreateinstance(systemtype,systemobject[]))
  - [CanResolveMembersForType](#dbaronenetmapperstructmemberresolvercanresolvemembersfortype(systemtype))
- [IncludeExclude](#dbaronenetmapperincludeexclude)
  - [Include](#dbaronenetmapperincludeexcludeinclude)
  - [Exclude](#dbaronenetmapperincludeexcludeexclude)
- [MapperEndPoint](#dbaronenetmappermapperendpoint)
  - [None](#dbaronenetmappermapperendpointnone)
  - [Source](#dbaronenetmappermapperendpointsource)
  - [Target](#dbaronenetmappermapperendpointtarget)
- [NamingConvention](#dbaronenetmappernamingconvention)
  - [None](#dbaronenetmappernamingconventionnone)
  - [CamelCaseNamingConvention](#dbaronenetmappernamingconventioncamelcasenamingconvention)
  - [PascalCaseNamingConvention](#dbaronenetmappernamingconventionpascalcasenamingconvention)
  - [SnakeCasingConvention](#dbaronenetmappernamingconventionsnakecasingconvention)
- [MapperBuildError](#dbaronenetmappermapperbuilderror)
  - [Type](#dbaronenetmappermapperbuilderrortype)
  - [EndPoint](#dbaronenetmappermapperbuilderrorendpoint)
  - [MemberName](#dbaronenetmappermapperbuilderrormembername)
  - [Message](#dbaronenetmappermapperbuilderrormessage)
  - [#ctor](#dbaronenetmappermapperbuilderror#ctor(systemtype,dbaronenetmappermapperendpoint,systemstring,systemstring))
- [MapperBuildException](#dbaronenetmappermapperbuildexception)
  - [Errors](#dbaronenetmappermapperbuildexceptionerrors)
  - [#ctor](#dbaronenetmappermapperbuildexception#ctor(systemstring,systemcollectionsgenericlist{dbaronenetmappermapperbuilderror}))
  - [#ctor](#dbaronenetmappermapperbuildexception#ctor(systemcollectionsgenericlist{dbaronenetmappermapperbuilderror}))
  - [#ctor](#dbaronenetmappermapperbuildexception#ctor(systemtype,dbaronenetmappermapperendpoint,systemstring,systemstring))
- [MapperConfigurationException](#dbaronenetmappermapperconfigurationexception)
  - [#ctor](#dbaronenetmappermapperconfigurationexception#ctor(systemstring))
- [MapperException](#dbaronenetmappermapperexception)
  - [#ctor](#dbaronenetmappermapperexception#ctor)
  - [#ctor](#dbaronenetmappermapperexception#ctor(systemstring))
- [MapperRuntimeException](#dbaronenetmappermapperruntimeexception)
  - [#ctor](#dbaronenetmappermapperruntimeexception#ctor(systemstring))
- [CaseChangeMemberRenameStrategy](#dbaronenetmappercasechangememberrenamestrategy)
  - [#ctor](#dbaronenetmappercasechangememberrenamestrategy#ctor(dbaronenetextensionscasetype,dbaronenetextensionscasetype))
  - [RenameMember](#dbaronenetmappercasechangememberrenamestrategyrenamemember(systemstring))
- [IMemberRenameStrategy](#dbaronenetmapperimemberrenamestrategy)
  - [RenameMember](#dbaronenetmapperimemberrenamestrategyrenamemember(systemstring))
- [PrefixSuffix](#dbaronenetmapperprefixsuffix)
  - [Prefix](#dbaronenetmapperprefixsuffixprefix)
  - [Suffix](#dbaronenetmapperprefixsuffixsuffix)
- [PrefixSuffixMemberRenameStrategy](#dbaronenetmapperprefixsuffixmemberrenamestrategy)
  - [#ctor](#dbaronenetmapperprefixsuffixmemberrenamestrategy#ctor(dbaronenetmapperprefixsuffix,systemstring))
  - [RenameMember](#dbaronenetmapperprefixsuffixmemberrenamestrategyrenamemember(systemstring))
- [ObjectMapper](#dbaronenetmapperobjectmapper)
  - [_onCreateOperator](#dbaronenetmapperobjectmapper_oncreateoperator)
  - [Map](#dbaronenetmapperobjectmappermap(systemtype,systemtype,systemobject))
  - [Map](#dbaronenetmapperobjectmappermap``2(``0))
  - [GetOperator](#dbaronenetmapperobjectmappergetoperator(systemtype,systemtype))
  - [GetOperator](#dbaronenetmapperobjectmappergetoperator``2)
- [ITypeConverter](#dbaronenetmapperitypeconverter)
  - [Convert](#dbaronenetmapperitypeconverterconvert(systemobject))
- [TypeConverter](#dbaronenetmappertypeconverter`2)
  - [#ctor](#dbaronenetmappertypeconverter`2#ctor(systemfunc{`0,`1}))
  - [Convert](#dbaronenetmappertypeconverter`2convert(systemobject))
- [EnumerableBuffer](#dbaronenetextensionsenumerablebuffer)
  - [To](#dbaronenetextensionsenumerablebufferto``1)
  - [To](#dbaronenetextensionsenumerablebufferto(systemtype))
  - [ToArrayList](#dbaronenetextensionsenumerablebuffertoarraylist)
  - [ToQueue](#dbaronenetextensionsenumerablebuffertoqueue)
  - [ToStack](#dbaronenetextensionsenumerablebuffertostack)
  - [ToGenericList](#dbaronenetextensionsenumerablebuffertogenericlist``1)
  - [ToGenericList](#dbaronenetextensionsenumerablebuffertogenericlist(systemtype))
  - [ToGenericEnumerable](#dbaronenetextensionsenumerablebuffertogenericenumerable``1)
  - [ToGenericEnumerable](#dbaronenetextensionsenumerablebuffertogenericenumerable(systemtype))
  - [ToArray](#dbaronenetextensionsenumerablebuffertoarray``1)
  - [ToArray](#dbaronenetextensionsenumerablebuffertoarray(systemtype))
  - [#ctor](#dbaronenetextensionsenumerablebuffer#ctor(systemcollectionsienumerable,mapperdelegate))
- [MapperDelegate](#mapperdelegate)



---
>## <a id='dbaronenetmappermapperignoreattribute'></a>type: MapperIgnoreAttribute
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Attribute to indicate that a property should not be mapped. 

### Type Parameters:
None


---
>## <a id='dbaronenetmapperbuildmember'></a>type: BuildMember
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Defines the build-time metadata for a member on a type. 

### Type Parameters:
None

>### <a id='dbaronenetmapperbuildmembermembername'></a>property: MemberName
#### Summary
 Member name for the mapping rule. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildmemberdatatype'></a>property: DataType
#### Summary
 Member data type of the member. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildmemberinternalmembername'></a>property: InternalMemberName
#### Summary
 The internal member name. Mapping from source to target is done via matching internal names. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildmemberignore'></a>property: Ignore
#### Summary
 Set to true to ignore this member in the mapping configuration. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildmembergetter'></a>property: Getter
#### Summary
 Delegate method to get the value from the instance. Returns a [Getter](#dbaronenetmapperbuildmembergetter) object. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildmembersetter'></a>property: Setter
#### Summary
 Delegate method to set the value to the instance. Returns a [Setter](#dbaronenetmapperbuildmembersetter) object. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildmemberiscalculation'></a>property: IsCalculation
#### Summary
 Set to true if a calculation. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildmembercalculation'></a>property: Calculation
#### Summary
 Used to create a custom calculation. Must be a [ITypeConverter](#dbaronenetmapperitypeconverter) type. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperbuildtype'></a>type: BuildType
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Defines the build-time metadata of a single type. 

### Type Parameters:
None

>### <a id='dbaronenetmapperbuildtypetype'></a>property: Type
#### Summary
 The type the metadata relates to. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildtypeoptions'></a>property: Options
#### Summary
 Defines the options for the type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildtypemembers'></a>property: Members
#### Summary
 Defines the members on the type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildtypememberresolver'></a>property: MemberResolver
#### Summary
 Provides the member resolving strategy for this type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildtypegetmemberrule(systemlinqexpressionsexpression)'></a>method: GetMemberRule
#### Signature
``` c#
BuildType.GetMemberRule(System.Linq.Expressions.Expression expr)
```
#### Summary
 Resolves a member/unary expression to a member configuration. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|expr: |A unary expression to select a member.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildtypememberfilterrule'></a>property: MemberFilterRule
#### Summary
 Provides a member filtering rule. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildtypeisnullable'></a>property: IsNullable
#### Summary
 Returns true if the type is nullable value type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildtypenullableunderlyingtype'></a>property: NullableUnderlyingType
#### Summary
 If the type is a nullable value type, returns the underlying type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildtypeisenumerable'></a>property: IsEnumerable
#### Summary
 Returns true if the type is an IEnumerable type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildtypeenumerableelementtype'></a>property: EnumerableElementType
#### Summary
 Gets the inner element type of collections or sequence types 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildtypeisgenerictype'></a>property: IsGenericType
#### Summary
 Returns true if the type is a generic type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperbuildtypeisopengeneric'></a>property: isOpenGeneric
#### Summary
 Returns true if the type is an open generic type, for example: List[]. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappermapperbuilder'></a>type: MapperBuilder
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Builds the mapping graph between 2 types. 

### Type Parameters:
None

>### <a id='dbaronenetmappermapperbuilderconfiguration'></a>property: Configuration
#### Summary
 The input configuration for the map. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuilderbuildtypes'></a>property: BuildTypes
#### Summary
 Build-time type information. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuilderoncreateoperator'></a>field: OnCreateOperator
#### Summary
 Callback for when new mapper operator is built. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuilder#ctor(dbaronenetmappermapperconfiguration)'></a>method: #ctor
#### Signature
``` c#
MapperBuilder.#ctor(Dbarone.Net.Mapper.MapperConfiguration configuration)
```
#### Summary
 Creates a new [MapperBuilder](#dbaronenetmappermapperbuilder) instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|configuration: |The configuration used to create mapper objects.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuildergetmapper(dbaronenetmappersourcetarget,dbaronenetmappermapperoperator)'></a>method: GetMapper
#### Signature
``` c#
MapperBuilder.GetMapper(Dbarone.Net.Mapper.SourceTarget sourceTarget, Dbarone.Net.Mapper.MapperOperator parent)
```
#### Summary
 Gets a mapper delegate which is able to map the SourceTarget pairing. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|sourceTarget: |The source and target types.|
|parent: |An optional parent operator.|

#### Exceptions:

Exception thrown: [T:System.Exception](#T:System.Exception): 

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuilderbuild'></a>method: Build
#### Signature
``` c#
MapperBuilder.Build()
```
#### Summary
 Pre-emptively builds all type information provided by the configuration. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuildergetbuildtypefor(systemtype)'></a>method: GetBuildTypeFor
#### Signature
``` c#
MapperBuilder.GetBuildTypeFor(System.Type type)
```
#### Summary
 Gets the build metadata for a single type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to get build information for.|

#### Exceptions:

Exception thrown: [T:System.Exception](#T:System.Exception): Throws an exception if the build type is not found.

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuildergetcreatorfor(systemtype)'></a>method: GetCreatorFor
#### Signature
``` c#
MapperBuilder.GetCreatorFor(System.Type type)
```
#### Summary
 Returns the creator delegate for a type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuilderadddynamicmembers(systemtype,systemstring,systemobject)'></a>method: AddDynamicMembers
#### Signature
``` c#
MapperBuilder.AddDynamicMembers(System.Type type, System.String path, System.Object obj)
```
#### Summary
 Adds members to a dynamic type at runtime. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: ||
|path: ||
|obj: ||

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperassignablemapperoperator'></a>type: AssignableMapperOperator
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Maps source type to target type if source is assignable to target. Occurs under following scenarios: 1. Source and target are the same type. 2. Source type is derived directly or indirectly from target type. 3. Target type is an interface which source implements. 4. Source is a generic type parameter, and target represents one of the constraints of source. 5. Source represents a value type, and target represents a Nullable version of it. For more information, refer to: https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignablefrom?view=net-7.0 

### Type Parameters:
None

>### <a id='dbaronenetmapperassignablemapperoperator#ctor(dbaronenetmappermapperbuilder,dbaronenetmapperbuildtype,dbaronenetmapperbuildtype,dbaronenetmappermapperoperator)'></a>method: #ctor
#### Signature
``` c#
AssignableMapperOperator.#ctor(Dbarone.Net.Mapper.MapperBuilder builder, Dbarone.Net.Mapper.BuildType from, Dbarone.Net.Mapper.BuildType to, Dbarone.Net.Mapper.MapperOperator parent)
```
#### Summary
 Creates a new [AssignableMapperOperator](#dbaronenetmapperassignablemapperoperator) instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|builder: |The [MapperBuilder](#dbaronenetmappermapperbuilder) instance.|
|from: |The From [BuildType](#dbaronenetmapperbuildtype) instance.|
|to: |The To [BuildType](#dbaronenetmapperbuildtype) instance.|
|parent: |An optional parent [MapperOperator](#dbaronenetmappermapperoperator) instance.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperassignablemapperoperatorpriority'></a>property: Priority
#### Summary
 Overrides the priority of the [AssignableMapperOperator](#dbaronenetmapperassignablemapperoperator) instance. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperassignablemapperoperatorcanmap'></a>method: CanMap
#### Signature
``` c#
AssignableMapperOperator.CanMap()
```
#### Summary
 The [AssignableMapperOperator](#dbaronenetmapperassignablemapperoperator) operator is able to map when the From object is assignable to the To type. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperassignablemapperoperatorgetmap'></a>method: GetMap
#### Signature
``` c#
AssignableMapperOperator.GetMap()
```
#### Summary
 Returns the [MapperDelegate](#mapperdelegate) object that performs the mapping. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperconvertermapperoperator'></a>type: ConverterMapperOperator
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Maps types based on the presence of a special converter function added to configuration. 

### Type Parameters:
None

>### <a id='dbaronenetmapperconvertermapperoperator#ctor(dbaronenetmappermapperbuilder,dbaronenetmapperbuildtype,dbaronenetmapperbuildtype,dbaronenetmappermapperoperator)'></a>method: #ctor
#### Signature
``` c#
ConverterMapperOperator.#ctor(Dbarone.Net.Mapper.MapperBuilder builder, Dbarone.Net.Mapper.BuildType from, Dbarone.Net.Mapper.BuildType to, Dbarone.Net.Mapper.MapperOperator parent)
```
#### Summary
 Creates a new [ConvertibleMapperOperator](#dbaronenetmapperconvertiblemapperoperator) instance. This operator uses a converter function provided in configuration to map. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|builder: |The [MapperBuilder](#dbaronenetmappermapperbuilder) instance.|
|from: |The From [BuildType](#dbaronenetmapperbuildtype) instance.|
|to: |The To [BuildType](#dbaronenetmapperbuildtype) instance.|
|parent: |An optional parent [MapperOperator](#dbaronenetmappermapperoperator) instance.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconvertermapperoperatorpriority'></a>property: Priority
#### Summary
 Overrides the priority of the [ConverterMapperOperator](#dbaronenetmapperconvertermapperoperator) instance. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconvertermapperoperatorcanmap'></a>method: CanMap
#### Signature
``` c#
ConverterMapperOperator.CanMap()
```
#### Summary
 The [ConverterMapperOperator](#dbaronenetmapperconvertermapperoperator) operator is able to map when a converter function exists between the from and to types. Note that in this case the operator does not recursively map the members. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconvertermapperoperatorgetmap'></a>method: GetMap
#### Signature
``` c#
ConverterMapperOperator.GetMap()
```
#### Summary
 Returns the [MapperDelegate](#mapperdelegate) object that performs the mapping. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperconvertiblemapperoperator'></a>type: ConvertibleMapperOperator
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Maps types that support the IConvertible interface. This is typically for built-in types. 

### Type Parameters:
None

>### <a id='dbaronenetmapperconvertiblemapperoperator#ctor(dbaronenetmappermapperbuilder,dbaronenetmapperbuildtype,dbaronenetmapperbuildtype,dbaronenetmappermapperoperator)'></a>method: #ctor
#### Signature
``` c#
ConvertibleMapperOperator.#ctor(Dbarone.Net.Mapper.MapperBuilder builder, Dbarone.Net.Mapper.BuildType from, Dbarone.Net.Mapper.BuildType to, Dbarone.Net.Mapper.MapperOperator parent)
```
#### Summary
 Creates a new instance of [ConvertibleMapperOperator](#dbaronenetmapperconvertiblemapperoperator). 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|builder: |The [MapperBuilder](#dbaronenetmappermapperbuilder) instance.|
|from: |The From [BuildType](#dbaronenetmapperbuildtype) instance.|
|to: |The To [BuildType](#dbaronenetmapperbuildtype) instance.|
|parent: |An optional parent [MapperOperator](#dbaronenetmappermapperoperator) instance.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconvertiblemapperoperatorpriority'></a>property: Priority
#### Summary
 Overrides the priority of the [ConvertibleMapperOperator](#dbaronenetmapperconvertiblemapperoperator) instance. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconvertiblemapperoperatorcanmap'></a>method: CanMap
#### Signature
``` c#
ConvertibleMapperOperator.CanMap()
```
#### Summary
 The [ConvertibleMapperOperator](#dbaronenetmapperconvertiblemapperoperator) operator is able to map when the From type implements the IConvertible interface, and can convert to the To type. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconvertiblemapperoperatorgetmap'></a>method: GetMap
#### Signature
``` c#
ConvertibleMapperOperator.GetMap()
```
#### Summary
 Returns the [MapperDelegate](#mapperdelegate) object that performs the mapping. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperenumerablemapperoperator'></a>type: EnumerableMapperOperator
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Maps types that implement IEnumerable or have sequences of elements. 

### Type Parameters:
None

>### <a id='dbaronenetmapperenumerablemapperoperator#ctor(dbaronenetmappermapperbuilder,dbaronenetmapperbuildtype,dbaronenetmapperbuildtype,dbaronenetmappermapperoperator)'></a>method: #ctor
#### Signature
``` c#
EnumerableMapperOperator.#ctor(Dbarone.Net.Mapper.MapperBuilder builder, Dbarone.Net.Mapper.BuildType from, Dbarone.Net.Mapper.BuildType to, Dbarone.Net.Mapper.MapperOperator parent)
```
#### Summary
 Creates a new [EnumerableMapperOperator](#dbaronenetmapperenumerablemapperoperator) instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|builder: |The [MapperBuilder](#dbaronenetmappermapperbuilder) instance.|
|from: |The From [BuildType](#dbaronenetmapperbuildtype) instance.|
|to: |The To [BuildType](#dbaronenetmapperbuildtype) instance.|
|parent: |An optional parent [MapperOperator](#dbaronenetmappermapperoperator) instance.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperenumerablemapperoperatorgetchildren'></a>method: GetChildren
#### Signature
``` c#
EnumerableMapperOperator.GetChildren()
```
#### Summary
 GetChildren implementation for [EnumerableMapperOperator](#dbaronenetmapperenumerablemapperoperator). 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:

Exception thrown: [T:Dbarone.Net.Mapper.MapperBuildException](#T:Dbarone.Net.Mapper.MapperBuildException): 

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperenumerablemapperoperatorpriority'></a>property: Priority
#### Summary
 Overrides the priority of the [EnumerableMapperOperator](#dbaronenetmapperenumerablemapperoperator) instance. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperenumerablemapperoperatorcanmap'></a>method: CanMap
#### Signature
``` c#
EnumerableMapperOperator.CanMap()
```
#### Summary
 The [EnumerableMapperOperator](#dbaronenetmapperenumerablemapperoperator) type is able to map when both From and To types are enumerable types. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperenumerablemapperoperatorgetmap'></a>method: GetMap
#### Signature
``` c#
EnumerableMapperOperator.GetMap()
```
#### Summary
 Returns the [MapperDelegate](#mapperdelegate) object that performs the mapping. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappermapperoperator'></a>type: MapperOperator
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Base class for all mapper operator types. 

### Type Parameters:
None

>### <a id='dbaronenetmappermapperoperatorbuilder'></a>property: Builder
#### Summary
 Reference to the current MapperBuilder instance. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorfrom'></a>property: From
#### Summary
 The 'From' [BuildType](#dbaronenetmapperbuildtype) object. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorto'></a>property: To
#### Summary
 The 'To' [BuildType](#dbaronenetmapperbuildtype) object. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorparent'></a>property: Parent
#### Summary
 Optional parent [MapperOperator](#dbaronenetmappermapperoperator) reference. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperator#ctor(dbaronenetmappermapperbuilder,dbaronenetmapperbuildtype,dbaronenetmapperbuildtype,dbaronenetmappermapperoperator)'></a>method: #ctor
#### Signature
``` c#
MapperOperator.#ctor(Dbarone.Net.Mapper.MapperBuilder builder, Dbarone.Net.Mapper.BuildType from, Dbarone.Net.Mapper.BuildType to, Dbarone.Net.Mapper.MapperOperator parent)
```
#### Summary
 Create a new [MapperOperator](#dbaronenetmappermapperoperator) instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|builder: |A [MapperBuilder](#dbaronenetmappermapperbuilder) instance.|
|from: |The 'From' [BuildType](#dbaronenetmapperbuildtype).|
|to: |The 'To' [BuildType](#dbaronenetmapperbuildtype).|
|parent: |Optional parent [MapperOperator](#dbaronenetmappermapperoperator) instance.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorpriority'></a>property: Priority
#### Summary
 Sets the priority of the node. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorcanmap'></a>method: CanMap
#### Signature
``` c#
MapperOperator.CanMap()
```
#### Summary
 Returns true if the current class can map the from / to types. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorgetmap'></a>method: GetMap
#### Signature
``` c#
MapperOperator.GetMap()
```
#### Summary
 When implemented in a class, should return a [MapperDelegate](#mapperdelegate) object that can map an object of 'from' type to 'to' type. This method should also perform any build-time validation, and add any errors to the errors collection. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorgetchildren'></a>method: GetChildren
#### Signature
``` c#
MapperOperator.GetChildren()
```
#### Summary
 Function to get the children of the current operation. Must be implemented in subclasses. This function is called by the Children property. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorchildren'></a>property: Children
#### Summary
 Returns the children of the current operation. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorcreate(dbaronenetmappermapperbuilder,dbaronenetmapperbuildtype,dbaronenetmapperbuildtype,dbaronenetmappermapperoperator,createoperatordelegate)'></a>method: Create
#### Signature
``` c#
MapperOperator.Create(Dbarone.Net.Mapper.MapperBuilder builder, Dbarone.Net.Mapper.BuildType from, Dbarone.Net.Mapper.BuildType to, Dbarone.Net.Mapper.MapperOperator parent, CreateOperatorDelegate onCreateOperator)
```
#### Summary
 Factory method to create a new MapperOperator instance based on from / to types. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|builder: |The current [MapperBuilder](#dbaronenetmappermapperbuilder) instance.|
|from: |The from type.|
|to: |The to type.|
|parent: |An optional parent operator.|
|onCreateOperator: |An optional [CreateOperatorDelegate](#createoperatordelegate) callback function. This callback function is executed each time an operator is created within the mapping operator graph.|

#### Exceptions:

Exception thrown: [T:Dbarone.Net.Mapper.MapperBuildException](#T:Dbarone.Net.Mapper.MapperBuildException): Throws an exception if no suitable mapper found.

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatortomapperoperatorinfo'></a>method: ToMapperOperatorInfo
#### Signature
``` c#
MapperOperator.ToMapperOperatorInfo()
```
#### Summary
 Returns a [MapperOperatorInfo](#dbaronenetmappermapperoperatorinfo) object that contains key information for the operator. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorgetpath'></a>method: GetPath
#### Signature
``` c#
MapperOperator.GetPath()
```
#### Summary
 Gets the path of the current [MapperOperator](#dbaronenetmappermapperoperator). 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:

Exception thrown: [T:System.Exception](#T:System.Exception): Throws an exception if an invalid child.

#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappermapperoperatorinfo'></a>type: MapperOperatorInfo
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Represents a mapping execution plan. Each execution plan node represents a mapper operation. 

### Type Parameters:
None

>### <a id='dbaronenetmappermapperoperatorinfopath'></a>property: Path
#### Summary
 The path of the mapper operator. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorinfomapperoperator'></a>property: MapperOperator
#### Summary
 Text name of the [MapperOperator](#dbaronenetmappermapperoperatorinfomapperoperator). 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorinfofromtype'></a>property: FromType
#### Summary
 The From type name. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorinfofrommemberresolver'></a>property: FromMemberResolver
#### Summary
 The From member resolver name. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorinfototype'></a>property: ToType
#### Summary
 The To type name. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorinfotomemberresolver'></a>property: ToMemberResolver
#### Summary
 The To member resolver name. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorinfo#ctor(systemstring,systemstring,systemstring,systemstring,systemstring,systemstring)'></a>method: #ctor
#### Signature
``` c#
MapperOperatorInfo.#ctor(System.String path, System.String mapperOperator, System.String fromType, System.String fromMemberResolver, System.String toType, System.String toMemberResolver)
```
#### Summary
 Creates a new [MapperOperatorInfo](#dbaronenetmappermapperoperatorinfo). 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|path: |The mapping operator path.|
|mapperOperator: |The mapping operator used.|
|fromType: |The from type.|
|fromMemberResolver: |The from member resolver.|
|toType: |The to type.|
|toMemberResolver: |The to member resolver.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoperatorinfotostring'></a>method: ToString
#### Signature
``` c#
MapperOperatorInfo.ToString()
```
#### Summary
 Implementation of ToString(). Returns a JSON string. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappersourcetarget'></a>type: SourceTarget
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Represents a source + target tuple. 

### Type Parameters:
None

>### <a id='dbaronenetmappersourcetargetsource'></a>property: Source
#### Summary
 The source type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappersourcetargettarget'></a>property: Target
#### Summary
 The target type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappersourcetarget#ctor(systemtype,systemtype)'></a>method: #ctor
#### Signature
``` c#
SourceTarget.#ctor(System.Type source, System.Type target)
```
#### Summary
 Creates a new [SourceTarget](#dbaronenetmappersourcetarget). 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|source: |The source type.|
|target: |The target type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappersourcetargetgethashcode'></a>method: GetHashCode
#### Signature
``` c#
SourceTarget.GetHashCode()
```
#### Summary
 Overrides implementation of GetHashCode. See: https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappersourcetargetequals(systemobject)'></a>method: Equals
#### Signature
``` c#
SourceTarget.Equals(System.Object obj)
```
#### Summary
 Overrides implementation of Equals. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|obj: ||

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappersourcetargetpath'></a>type: SourceTargetPath
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Represents a particular path within a source + target ruleset. Used as key to store mapping rules. 

### Type Parameters:
None

>### <a id='dbaronenetmappersourcetargetpathsourcetarget'></a>property: SourceTarget
#### Summary
 The source and target types. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappersourcetargetpathpath'></a>property: Path
#### Summary
 The path within the source / target map. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappersourcetargetpath#ctor(dbaronenetmappersourcetarget,systemstring)'></a>method: #ctor
#### Signature
``` c#
SourceTargetPath.#ctor(Dbarone.Net.Mapper.SourceTarget sourceTarget, System.String path)
```
#### Summary
 Creates a new SourceTargetPath instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|sourceTarget: |The source and target tuple.|
|path: |The path.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappersourcetargetpathgethashcode'></a>method: GetHashCode
#### Signature
``` c#
SourceTargetPath.GetHashCode()
```
#### Summary
 Overrides implementation of GetHashCode. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappersourcetargetpathequals(systemobject)'></a>method: Equals
#### Signature
``` c#
SourceTargetPath.Equals(System.Object obj)
```
#### Summary
 Overrides implementation of Equals. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|obj: ||

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappersourcetargetpathrules'></a>type: SourceTargetPathRules
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Mapper rules defined for a particular Source + target tuple, and path. 

### Type Parameters:
None

>### <a id='dbaronenetmappersourcetargetpathrulessourcetarget'></a>property: SourceTarget
#### Summary
 The source and target types the rule belongs to. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappersourcetargetpathrulespath'></a>property: Path
#### Summary
 The path within the mapping graph that the rules belong to. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappersourcetargetpathrulesmaps'></a>property: Maps
#### Summary
 The mapper rules. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperconfig'></a>type: Config
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Stores all configuration information. 

### Type Parameters:
None

>### <a id='dbaronenetmapperconfigautoregistertypes'></a>property: AutoRegisterTypes
#### Summary
 If set to true, types will be automatically registered on demand with default configuration if not registered in advance 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigresolvers'></a>property: Resolvers
#### Summary
 List of resolvers used to provide mapper services for types. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigtypes'></a>property: Types
#### Summary
 Registered types that can participate in mappings. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigcalculations'></a>property: Calculations
#### Summary
 Calculated members allow for dervived members to be added to types. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigmemberinclusions'></a>property: MemberInclusions
#### Summary
 Individual member include / exclude rules. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigconverters'></a>property: Converters
#### Summary
 Converters enable a member to be converted from one type to another. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigmemberfilters'></a>property: MemberFilters
#### Summary
 Member filter rules provide a function to determine which members to include or exclude from mapping. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigmemberrenames'></a>property: MemberRenames
#### Summary
 Member renames are functions which rename members. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperconfigcalculation'></a>type: ConfigCalculation
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Configuration relating to a calculation. A calculation is like a simple mapping, that does not execute recursively. 

### Type Parameters:
None

>### <a id='dbaronenetmapperconfigcalculationsourcetype'></a>property: SourceType
#### Summary
 The source type that the calculation calculates over. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigcalculationmembername'></a>property: MemberName
#### Summary
 The name of the calculation. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigcalculationmembertype'></a>property: MemberType
#### Summary
 The type returned by the calculation. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigcalculationcalculation'></a>property: Calculation
#### Summary
 The calculation implementation. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperconfigmemberinclusion'></a>type: ConfigMemberInclusion
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Configuration of member inclusion / exclusion for mapping. 

### Type Parameters:
None

>### <a id='dbaronenetmapperconfigmemberinclusiontype'></a>property: Type
#### Summary
 The type that the member belongs to. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigmemberinclusionmember'></a>property: Member
#### Summary
 The (public) member name. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigmemberinclusionincludeexclude'></a>property: IncludeExclude
#### Summary
 The include / exclude behaviour for mapping. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperconfigmemberrename'></a>type: ConfigMemberRename
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Configuration relating to member renaming. 

### Type Parameters:
None

>### <a id='dbaronenetmapperconfigmemberrenametype'></a>property: Type
#### Summary
 The type that the member belongs to. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigmemberrenamemembername'></a>property: MemberName
#### Summary
 The (public) member name. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigmemberrenameinternalmembername'></a>property: InternalMemberName
#### Summary
 The (renamed) member name which is used for internal mapping. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperconfigtype'></a>type: ConfigType
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Configuration relating to a type. 

### Type Parameters:
None

>### <a id='dbaronenetmapperconfigtypetype'></a>property: Type
#### Summary
 The type being added to configuration 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperconfigtypeoptions'></a>property: Options
#### Summary
 Mapper options relating to the type. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappermapperconfiguration'></a>type: MapperConfiguration
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Provides the configuration for an [ObjectMapper](#dbaronenetmapperobjectmapper) . 

### Type Parameters:
None

>### <a id='dbaronenetmappermapperconfigurationsetautoregistertypes(systemboolean)'></a>method: SetAutoRegisterTypes
#### Signature
``` c#
MapperConfiguration.SetAutoRegisterTypes(System.Boolean autoRegisterTypes)
```
#### Summary
 Sets the auto-register types flag. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|autoRegisterTypes: |The auto-register types flag.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregisterresolvers``1'></a>method: RegisterResolvers
#### Signature
``` c#
MapperConfiguration.RegisterResolvers<TResolver>()
```
#### Summary
 Registers an IMemberResolver using a generic type. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|TResolver: |The resolver type.|

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregisterresolver(systemtype)'></a>method: RegisterResolver
#### Signature
``` c#
MapperConfiguration.RegisterResolver(System.Type resolverType)
```
#### Summary
 Registers an IMemberResolver by type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|resolverType: |The type of a resolver.|

#### Exceptions:

Exception thrown: [T:System.Exception](#T:System.Exception): Throws an exception if the type does not implement IMemberResolver.

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregisterresolver(dbaronenetmapperimemberresolver)'></a>method: RegisterResolver
#### Signature
``` c#
MapperConfiguration.RegisterResolver(Dbarone.Net.Mapper.IMemberResolver resolver)
```
#### Summary
 Registers an IMemberResolver 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|resolver: |An IMemberResolver instance.|

#### Exceptions:

Exception thrown: [T:System.Exception](#T:System.Exception): Throws an exception if the type of resolver has already been registered.

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregistertypes(systemtype[],dbaronenetmappermapperoptions)'></a>method: RegisterTypes
#### Signature
``` c#
MapperConfiguration.RegisterTypes(System.Type[] types, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Registers a collection of types. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|types: |An array of Types.|
|options: |An optional [MapperOptions](#dbaronenetmappermapperoptions) object containing configuration details for all the types to be registered.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregistertype``1(dbaronenetmappermapperoptions)'></a>method: RegisterType
#### Signature
``` c#
MapperConfiguration.RegisterType<T>(Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Registers a type using generic types. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of the entity to register.|

#### Parameters:
|Name | Description |
|-----|------|
|options: |An optional [MapperOptions](#dbaronenetmappermapperoptions) object containing configuration details for the type being registered.|

#### Exceptions:
None
#### Examples:

#### Examples:


The following example shows how to register a type:
``` c#
    // Configure mapper
    var mapper = MapperConfiguration
        .Create()
        .RegisterType<CustomerEntity>()
        .RegisterType<CustomerModel>()
        .Build();
        
    // Map object
    var obj2 = mapper.MapOne<CustomerEntity, CustomerModel>(obj1); 
    
```



<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregistertype(systemtype,dbaronenetmappermapperoptions)'></a>method: RegisterType
#### Signature
``` c#
MapperConfiguration.RegisterType(System.Type type, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Registers a single type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to be registered.|
|options: |An optional [MapperOptions](#dbaronenetmappermapperoptions) object containing configuration details for the type being registered.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregistercalculation``2(systemstring,systemfunc{``0,``1})'></a>method: RegisterCalculation
#### Signature
``` c#
MapperConfiguration.RegisterCalculation<TSource, TReturn>(System.String memberName, System.Func<TSource,TReturn> calculation)
```
#### Summary
 Registers a calculation for a type. Calculations are used to transform a value into another value. A calculation can be given a name allowing it to be used in mappings. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|TSource: |The source entity type.|
|TReturn: |The type of the return value for the calculated member.|

#### Parameters:
|Name | Description |
|-----|------|
|memberName: |The calculated member name.|
|calculation: |The calculation.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationexclude``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}})'></a>method: Exclude
#### Signature
``` c#
MapperConfiguration.Exclude<T>(System.Linq.Expressions.Expression<System.Func<T,System.Object>> member)
```
#### Summary
 Defines a member that will not be mapped. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The source object type.|

#### Parameters:
|Name | Description |
|-----|------|
|member: |A unary member expression to select a member on the source object type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationexclude(systemtype,systemstring[])'></a>method: Exclude
#### Signature
``` c#
MapperConfiguration.Exclude(System.Type type, System.String[] members)
```
#### Summary
 Sets one or more members on a type to be ignored for mapping purposes. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type.|
|members: |The list of members to ignore|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationinclude``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}})'></a>method: Include
#### Signature
``` c#
MapperConfiguration.Include<T>(System.Linq.Expressions.Expression<System.Func<T,System.Object>> member)
```
#### Summary
 Defines a member that will be mapped. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The source object type.|

#### Parameters:
|Name | Description |
|-----|------|
|member: |A unary member expression to select a member on the source object type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationinclude(systemtype,systemstring[])'></a>method: Include
#### Signature
``` c#
MapperConfiguration.Include(System.Type type, System.String[] members)
```
#### Summary
 Sets one or more members on a type to be included for mapping purposes. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type.|
|members: |The list of members to include.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationsetmemberfilterrule``1(dbaronenetmappermemberfilterdelegate)'></a>method: SetMemberFilterRule
#### Signature
``` c#
MapperConfiguration.SetMemberFilterRule<T>(Dbarone.Net.Mapper.MemberFilterDelegate memberFilterRule)
```
#### Summary
 Sets a member filter rule for a single type. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type to set the member filter rule for.|

#### Parameters:
|Name | Description |
|-----|------|
|memberFilterRule: |The filter rule.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationsetmemberfilterrule(systemtype,dbaronenetmappermemberfilterdelegate)'></a>method: SetMemberFilterRule
#### Signature
``` c#
MapperConfiguration.SetMemberFilterRule(System.Type type, Dbarone.Net.Mapper.MemberFilterDelegate memberFilterRule)
```
#### Summary
 Sets a member filter rule for a single type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to set the member filter rule on.|
|memberFilterRule: |The filter rule.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregisterconverter``2(systemfunc{``0,``1})'></a>method: RegisterConverter
#### Signature
``` c#
MapperConfiguration.RegisterConverter<T, U>(System.Func<T,U> converter)
```
#### Summary
 Adds a type converter. Type converters are used to convert simple / native types where the members in the source and targets have different types. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of the source member.|
|U: |The type of the target member.|

#### Parameters:
|Name | Description |
|-----|------|
|converter: |A converter func.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationrename``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemstring)'></a>method: Rename
#### Signature
``` c#
MapperConfiguration.Rename<T>(System.Linq.Expressions.Expression<System.Func<T,System.Object>> member, System.String newName)
```
#### Summary
 Defines a custom name for a member when mapping to other types. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The source object type.|

#### Parameters:
|Name | Description |
|-----|------|
|member: |A unary member expression to select a member on the source object type.|
|newName: |The new name for the member.|

#### Exceptions:

Exception thrown: [T:System.ArgumentNullException](#T:System.ArgumentNullException): 

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationrename(systemtype,systemstring,systemstring)'></a>method: Rename
#### Signature
``` c#
MapperConfiguration.Rename(System.Type type, System.String member, System.String newName)
```
#### Summary
 Defines a custom name for a member when mapping to other types. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type containing the member.|
|member: |The member to rename.|
|newName: |The new name for the member.|

#### Exceptions:

Exception thrown: [T:System.ArgumentNullException](#T:System.ArgumentNullException): 

#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappermapperoptions'></a>type: MapperOptions
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Defines a set of mapping options. These options can be applied to a number of type registrations via the `Register` methods. 

### Type Parameters:
None

>### <a id='dbaronenetmappermapperoptionsincludefields'></a>property: IncludeFields
#### Summary
 Set to true to include mapping of fields as well as properties. Default is false. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoptionsincludeprivatemembers'></a>property: IncludePrivateMembers
#### Summary
 Set to true to include private fields and properties. Default is false.1 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoptionsmemberrenamestrategy'></a>property: MemberRenameStrategy
#### Summary
 Optional member renaming strategy. Must implement [IMemberRenameStrategy](#dbaronenetmapperimemberrenamestrategy). 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoptionsendpointvalidation'></a>property: EndPointValidation
#### Summary
 Defines implicit assertion of mapping rules prior to any map function call. Defaults to 'None'. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperoptionsmemberfilterrule'></a>property: MemberFilterRule
#### Summary
 Provides a member filtering rule. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappermemberfilterdelegate'></a>type: MemberFilterDelegate
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Specifies a delegate to filter a member based on the member name. 

### Type Parameters:
None


---
>## <a id='dbaronenetmappercreateinstance'></a>type: CreateInstance
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Delegate to create a new instance of an object. 

### Type Parameters:
None


---
>## <a id='dbaronenetmappergetter'></a>type: Getter
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Defines a basic getter delegate. 

### Type Parameters:
None


---
>## <a id='dbaronenetmappersetter'></a>type: Setter
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Defines a basic setter delegate. 

### Type Parameters:
None


---
>## <a id='dbaronenetmapperclassmemberresolver'></a>type: ClassMemberResolver
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 General resolver for classes. 

### Type Parameters:
None

>### <a id='dbaronenetmapperclassmemberresolvergettypemembers(systemtype,dbaronenetmappermapperoptions)'></a>method: GetTypeMembers
#### Signature
``` c#
ClassMemberResolver.GetTypeMembers(System.Type type, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Gets the type members for reference types. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to get the members for.|
|options: |The options.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperclassmemberresolverdeferbuild'></a>property: DeferBuild
#### Summary
 Set to true for dictionary and dynamic types, where the member information must be deferred until mapping time. If set to false, the member information is obtained at build time. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperclassmemberresolvercreateinstance(systemtype,systemobject[])'></a>method: CreateInstance
#### Signature
``` c#
ClassMemberResolver.CreateInstance(System.Type type, System.Object[] args)
```
#### Summary
 Returns a delete that creates instances for reference types. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to create the delegate for.|
|args: |The arguments provides to the constructor.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperclassmemberresolvergetgetter(systemtype,systemstring,dbaronenetmappermapperoptions)'></a>method: GetGetter
#### Signature
``` c#
ClassMemberResolver.GetGetter(System.Type type, System.String memberName, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Returns a getter delegate that gets a member value for an object. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to get the getter for.|
|memberName: |The member name.|
|options: |The mapper options provided for the type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperclassmemberresolvergetsetter(systemtype,systemstring,dbaronenetmappermapperoptions)'></a>method: GetSetter
#### Signature
``` c#
ClassMemberResolver.GetSetter(System.Type type, System.String memberName, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Returns a setter delegate that sets a member value for an object. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to get the setter for.|
|memberName: |The member name|
|options: |The mapper options provided for the type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperclassmemberresolvergetmembertype(systemtype,systemstring,dbaronenetmappermapperoptions)'></a>method: GetMemberType
#### Signature
``` c#
ClassMemberResolver.GetMemberType(System.Type type, System.String memberName, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Gets a member type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type containing the member.|
|memberName: |The member name.|
|options: |The mapper options provided for the type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperclassmemberresolvergetinstancemembers(systemobject)'></a>method: GetInstanceMembers
#### Signature
``` c#
ClassMemberResolver.GetInstanceMembers(System.Object obj)
```
#### Summary
 Gets members on an instance. This method is normally implemented only if DeferMemberResolution is set to true. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|obj: |The object instance.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperclassmemberresolvercanresolvemembersfortype(systemtype)'></a>method: CanResolveMembersForType
#### Signature
``` c#
ClassMemberResolver.CanResolveMembersForType(System.Type type)
```
#### Summary
 Set to true if the current IMemberResolver can resolve members of the specified type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to resolve members for.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperdictionarymemberresolver'></a>type: DictionaryMemberResolver
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Member resolver for dictionaries. 

### Type Parameters:
None

>### <a id='dbaronenetmapperdictionarymemberresolverdeferbuild'></a>property: DeferBuild
#### Summary
 Set to true for dictionary and dynamic types, where the member information must be deferred until mapping time. If set to false, the member information is obtained at build time. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperdictionarymemberresolvergetgetter(systemtype,systemstring,dbaronenetmappermapperoptions)'></a>method: GetGetter
#### Signature
``` c#
DictionaryMemberResolver.GetGetter(System.Type type, System.String memberName, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Returns a getter delegate that gets a member value for an object. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to get the getter for.|
|memberName: |The member name.|
|options: |The mapper options provided for the type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperdictionarymemberresolvergetsetter(systemtype,systemstring,dbaronenetmappermapperoptions)'></a>method: GetSetter
#### Signature
``` c#
DictionaryMemberResolver.GetSetter(System.Type type, System.String memberName, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Returns a setter delegate that sets a member value for an object. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to get the setter for.|
|memberName: |The member name|
|options: |The mapper options provided for the type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperdictionarymemberresolvercreateinstance(systemtype,systemobject[])'></a>method: CreateInstance
#### Signature
``` c#
DictionaryMemberResolver.CreateInstance(System.Type type, System.Object[] args)
```
#### Summary
 Returns a CreateInstance delegate that can create a new instance of a particular type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to create the CreateInstance delegate for.|
|args: |The arguments to provide to the constructor function to create the new instance.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperdictionarymemberresolvergetmembertype(systemtype,systemstring,dbaronenetmappermapperoptions)'></a>method: GetMemberType
#### Signature
``` c#
DictionaryMemberResolver.GetMemberType(System.Type type, System.String memberName, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Gets a member type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type containing the member.|
|memberName: |The member name.|
|options: |The mapper options provided for the type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperdictionarymemberresolvergetinstancemembers(systemobject)'></a>method: GetInstanceMembers
#### Signature
``` c#
DictionaryMemberResolver.GetInstanceMembers(System.Object obj)
```
#### Summary
 Gets members on an instance. This method is normally implemented only if DeferMemberResolution is set to true. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|obj: |The object instance.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperdictionarymemberresolvergettypemembers(systemtype,dbaronenetmappermapperoptions)'></a>method: GetTypeMembers
#### Signature
``` c#
DictionaryMemberResolver.GetTypeMembers(System.Type type, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Returns the member names for a type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to get the members for.|
|options: |The options.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperdictionarymemberresolvercanresolvemembersfortype(systemtype)'></a>method: CanResolveMembersForType
#### Signature
``` c#
DictionaryMemberResolver.CanResolveMembersForType(System.Type type)
```
#### Summary
 Set to true if the current IMemberResolver can resolve members of the specified type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to resolve members for.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperimemberresolver'></a>type: IMemberResolver
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Interface for classes that can perform member resolver services. 

### Type Parameters:
None

>### <a id='dbaronenetmapperimemberresolvergetgetter(systemtype,systemstring,dbaronenetmappermapperoptions)'></a>method: GetGetter
#### Signature
``` c#
IMemberResolver.GetGetter(System.Type type, System.String memberName, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Returns a getter delegate that gets a member value for an object. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to get the getter for.|
|memberName: |The member name.|
|options: |The mapper options provided for the type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperimemberresolvergetsetter(systemtype,systemstring,dbaronenetmappermapperoptions)'></a>method: GetSetter
#### Signature
``` c#
IMemberResolver.GetSetter(System.Type type, System.String memberName, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Returns a setter delegate that sets a member value for an object. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to get the setter for.|
|memberName: |The member name|
|options: |The mapper options provided for the type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperimemberresolvercreateinstance(systemtype,systemobject[])'></a>method: CreateInstance
#### Signature
``` c#
IMemberResolver.CreateInstance(System.Type type, System.Object[] args)
```
#### Summary
 Returns a CreateInstance delegate that can create a new instance of a particular type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to create the CreateInstance delegate for.|
|args: |The arguments to provide to the constructor function to create the new instance.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperimemberresolvergettypemembers(systemtype,dbaronenetmappermapperoptions)'></a>method: GetTypeMembers
#### Signature
``` c#
IMemberResolver.GetTypeMembers(System.Type type, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Returns the member names for a type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to get the members for.|
|options: |The options.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperimemberresolvergetinstancemembers(systemobject)'></a>method: GetInstanceMembers
#### Signature
``` c#
IMemberResolver.GetInstanceMembers(System.Object obj)
```
#### Summary
 Gets members on an instance. This method is normally implemented only if DeferMemberResolution is set to true. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|obj: |The object instance.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperimemberresolvergetmembertype(systemtype,systemstring,dbaronenetmappermapperoptions)'></a>method: GetMemberType
#### Signature
``` c#
IMemberResolver.GetMemberType(System.Type type, System.String memberName, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Gets a member type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type containing the member.|
|memberName: |The member name.|
|options: |The mapper options provided for the type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperimemberresolverdeferbuild'></a>property: DeferBuild
#### Summary
 Set to true for dictionary and dynamic types, where the mapper build process must be deferred until mapping time. If set to false, the mapping process is member information is obtained at build time. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperimemberresolverhasmembers'></a>property: HasMembers
#### Summary
 Returns true if this resolver can return members on a type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperimemberresolvercanresolvemembersfortype(systemtype)'></a>method: CanResolveMembersForType
#### Signature
``` c#
IMemberResolver.CanResolveMembersForType(System.Type type)
```
#### Summary
 Set to true if the current IMemberResolver can resolve members of the specified type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to resolve members for.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperinterfacememberresolver'></a>type: InterfaceMemberResolver
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 General resolver for interfaces. 

### Type Parameters:
None

>### <a id='dbaronenetmapperinterfacememberresolvercreateinstance(systemtype,systemobject[])'></a>method: CreateInstance
#### Signature
``` c#
InterfaceMemberResolver.CreateInstance(System.Type type, System.Object[] args)
```
#### Summary
 Returns a CreateInstance delegate that can create a new instance of a particular type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to create the CreateInstance delegate for.|
|args: |The arguments to provide to the constructor function to create the new instance.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperinterfacememberresolvercanresolvemembersfortype(systemtype)'></a>method: CanResolveMembersForType
#### Signature
``` c#
InterfaceMemberResolver.CanResolveMembersForType(System.Type type)
```
#### Summary
 Set to true if the current IMemberResolver can resolve members of the specified type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to resolve members for.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperstructmemberresolver'></a>type: StructMemberResolver
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 General resolver for structs. 

### Type Parameters:
None

>### <a id='dbaronenetmapperstructmemberresolvercreateinstance(systemtype,systemobject[])'></a>method: CreateInstance
#### Signature
``` c#
StructMemberResolver.CreateInstance(System.Type type, System.Object[] args)
```
#### Summary
 Returns a CreateInstance delegate that can create a new instance of a particular type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to create the CreateInstance delegate for.|
|args: |The arguments to provide to the constructor function to create the new instance.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperstructmemberresolvercanresolvemembersfortype(systemtype)'></a>method: CanResolveMembersForType
#### Signature
``` c#
StructMemberResolver.CanResolveMembersForType(System.Type type)
```
#### Summary
 Set to true if the current IMemberResolver can resolve members of the specified type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to resolve members for.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperincludeexclude'></a>type: IncludeExclude
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Enum specifying behaviour to include or exclude something. 

### Type Parameters:
None

>### <a id='dbaronenetmapperincludeexcludeinclude'></a>field: Include
#### Summary
 Include behaviour. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperincludeexcludeexclude'></a>field: Exclude
#### Summary
 Exclude behaviour. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappermapperendpoint'></a>type: MapperEndPoint
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Defines the mapper end point type. 

### Type Parameters:
None

>### <a id='dbaronenetmappermapperendpointnone'></a>field: None
#### Summary
 No end point type specified. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperendpointsource'></a>field: Source
#### Summary
 Source mapper endpoint. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperendpointtarget'></a>field: Target
#### Summary
 Target mapper endpoint. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappernamingconvention'></a>type: NamingConvention
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Defines the default member naming convention for the type. 

### Type Parameters:
None

>### <a id='dbaronenetmappernamingconventionnone'></a>field: None
#### Summary
 No defined naming convention used. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappernamingconventioncamelcasenamingconvention'></a>field: CamelCaseNamingConvention
#### Summary
 Members are named in CamelCase, for example 'memberName'. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappernamingconventionpascalcasenamingconvention'></a>field: PascalCaseNamingConvention
#### Summary
 Members are named in PascalCase, for example: 'MemberName'. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappernamingconventionsnakecasingconvention'></a>field: SnakeCasingConvention
#### Summary
 Members are named in SnakeCase, for example 'member_name'. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappermapperbuilderror'></a>type: MapperBuildError
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 A single build-time error raised during the mapper build process. 

### Type Parameters:
None

>### <a id='dbaronenetmappermapperbuilderrortype'></a>property: Type
#### Summary
 The type relating to the mapper build error. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuilderrorendpoint'></a>property: EndPoint
#### Summary
 The endpoint relating to the mapper build error. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuilderrormembername'></a>property: MemberName
#### Summary
 The optional member relating to the mapper build error. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuilderrormessage'></a>property: Message
#### Summary
 The message relating to the mapper build error. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuilderror#ctor(systemtype,dbaronenetmappermapperendpoint,systemstring,systemstring)'></a>method: #ctor
#### Signature
``` c#
MapperBuildError.#ctor(System.Type type, Dbarone.Net.Mapper.MapperEndPoint endPoint, System.String memberName, System.String message)
```
#### Summary
 Creates a new MapperBuildError instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type.|
|endPoint: |The end point type.|
|memberName: |The optional member name|
|message: |The error message.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappermapperbuildexception'></a>type: MapperBuildException
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 The MapperBuild exception class is used for all exceptions throw during the build process of the mapper. 

### Type Parameters:
None

>### <a id='dbaronenetmappermapperbuildexceptionerrors'></a>property: Errors
#### Summary
 The list of notifications occurring during the build process. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuildexception#ctor(systemstring,systemcollectionsgenericlist{dbaronenetmappermapperbuilderror})'></a>method: #ctor
#### Signature
``` c#
MapperBuildException.#ctor(System.String message, System.Collections.Generic.List<Dbarone.Net.Mapper.MapperBuildError> errors)
```
#### Summary
 Creates a new [MapperBuildException](#dbaronenetmappermapperbuildexception). 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|message: |The message.|
|errors: |The build errors.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuildexception#ctor(systemcollectionsgenericlist{dbaronenetmappermapperbuilderror})'></a>method: #ctor
#### Signature
``` c#
MapperBuildException.#ctor(System.Collections.Generic.List<Dbarone.Net.Mapper.MapperBuildError> errors)
```
#### Summary
 Creates a new [MapperBuildException](#dbaronenetmappermapperbuildexception). 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|errors: |The build errors.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperbuildexception#ctor(systemtype,dbaronenetmappermapperendpoint,systemstring,systemstring)'></a>method: #ctor
#### Signature
``` c#
MapperBuildException.#ctor(System.Type type, Dbarone.Net.Mapper.MapperEndPoint endPoint, System.String memberName, System.String message)
```
#### Summary
 Creates a new [MapperBuildException](#dbaronenetmappermapperbuildexception). 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type generating the error(s).|
|endPoint: |Signifies whether the type is a source or target type.|
|memberName: |Optional member name.|
|message: |The error message.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappermapperconfigurationexception'></a>type: MapperConfigurationException
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 The MapperConfigurationException class is used for all exceptions throw during the configuration process. 

### Type Parameters:
None

>### <a id='dbaronenetmappermapperconfigurationexception#ctor(systemstring)'></a>method: #ctor
#### Signature
``` c#
MapperConfigurationException.#ctor(System.String message)
```
#### Summary
 Creates a new [MapperConfigurationException](#dbaronenetmappermapperconfigurationexception) instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|message: |The configuration error message.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappermapperexception'></a>type: MapperException
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Mapper exception class. Used for all exceptions thrown by Dbarone.Net.Mapper. 

### Type Parameters:
None

>### <a id='dbaronenetmappermapperexception#ctor'></a>method: #ctor
#### Signature
``` c#
MapperException.#ctor()
```
#### Summary
 Parameterless constructor. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperexception#ctor(systemstring)'></a>method: #ctor
#### Signature
``` c#
MapperException.#ctor(System.String message)
```
#### Summary
 Create a mapper exception with message. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|message: ||

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappermapperruntimeexception'></a>type: MapperRuntimeException
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 The MapperRuntimeException class is used for all exceptions throw during the runtime mapping process. 

### Type Parameters:
None

>### <a id='dbaronenetmappermapperruntimeexception#ctor(systemstring)'></a>method: #ctor
#### Signature
``` c#
MapperRuntimeException.#ctor(System.String message)
```
#### Summary
 Creates a new [MapperRuntimeException](#dbaronenetmappermapperruntimeexception) instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|message: |The error message.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappercasechangememberrenamestrategy'></a>type: CaseChangeMemberRenameStrategy
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Converts member names from specified case type to a common (lowercase). Implementation of [IMemberRenameStrategy](#dbaronenetmapperimemberrenamestrategy). 

### Type Parameters:
None

>### <a id='dbaronenetmappercasechangememberrenamestrategy#ctor(dbaronenetextensionscasetype,dbaronenetextensionscasetype)'></a>method: #ctor
#### Signature
``` c#
CaseChangeMemberRenameStrategy.#ctor(Dbarone.Net.Extensions.CaseType matchCase, Dbarone.Net.Extensions.CaseType newCase)
```
#### Summary
 Creates a new instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|matchCase: |The case to match.|
|newCase: |The case to change the member to.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappercasechangememberrenamestrategyrenamemember(systemstring)'></a>method: RenameMember
#### Signature
``` c#
CaseChangeMemberRenameStrategy.RenameMember(System.String member)
```
#### Summary
 Renames a member. If the case of the member matches the `matchCase`, then it is converted to `newCase`. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|member: |The input member name.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperimemberrenamestrategy'></a>type: IMemberRenameStrategy
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Interface for classes that can provide member renaming strategies. 

### Type Parameters:
None

>### <a id='dbaronenetmapperimemberrenamestrategyrenamemember(systemstring)'></a>method: RenameMember
#### Signature
``` c#
IMemberRenameStrategy.RenameMember(System.String member)
```
#### Summary
 Renames member names. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|member: |The member name.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperprefixsuffix'></a>type: PrefixSuffix
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Denotes either prefix or suffix. 

### Type Parameters:
None

>### <a id='dbaronenetmapperprefixsuffixprefix'></a>field: Prefix
#### Summary
 Prefix (start of string). 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperprefixsuffixsuffix'></a>field: Suffix
#### Summary
 Suffix (end of string). 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperprefixsuffixmemberrenamestrategy'></a>type: PrefixSuffixMemberRenameStrategy
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Removes prefix/suffix characters from member names. 

### Type Parameters:
None

>### <a id='dbaronenetmapperprefixsuffixmemberrenamestrategy#ctor(dbaronenetmapperprefixsuffix,systemstring)'></a>method: #ctor
#### Signature
``` c#
PrefixSuffixMemberRenameStrategy.#ctor(Dbarone.Net.Mapper.PrefixSuffix stringType, System.String stringToRemove)
```
#### Summary
 Creates a new instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|stringType: |The [PrefixSuffix](#dbaronenetmapperprefixsuffix) type.|
|stringToRemove: |The string to remove.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperprefixsuffixmemberrenamestrategyrenamemember(systemstring)'></a>method: RenameMember
#### Signature
``` c#
PrefixSuffixMemberRenameStrategy.RenameMember(System.String member)
```
#### Summary
 Renames a member, removing either a Pre or Post 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|member: ||

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperobjectmapper'></a>type: ObjectMapper
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 The ObjectMapper class provides mapping functions to transform objects from one type to another. 

### Type Parameters:
None

>### <a id='dbaronenetmapperobjectmapper_oncreateoperator'></a>field: _onCreateOperator
#### Summary
 Event raised whenever a new mapper operator is built. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperobjectmappermap(systemtype,systemtype,systemobject)'></a>method: Map
#### Signature
``` c#
ObjectMapper.Map(System.Type fromType, System.Type toType, System.Object obj)
```
#### Summary
 Maps / transforms an object from one type to another. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|fromType: |The type to transform the object from.|
|toType: |The type to transform the object to.|
|obj: |The object being transformed from. Must be assignable to `fromType`.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperobjectmappermap``2(``0)'></a>method: Map
#### Signature
``` c#
ObjectMapper.Map<TSource, TTarget>(TSource obj)
```
#### Summary
 Maps / transforms an object from one type to another. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|TSource: ||
|TTarget: ||

#### Parameters:
|Name | Description |
|-----|------|
|obj: |Returns an object of the target type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperobjectmappergetoperator(systemtype,systemtype)'></a>method: GetOperator
#### Signature
``` c#
ObjectMapper.GetOperator(System.Type from, System.Type to)
```
#### Summary
 Gets the mapping execution plan. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|from: |The from type.|
|to: |The to type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperobjectmappergetoperator``2'></a>method: GetOperator
#### Signature
``` c#
ObjectMapper.GetOperator<TSource, TTarget>()
```
#### Summary
 Gets the mapping execution plan. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|TSource: |The from type.|
|TTarget: |The to type.|

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmapperitypeconverter'></a>type: ITypeConverter
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Interface for classes that can convert values / types. 

### Type Parameters:
None

>### <a id='dbaronenetmapperitypeconverterconvert(systemobject)'></a>method: Convert
#### Signature
``` c#
ITypeConverter.Convert(System.Object obj)
```
#### Summary
 Converts an object. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|obj: |The object to be converted.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetmappertypeconverter`2'></a>type: TypeConverter
### Namespace:
`Dbarone.Net.Mapper`
### Summary
 Converts an object using a generic lambda function or Func. 

### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The source type.|
|U: |The target type.|

>### <a id='dbaronenetmappertypeconverter`2#ctor(systemfunc{`0,`1})'></a>method: #ctor
#### Signature
``` c#
TypeConverter.#ctor(System.Func<T,U> converter)
```
#### Summary
 Creates a TypeConverter instance using a m 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|converter: ||

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappertypeconverter`2convert(systemobject)'></a>method: Convert
#### Signature
``` c#
TypeConverter.Convert(System.Object obj)
```
#### Summary
 Implementation of interface Convert method. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|obj: |The object to be converted.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetextensionsenumerablebuffer'></a>type: EnumerableBuffer
### Namespace:
`Dbarone.Net.Extensions`
### Summary
 Buffer to convert an IEnumerable to other common IEnumerable types (generic and non-generic). 

### Type Parameters:
None

>### <a id='dbaronenetextensionsenumerablebufferto``1'></a>method: To
#### Signature
``` c#
EnumerableBuffer.To<T>()
```
#### Summary
 Generic convert-to method. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type to convert the IEnumerable to. Must be one of the common enumerable or collection types.|

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetextensionsenumerablebufferto(systemtype)'></a>method: To
#### Signature
``` c#
EnumerableBuffer.To(System.Type type)
```
#### Summary
 Converts an [EnumerableBuffer](#dbaronenetextensionsenumerablebuffer) instance to another enumerable or collection type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to convert to.|

#### Exceptions:

Exception thrown: [T:Dbarone.Net.Mapper.MapperRuntimeException](#T:Dbarone.Net.Mapper.MapperRuntimeException): Throws an exception if the specified type is not one of the common enumerable or collection types.

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetextensionsenumerablebuffertoarraylist'></a>method: ToArrayList
#### Signature
``` c#
EnumerableBuffer.ToArrayList()
```
#### Summary
 Converts the [EnumerableBuffer](#dbaronenetextensionsenumerablebuffer) instance to an [ArrayList](#systemcollectionsarraylist). 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetextensionsenumerablebuffertoqueue'></a>method: ToQueue
#### Signature
``` c#
EnumerableBuffer.ToQueue()
```
#### Summary
 Converts the [EnumerableBuffer](#dbaronenetextensionsenumerablebuffer) instance to a [Queue](#systemcollectionsqueue). 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetextensionsenumerablebuffertostack'></a>method: ToStack
#### Signature
``` c#
EnumerableBuffer.ToStack()
```
#### Summary
 Converts the [EnumerableBuffer](#dbaronenetextensionsenumerablebuffer) instance to a [Stack](#systemcollectionsstack). 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetextensionsenumerablebuffertogenericlist``1'></a>method: ToGenericList
#### Signature
``` c#
EnumerableBuffer.ToGenericList<T>()
```
#### Summary
 Converts the [EnumerableBuffer](#dbaronenetextensionsenumerablebuffer) instance to a generic list. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of the generic list elements.|

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetextensionsenumerablebuffertogenericlist(systemtype)'></a>method: ToGenericList
#### Signature
``` c#
EnumerableBuffer.ToGenericList(System.Type elementType)
```
#### Summary
 Converts the [EnumerableBuffer](#dbaronenetextensionsenumerablebuffer) instance to a generic list. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|elementType: |The type of the generic list elements.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetextensionsenumerablebuffertogenericenumerable``1'></a>method: ToGenericEnumerable
#### Signature
``` c#
EnumerableBuffer.ToGenericEnumerable<T>()
```
#### Summary
 Converts the [EnumerableBuffer](#dbaronenetextensionsenumerablebuffer) instance to a generic IEnumerable. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of the generic IEnumerable elements.|

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetextensionsenumerablebuffertogenericenumerable(systemtype)'></a>method: ToGenericEnumerable
#### Signature
``` c#
EnumerableBuffer.ToGenericEnumerable(System.Type elementType)
```
#### Summary
 Converts the [EnumerableBuffer](#dbaronenetextensionsenumerablebuffer) instance to a generic IEnumerable. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|elementType: |The type of the generic IEnumerable elements.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetextensionsenumerablebuffertoarray``1'></a>method: ToArray
#### Signature
``` c#
EnumerableBuffer.ToArray<T>()
```
#### Summary
 Converts the [EnumerableBuffer](#dbaronenetextensionsenumerablebuffer) instance to a generic [Array](#systemarray). 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of the generic Array elements.|

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetextensionsenumerablebuffertoarray(systemtype)'></a>method: ToArray
#### Signature
``` c#
EnumerableBuffer.ToArray(System.Type elementType)
```
#### Summary
 Converts the [EnumerableBuffer](#dbaronenetextensionsenumerablebuffer) instance to a generic [Array](#systemarray). 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|elementType: |The type of the Array elements.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetextensionsenumerablebuffer#ctor(systemcollectionsienumerable,mapperdelegate)'></a>method: #ctor
#### Signature
``` c#
EnumerableBuffer.#ctor(System.Collections.IEnumerable source, MapperDelegate mapper)
```
#### Summary
 Constructor to create a new [EnumerableBuffer](#dbaronenetextensionsenumerablebuffer) instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|source: |The source IEnumerable object.|
|mapper: |An optional mapper function. If provided, this function will be applied on the source.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='mapperdelegate'></a>type: MapperDelegate
### Namespace:
``
### Summary
 Delegate which handles mapping of source to target. 

### Type Parameters:
None

