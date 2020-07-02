using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ServiceModel;
using WCFMiddleware;
using System.Net;
using System.IO;
using WCFServer.JEEWebservice;
using NLog;

namespace WCFServer
{
    class Program
    {
        private static ServiceHost host;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            host = new ServiceHost(typeof(ServerEntryPoint));

            try
            {
                LoggerSetup();
                logger.Info("Server initializing..");
                host.Open();
                logger.Info("Server opened..\n");

                logger.Info("Server infos:\n");
                for (int i = 0; i < host.Description.Endpoints.Count; i++)
                {
                    logger.Info("Adresse : " + host.Description.Endpoints[i].Address);
                    logger.Info("Binding : " + host.Description.Endpoints[i].Binding);
                    logger.Info("Contract Type : " + host.Description.Endpoints[i].Contract.ContractType);
                    logger.Info("Contract Name : " + host.Description.Endpoints[i].Contract.Name);
                    logger.Info("Uri : " + host.Description.Endpoints[i].ListenUri.Host + "\n");
                }

                Console.WriteLine("Press <Enter> to close connection..\n");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                if(host.State == CommunicationState.Opening || host.State == CommunicationState.Opened)
                    host.Close();
                logger.Info("Server closed..");
            }

        }
            private static void LoggerSetup()
            {
                //Logger configuration
                var config = new NLog.Config.LoggingConfiguration();
                // Where to lo to: File & Console
                var logFile = new NLog.Targets.FileTarget("logFile") { FileName = "log_file.txt" };
                var logConsole = new NLog.Targets.ConsoleTarget("logConsole");
                // Logging rules (where to log what)
                config.AddRule(LogLevel.Debug, LogLevel.Fatal, logFile);
                config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);

                LogManager.Configuration = config;
            }
    }
}
