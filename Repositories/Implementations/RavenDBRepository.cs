using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Document;

namespace Repositories.Implementations
{
    public class RavenDBRepository
    {
        public RavenDBRepository()
        {
            var documentStore = new DocumentStore { ConnectionStringName = "localhost" }.Initialize();
        }
    }
}
