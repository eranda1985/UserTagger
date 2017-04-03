using System;
using System.Collections.Generic;
using UniSA.UserTagger.Core.Constants;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Models;

using AppLogger = UniSA.UserTagger.Core.Logger;

namespace UniSA.UserTagger.Core.Repository
{
    public class TagRepository : DBContext, IRepository<TagModel>
    {
        private AppLogger.Logger _logger;

        public TagRepository(): base(ConnectionStrings.UniSAStudentMobileDB)
        {
            _logger = new AppLogger.Logger(GetType());
        }

        public List<TagModel> List(string sql)
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
                    return Context.Fetch<TagModel>();
                }
            }

            using (Context)
            {
                return Context.Fetch<TagModel>(sql);
            }
        }

        public void Add(TagModel entity)
        {
            if (Context == null)
            {
                _logger.Error("DBContext cannot be null");
            }
            using (Context)
            {
                Context.Insert(entity);
            }
        }

        public void Delete(TagModel entity)
        {
            if (Context == null)
            {
                _logger.Error("DBContext cannot be null");
            }
            using (Context)
            {
                Context.Delete<TagModel>(entity);
            }
        }

        public TagModel FindById(int Id)
        {
            if (Context == null)
            {
                _logger.Error("DBContext cannot be null");
            }
            using (Context)
            {
                return Context.SingleOrDefaultById<TagModel>(Id);
            }
        }

        public void Update(TagModel entity)
        {
            if (Context == null)
            {
                _logger.Error("DBContext cannot be null");
            }
            using (Context)
            {
                Context.Save(entity);
            }
        }
    }
}
