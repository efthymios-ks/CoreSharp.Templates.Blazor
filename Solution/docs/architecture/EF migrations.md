### Setup migration 
1. Install `Microsoft.EntityFrameworkCore.Design` in the start-up project the `DbContext` is regirested to.  
Usually `Web` > `Server` > `WebApp`.  
2. Install `Microsoft.EntityFrameworkCore.Tools` in the project the migrations are going to be created to.  
Usually `Infrastructure` > `Infrastructure`.  

### Add initial migration 
1. Set `WebApp` as start-up project. 
2. Open `Package Manager Console`. 
2. Set `Default project` > `Infrastructure`. 
4. Run `Add-Migration MyMigrationName` to create migration. 
5. Run `Update-Database` to update to the latest migration. 

### Remove migration 
1. Find the migration you want to rollback to. 
2. Run `Update-Database MigrationNameToRollbackTo`. 
3. Run `Remove-Migration` (repeatedly, until you reach the target migration). 
