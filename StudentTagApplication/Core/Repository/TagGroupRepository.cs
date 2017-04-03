using UniSA.UserTagger.Core.Constants;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogger = UniSA.UserTagger.Core.Logger;

namespace UniSA.UserTagger.Core.Repository
{
    public class TagGroupRepository : DBContext, IRepository<TagGroupModel>
    {
        private AppLogger.Logger _logger;

        public TagGroupRepository(): base(ConnectionStrings.UniSAStudentMobileDB)
        {
            _logger = new AppLogger.Logger(GetType());
        }

        public void Add(TagGroupModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TagGroupModel entity)
        {
            throw new NotImplementedException();
        }

        public TagGroupModel FindById(int Id)
        {
            if (Context == null)
            {
                _logger.Error("DBContext cannot be null");
            }
            using (Context)
            {
                return Context.SingleOrDefaultById<TagGroupModel>(Id);
            }
        }

        public List<TagGroupModel> List(string sql = null)
        {
            throw new NotImplementedException();
        }

        public void Update(TagGroupModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
