using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniSA.UserTagger.ApiClientWorker.Interfaces;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Logger;
using UniSA.UserTagger.Core.Repository;

namespace UniSA.UserTagger.Subscribers
{
    public class ScholarshipTagHandler : BaseHandler, ISubscriber<TagUpdateEvent>
    {
        private Logger _logger;
        private IUrbanAirshipClientWorker _worker;

        public ScholarshipTagHandler(string tagName, IUrbanAirshipClientWorker worker) : base(tagName)
        {
            _logger = new Logger(GetType());
            _worker = worker;
        }

        public void Subscribe(TagUpdateEvent args)
        {
            args.Subscribe(async incomingTag =>
            {
                if(tagName == incomingTag.Name)
                {
                    // Implement Scholarship tag logic here
                    _logger.Debug(string.Format("Entering {0} tag handler logic", tagName));

                    if (incomingTag.IsNew && incomingTag.IsInstall)
                    {
                        // Logic for Add tag
                        TagStructureDTO source = new TagStructureDTO();

                        using (var repo = new ScholarshipUserRepository())
                        {
                            var result = repo.List("select u.UId from ScholarshipUserDetail u");
                            source.UidList.AddRange(result.Select(x => x.UserId));
                        }

                        // Given a tag there should be only one group
                        var tagGroupName = incomingTag.TagGroup.SingleOrDefault().Name;

                        if (!string.IsNullOrEmpty(tagGroupName))
                            source.TagGroups.Add(tagGroupName, new List<string> { incomingTag.Name });

                        // Call the worker to process payload. 
                        var res = await _worker.ProcessAll(source);

                        string d = "";
                    }

                    else if(incomingTag.IsNew && (!incomingTag.IsInstall))
                    {
                        // Logic for Remove tag
                    }
                }
            });
        }
    }
}
