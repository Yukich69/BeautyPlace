using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Model1 db = new Model1())
            {
                Client c1 = new Client { Id = 12, FirstName = "Yuk", LastName = "Kryv", Address = "KPI", Email = "casias@kpi.com", Phone = "0667654321" };
                db.Clients.Add(c1);
                db.SaveChanges();

                foreach (Client c in db.Clients)
                    Console.WriteLine("{0}  {1}  {2}  {3}", c.Id, c.FirstName, c.LastName, c.Phone);
                Console.WriteLine("--------------------------------------------------------------------");


                c1.Address = "Sharaga";
                db.SaveChanges();

                foreach (Client c in db.Clients)
                    Console.WriteLine("{0}  {1}  {2}  {3}", c.Id, c.FirstName, c.LastName, c.Phone);
                Console.WriteLine("--------------------------------------------------------------------");

                db.Clients.Remove(c1);
                db.SaveChanges();

                foreach (Client c in db.Clients)
                    Console.WriteLine("{0}  {1}  {2}  {3}", c.Id, c.FirstName, c.LastName, c.Phone);
                Console.WriteLine("--------------------------------------------------------------------");



                var t_c = db.Clients.Take(3);
                foreach (Client c in t_c)
                    Console.WriteLine("{0}  {1}  {2}  {3}", c.Id, c.FirstName, c.LastName, c.Phone);
                Console.WriteLine("--------------------------------------------------------------------");



                var c_c = db.Clients.Count();
                Console.WriteLine(c_c);
                Console.WriteLine("--------------------------------------------------------------------");


                var se = db.Services.OrderBy(x => x.Price).Skip(1).Take(3);
                foreach (Service s in se)
                    Console.WriteLine("{0}  {1}", s.Id, s.NameService);
                Console.WriteLine("--------------------------------------------------------------------");



                var se1 = db.Services.OrderByDescending(x => x.Price).Skip(1).Take(3);
                foreach (Service s in se1)
                    Console.WriteLine("{0}  {1}", s.Id, s.NameService);
                Console.WriteLine("--------------------------------------------------------------------");



                var cl = db.Clients
                    .Where(x => x.Phone.Contains("+3809"));
                foreach (Client c in cl)
                    Console.WriteLine("{0}  {1}", c.FirstName, c.Phone);


                var s2 = db.Services.Where(x => x.Price > 150)
                                    .Where(x => x.Group_Id == 2);
                foreach (Service s in s2)
                    Console.WriteLine("{0}  {1}  {2}", s.Group_Id, s.NameService, s.Price);
                Console.WriteLine("--------------------------------------------------------------------");



                var ut = db.Clients.Select(x => new { FirstName = x.FirstName })
                .Union(db.Employees.Select(y => new { FirstName = y.FirstName }));
                foreach (var c in ut)
                    Console.WriteLine(c.FirstName);
                Console.WriteLine("--------------------------------------------------------------------");



                var ext = db.Clients.Select(x => new { FirstName = x.FirstName })
                    .Except(db.Employees.Select(y => new { FirstName = y.FirstName }));
                foreach (var c in ext)
                    Console.WriteLine(c.FirstName);
                Console.WriteLine("--------------------------------------------------------------------");


                var exi = db.Clients.Select(x => new { FirstName = x.FirstName })
                    .Intersect(db.Employees.Select(y => new { FirstName = y.FirstName }));
                foreach (var c in exi)
                    Console.WriteLine(c.FirstName);
                Console.WriteLine("--------------------------------------------------------------------");



                var j2 = db.Clients.Join(db.Visits,
                       x => x.Id,
                       y => y.Client_Id, (x, y) => new
                       {
                           Name = x.FirstName
                       });
                foreach (var j in j2)
                    Console.WriteLine("{0}", j.Name);
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");



                var jto = from cli in db.Clients
                          join v in db.Visits
                          on cli.Id equals v.Client_Id
                          join e in db.Employees
                          on v.Employee_Id equals e.Id
                          where e.Positions_Id == 1
                          select new { FirstName = cli.FirstName };
                foreach (var x in jto)
                    Console.WriteLine("{0}", x.FirstName);
                Console.WriteLine("--------------------------------------------------------------------");



                var s_jt = db.Clients.GroupBy(x => x.FirstName);
                foreach (IGrouping<string, Client> x in s_jt)
                {
                    foreach (var t in x)
                        Console.WriteLine("{0}", t.FirstName);
                }
                Console.WriteLine("--------------------------------------------------------------------");


                var sum1 = db.Services.Sum(x => x.Price);
                Console.WriteLine(sum1);
                Console.WriteLine("--------------------------------------------------------------------");

                var max1 = db.Services.Max(x => x.Price);
                Console.WriteLine(max1);
                Console.WriteLine("--------------------------------------------------------------------");

                var min1 = db.Services.Min(x => x.Price);
                Console.WriteLine(min1);
                Console.WriteLine("--------------------------------------------------------------------");

                var join_client = db.Clients.Join(db.Visits,
                       x => x.Id,
                       y => y.Client_Id, (x, y) => new
                       {
                           Name = x.FirstName,
                           ID = x.Id
                       });
                foreach (var j in join_client)
                    Console.WriteLine("{0}  {1}", j.ID, j.Name);
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");


                var g_jt = join_client.GroupBy(x => x.ID)
                                      .Select(x => new { ID = x.Key, Count = x.Count() })
                                      .Where(x => x.Count >= 2);


                foreach (var x in g_jt)
                    Console.WriteLine("{0}", x.ID);
                Console.WriteLine("--------------------------------------------------------------------");

                System.Data.SqlClient.SqlParameter contr = new System.Data.SqlClient.SqlParameter("@maxPrice", db.Services.Max(x => x.Price));
                System.Data.SqlClient.SqlParameter contr1 = new System.Data.SqlClient.SqlParameter("@minPrice", db.Services.Min(x => x.Price));
                var InsuredEvents = db.Database.SqlQuery<Service>("GetServices @minPrice, @maxPrice", contr, contr1);
                Console.WriteLine($"{contr.Value} - {contr1.Value}");

                Console.WriteLine("--------------------------------------------------------------------");

                System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@min_price", 70);
                System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("@max_price", 170);
                var hands = db.Database.SqlQuery<Service>("SELECT * FROM [GetServicesByPrice] (@min_price, @max_price)", param, param1);
                foreach (var p in hands)
                    Console.WriteLine("{0} - {1}", p.NameService, p.Price);



                Console.Read();

            }
        }
    }
}
