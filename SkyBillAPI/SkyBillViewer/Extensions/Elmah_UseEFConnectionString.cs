using System.Collections;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using Elmah;

namespace Web.Extensions
{
    public class ElmahUseEfConnString : SqlErrorLog
    {
        protected string ConnectionStringName;
        public ElmahUseEfConnString(IDictionary config) : base(config)
        {
            ConnectionStringName = (string)config["connectionStringName"];
        }

        public override string ConnectionString
        {
            get
            {
                var connString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
                var entityBuilder = new EntityConnectionStringBuilder(connString);

                // read the db connectionstring
                return entityBuilder.ProviderConnectionString;
            }
        }
    }
}