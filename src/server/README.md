<div align="center">
  <img src="images/library.png" alt="logo" width="100" height="auto" />
  <h1>Tyle</h1>
  <h4>TypeLibrary</h4>
  <p>Server API for creating types!</p>
</div>

<br />

<h3>Where is da code?</h3>
<a href="https://github.com/mimir-org/typelibrary">TypeLibrary (Tyle) on GitHub</a>
```bash
git clone git@github.com:mimir-org/typelibrary.git
```
<h3>Git branches?</h3>
We use the 'dev' branch during sprints and the 'main' branch for production. Remember to checkout the 'dev' branch after you clone the project. There could be more branches (e.g. 'stage').
```bash
git fetch
git checkout dev
```

<h3>Docker setup and database?</h3>
If you want to use docker please see the 'Git clone and Docker setup' section in the README.md at the root in 'mimir' folder (git@github.com:mimir-org/mimir.git)

<h3>Local setup and database?</h3>
<p>TypeLibrary is using a MS SQL database. Remember to set the correct <i>server authentication</i>.</p>  
<img src="/Images/SqlDbProperty.PNG" alt="logo" width="400" height="auto" />

<h3>Do I need to do something with my local database?</h3>
<p>Yes. You need to create two empty databases:<p>
<img src="/images/SqlDatabases.png" alt="logo" width="200" height="auto" />
<p>You also need to add a user <i>mimir</i> under security logins:</p>
<img src="/images/SqlMimirUser.png" alt="logo" width="300" height="auto" />
<p>Give the <i>mimir</i> user <i>db_owner</i> and <i>public</i> properties for <i>MimirorgAuthentication</i> and <i>TypeLibrary</i>:</p>
<img src="/images/SqlMimirMapping.png" alt="logo" width="200" height="auto" />

<h3>But what if I mess up the local/docker database during development?</h3>
<p>Do not despair! You can use this KILL script to restore you local database to its virginity:</p>
```bash
USE [master];
DECLARE @kill varchar(8000) = '';
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'
FROM sys.dm_exec_sessions
WHERE database_id = db_id('TypeLibrary')
EXEC(@kill);
DROP DATABASE TypeLibrary
CREATE Database TypeLibrary
```
<p>Remember to remap the <i>mimir</i> user in security logins to have <i>db_owner</i> and <i>public</i> property in user mapping page.</p>

<h3>Do I need any special configuration file when running the solution?</h3>
Yes you do. Located in 'TypeLibrary.API' you will find the 'appsettings.json' file. Add a new file at the same location and name it 'appsettings.json.local'. This file should look like this:
```bash
{
  "DatabaseConfiguration": {
    "DataSource": "127.0.0.1",
    "Port": 1433,
    "InitialCatalog": "TypeLibrary",
    "DbUser": "sa",
    "Password": "P4ssw0rd1"
  },
  "MimirorgAuthSettings": {
    "DatabaseConfiguration": {
      "DataSource": "127.0.0.1",
      "Port": 1433,
      "InitialCatalog": "MimirorgAuthentication",
      "DbUser": "sa",
      "Password": "P4ssw0rd1"
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
To keep it simple in this example we use db user: 'sa' and db passord: 'P4ssw0rd1'. You can change this to your liking.

<h3>The code</h3>
<h5>Overview</h5>
<img src="images/code_overview.png" alt="logo" width="250" height="auto" />
</br>
Set 'TypeLibrary.Api' as the startup project.

<h5>Tests</h5>
<img src="images/code_tests.png" alt="logo" width="250" height="auto" />
</br>
Here are all the tests located.


<h5>Mimirorg.Authentication</h5>
<img src="images/code_authentication.png" alt="logo" width="250" height="auto" />
</br>
The 'Mimirorg.Authentication' projects is used to authenticate users and have it's own database 'MimirorgAuthentication'. It also has 'hooks' the invalidate cache. 

