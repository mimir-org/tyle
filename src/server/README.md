<div align="center">
  <img src="/src/server/images/library.png" alt="logo" width="100" height="auto" />
  <h1>Tyle</h1>
  <h4>TypeLibrary</h4>
  <p>Server API for creating types!</p>
</div>

<br />

<h4>Where is da code?</h4>
<a href="https://github.com/mimir-org/typelibrary">TypeLibrary on GitHub</a>
```bash
git clone git@github.com:mimir-org/typelibrary.git
```

<h4>Do I need a database?</h4>
<p>Yes. TypeLibrary is using a MS SQL database. If you use a local database remember to set correct <i>server authentication</i>.</p>  
<img src="/images/SqlDbProperty.png" alt="logo" width="400" height="auto" />

<h4>Do I need to do something with my database?</h4>
<p>Yes. You need to create two empty databases:<p>
<img src="/images/SqlDatabases.png" alt="logo" width="200" height="auto" />
<p>You also need to add a user <i>mimir</i> under security logins:</p>
<img src="/images/SqlMimirUser.png" alt="logo" width="300" height="auto" />
<p>Give the <i>mimir</i> user <i>db_owner</i> and <i>public</i> properties for <i>MimirorgAuthentication</i> and <i>TypeLibrary</i>:</p>
<img src="/images/SqlMimirMapping.png" alt="logo" width="200" height="auto" />

<h4>But what if I mess up the database during development?</h4>
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
