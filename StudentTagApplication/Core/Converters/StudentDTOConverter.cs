//using UniSA.UserTagger.Constants;
//using UniSA.UserTagger.Core.DTO;
//using UniSA.UserTagger.Entity;
//using UniSA.UserTagger.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace UniSA.UserTagger.Core.Converters
//{
//    public class StudentDTOConverter : IConverter<TagStructure, TagStructureDTO>
//    {
//        public void Convert(TagStructure source, out TagStructureDTO dest)
//        {
//            dest = new TagStructureDTO
//            {
//                Uid = source.Uid,
//                TagGroups = new Dictionary<string, IEnumerable<string>>()
//            };

//            if (source.ProgramType == null)
//                throw new Exception("Program type cannot be null");

//            if (source.Campus == null)
//                throw new Exception("Campus type cannot be null");

//            if (source.Courses == null)
//                throw new Exception("Course cannot be null");

//            // Populate the rest of the DTO properties here i.e. TagGroups etc. 
//            dest.TagGroups.Add(TagGroupings.PROGRAMTYPE, new List<string> { source.ProgramType.Name });

//            dest.TagGroups.Add(TagGroupings.CAMPUS, new List<string> { source.Campus });

//            dest.TagGroups.Add(TagGroupings.COURSE, new List<string>());

//            dest.TagGroups[TagGroupings.COURSE] = source.Courses.Select(x => x.CourseName);
//        }
//    }
//}
