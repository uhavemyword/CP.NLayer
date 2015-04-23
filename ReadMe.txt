1. Introduction
--This is a N-Layer architecture demo.

--Key words:
WPF
WCF
MVC
MVVM
EntityFramework
Prism
Unity
T4 template
Localization
Ajax
jQuery
jQuery-ui
Blueprint
DependencyInjection
UnitofWork
Repository
Autofac
log4net
ELMAH
Glimpse

--There're 3 excutable applications:
----CP.NLayer.Client.WpfClient.Main
----CP.NLayer.Service.ConsoleHost
----CP.NLayer.Web.Mvc4

--Before run any application:
----Create your empty database and modify the connection string in Database.config(under project CP.NLayer.Data).
----Build the whole solution and run the test case - InitialDatabaseWithDataTest to insert sample data.

2. About CP.NLayer.Client.WpfClient.Main
--Plugins are loose coupled, they are discovered by location, not by reference. 
--Make sure build the whole solution first, or you won't find any plugin.
--The client has 2 ways to connect to the DB - one way is call WCF service,
and the other is call service dll directly. The switch is set in the app.config.

--If you switch to WCF service, you need to start CP.NLayer.Service.ConsoleHost first.

3. About CP.NLayer.Web.Mvc4
--Basic CRUD (Create, read, update and delete)
----There is a base CrudContorller for handling basic CRUD;
----There are 3 common views under the controller, "index", "list" and "details".
"index" view just contain a specific DIV element(contains data-source attribut), 
the javascript will asyncly update the DIV by fetching data from "list" view.
The "details" view is shared for CRUD actions.

--To enable ELMAH(Error Logging Module And Handling), 
run the src\Web\Mcv4\App_Readme\Elmah.SqlServer.sql script against your database first.