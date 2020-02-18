using Columbus.InnerSource.Core.Commands;
using Microsoft.Xrm.Sdk;
using System;

namespace Columbus.InnerSource.Infrastructure.Microsoft.Crm.Plugins
{
    public class PluginContext
    {
        public const string PreImageName = "PreImage";
        public const string PostImageName = "PostImage";

        internal IServiceProvider ServiceProvider { get; }
        public IServiceProvider Resolver { get; }
        public ICommandBus CommandBus { get; }
        internal IPluginExecutionContext PluginExecutionContext { get; }
        internal ITracingService TracingService { get; }
        private readonly Lazy<IOrganizationService> _organizationService;
        private readonly Lazy<IOrganizationService> _organizationServiceAsSystem;
        public IOrganizationService OrganizationService => _organizationService.Value;
        public IOrganizationService OrganizationServiceAsSystem => _organizationServiceAsSystem.Value;

        public string Message => PluginExecutionContext.MessageName;
        public Entity Target => (Entity)PluginExecutionContext.InputParameters["Target"];
        public Entity PreIamge => 
            PluginExecutionContext.PreEntityImages.ContainsKey(PreImageName) 
                ? PluginExecutionContext.PreEntityImages[PreImageName] : null;

        public Entity PostImage =>
            PluginExecutionContext.PostEntityImages.ContainsKey(PostImageName)
                ? PluginExecutionContext.PostEntityImages[PostImageName] : null;

        public EntityReference TargetReference => (EntityReference)PluginExecutionContext.InputParameters["Target"];
        public T GetTarget<T>() where T : Entity => Target.ToEntity<T>();
        public T GetPreImage<T>() where T : Entity => PreIamge?.ToEntity<T>();
        public T GetPostImage<T>() where T : Entity => PostImage?.ToEntity<T>();

        public PluginContext(IServiceProvider crmServiceProvider, IServiceProvider resolver)
        {
            ServiceProvider = crmServiceProvider;
            Resolver = resolver;
            CommandBus = Resolver.GetService(typeof(ICommandBus)) as ICommandBus;
            PluginExecutionContext = (IPluginExecutionContext)crmServiceProvider.GetService(typeof(IPluginExecutionContext));
            TracingService = (ITracingService)crmServiceProvider.GetService(typeof(ITracingService));
            var factory = (IOrganizationServiceFactory)crmServiceProvider.GetService(typeof(IOrganizationServiceFactory));
            _organizationService = new Lazy<IOrganizationService>(() => factory.CreateOrganizationService(PluginExecutionContext.UserId));
            _organizationServiceAsSystem = new Lazy<IOrganizationService>(() => factory.CreateOrganizationService(null));
        }

        public void Trace(string message)
        {
            if (string.IsNullOrWhiteSpace(message) || TracingService == null)
            {
                return;
            }

            TracingService.Trace(PluginExecutionContext == null
                ? message
                : $"{message}, Correlation Id: {PluginExecutionContext.CorrelationId}, Initiating User: {PluginExecutionContext.InitiatingUserId}");
        }
    }
}
