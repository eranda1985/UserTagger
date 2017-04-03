using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Repository
{
    public class DBContext : IDisposable
    {
        protected IDatabase Context;
        public void Dispose()
        {
            if(Context != null)
            {
                Context.Dispose();
            }
        }

        public DBContext(string connString)
        {
            Context = new Database(connString);
        }
    }
}
