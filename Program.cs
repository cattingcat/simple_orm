using DataAccessor.Accessors;
using DataAccessor.Entity;
using System;
using System.Diagnostics;

namespace DataAccessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select data accessor ");
            Console.WriteLine("Person accessor - 1");
            Console.WriteLine("Second entity accessor - 2");
            int i = int.Parse(Console.ReadLine());
            if (i == 1)
            {
                IAccessor<Person> accessor = new ADOPersonAccessor();// OrmPersonAccessor();
                RunPersonCUI(accessor);
            }
            else
            {
                IAccessor<SecondEntity> acc = new OrmSecondEntityAccessor();
                RunSecondEntityCUI(acc);
            }
        }

        private static void RunPersonCUI(IAccessor<Person> accessor)
        {
            Console.WriteLine("Commands:\np - print all\np [id] - print one\ni [id] - insert with id\nd [id] - delete by id\nx - quit");             
            Console.WriteLine("Now using: {0} ", accessor.GetType().Name);
            while (true)
            {
                string[] command = Console.ReadLine().Split(' ', ',');
                if (command[0] == "p")
                {
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    if (command.Length == 1)
                    {
                        foreach (Person p in accessor.GetAll())
                        {
                            Console.WriteLine(p);
                        }
                    }
                    else if (command.Length == 2)
                    {
                        int id = Int32.Parse(command[1]);
                        Person p = accessor.GetById(id);
                        Console.WriteLine(p.ToString());
                    }
                    s.Stop();
                    Console.WriteLine("Complete! elapsed: {0}ms", s.ElapsedMilliseconds);
                }
                else if (command[0] == "d")
                {
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    int id = Int32.Parse(command[1]);
                    accessor.DeleteById(id);
                    s.Stop();
                    Console.WriteLine("Complete! elapsed: {0}ms", s.ElapsedMilliseconds);
                }
                else if (command[0] == "i")
                {
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    int id = Int32.Parse(command[1]);
                    Person p = new Person() { ID = id };
                    if (command.Length >= 4)
                    {
                        p.Name = command[2];
                        p.LastName = command[3];
                    }
                    accessor.Insert(p);
                    s.Stop();
                    Console.WriteLine("Complete! elapsed: {0}ms", s.ElapsedMilliseconds);
                }
                else if (String.IsNullOrEmpty(command[0]) || command[0] == "x")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Unknown command");
                }
            }  
        }
        private static void RunSecondEntityCUI(IAccessor<SecondEntity> accessor)
        {
            Console.WriteLine("Commands:\np - print all\np [id] - print one\ni [id] - insert with id\nd [id] - delete by id\nx - quit");
            Console.WriteLine("Now using accessor: {0} ", accessor.GetType().Name);
            while (true)
            {
                string[] command = Console.ReadLine().Split(' ', ',');
                if (command[0] == "p")
                {
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    if (command.Length == 1)
                    {
                        foreach (SecondEntity p in accessor.GetAll())
                        {
                            Console.WriteLine(p);
                        }
                    }
                    else if (command.Length == 2)
                    {
                        string id = command[1];
                        SecondEntity p = accessor.GetById(id);
                        Console.WriteLine(p.ToString());
                    }
                    s.Stop();
                    Console.WriteLine("Complete! elapsed: {0}ms", s.ElapsedMilliseconds);
                }
                else if (command[0] == "d")
                {
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    int id = Int32.Parse(command[1]);
                    accessor.DeleteById(id);
                    s.Stop();
                    Console.WriteLine("Complete! elapsed: {0}ms", s.ElapsedMilliseconds);
                }
                else if (command[0] == "i")
                {
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    string id = command[1];
                    SecondEntity p = new SecondEntity() { Field = id };
                    if (command.Length >= 3)
                    {
                        p.UnattributeField = command[2];
                    }
                    accessor.Insert(p);
                    s.Stop();
                    Console.WriteLine("Complete! elapsed: {0}ms", s.ElapsedMilliseconds);
                }
                else if (String.IsNullOrEmpty(command[0]) || command[0] == "x")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Unknown command");
                }
            }
        }
    }
}
