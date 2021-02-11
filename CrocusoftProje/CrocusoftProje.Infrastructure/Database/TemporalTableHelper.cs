using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrocusoftProje.Infrastructure.Database
{
    public static class TemporalTableHelper
    {
        public static void CreateTemporalTables(CrocusoftProjeDbContext context)
        {
            var entityTypes = context.Model.GetEntityTypes();
            var config = new TemporalTableConfiguration();

            foreach (var entityType in entityTypes)
            {
                if (config.TemporalTypes.Contains(entityType.ClrType))
                {
                    AddAsTemporalTable(context, entityType);
                }
                else
                {
                    RemoveAsTemporalTable(context, entityType);
                }
            }
        }

        public static string RemoveAsTemporalTableSql(string tableName, string schemaName)
        {
            var temporalTableName = GetTemporalTableName(tableName);
            return $@"IF EXISTS (SELECT * FROM sys.[tables] t INNER JOIN sys.schemas s ON s.schema_id = t.schema_id WHERE t.name = '{tableName}' AND temporal_type = 2 and s.name = '{schemaName}')
                                BEGIN
                                    ALTER TABLE [{schemaName}].[{tableName}] SET (SYSTEM_VERSIONING = OFF);
                                    ALTER TABLE [{schemaName}].[{tableName}] DROP PERIOD FOR SYSTEM_TIME;
                                    ALTER TABLE [{schemaName}].[{tableName}] DROP DF_{tableName}_SysStartTime, DF_{tableName}_SysEndTime
                                    ALTER TABLE [{schemaName}].[{tableName}] DROP COLUMN SysStartTime, COLUMN SysEndTime
                                    DROP TABLE [{schemaName}].[{temporalTableName}]
                                END
                            ";
        }

        private static void AddAsTemporalTable(CrocusoftProjeDbContext context, IEntityType entityType)
        {
            var tableName = entityType.GetTableName();
            var temporalTableName = GetTemporalTableName(entityType);
            var schemaName = entityType.GetSchema() ?? "dbo";
            string query = $@"
                    IF NOT EXISTS (SELECT * FROM sys.[tables] t INNER JOIN sys.schemas s ON s.schema_id = t.schema_id WHERE t.name = '{tableName}' AND temporal_type = 2 and s.name = '{schemaName}')
                    BEGIN
                        ALTER TABLE [{schemaName}].[{tableName}] 
                        ADD  SysStartTime datetime2(7) GENERATED ALWAYS AS ROW START HIDDEN    
                                constraint DF_{tableName}_SysStartTime DEFAULT DATEADD(second, -1, SYSUTCDATETIME())  
                            , SysEndTime datetime2(7)  GENERATED ALWAYS AS ROW END HIDDEN     
                                constraint DF_{tableName}_SysEndTime DEFAULT '9999-12-31 23:59:59.9999999'  
                            , PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime);
 
                        ALTER TABLE [{schemaName}].[{tableName}]    
                        SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [{schemaName}].{temporalTableName})); 
                    END
                ";
            context.Database.ExecuteSqlRaw(query);
        }

        private static void RemoveAsTemporalTable(CrocusoftProjeDbContext context, IEntityType entityType)
        {
            var tableName = entityType.GetTableName();
            var schemaName = entityType.GetSchema() ?? "dbo";
            var query = RemoveAsTemporalTableSql(tableName, schemaName);
            context.Database.ExecuteSqlRaw(query);
        }

        private static string GetTemporalTableName(IEntityType entityType)
        {
            var tableName = entityType.GetTableName();
            return GetTemporalTableName(tableName);
        }

        private static string GetTemporalTableName(string tableName)
        {
            return $"{tableName}History";
        }
    }
}
