<div align="center">

  <img src="src/client/public/library.png" alt="logo" width="200" height="auto" />
  <h1>Tyle (typelibrary)</h1>
  
  <p>
    A tool for building semantically supported templates!
  </p>

  
<!-- Badges -->
<p>
  <a href="https://github.com/mimir-org/typelibrary/graphs/contributors">
    <img src="https://img.shields.io/github/contributors/mimir-org/typelibrary" alt="contributors" />
  </a>
  <a href="https://github.com/mimir-org/typelibrary/commits/main">
    <img src="https://img.shields.io/github/last-commit/mimir-org/typelibrary" alt="last update" />
  </a>
  <a href="https://github.com/mimir-org/typelibrary/issues/">
    <img src="https://img.shields.io/github/issues/mimir-org/typelibrary" alt="open issues" />
  </a>
  <a href="https://github.com/mimir-org/typelibrary/blob/master/LICENSE">
    <img src="https://img.shields.io/github/license/mimir-org/typelibrary.svg" alt="license" />
  </a>
</p>
   
<h4>
    <a href="https://github.com/mimir-org/typelibrary/issues">Report Bug</a>
  <span> · </span>
    <a href="https://github.com/mimir-org/typelibrary/issues">Request Feature</a>
  </h4>
</div>

<br />

<!-- Table of Contents -->
# :notebook_with_decorative_cover: Table of Contents

