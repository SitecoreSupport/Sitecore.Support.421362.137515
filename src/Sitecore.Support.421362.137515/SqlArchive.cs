namespace Sitecore.Support.Data.Archiving
{
    using Sitecore.Data;
    using Sitecore.Data.DataProviders.Sql;
    using Sitecore.Security.Accounts;
    using System.Collections.Generic;

    public class SqlArchive : Sitecore.Data.Archiving.SqlArchive
    {
        public SqlArchive(string name, Database database) : base(name, database)
        {
        }

        protected override int GetEntryCount(User user)
        {
            List<string> list = new List<string>(new string[] { "archiveName", base.Name });
            string sql = "SELECT COUNT(*) FROM {0}Archive{1}\r\n        WHERE {0}ArchiveName{1} = {2}archiveName{3}";
            if (!((user == null) || user.IsAdministrator))
            {
                //sql = sql + " AND {0}ArchivedBy{1} = {2}archivedBy{3}";
                sql = sql + " AND {0}ArchivalId{1} IN (SELECT {0}ArchivalId{1}\r\n          FROM {0}ArchivedVersions{1} WHERE {0}ArchivedBy{1} = {2}archivedBy{3}) ";
                list.AddRange(new string[] { "archivedBy", user.Name });
            }
            using (DataProviderReader reader = base.Api.CreateReader(sql, list.ToArray()))
            {
                return (!reader.Read() ? 0 : base.Api.GetInt(0, reader));
            }
        }
    }
}