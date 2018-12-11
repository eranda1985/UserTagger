using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.ApiClientWorker.Interfaces;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Logger;
using UniSA.UserTagger.Core.Models;
using UniSA.UserTagger.Core.Repository;

namespace UniSA.UserTagger.Handlers
{
    public class GenericTagHandler : BaseHandler, ISubscriber<TagUpdateEvent>
    {
        private string _connString;
        private Logger _logger;
        private IUrbanAirshipClientWorker _worker;
        private IConverter<TagDTO, TagModel> _tagDTOConverter;

        public GenericTagHandler(string tagName, 
            string connString,
            IUrbanAirshipClientWorker worker,
            IConverter<TagDTO, TagModel> tagDTOConverter) : base(tagName)
        {
            _connString = connString;
            _logger = new Logger(GetType());
            _worker = worker;
            _tagDTOConverter = tagDTOConverter;
        }

        public void Subscribe(TagUpdateEvent args)
        {
            args.Subscribe(incomingTag =>
            {
                if (tagName == incomingTag.Name)
                {
                    // Do stuff related to this tag
                    if (incomingTag.IsInstall)
                    {
                        // Add tag logic
                        TagStructureDTO source = new TagStructureDTO();

                        // Get the relevant Uids
                        using (var repo = new GenericUserRepository(_connString))
                        {
                            var result = repo.List("select u.UId as Username from ScholarshipUserDetail u");
                            source.UidList.AddRange(result.Select(x => x.Username));
                        }

                        // Set the Tag group
                        var tagGroupName = incomingTag.TagGroup.SingleOrDefault().Name;

                        if (!string.IsNullOrEmpty(tagGroupName))
                            source.TagGroups.Add(tagGroupName, new List<string> { incomingTag.Name });

                        _worker.ProcessTagRemove(incomingTag);

                        // Call the worker to process payload. 
                        var res = _worker.ProcessTagAdd(source);

                        if (res.IsActionCompleted && res.IsSuccess && res.OriginalAPIResponse != null)
                        {
                            _logger.Debug(string.Format("Successfully added the tag - {0}", incomingTag.Name));

                            //Update the tag modified date in tag registry 
                            using (var repo = new TagRepository())
                            {
                                incomingTag.ModifiedDate = DateTime.Now;
                                incomingTag.IsActivated = true;
                                TagModel entry = new TagModel();
                                _tagDTOConverter.Convert(incomingTag, out entry);
                                repo.Update(entry);
                            }
                        }
                    }

                    else if (!incomingTag.IsInstall)
                    {
                        // Logic for Remove tag
                        var res = _worker.ProcessTagRemove(incomingTag);

                        if (res.IsActionCompleted && res.IsSuccess && res.OriginalAPIResponse != null)
                        {
                            _logger.Debug(string.Format("Successfully removed the tag - {0}", incomingTag.Name));

                            //Update the tag modified date in tag registry 
                            using (var repo = new TagRepository())
                            {
                                incomingTag.ModifiedDate = DateTime.Now;
                                incomingTag.IsActivated = true;
                                TagModel entry = new TagModel();
                                _tagDTOConverter.Convert(incomingTag, out entry);
                                repo.Update(entry);
                            }
                        }
                    }
                }
            });
        }
    }
}
