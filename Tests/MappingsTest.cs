using System;
using System.Collections;
using FluentNHibernate.Testing;
using Models.Entities;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MappingsTest : BaseTestFixture
    {
        [Test]
        public void CanCorrectlyMapEmployee()
        {
            new PersistenceSpecification<Employee>(Session, new CustomEqualityComparer())
                .CheckProperty(x => x.Id, 1)
                .CheckProperty(x => x.FirstName, "Zenek")
                .CheckProperty(x => x.LastName, "Benek")
                .CheckReference(x => x.Store, new Store() { Name = "Some store" })
                .VerifyTheMappings();
        }
    }

    public class CustomEqualityComparer: IEqualityComparer
    {
        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(null, x) || ReferenceEquals(null, y))
                return false;

            if (ReferenceEquals(x, y))
                return true;

            if (x is Store && y is Store)
            {
                return ((Store)x).Id == ((Store)y).Id;
            }

            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
