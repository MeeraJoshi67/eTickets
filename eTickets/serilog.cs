using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;

namespace eTickets
{
    public static class serilog
    {
        public static void Initializeloggers(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Logger(logger => ApplySystemLog(logger, configuration))
                .CreateLogger();

        }

        [Obsolete]
        private static void ApplySystemLog(LoggerConfiguration logger, IConfiguration configuration)
        {
            string ConnectionString = configuration.GetConnectionString("DefaultConnectionString");

            LogEventLevel logLevelSystemLog = GetLogEventLevelFromString(configuration["Serilog:LevelSwitches:$systemLogSwitch"]);
            var sinkOptions = new MSSqlServerSinkOptions { TableName = "SystemLog", AutoCreateSqlTable = true, SchemaName = "eTickets" };
            var ColumnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn> { new SqlColumn("Username", System.Data.SqlDbType.NVarChar) }



            };
            //ColumnOptions.Store.Remove(StandardColumn.Properties);
            //ColumnOptions.Store.Add(StandardColumn.LogEvent);
            //ColumnOptions.LogEvent.ExcludeStandardColumns = true;

            logger
                    .WriteTo.MSSqlServer(ConnectionString, sinkOptions: sinkOptions, restrictedToMinimumLevel: logLevelSystemLog, columnOptions: ColumnOptions);
        }

        private static LogEventLevel GetLogEventLevelFromString(string logLevelEvent)
        {
            LogEventLevel logEventLevel;
            Enum.TryParse<LogEventLevel>(logLevelEvent ,true, out logEventLevel);
            return logEventLevel;
        }
    }

}

