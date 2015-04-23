﻿SET IDENTITY_INSERT [Department] ON 

GO
INSERT [Department] ([Id], [Name], [Description]) VALUES (1, N'Dept 1', N'Dept 1 description')
GO
INSERT [Department] ([Id], [Name], [Description]) VALUES (2, N'Dept 2', N'Dept 2 description')
GO
INSERT [Department] ([Id], [Name], [Description]) VALUES (3, N'Dept 3', N'Dept 3 description')
GO
INSERT [Department] ([Id], [Name], [Description]) VALUES (4, N'Dept 4', N'Dept 4 description')
GO
SET IDENTITY_INSERT [Department] OFF
GO
SET IDENTITY_INSERT [User] ON 

GO
INSERT [User] ([Id], [UserName], [PasswordHash], [PasswordSalt], [FullName], [Email], [ContactInfo], [IsActive], [LastLoginAt], [LastLoginIP], [LastLoginLocation], [MapData], [DepartmentId]) VALUES (1, N'User1', N'gA73WzFtql5Tw7OgS58zjA==', N'21bff0d2c22e4ef798412977e9a771c8', N'User 1', NULL, NULL, 1, NULL, NULL, NULL, N'<?xml version="1.0" encoding="utf-16"?>
<ShapeData xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Latitude>1.2472255025012304</Latitude>
  <Longitude>-1.2014266304347943</Longitude>
  <Width>116.73043478260877</Width>
  <Height>35</Height>
</ShapeData>', 1)
GO
INSERT [User] ([Id], [UserName], [PasswordHash], [PasswordSalt], [FullName], [Email], [ContactInfo], [IsActive], [LastLoginAt], [LastLoginIP], [LastLoginLocation], [MapData], [DepartmentId]) VALUES (2, N'User2', N'ciWY+qJg3aqCjDModk+EDg==', N'4d237648370d4c65bffc57f2f20facf4', N'User 2', NULL, NULL, 1, NULL, NULL, NULL, N'<?xml version="1.0" encoding="utf-16"?>
<ShapeData xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Latitude>1.2472255025012304</Latitude>
  <Longitude>0.043000965857761253</Longitude>
  <Width>74.991304347826144</Width>
  <Height>35</Height>
</ShapeData>', 2)
GO
INSERT [User] ([Id], [UserName], [PasswordHash], [PasswordSalt], [FullName], [Email], [ContactInfo], [IsActive], [LastLoginAt], [LastLoginIP], [LastLoginLocation], [MapData], [DepartmentId]) VALUES (3, N'User3', N'tARvLVVEbBpRKcyZfD030Q==', N'b2f3d0d3262348c1b4273c64259e3550', N'User 3', NULL, NULL, 1, NULL, NULL, NULL, N'<?xml version="1.0" encoding="utf-16"?>
<ShapeData xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Latitude>-0.11922358741895256</Latitude>
  <Longitude>-1.2014266304347943</Longitude>
  <Width>211.89565217391316</Width>
  <Height>91.7652173913043</Height>
</ShapeData>', 3)
GO
INSERT [User] ([Id], [UserName], [PasswordHash], [PasswordSalt], [FullName], [Email], [ContactInfo], [IsActive], [LastLoginAt], [LastLoginIP], [LastLoginLocation], [MapData], [DepartmentId]) VALUES (4, N'User4', N'heR686ArVHLwQ1t7Ks6u2A==', N'f50babfea41d470b9c7a2aeaeb2a9ac3', N'User 4', NULL, NULL, 1, NULL, NULL, NULL, N'<?xml version="1.0" encoding="utf-16"?>
<ShapeData xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Latitude>0.81624064564308485</Latitude>
  <Longitude>-1.2014266304347943</Longitude>
  <Width>116.73043478260877</Width>
  <Height>91.7652173913043</Height>
</ShapeData>', 1)
GO
SET IDENTITY_INSERT [User] OFF
GO
SET IDENTITY_INSERT [Role] ON 

GO
INSERT [Role] ([Id], [Name], [Description]) VALUES (1, N'Role 1', N'Role 1 description')
GO
INSERT [Role] ([Id], [Name], [Description]) VALUES (2, N'Role 2', N'Role 2 description')
GO
INSERT [Role] ([Id], [Name], [Description]) VALUES (3, N'Role 3', N'Role 3 description')
GO
INSERT [Role] ([Id], [Name], [Description]) VALUES (4, N'Role 4', N'Role 4 description')
GO
SET IDENTITY_INSERT [Role] OFF
GO
INSERT [UserRoleMapping] ([UserId], [RoleId]) VALUES (1, 1)
GO
INSERT [UserRoleMapping] ([UserId], [RoleId]) VALUES (4, 1)
GO
INSERT [UserRoleMapping] ([UserId], [RoleId]) VALUES (2, 2)
GO
INSERT [UserRoleMapping] ([UserId], [RoleId]) VALUES (4, 2)
GO
INSERT [UserRoleMapping] ([UserId], [RoleId]) VALUES (3, 3)
GO
INSERT [UserRoleMapping] ([UserId], [RoleId]) VALUES (4, 3)
GO
INSERT [UserRoleMapping] ([UserId], [RoleId]) VALUES (3, 4)
GO
INSERT [UserRoleMapping] ([UserId], [RoleId]) VALUES (4, 4)
GO
SET IDENTITY_INSERT [Permission] ON 

GO
INSERT [Permission] ([Id], [Code], [CodeEnum], [Name], [Description], [DisplayOrder], [Group]) VALUES (1, N'Code A', 0, N'None', N'None', 0, N'System')
GO
INSERT [Permission] ([Id], [Code], [CodeEnum], [Name], [Description], [DisplayOrder], [Group]) VALUES (2, N'Code B', 1, N'User Management', N'User Management', 1, N'System')
GO
INSERT [Permission] ([Id], [Code], [CodeEnum], [Name], [Description], [DisplayOrder], [Group]) VALUES (3, N'Code C', 2, N'Role Management', N'Role Management', 2, N'System')
GO
SET IDENTITY_INSERT [Permission] OFF
GO
INSERT [RolePermissionMapping] ([PermissionId], [RoleId]) VALUES (2, 1)
GO
INSERT [RolePermissionMapping] ([PermissionId], [RoleId]) VALUES (3, 1)
GO
INSERT [RolePermissionMapping] ([PermissionId], [RoleId]) VALUES (3, 2)
GO
INSERT [RolePermissionMapping] ([PermissionId], [RoleId]) VALUES (2, 3)
GO
INSERT [__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201504201422407_AutomaticMigration', N'CP.NLayer.Data.Migrations.Configuration', 0x1F8B0800000000000400ED5D6D6FE3B811FE5EA0FF41D0A7B6C8D971B65B5C83F80ED9BCDC198D37419C3DF45B404BB423546F2751D918457FD97DB89FD4BF5052AF7C954859F63A69B0C06223920F6786C399218793FDEF6FBF9FFDF812F8D6334C522F0AA7F664746C5B307422D70BD7533B43ABEFBEB77FFCE18F7F38BB728317EB97AADF07D20F8F0CD3A9FD84507C3A1EA7CE130C403A0A3C2789D26885464E148C811B8D4F8E8FFF3E9E4CC61043D818CBB2CEEEB3107901CC7FC03F5E44A1036394017F1EB9D04FCBEFB86591A35A9F4100D31838706A5FDC8D3EDF800D4C46970001DB3AF73D80A958407F655B200C230410A6F1F44B0A172889C2F522C61F80FFB08921EEB7027E0A4BDA4F9BEEBA6C1C9F1036C6CDC00ACAC95214058680930FA55CC6FCF05ED2B56BB961C95D6109A30DE13A971E165C14AEBCB56DF1539D5EF809E9468BB65886518EE1C174548C3DB2F81E47B54A60CD217F7097CC475902A721CC5002FC23EB2E5BFA9EF30FB87988FE05C36998F93E4D282615B7311FF0A7BB248A618236F77055923F736D6BCC8E1BF303EB61D49882B55988FEF657DBFA8C27074B1FD67A40896181A204FE0443980004DD3B80104C42820173490AB373735DE051EB28D9543362E5C37BC8B6E6E0E506866BF434B5F152D9D6B5F702DDEA4B49C597D0C35B0E0F4249062554B6CF4CFEDEFFACBF003F6B9B16FF536BDAF6592EE10A6085DAD764A9937871B1FFB69667FB5CF7D1D772EB54537DF24240D48718C32C49B015DECC73B87C2E8684EFE504F4D0EE8B28883304B9BD75366EAC47AB4DB9843148508077483FBBD28C7FB72DFD77F8C79D6CF0F7DDD090FB193C7BEB7C2847388E3292D4B6EEA19FB7A64F5E5CC42223D2F248EF8FEB240AEE23BF1C44353D3E80640D718F8748DEBE88B2C4813DB72801EBB739C9C8F76DD9321711D037D99A77204DBF4689FB33489FF6EFF9ABD917C047DBB3DE3ED73526ED9B88F82A009EBF73BB87AD1B020E9A85AB68E773CDD2730779CFB5283F45D8D880D0583237204537D1DA0BCFEBD5C72731F8E005FA4367772DEC4E0661B79EEB26724087131B66C639888B2369DB420E3151E31A186BF7E1E4CDBA59DA8F6EE76B2B5FAAF2B5952FD6A58CA0B404006533474FFE554E4AD124A342DBE313887E1E9F8C7CF7F8EF81F82BB410773009BC94102EDF8D4DBBB827F9362120173AC822F22D8E096D56427A385053A16D251A9EFAD98A66FCBBC5688DF0DCFD5B0C32E9559805F5C4EC0E689A87BD697CFDE6EFD24B631F6C6E13971C9A99C8CA90EA9F92288B777D3A7AB5D65A1D3519D9693E82521A723D0B990552FBD86C97597AED8375934532B09B22DC7066132B0DD6577F83758C5659762DE63058C2A4DAC85188C9CF6FD5A7F6B1B06E4C5FE270E620046B58C4D6E5A849FB28227FC9A813712D0AA9D31FCFD33472BC5CA2140D7480CF4E7C15BA5647B4DF98C1E23E6C8EE5EAC558925811A6F65F045ED490B53F6E20E993070B7C3C1A4D788E29EEDA9916B6828A44F5BE90597F13E6D59151835C1C375A310D98A6E2A1D6155132DA6B7D77C25C6166F22B1E2FAC37C67C73B924DFE08B2C6782A9296D485ABA009E6E82B9808849EA622BD9D8B492F22ADD2B30CE0234BA2B03A135BB03A80C710588623D3A0697A65E185C88BF633013FA0B10B4E27340D4BAB17C307709542FC575031FA576DBA39A855A6E637D904A4D29106615F9709865534304A20F1665D06E9CF4CC13C500B3862DB2501A240AABA4796B31D04733851274B12E3155E64B3F18A355E4539BA4E69DCDB87868533DC8192B5EE49CCD411CE330837AA1537EB116E5F39CEF16E66F57820263ECA492272C35B5F54C380AC57105D75A9C7AAEBD2445E4167609488076E1064237DA002B0C4B35136B63C565AA4C4DD59FFCBB1CC3BC52AA8C3187D048EF1A3344766FCE1BA4965835307F1A057C90488EB617919F05A1EA78DC36BA79C34263345FF5918A93228D527CD147289F7CD010E5277D0CF6F5080DC5B6982052875116906AD0C7A34F70341CFD5D443B1B737AC3ABE658D04DCE46F0AAAEB511682FB3C566507B4E8D0DD13678379B627B55FEBF569AC2C96DA12E790069AE28F261BB5191E621008DD17CD5476233FB341ADB628E5864EB6588458B3E629393A7D19AAFFA4865929D86293F19F82C3A85CEB82DBA411FAF499333FA507FD5476212E53418D3D0038F64CFA578A4A1075E932197A236CDFAD8752E9C46AC3F9A984E3AD5CDDA4EBAE58D1A4FD5F942DB78E6076873E3291FF6EE5F0F5045E893F3168A425D9398AB4BDBE01D9D54F2B41A6BEE5D23A5696EF57994E2EB6B5660368DC500322DFA88653A8B862A3FBDF66DC5DE9688B12B7317D81AA4323D75C25172D123892425777DA254B4F6140524DB5E44343501E6B495375A3D69332608C773AE97670A66294952D659386D8EF9AB31637D102F4635EC69D5556D3465DE56227FD53D6ACF0568E006D00DD5BD6C4FDA088619555BAF2D7DDBDBBECD15EB49DA345752BC16EE29270234D4CE3ED475136EADF92EB587A86FAFB95BEAB3F2C658A3B894BF422EBAD816A6FDD973C9F5F16293221814EAB0F8D5BFF0BDDCDE571DE620F4563045457ADE3E399E9C7035AA87532F3A4E53D797DCB88B45A3EC92EDE161D6D2C307CFCA8DB6BDBE327D19C5556986CF20719E80AC426616BAF0656AFF3B1F796ACDFEF9580D7E245F8EAC3C883AB52647D8217D09BD5F33DCEB21C9A0F51FF165D2160FAD0620F1640724322599158D7F0AC0CB9F69A8BE359E5B020A2FC61A21AAD9EEF7E22A89BE3E575F3ADF47B5CAB84FD5E59BD998BD35BE50F456F5FE68AEDD6F5D85C4E3C86B561EBE10EF00144856A2A7A341BD2BF0A4E01F8DB5932FB8D38035AFA71B6A2B49CAE78682E6ABE5961E1AA252CEC50A8FF24A393372249573524E27E69C2A0BE5869A80AB8B53AD9031AEAC0C2E372F87638FDB887E14EDA286B562C6D701DE3131586D6C9B158FBD19C770604EE1AD4715AA3CC06B5621BA80672867C8D7E770566B0BD53E58FD9455D6F4619BA9AB1928E639B0BDD3D704775CE88A03E4B775DD9B8E9DC8D885D1C3551E4C4B172AFA8D2928061ACC6D74AEEABB7ECACBDCE156AE9AC25862C5C057BE5AFDEB8AF81C608FC21F8B7FD7DDA36A052B004C881F033E3EFAA428019E980FBD4BBCD0F162E08BB48BB7FA3A5E9808B406E55B303A0C896FA539D399A733375923733ADE25842D2AACA4D91869F581666DD5444806DD8697D087085AE47849AEFD2F40EA00F1E9429E31E9A6A3289091D2A251BAD457A55A1F8B0CAE52ED89D23EAEF09BE8962CB346ADA9B092B2F57BA3FAA4BDAEFBD7247546730F3AD491273675283BD29E3AE6E1E7DFA1C668BB9901DC99B6AEA8B3F1FBD59503B634DF4257F6655D8C74658F76455E872C56CBF16B26AB346E29342E9E4A4C6D77492EE38BE0DCB808B9A306593687618DB2B2445986AD5DBCACAC5D96A11A5635771435CB66A0DBB5E751F0C1775071D4F42B9FDF682D8762D2A649B52CC52F98904FB4FBC26E5163F9EAB4AEB25EA112780785DB75D17177FDB6E2219AE2F0237FFBAD57B84DAFB8EC21E40EC450D54E6B8841FEFE4DF09E7CADC4A1B02E29386F2B5DEF5E752D5555B878D9E3C8415954AF6BE78BC6AD567428160D0AF2C5278C3822A0FE170D1C8DA4DEBA8120CFA543E830B140DD8724CCAB9084A3A8EAC2E77021022E0E14CE13E4AD808370B303B1E212E35BFE6A9FAB6009DD59789BA138439865182C7DA6669C84366DF3E7BF7580A5F9EC364F46A443B080C9F448FAFD36FC9479BE5BD37D2DB94054409098A9CC0490B5442423B0DED448C56F55D2012AC557877A0F30887D0C96DE860BF00CFBD086D5EF06AE81B3A95EA2AA41BA178215FBD9A507D60908D212A3198F7FC43AEC062F3FFC0F660611C84C660000, N'6.1.0-30225')
GO
INSERT [__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201504201450389_AutomaticMigration', N'CP.NLayer.Data.Migrations.Configuration', 0x1F8B0800000000000400ED5D6D6FE3B811FE5EA0FF41D0A7B6C8D971B65B5C03FB0EBB79B9331A6F82387BE8B7052DD18E5089D249543646D15F761FEE27F52F94D42B5F254A96BD711A2CB0D8909A6786C3E10C5F66B2FFFDEDF7E98FCF816F3DC138F14234B327A353DB82C8095D0F6D66768AD7DF7D6FFFF8C31FFF30BD728367EB97F2BB77F43B42899299FD8871743E1E27CE230C40320A3C270E93708D474E188C811B8ECF4E4FFF3E9E4CC69040D804CBB2A6F729C25E00B31FC88F172172608453E02F4217FA49D14E7A9619AAF509043089800367F6C5DDE8D30DD8C278740930B0AD0FBE0788144BE8AF6D0B20146280898CE79F13B8C4718836CB883400FF611B41F2DD1AF8092C643FAF3F371DC6E9191DC6B8262CA19C34C161D01170F2AED0CB5824EFA55DBBD21BD1DC15D130DED25167DA238A0BD1DADBD896C8EAFCC28FE967AC6AF3691865181E4C4639ED89257E71529904B11CFA877C92FA388DE10CC114C7C03FB1EED295EF39FF80DB87F05F10CD50EAFBACA04454D2C73590A6BB388C608CB7F7705D883F776D6BCCD38D45C28A8CA1C9873647F86F7FB5AD4F843958F9B0B203460D4B1CC6F02788600C3074EF00C630461403669A94B80BBC2E08D5268CB72547627C640DD9D6023CDF40B4C18F339B4C956D5D7BCFD02D5B0A293E238F2C394284E3142AA46CE64CFF3E3CD75F809F36B125FF3462DBCCE512AE0131A843314B9CD88BF2F5B7B33E9B79DD875F8BA553B2FAE82140CD873AC3348E8917DE2E32B88C1727C2F76A017A58F74518442986C2DA9A8E6BEFD1E8532E6104621C9015D2CFAFD4F46FBEA5FF0A7FBF9705FEB61A6A713F81276F93910A82935D469CD8D63DF4B3DEE4D18BF2BDC888F67C61D7C7751C06F7A15F10315D5F1E40BC81E48B8750DDBF0CD3D8813D972805EBB73829E5DBB26CE04515F44D96E61D4892AF61ECFE0C92C7C347FE92FB12F878F7A137F3BA26A27D13155F05C0F3F7EEF78877C3C0C173B40EF7CE6B9E7C70B0F754A9F263489C0D409D357303127C136E3CF4A19A7D7212830F5E604E3ABF6B18EE6490E156BC6E4207B404B161382E40941F499B26720846756890BCDD2B0DB36C1CDD2DD696B154176BCB586C2A194569D80014DD823C59AB5A94BC4B258571C4A710FD223EA57C8BF86F1BF123F41077300EBC840AAE5E8D75BFBC26C53E69432E7DA0DA91EF704C68F212CAC3815E0A632F518FA99FAFA8E9DF3C46E30ECF3DBCC7A04CAF501A548CF91550770F7BD378FCEEEFD24B221F6C6F63971E9A2B1B7977D659EA9FE2308DF67D3A3A5A6FADDF3575F2D3E20E4AEBC8CD3C641A28FD63BD5CE6C9B50F36F52B5207BF29C30DE73689D1107BF5B7C4C65893E5E7620183158CCB851C22227E76AB3EB34FA579E3BEA501670110D8C07C6F5D504D9AA9A8FE155467F25CE45A671B3F2449E87899461919D80D3ECFF80AB956CB6EBF7683F97DD882E8D58B88268921CCECBF4863D14356F1B886644F1E3CF0E96824EB89042F18D3E8017CB24C1332AF1EC272A4F390E345C06F164320330C9154F11503B18730818846B7669D9A70E60FAAB20C152B2180B769683A664CA4D972247FA29B67BD735185D02E16A4DF5ED6C8F999AD11B3C3A0994D65A3596B07DA6B91EC6570B9AFCEEEC93C547997C5F67245DBE0B3EAE189485338E2A488A3A2DC14730931F7324E424D1D180AC9CB377369E03C406DE72A10D63DB40015E70409229F8F16E2225E4AC4B9FA5B88B9F39304C11ABE00C4CC1B3F0EEE4286F94A7367233A8776A75E0DA1D29BE461DADD3803C2CDA278A6E08769A002792323EBA0D93999B9276600DC1C36E842EB9018AC42E69DD5C09E6F3546D0367485ABEA3EF5830DB4DC3E562EA94E561AE7D94A6556D35893D6345D8028227B3526CDA968B196458ED377CBEE0940418E317612451E50256DC5896CE5C9E64CE8CD8F8ED75E9C607A95BD0274977BE106D267AC03D638969213EF63E5692A5D4DF93DFD7741C3A57A95CE58DE781494D7644074F5666383CC14EB08B3FC32E08358713F7011FA6980F41B283D759D08C462D4ADE648F9719B45C95BCC118ABC1916A26832C7E053705828BEA70B2273A2E701990E733CF618CCC2B1ED32DA742CD88DB43D956C53F011A2A91B2D0436CAECB018F491D360413411EF6751EC6ECAFFD7469307B91DCC25DB4076371435D97E4CA4CEA66031EA5673243E3D8245E37BBA23E6290F2AC4BCC71CB14E6C60D1EA5673A422538185299A3AC42C360F810B5B6C87395E9D6BC0D943D56A8EC4651BB0605C470F3C9A82A0C4A31D3DF0EA3403256ADD6D8E5D2514B088556317D7C95EC3F0BEB3E982468F7854CE5377BE30769ED901BABBF35493BDC5D7176822ECC979074361AE49BA9B4B13F19E4E2AD9DB24EFEEDD4E46533F8D882879EB311B30FF16C801723DE688C59B200B55341DFBB2E26F4B746BABBC0732583FE5A7FA45A2F2AEF4D2479B17C25F21C92A325A60359CFA458311A0876CDA471D23D9284637A9C46BAECE73CBDEEE359E3D74F349FB0C6752BE06ECA9270A34C0ECC9D78A2F68DEA45B4AF193CA2354B795C2ADE4B4B82134A8C814AF0CF34F6C8BC8FEE4B9F4BA70B94D300C727358FEEA5FF85EF62A507EB000C85BC304E76FDAF6D9E9E44C28EC7C394596E324717DC50DAB5C69C94FD901B299561E3968E0629135A52C754D27124A1BD113889D47A02A2B9923173ECFEC7F6794E7D6FC9F5F4AE22FB4E5C4CA82E6B93539B1E6C967E4FD9A92AF1EE2145AFF91D37976C84E1A40C4B33D88C8D5319632FE2900CF7F66A1FA1646EE0828A559D54AD40FBB5F9A521C7E7D2A5B5A938A1A75DCA754F1D52CCCDE169F1B7AA379BFEF6EDDAFDD84E4DBD063361EB17AED051890AAAECDC4827A97AD29C1DF77B64EB14ACD00B67B11DA504B4951733614B45862B6F2F010E5652E31789C9597751347516EA61CE9A4FB48B5D5654331108AC97433D41957553B567A188375CF92573BA553BAF28FD09FCB07CF63F6E72FCC97BFF6CD80EEBAF6984D882D56192A8689B52899E08398F68BB54F5515499F61733524036D555ED8DAE9EB825BEE616502F5255BFBA2E319758E972CB92E5E1AD942297F670972C20EBC3B1D87FACE9FF60E76B8992B5974D6584E78E4B3D5BF86864DDBEE5FE4C2A7DFF6282EE85525D39815B797CA9863AD8351BEA12873C40D2B6026D213CE2DBA843EC4D0A287427A597F011207C80FCCD93B47BB1C7919835216830293BE16D5F8A43FB845353F6FF68984DFC4B654EF61CC9C4A33A99ABF576A4FC6F37A784BD2BF431EC0865A5E77BBC6933D594FB5E511F9EFD1628CA3CC00D1CCD856F46FE887B59517EC69BE85AD1CCABB74B29503FA1575B5A85CD324CE99AA1EB4A11C344F7098D9EE8A5EA1E77BF3CEA5A22D95A22A1E1D2B49B585A42A6CE312536D85A90AB563ED694BE9A98A03DB6FCC47330EF103DD88EAEF8AA419A3E9D030ADBB74D392FF2E0535A3FD97DFCA162BD610B5155F4AF59A7B28AFAD4A43DBAB6C35E9639AC38F3A43D7ACBC969D7155FAE21ED45056B81AA8419DB526454F31A3FDA50C5D5116DC5460DC3EEB46A6AA09F1AA94C64187A89FD7D63CC49D6674A82176289B96130FC98E80F90F23C86E24F1363504FDEF231074B8BD40F50D7DE62EB7248244E527E2CB2BC4C0251B850F31F6D6C0C1A4DB81C470A9F32D7E8BCD55B082EE1CDDA6384A3119320C563E57D94BB7364DFCB3DA705EE6E96DF616910C310422A6471FCD6FD1C7D4F3DD4AEE6BC5FDA10682EE998A87003A97983E086CB61552FE0B844C800AF5555BBD0718443E014B6ED1123CC13EB211F3BB811BE06CCBFC513D48FB44F06A9F5E7A6013832029306A7AF223B1613778FEE17FB330034F37650000, N'6.1.0-30225')
GO