- [About the Project](#star2-about-the-project)
  * [Tech Stack](#space_invader-tech-stack)
  * [Features](#dart-features)
  * [Design system](#art-design-system)
  * [Environment Variables](#key-environment-variables)
- [Getting Started](#toolbox-getting-started)
  * [Prerequisites](#bangbang-prerequisites)
  * [Running Locally](#running-running)
- [Usage](#eyes-usage)
- [Contributing](#wave-contributing)
  * [Code of Conduct](#scroll-code-of-conduct)
- [License](#warning-license)
- [Contact](#handshake-contact)
- [Acknowledgements](#gem-acknowledgements)
  

<!-- About the Project -->
## :star2: About the Project


<!-- TechStack -->
### :space_invader: Tech Stack

<details>
  <summary>Client</summary>
  <ul>
    <li><a href="https://www.typescriptlang.org/">Typescript</a></li>
    <li><a href="https://reactjs.org/">React.js</a></li>
    <li><a href="https://reactrouterdotcom.fly.dev/">React Router</a></li>
    <li><a href="https://react-hook-form.com/">React Hook Form</a></li>
    <li><a href="https://react-query.tanstack.com/">React Query</a></li>
    <li><a href="https://axios-http.com/">Axios</a></li>
    <li><a href="https://fakerjs.dev/">Faker</a></li>
    <li><a href="https://storybook.js.org/">Storybook</a></li>
    <li><a href="https://www.framer.com/motion/">Framer Motion</a></li>
    <li><a href="https://www.radix-ui.com/">Radix UI (Primitives)</a></li>
    <li><a href="https://styled-components.com/">styled-components</a></li>
    <li><a href="https://styled-icons.dev/">styled-icons</a></li>
    <li><a href="https://polished.js.org/">polished</a></li>
  </ul>
</details>

<details>
  <summary>Server</summary>
  <ul>
    <li><a href="https://dotnet.microsoft.com/en-us/languages/csharp">C#</a></li>
    <li><a href="https://docs.microsoft.com/en-us/aspnet/core/">ASP.NET</a></li>
    <li><a href="https://docs.microsoft.com/en-us/azure/active-directory/develop/">MSAL.NET</a></li>
    <li><a href="https://www.newtonsoft.com/json">Json.NET</a></li>    
    <li><a href="https://docs.microsoft.com/en-us/ef/">Entity Framework</a></li>
    <li><a href="https://automapper.org/">AutoMapper</a></li>
    <li><a href="https://xunit.net/">xUnit.NET</a></li>  
    <li><a href="https://github.com/domaindrivendev/Swashbuckle.AspNetCore">Swashbuckle</a></li>    
    <li><a href="https://github.com/moq/moq4">Moq</a></li>   
    <li><a href="https://sendgrid.com/">Sendgrid</a></li>
    <li><a href="https://github.com/pankleks/TypeScriptBuilder">TypeScriptBuilder</a></li>
  </ul>
</details>

<details>
<summary>Database</summary>
  <ul>
    <li><a href="https://www.microsoft.com/en-us/sql-server/">MSSQL</a></li>
  </ul>
</details>

<details>
<summary>DevOps</summary>
  <ul>
    <li><a href="https://www.docker.com/">Docker</a></li>
    <li><a href="https://github.com/features/actions">Github Actions</a></li>
    <li><a href="https://www.terraform.io/">Terraform</a></li>
  </ul>
</details>

<!-- Features -->
### :dart: Features

Coming soon...

<!-- Design System -->
### :art: Design System

All resusable components and the design tokens that they consume can be viewed in our storybook

<a href="https://github.com/mimir-org/typelibrary/tree/dev/src/client/src/complib">Component library</a>

```bash
cd src/client

npm install

npm run storybook
```

<!-- Env Variables -->
### :key: Environment Variables

#### Client

To run this project, you will need to add the following environment variables to your .env.local file

`REACT_APP_API_BASE_URL`

`REACT_APP_SOCKET_BASE_URL`

If you are running the server locally then the values will most likely be  
```js
// where x and y = api version
REACT_APP_API_BASE_URL = http://localhost:5001/v{x}.{y}/
REACT_APP_SOCKET_BASE_URL = http://localhost:5001/
```

#### Server

To run this project, you will need to add the following environment variables to your .appsettings.local.json file inside the TypeLibrary.Api project

```json
{
  "DatabaseConfiguration": {
    "DataSource": "database source",
    "Port": "database port",
    "InitialCatalog": "database name",
    "DbUser": "username",
    "Password": "password"
  },
  "MimirorgAuthSettings": {
     "DatabaseConfiguration": {
     "DataSource": "database source",
     "Port": "database port",
     "InitialCatalog": "database name",
      "DbUser": "username",
      "Password": "password"
    }
  }
}
```

Here is an example of local file from a developer running the database in a docker container
```json
{
  "DatabaseConfiguration": {
    "DataSource": "localhost",
    "Port": 1433,
    "InitialCatalog": "TypeLibrary",
    "DbUser": "sa",
    "Password": "locallysourcedpassword"
  },
  "MimirorgAuthSettings": {
    "DatabaseConfiguration": {
      "DataSource": "localhost",
      "Port": 1433,
      "InitialCatalog": "MimirorgAuthentication",
      "DbUser": "sa",
      "Password": "locallysourcedpassword"
    }
  }
}
```

<!-- Getting Started -->
## 	:toolbox: Getting Started

<!-- Prerequisites -->
### :bangbang: Prerequisites

This project uses .NET 6 for the server and NPM as package manager for the client,
make sure that you have these installed before continuing.

Start by cloning the project
```git 
git clone git@github.com:mimir-org/typelibrary.git
```

Navigate to the new directory
```bash
cd ./typelibrary
```

<!-- Running Locally -->
### :running: Running
|                         | Client      | Server      |
| ----------------------- | ----------- | ----------- |
| :gear: Installation     | ```cd src/client``` <br /> ```npm install```   | ```cd src/server``` <br /> ```dotnet build```      |
| :running: Run Locally   | ```cd src/client``` <br /> ```npm run local``` | ```cd src/server/TypeLibrary.Api``` <br /> ```dotnet run```      |


<!-- Usage -->
## :eyes: Usage

Coming soon...


<!-- Contributing -->
## :wave: Contributing

Coming soon...

<!-- Code of Conduct -->
### :scroll: Code of Conduct

Coming soon...


<!-- License -->
## :warning: License

Distributed under the MIT License. See LICENSE.txt for more information.

<!-- Contact -->
## :handshake: Contact

Mimir-org - orgmimir@gmail.com

Project Link: [https://github.com/mimir-org/typelibrary](https://github.com/mimir-org/typelibrary)

<!-- Acknowledgments -->
## :gem: Acknowledgements

 - [Material Design](https://m3.material.io/)
