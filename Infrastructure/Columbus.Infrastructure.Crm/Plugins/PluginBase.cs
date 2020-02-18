using System;
using System.Diagnostics;
using Microsoft.Xrm.Sdk;

namespace Columbus.InnerSource.Infrastructure.Microsoft.Crm.Plugins
{
    public abstract class PluginBase : IPlugin
    {
        protected string SecureConfig { get; }
        protected string UnSecureConfig { get; }

        protected PluginBase(string unSecure, string secure)
        {
            UnSecureConfig = unSecure;
            SecureConfig = secure;
        }

        protected PluginBase() : this(null, null)
        {
        }

        void IPlugin.Execute(IServiceProvider serviceProvider)
        {
            serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            var stopwatch = new Stopwatch();
            var resolver = CreateResolver(serviceProvider);
            var localContext = new PluginContext(serviceProvider, resolver);
            stopwatch.Start();
            localContext.Trace($"Started {GetType()}.Execute()");

            try
            {
                Execute(localContext);
            }
            catch (Exception exception)
            {
                localContext.Trace($"Exception {GetType()}.Execute(): {exception}");
                throw;
            }
            finally
            {
                stopwatch.Stop();
                localContext.Trace($"Exiting {GetType()}.Execute() in {stopwatch.ElapsedMilliseconds}ms");
            }
        }

        public abstract void Execute(PluginContext context);

        public abstract IServiceProvider CreateResolver(IServiceProvider crmServiceProvider);
    }
}