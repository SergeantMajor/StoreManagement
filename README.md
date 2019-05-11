# StoreManagement
Simple app to manage stores

Possible actions:
1. Show list of stores from excel file
2. Add new store/update existing store/remove store from file
3. Show total stock/min/max values

To start app:
1. Clone repository
2. Enter clientApp folder => in command line type npm install => ng build
3. Make sure that excel file "Site Data" with corresponding values are placed into {solutionDir}/Data folder
    (path could be edited in appSettings.json file
4. Open solution file (.sln)
5. Build app
6. Press F5

Server API - ASP.NET Core;

UI part - Angular 7;

1. To see the list of stores - just start app.
2. To add new store - press Add Store, fill all mandatory fields and press Save.
3. To modify store - start typing inside table cell if store list table. Changes are saving automatically when cell lost focus.
4. To remove store - press Remove.
5. Numeric fields must be typed only with numeric values.
6. Email fields and Country Code must be in specific format.

