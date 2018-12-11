using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Models;

namespace UniSA.UserTagger.Core.Repository
{
    public class GenericUserRepository: DBContext, IRepository<GenericUserModel>
    {
        private Logger.Logger _logger;

        public GenericUserRepository(string constr) : base(constr)
        {
            _logger = new Logger.Logger(GetType());
        }
        public void Add(GenericUserModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(GenericUserModel entity)
        {
            throw new NotImplementedException();
        }

        public GenericUserModel FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<GenericUserModel> List(string sql = null)
        {
            if (Context == null)
            {
                _logger.Error("DBContext cannot be null");
                return null;
            }

            if (string.IsNullOrEmpty(sql))
            {
                using (Context)
                {
                    return Context.Fetch<GenericUserModel>();
                }
            }

            using (Context)
            {
                return Context.Fetch<GenericUserModel>(sql);
            }
        }

        public void Update(GenericUserModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
