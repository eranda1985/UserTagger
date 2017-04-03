using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.Core.Constants;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Models;

namespace UniSA.UserTagger.Core.Repository
{
    public class ScholarshipUserRepository : DBContext, IRepository<ScholarshipUserModel>
    {
        private Logger.Logger _logger; 

        public ScholarshipUserRepository():base(ConnectionStrings.ScholashipsDB)
        {
            _logger = new Logger.Logger(GetType());
        }
        public void Add(ScholarshipUserModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ScholarshipUserModel entity)
        {
            throw new NotImplementedException();
        }

        public ScholarshipUserModel FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<ScholarshipUserModel> List(string sql = null)
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
                    return Context.Fetch<ScholarshipUserModel>();
                }
            }

            using (Context)
            {
                return Context.Fetch<ScholarshipUserModel>(sql);
            }
        }

        public void Update(ScholarshipUserModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
