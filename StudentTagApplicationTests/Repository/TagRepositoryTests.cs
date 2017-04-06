using NUnit.Framework;
using UniSA.UserTagger.Core.Repository;

namespace UniSA.UserTaggerTests
{
    [TestFixture]
    public class TagRepositoryTests
    {
        [Test]
        public void Should_Return_All_Tags_From_Repository()
        {
            var repo = new TagRepository();
            var result = repo.List("select * from Tags");
            Assert.That(result.Count > 0);
        }

        [Test]
        public void Should_Return_All_Tags_When_No_Parameters_From_Repository()
        {
            var repo = new TagRepository();
            var result = repo.List(null);
            Assert.That(result.Count > 0);
        }

        [Test]
        public void Should_Return_Tags_With_Groups_From_Repository()
        {
            var repo = new TagRepository();
            var list = repo.List("select t.* , g.* from Tags t inner join TagGroups g on t.TagGroup = g.Id where t.IsDeleted = 0");
            Assert.That(list.Count > 0);
        }
    }
}
