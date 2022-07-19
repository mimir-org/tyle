<div align="center">
  <br/>
  
  <img src="src/client/src/assets/icons/logo/logoWhite.svg" alt="logo" width="200" height="auto" />
  
  <br/>
  <br/>
  
  <p>A tool for building semantically supported templates!</p>

  <div>
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
  </div>

  <div>
    <a href="https://github.com/mimir-org/typelibrary/releases">
      <img src="https://img.shields.io/github/v/release/mimir-org/typelibrary" alt="releases">
    </a>
    <a href="https://hub.docker.com/repository/docker/mimirorg/typelibrary-client">
      <img alt="Docker client" src="https://img.shields.io/docker/v/mimirorg/typelibrary-client?label=docker%20client">
    </a>
    <a href="https://hub.docker.com/repository/docker/mimirorg/typelibrary-server">
      <img alt="Docker server" src="https://img.shields.io/docker/v/mimirorg/typelibrary-server?label=docker%20server">
    </a>
  </div>

  <div>
    <a href="https://github.com/mimir-org/typelibrary/actions/workflows/main.yaml">
      <img src="https://github.com/mimir-org/typelibrary/actions/workflows/main.yaml/badge.svg?branch=main" alt="build status" />
    </a>
    <a href="https://github.com/mimir-org/typelibrary/actions/workflows/dev.yaml">
      <img src="https://github.com/mimir-org/typelibrary/actions/workflows/dev.yaml/badge.svg?branch=dev" alt="build status" />
    </a>
  </div>
  
  <h4>
  <a href="https://github.com/mimir-org/typelibrary/issues">Report a bug or request a feature</a>
  </h4>
</div>

# :notebook_with_decorative_cover: Table of Contents

- [:notebook_with_decorative_cover: Table of Contents](#notebook_with_decorative_cover-table-of-contents)
  - [:star2: About the Project](#star2-about-the-project)
    - [:space_invader: Tech Stack](#space_invader-tech-stack)
    - [:art: Design System](#art-design-system)
    - [:key: Environment Variables](#key-environment-variables)
      - [Client](#client)
      - [Server](#server)
  - [:toolbox: Getting Started](#toolbox-getting-started)
    - [:bangbang: Prerequisites](#bangbang-prerequisites)
    - [:running: Running](#running-running)
  - [:wave: Contributing](#wave-contributing)
    - [:scroll: Code of Conduct](#scroll-code-of-conduct)
  - [:warning: License](#warning-license)
  - [:handshake: Contact](#handshake-contact)
  
## :star2: About the Project

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
    <li><a href="https://react.i18next.com/">react-i18next</a></li>
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

### :art: Design System

All resusable components and the design tokens that they consume can be viewed in our storybook

<a href="https://github.com/mimir-org/typelibrary/tree/dev/src/client/src/complib">Component library</a>

```bash
cd src/client

npm install

npm run storybook
```

### :key: Environment Variables

#### Client

To adjust the environment variables for the client in development you can edit the .env file in the root of the client folder.  
For the production build, you have to set the environment variables into the container itself.  
You can override the .env with a .env.local file, this file is not included in git repo.

`REACT_APP_API_BASE_URL` - Url to backend server

If you are running the server locally then the values will most likely be  

```js
// where x and y = api version
REACT_APP_API_BASE_URL = http://localhost:5001/v{x}.{y}/
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
  },
  "ApplicationSettings": {
    "ApplicationSemanticUrl": "path to an ontology for types created via application",
    "ApplicationUrl": "backend url"
  },
  "CorsConfiguration": {
    "ValidOrigins": "client url"
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
  },
  "ApplicationSettings": {
    "ApplicationSemanticUrl": "http://localhost:5001/v1/ont",
    "ApplicationUrl": "http://localhost:5001"
  },
  "CorsConfiguration": {
    "ValidOrigins": "http://localhost:3001"
  }
}
```

## :toolbox: Getting Started

### :bangbang: Prerequisites

This project uses .NET 6 for the server and NPM as package manager for the client,
make sure that you have these installed before continuing.

Start by cloning the project

```bash
git clone git@github.com:mimir-org/typelibrary.git
```

Navigate to the new directory

```bash
cd ./typelibrary
```

### :running: Running

|                         | Client      | Server      |
| ----------------------- | ----------- | ----------- |
| :gear: Installation     | ```cd src/client``` <br /> ```npm install```   | ```cd src/server``` <br /> ```dotnet build```      |
| :running: Run Locally   | ```cd src/client``` <br /> ```npm run start``` | ```cd src/server/TypeLibrary.Api``` <br /> ```dotnet run```      

## :wave: Contributing

We welcome community pull requests for bug fixes, enhancements, and documentation. See [how to contribute](./CONTRIBUTING.md) for more information.

### :scroll: Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behavior in our community. See [.NET Foundation Code of Conduct](https://dotnetfoundation.org/about/code-of-conduct) for more information.

## :warning: License

Distributed under the MIT License. See [license](./LICENSE) for more information.

## :handshake: Contact

Mimir-org - orgmimir@gmail.com

Project Link: [https://github.com/mimir-org/typelibrary](https://github.com/mimir-org/typelibrary)
