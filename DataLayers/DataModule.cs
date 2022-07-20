namespace DataLayers
{
    using Autofac;
    using DataLayers.Contract;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<V1.UserData>().As<AbstractUserData>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
