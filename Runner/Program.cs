using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using Models.Entities;

namespace NHibernateProject
{
    class Program
    {
        private const string DbFile = "demo.db";
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                                            .ConnectionString(c => c
                                            .Server(@".\SQLEXPRESS2008")
                                            .Database("NHib")
                                            .Username("sa")
                                            .Password("1234qwer")))
                //.Database(SQLiteConfiguration.Standard
                //    .UsingFile(DbFile))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<MusicFile>())
                //.ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            //if (File.Exists(DbFile))
            //    File.Delete(DbFile);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config).Create(false, true);
        }

        static void Main(string[] args)
        {
            _sessionFactory = CreateSessionFactory();
            addFiles();

            Console.WriteLine("[end]");
            Console.ReadKey();
        }

        public static void addFiles()
        {
            using (var session = _sessionFactory.OpenSession())
            {

                using (var transaction = session.BeginTransaction())
                {
                    //var res = session.CreateCriteria(typeof(MusicCollection)).List<MusicCollection>();

                    //foreach (var item in res)
                    //{
                    //    Console.WriteLine(item.MusicName);

                    //    foreach (var f in item.Files)
                    //    {
                    //        Console.WriteLine("\t{0}", f.Name);
                    //    }
                    //}

                    var res = session.CreateCriteria<MusicFile>().List<MusicFile>();

                    foreach (var item in res)
                    {
                        Console.WriteLine(item.Name);
                    }
                }

                using (var transaction = session.BeginTransaction())
                {
                    var file = new MusicFile() { Name = "example.mp3", Path = @"c:\myMusic\" };
                    var file2 = new MusicFile() { Name = "dvpa.flac", Path = @"c:\myMusic\" };
                    //var file3 = new MusicFile() { Name = "sample.flac", Path = @"c:\myMusic\" };

                    var c = new MusicCollection() { MusicName = "One" };

                    c.AddFile(file);
                    c.AddFile(file2);
                    //c.AddFile(file3);

                    //session.SaveOrUpdate(file);
                    //session.SaveOrUpdate(file2);

                    session.SaveOrUpdate(c);

                    transaction.Commit();
                }
            }
            
        }

        public static void addSomeThings()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // create a couple of Stores each with some Products and Employees
                    var barginBasin = new Store { Name = "Bargin Basin" };
                    var superMart = new Store { Name = "SuperMart" };

                    var potatoes = new Product { Name = "Potatoes", Price = 3.60 };
                    var fish = new Product { Name = "Fish", Price = 4.49 };
                    var milk = new Product { Name = "Milk", Price = 0.79 };
                    var bread = new Product { Name = "Bread", Price = 1.29 };
                    var cheese = new Product { Name = "Cheese", Price = 2.10 };
                    var waffles = new Product { Name = "Waffles", Price = 2.41 };

                    var daisy = new Employee { FirstName = "Daisy", LastName = "Harrison" };
                    var jack = new Employee { FirstName = "Jack", LastName = "Torrance" };
                    var sue = new Employee { FirstName = "Sue", LastName = "Walkters" };
                    var bill = new Employee { FirstName = "Bill", LastName = "Taft" };
                    var joan = new Employee { FirstName = "Joan", LastName = "Pope" };

                    // add products to the stores, there's some crossover in the products in each
                    // store, because the store-product relationship is many-to-many
                    AddProductsToStore(barginBasin, potatoes, fish, milk, bread, cheese);
                    AddProductsToStore(superMart, bread, cheese, waffles);

                    // add employees to the stores, this relationship is a one-to-many, so one
                    // employee can only work at one store at a time
                    AddEmployeesToStore(barginBasin, daisy, jack, sue);
                    AddEmployeesToStore(superMart, bill, joan);

                    // save both stores, this saves everything else via cascading
                    session.SaveOrUpdate(barginBasin);
                    session.SaveOrUpdate(superMart);

                    transaction.Commit();
                }


                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    var stores = session.CreateCriteria(typeof(Store))
                      .List<Store>();

                    foreach (var store in stores)
                    {
                        //WriteStorePretty(store);
                    }
                }
            }
        }


        public static void AddProductsToStore(Store store, params Product[] products)
        {
            foreach (var product in products)
            {
                store.AddProduct(product);
            }
        }

        public static void AddEmployeesToStore(Store store, params Employee[] employees)
        {
            foreach (var employee in employees)
            {
                store.AddEmployee(employee);
            }
        }

    }
}
