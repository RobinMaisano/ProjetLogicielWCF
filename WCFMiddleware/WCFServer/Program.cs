using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ServiceModel;
using WCFMiddleware;

namespace WCFServer
{
    class Program
    {
        private static ServiceHost host;

        static void Main(string[] args)
        {

            host = new ServiceHost(typeof(ServerEntryPoint));

            try
            {
                Console.WriteLine("Server initializing..");
                host.Open();
                Console.WriteLine("Server opened..\n");

                Console.WriteLine("Server infos:\n");
                for (int i = 0; i < host.Description.Endpoints.Count; i++)
                {
                    Console.WriteLine("Adresse : " + host.Description.Endpoints[i].Address);
                    Console.WriteLine("Binding : " + host.Description.Endpoints[i].Binding);
                    Console.WriteLine("Contract Type : " + host.Description.Endpoints[i].Contract.ContractType);
                    Console.WriteLine("Contract Name : " + host.Description.Endpoints[i].Contract.Name);
                    Console.WriteLine("Uri : " + host.Description.Endpoints[i].ListenUri.Host + "\n");
                }

                Console.WriteLine("Press <Enter> to close connection..");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if(host.State == CommunicationState.Opening || host.State == CommunicationState.Opened)
                    host.Close();
                Console.WriteLine("Server closed..");
            }

            //string ConnectionString = @"Data Source=ROBIN;Initial Catalog=WSF2;Integrated Security=true;";
            //string ConnectionString = @"Data Source=ROBIN;User ID=user;Password=root";

            //SqlConnection conn = new SqlConnection(ConnectionString);

            //try
            //{
            //    Console.WriteLine("Begin connection");
            //    conn.Open();
            //    Console.WriteLine("Connection opened");

            //    string query = @"SELECT * FROM [user]";
            //    SqlCommand cmd = new SqlCommand(query, conn);
            //    Console.WriteLine("Retrieving data");
            //    SqlDataReader reader = cmd.ExecuteReader();

            //    if (reader.HasRows)
            //    {
            //        while (reader.Read())
            //        {
            //            Console.WriteLine("Id: " + reader.GetGuid(0));
            //            Console.WriteLine("Login: " + reader.GetString(1));
            //            Console.WriteLine("Password: " + reader.GetString(2));
            //            Console.WriteLine("Email: " + reader.GetString(3));
            //        }
            //    }

            //    conn.Close();
            //    Console.WriteLine("Connection closed");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            Console.ReadLine();
        }
    }
}
