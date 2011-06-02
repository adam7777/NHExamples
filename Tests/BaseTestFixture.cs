using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BaseTestFixture
    {
        protected SessionSource SessionSource { get; set; }
        protected ISession Session { get; set; }

        [SetUp]
        public void SetupContext()
        {
            var cfg = Fluently.Configure().Database(SQLiteConfiguration.Standard.InMemory);

            SessionSource = new SessionSource(cfg.BuildConfiguration().Properties, new TestModel());
            Session = SessionSource.CreateSession();
            SessionSource.BuildSchema(Session);

        }

        [TearDown]
        public void TearDownContext()
        {
            Session.Close();
            Session.Dispose();
        }


    }
}
