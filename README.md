# CustomerManagementSystem
The Solution has API (back-end) project which has dependency on Infrastructure library with their respective tests projects too
Similary for the App (fron-end) project which has dependency on Core library with their respective test project.

The controllers expose ReST-ful HTTP endpoints to allow Blazor WebAssembly as the front-end to interact with the API using ASP.NET Core (TargetFramwork 6.0) and C# throughout the implementation.
Note: to run the application which contains data seeding, you will need to set Solution Property page to 'Multiple startup projects' by right click on the global project Solution on the top, then select 'Properties'. When the properties windows opens make sure you tick on both CustomerManagementSystem.API action dropdown to 'start' and CustomerMangementSystem.App action to 'start' then apply the changes to validate both api and UI project to run simultaneously for your exploratory.

