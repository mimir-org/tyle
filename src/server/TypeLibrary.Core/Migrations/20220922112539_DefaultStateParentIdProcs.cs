using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class DefaultStateParentIdProcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var updateStateProc = @"CREATE OR ALTER PROCEDURE dbo.UpdateState @TableName VARCHAR(128), @State VARCHAR(31), @IdList VARCHAR(max)
                AS
                BEGIN

                    IF OBJECT_ID('tempdb..#TempIds') IS NOT NULL
					BEGIN
						DROP TABLE #TempIds
					END

                    SET NOCOUNT ON;
                    DECLARE @Sql NVARCHAR(MAX);
                    DECLARE @SqlSelect NVARCHAR(MAX);

                    SELECT value INTO #TempIds FROM STRING_SPLIT(@IdList, ',');

	                SET @Sql = N'UPDATE ' + QUOTENAME(@TableName) + N' SET State = '''+ @State +''' WHERE Id IN (SELECT value FROM #TempIds)';
                    SET @SqlSelect = N'SELECT COUNT(*) AS Number FROM ' + QUOTENAME(@TableName) + N' WHERE State = '''+ @State +'''';

                    EXECUTE sp_executesql @Sql

                    IF OBJECT_ID('tempdb..#TempIds') IS NOT NULL
					BEGIN
						DROP TABLE #TempIds
					END
                    
                    EXECUTE sp_executesql @SqlSelect

                END
            ";

            var updateParentId = @"CREATE OR ALTER PROCEDURE dbo.UpdateParentId @TableName VARCHAR(128), @OldId VARCHAR(128), @NewId VARCHAR(128)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Sql NVARCHAR(MAX);
                    DECLARE @SqlSelect NVARCHAR(MAX);

	                SET @sql = N'UPDATE ' + QUOTENAME(@TableName) + ' SET ParentId = '''+ @NewId +''' where ParentId = '''+ @OldId +'''';
                    SET @SqlSelect = N'SELECT COUNT(*) AS Number FROM ' + QUOTENAME(@TableName) + N' WHERE ParentId = '''+ @NewId +'''';
                    
                    EXECUTE sp_executesql @Sql
                    EXECUTE sp_executesql @SqlSelect                    
                END
            ";

            var hasCompany = @"CREATE OR ALTER PROCEDURE dbo.HasCompany @TableName VARCHAR(128), @Id VARCHAR(128)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Sql NVARCHAR(MAX);
                    
	                SET @sql = N'SELECT CompanyId FROM ' + QUOTENAME(@TableName) + N' WHERE Id = '''+ @Id +'''';
	                EXECUTE sp_executesql @Sql                    
                END
            ";

            migrationBuilder.Sql(updateStateProc);
            migrationBuilder.Sql(updateParentId);
            migrationBuilder.Sql(hasCompany);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.UpdateState");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.UpdateParentId");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.HasCompany");
        }
    }
}