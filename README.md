# AuctionWoodcutsEngravingsAngularDotnetCRUD

An auction application of woodcuts and engravings that can be run either locally or with _Docker Desktop/Docker_. 

New auctions can be added and updated. Bids can be placed on auctions (with SignalR).

The backend (C#/.NET10 with _Hangfire_) can close an auction that has ended.

This application consists of:

1.  Backend: C#/.NET10 WebApi (Clean Architecture) + EntityFramework + Hangfire.
2.  Frontend: _Angular application_ ([Angular CLI](https://github.com/angular/angular-cli) version 22.2.7) that makes use of the HttpResourceApi + _daisyUI/TailwindCSS_.
3.  Database: _PostgreSQL_ (version 17).

Functionality:

*   Create, update and delete auctions.
*   From the frontend application auctions can be sorted and filtered.
*   Upload images (as byte array).
*   The C#/.NET10 WebApi uses Hangfire for schelduling jobs and closing Auctions.
*   Both the Angular application and the C#/.NET10 WebApi uses SignalR for the auction bids.

From the WebApi auctions bids by users can be simulated and auctions can be stopped within 3 minutes. 

See the endpoints: _/helpers/SimulateBidForUser_ and _/helpers/EndAuctionSoon_

Further are the pages of the frontend application responsive.

See the images at the root of this project for examples.

## Running the application

### _1\. Docker Desktop / Docker_

To see the application in action within a _Docker Container_, use the command from the root of this project:

**docker compose up --build -d**

And to remove the _Docker Container_ use the command command from the root of this project:

**docker compose down -v**

Note: _\-v_ also deletes all data in the Docker database.

### _2\. Local installation_

A) PostgreSQL database  
See the folder 'postgresql\_database' in this project. Run from that folder the command:  
**docker compose up --build -d**

This will setup the _PostgreSQL_ database.

B) Backend C#/.NET WebApi  
.NET10 is needed, check wether this installed with the command:  
_dotnet --list-sdks_

When not installed, download and install it from the link: [download .NET10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

Install the **dotnet ef-tool** - version: 10.0.10 or above

When the tool is installed, run the command for a _database migration:_

**dotnet ef database update**

Or use the longer command (and replace \*_your\_workspace\*_ by the location of your workspace):

_dotnet ef database update -p "\*your\_workspace\*\\woodcuts-and-engravings-auction-angular-dotnet\\backend\\Infrastructure\\Infrastructure.csproj" --startup-project "\*your\_workspace\*\\woodcuts-and-engravings-auction-angular-dotnet\\backend\\AuctionWoodcutsEngravingsWebApi\\AuctionWoodcutsEngravingsWebApi.csproj"_

For more information see the link below:

[https://learn.microsoft.com/en-us/ef/core/cli/dotnet](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

C) Angular 22 application.

**Angular 22** needs a **Node.js** version of at least _22.22.4_

**Command to install Angular and dependencies (from the frontend folder)**

_npm install_

or shorter:

_npm i_

**Command to run the application (from the frontend folder):**

_ng serve --open_

or shorter:

_ng s --o_

### **Changelog:**

_November 2026_

**Backend:**

\- Solved a major issue with the consumption of a lot of memory/RAM by the process 'VBCSCompiler' during a build or  
rebuild of the WebApi.  
Cause: the images were included in the _Database Migrations_ and this data was written into the C# source code.  
And also _builder.HasData()_ is not meant for large byte arrays (for files/images).  
Solution: creation of new _Database Migrations_ without the images as byte array.  
At startup of the WebApi, the images are added to the database once.

\- Updated some _Nuget packages_ of the _WebApi_.

\- Added 10 new images (5 new woodcuts and 5 new engravings).

**Frontend:**

\- Updated packages.

_August 2026_

*   First commit.