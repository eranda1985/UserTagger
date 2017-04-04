using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.UserTagger.ApiClientWorker.Interfaces;
using UniSA.UserTagger.Core.Components;
using UniSA.UserTagger.Core.DTO;
using UniSA.UserTagger.Core.Events;
using UniSA.UserTagger.Core.Interfaces;
using UniSA.UserTagger.Core.Models;
using UniSA.UserTagger.Core.Repository;
using UniSA.UserTagger.Handlers;

namespace StudentTagApplicationTests.Handlers
{
    [TestFixture]
    public class MockTagHandlerTest
    {
        private IUrbanAirshipClientWorker _worker;
        private IConverter<TagDTO, TagModel> _tagDTOConverter;
        private MockTagHandler _mockTagHandler;
        private TagUpdateEvent _event;

        [SetUp]
        public void SetUp()
        {
            _worker = Substitute.For<IUrbanAirshipClientWorker>();
            _tagDTOConverter = Substitute.For<IConverter<TagDTO, TagModel>>();
            _mockTagHandler = new MockTagHandler("tagName", _worker, _tagDTOConverter);

            var eventAggtor = new EventAggregator();
            eventAggtor.RegisterEvent<TagUpdateEvent>();
            _event = eventAggtor.RegisteredTagUpdateEvent;
            _event.Initialize(eventAggtor);
            _mockTagHandler.Subscribe(_event);

        }

        [Test]
        public void Should_Call_Worker_ProcessTagAdd_Successfully()
        {
            TagDTO dto = new TagDTO
            {
                IsNew =true,
                IsInstall =true,
                Name = "tagName",
                TagGroup = new List<TagGroupDTO>
                {
                    new TagGroupDTO
                    {
                        Id = 1,
                        Name = "tag_group_1"
                    }
                }
            };

            _event.Publish(dto);
            _worker.Received().ProcessTagAdd(Arg.Any<TagStructureDTO>());
        }

        [Test]
        public void Should_Call_Worker_ProcessTagRemove_Successfully()
        {
            TagDTO dto = new TagDTO
            {
                IsNew = true,
                IsInstall = false,
                Name = "tagName",
                TagGroup = new List<TagGroupDTO>
                {
                    new TagGroupDTO
                    {
                        Id = 1,
                        Name = "tag_group_1"
                    }
                }
            };

            _event.Publish(dto);
            _worker.Received().ProcessTagRemove(Arg.Any<TagDTO>());
        }
    }

    public class MockTagHandler : BaseHandler, ISubscriber<TagUpdateEvent>
    {
        private IUrbanAirshipClientWorker _worker;
        private IConverter<TagDTO, TagModel> _tagDTOConverter;

        public MockTagHandler(
            string tagName,
            IUrbanAirshipClientWorker worker,
            IConverter<TagDTO, TagModel> tagDTOConverter) : base(tagName)
        {
            _worker = worker;
            _tagDTOConverter = tagDTOConverter;
        }

        public void Subscribe(TagUpdateEvent args)
        {
            args.Subscribe(incomingTag =>
            {
                if (tagName == incomingTag.Name)
                {
                    if (incomingTag.IsNew && incomingTag.IsInstall)
                    {
                        #region Logic for Add tag
                        TagStructureDTO source = new TagStructureDTO();

                        // Get the relevant Uids
                        using (var repo = new ScholarshipUserRepository())
                        {
                            var result = repo.List("select u.UId from ScholarshipUserDetail u");

                            source.UidList.AddRange(result.Select(x => x.UserId));
                        }

                        // Set the Tag group
                        var tagGroupName = incomingTag.TagGroup.SingleOrDefault().Name;

                        if (!string.IsNullOrEmpty(tagGroupName))
                            source.TagGroups.Add(tagGroupName, new List<string> { incomingTag.Name });

                        // Call the worker to process payload. 
                        var res = _worker.ProcessTagAdd(source);

                        if (res == null) return;

                        if (res.IsActionCompleted && res.IsSuccess && res.OriginalAPIResponse != null)
                        {
                            //Update the tag status in tag registry 
                            using (var repo = new TagRepository())
                            {
                                incomingTag.IsNew = false;
                                incomingTag.ModifiedDate = DateTime.Now;
                                TagModel entry = new TagModel();
                                _tagDTOConverter.Convert(incomingTag, out entry);
                                repo.Update(entry);
                            }
                        }
                        #endregion
                    }

                    else if (incomingTag.IsNew && (!incomingTag.IsInstall))
                    {
                        #region Logic for Remove tag
                        // Logic for Remove tag
                        var res = _worker.ProcessTagRemove(incomingTag);

                        if (res == null) return;

                        if (res.IsActionCompleted && res.IsSuccess && res.OriginalAPIResponse != null)
                        {
                            //Update the tag status in tag registry 
                            using (var repo = new TagRepository())
                            {
                                incomingTag.IsNew = false;
                                incomingTag.ModifiedDate = DateTime.Now;
                                TagModel entry = new TagModel();
                                _tagDTOConverter.Convert(incomingTag, out entry);
                                repo.Update(entry);
                            }
                        }
                        #endregion
                    }
                }
            });
        }
    }
}
