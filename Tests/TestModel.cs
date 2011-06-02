using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate;
using Models;

namespace Tests
{
    public class TestModel : PersistenceModel
    {
        public TestModel()
        {
            //AddMappingsFromAssembly(typeof(EmployeeMap).Assembly);
        }
    }
}
