using System.Data.Entity;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Layouts;

using System.Linq;
using System.Reflection;
using Autofac.Core;
using Module = Autofac.Module;

namespace Previgesst
{
    public class LogConfig
    {
        public static void RegisterLog(string connectionString)
        {
            //NLog:
            var config = new LoggingConfiguration();

            //Database log :
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                var databaseTarget = new DatabaseTarget();
                config.AddTarget("database", databaseTarget);

                databaseTarget.ConnectionString = connectionString;
                databaseTarget.CommandText = @"INSERT INTO [dbo].[__LogEntries] ([CallSite], [Date], [Exception], [Level], [Logger], [MachineName], [Message], [StackTrace], [Thread], [Username]) VALUES (@CallSite, @Date, @Exception, @Level, @Logger, @MachineName, @Message, @StackTrace, @Thread, @Username);";
                databaseTarget.Parameters.Add(new DatabaseParameterInfo("@CallSite", new SimpleLayout("${callsite:filename=true}")));
                databaseTarget.Parameters.Add(new DatabaseParameterInfo("@Date", new SimpleLayout(@"${date:format=yyyy-MM-dd HH\:mm\:ss.fff}")));
                databaseTarget.Parameters.Add(new DatabaseParameterInfo("@Exception", new SimpleLayout("${exception:format=ToString}")));
                databaseTarget.Parameters.Add(new DatabaseParameterInfo("@Level", new SimpleLayout("${level}")));
                databaseTarget.Parameters.Add(new DatabaseParameterInfo("@Logger", new SimpleLayout("${logger}")));
                databaseTarget.Parameters.Add(new DatabaseParameterInfo("@MachineName", new SimpleLayout("${machinename}")));
                databaseTarget.Parameters.Add(new DatabaseParameterInfo("@Message", new SimpleLayout("${message}")));
                databaseTarget.Parameters.Add(new DatabaseParameterInfo("@StackTrace", new SimpleLayout("${stacktrace}")));
                databaseTarget.Parameters.Add(new DatabaseParameterInfo("@Thread", new SimpleLayout("${threadid}")));
                databaseTarget.Parameters.Add(new DatabaseParameterInfo("@Username", new SimpleLayout("${windows-identity:domain=true}")));
                databaseTarget.Name = "database";

                var ruleDatabase = new LoggingRule("*", LogLevel.Debug, databaseTarget);
                config.LoggingRules.Add(ruleDatabase);
            }

            //File log
            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            fileTarget.Name = "fileLog";
            fileTarget.Layout = "${longdate} ${uppercase:${level}} ${message} - ${exception:format=ToString}";
            fileTarget.FileName = "${basedir}/Logs/${shortdate}.log";

            var ruleFile = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(ruleFile);

            // Step 5. Activate the configuration
            LogManager.Configuration = config;
        }
    }

    /// <summary>
    /// Module Nlog pour l'injection de dépendence
    /// </summary>
    public class NLogModule : Module
    {
        private static void InjectLoggerProperties(object instance)
        {
            var instanceType = instance.GetType();

            // Get all the injectable properties to set.
            // If you wanted to ensure the properties were only UNSET properties,
            // here's where you'd do it.
            var properties = instanceType
              .GetProperties(BindingFlags.Public | BindingFlags.Instance)
              .Where(p => p.PropertyType == typeof(ILogger) && p.CanWrite && p.GetIndexParameters().Length == 0);

            // Set the properties located.
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, LogManager.GetLogger(instanceType.FullName), null);
            }
        }

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            var t = e.Component.Activator.LimitType;
            e.Parameters = e.Parameters.Union(
                new[]
                {
                    new ResolvedParameter((p, i) => p.ParameterType == typeof (ILogger),
                        (p, i) => LogManager.GetLogger(t.FullName))
                });
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            // Handle constructor parameters.
            registration.Preparing += OnComponentPreparing;

            // Handle properties.
            registration.Activated += (sender, e) => InjectLoggerProperties(e.Instance);
        }
    }
}