namespace ServicesLayers
{
    using Autofac;
    using DataLayers;
    using ServicesLayers.Contract;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DataModule>();
            builder.RegisterType<V1.UserServices>().As<AbstractUserServices>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
