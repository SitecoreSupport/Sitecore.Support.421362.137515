namespace Sitecore.Support.Data.Archiving
{
    using Sitecore.Data;
    using Sitecore.Data.Archiving;
    using Sitecore.Xml;
    using System.Xml;

    public class SqlArchiveProvider : Sitecore.Data.Archiving.SqlArchiveProvider
    {
        protected override Archive GetArchive(XmlNode configNode, Database database)
        {
            string attribute = XmlUtil.GetAttribute("name", configNode);
            if (string.IsNullOrEmpty(attribute))
            {
                return null;
            }
            return new Sitecore.Support.Data.Archiving.SqlArchive(attribute, database);
        }
    }
}