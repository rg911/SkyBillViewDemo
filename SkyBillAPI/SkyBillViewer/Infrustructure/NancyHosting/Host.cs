using System;
using Nancy.Hosting.Self;

namespace SkyBillViewer.Infrustructure.NancyHosting
{
    public class Host : IDisposable
    {
        public static int Port { get; private set; }
        public static int WarningMaxDbGetTime { get; set; }
        public static int WarningMaxDbSetTime { get; set; }
        public static int WarningMaxApiTime { get; set; }
        public static int CacheSeconds { get; set; }

        public static string EventLogName { get; set; }
        public static string EventLogSource { get; set; }

        private NancyHost _host;

        public Host(int httpPort, string eventLogName, string eventLogSource, int cacheSeconds = 60, int warningMaxDbGetTime = 100, int warningMaxDbSetTime = 100, int warningMaxApiTime = 100)
        {
            Port = httpPort;
            EventLogName = eventLogName;
            EventLogSource = eventLogSource;
            WarningMaxDbGetTime = warningMaxDbGetTime;
            WarningMaxDbSetTime = warningMaxDbSetTime;
            WarningMaxApiTime = warningMaxApiTime;
            CacheSeconds = cacheSeconds;

            _host = new NancyHost(new Uri(string.Format("http://localhost:{0}", httpPort)));
            _host.Start();

        }

        #region IDisposable - Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_host == null) return;

            _host.Dispose();
            _host = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
