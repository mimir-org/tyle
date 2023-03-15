using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class DefaultStateParentIdProcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var updateStateProc = @"CREATE OR ALTER PROCEDURE dbo.UpdateState @TableName VARCHAR(128), @State VARCHAR(31), @IdList VARCHAR(max)
                AS
                BEGIN
                    SET NOCOUNT ON;

                    DECLARE @Sql NVARCHAR(MAX);
                    DECLARE @SqlSelect NVARCHAR(MAX);

                    SET @Sql = N'UPDATE ' + QUOTENAME(@TableName) + N' SET State = @State WHERE Id IN (SELECT CAST(value AS INT) FROM STRING_SPLIT(@IdList, '',''))';
                    SET @SqlSelect = N'SELECT COUNT(*) AS Number FROM ' + QUOTENAME(@TableName) + N' WHERE State = @State';

                    EXECUTE sp_executesql @Sql, N'@State VARCHAR(31)', @State = @State;

                    EXECUTE sp_executesql @SqlSelect;

                END
            ";

            var updateParentId = @"CREATE OR ALTER PROCEDURE dbo.UpdateParentId @TableName VARCHAR(128), @OldId INT, @NewId INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Sql NVARCHAR(MAX);
                    DECLARE @SqlSelect NVARCHAR(MAX);

	                SET @sql = N'UPDATE ' + QUOTENAME(@TableName) + N' SET ParentId = ' + CONVERT(NVARCHAR(MAX), @NewId) + N' WHERE ParentId = ' + CONVERT(NVARCHAR(MAX), @OldId) + N';';
                    SET @SqlSelect = N'SELECT COUNT(*) AS Number FROM ' + QUOTENAME(@TableName) + N' WHERE ParentId = ' + CONVERT(NVARCHAR(MAX), @NewId) + N';';
                    
                    EXECUTE sp_executesql @Sql
                    EXECUTE sp_executesql @SqlSelect                    
                END
            ";

            var hasCompany = @"CREATE OR ALTER PROCEDURE dbo.HasCompany @TableName VARCHAR(128), @Id INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Sql NVARCHAR(MAX);
                    
	                SET @sql = N'SELECT CompanyId FROM ' + QUOTENAME(@TableName) + N' WHERE Id = ' + CONVERT(NVARCHAR(MAX), @Id) + N';';
	                EXECUTE sp_executesql @Sql                    
                END
            ";

            migrationBuilder.Sql(updateStateProc);
            migrationBuilder.Sql(updateParentId);
            migrationBuilder.Sql(hasCompany);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.UpdateState");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.UpdateParentId");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.HasCompany");
        }
    }
}
