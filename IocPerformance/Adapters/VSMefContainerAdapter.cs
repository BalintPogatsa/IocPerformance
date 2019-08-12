using System;
using Microsoft.VisualStudio.Composition;
using IocPerformance.Classes.Complex;
using IocPerformance.Classes.Dummy;
using IocPerformance.Classes.Generics;
using IocPerformance.Classes.Multiple;
using IocPerformance.Classes.Properties;
using IocPerformance.Classes.Standard;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace IocPerformance.Adapters
{
    public sealed class VSMefContainerAdapter : ContainerAdapterBase
    {
        private static readonly PartDiscovery partDiscovery = new AttributedPartDiscovery(Resolver.DefaultInstance);

        private ExportProvider container;

        private readonly Dictionary<Type, Func<VSMefContainerAdapter, object>> resolverLookup = new Dictionary<Type, Func<VSMefContainerAdapter, object>>()
            {
                { typeof(ITransient1), (adapter) => adapter.container.GetExportedValue<ITransient1>() },
                { typeof(ITransient2), (adapter) => adapter.container.GetExportedValue<ITransient2>() },
                { typeof(ITransient3), (adapter) => adapter.container.GetExportedValue<ITransient3>() },
                { typeof(ISingleton1), (adapter) => adapter.container.GetExportedValue<ISingleton1>() },
                { typeof(ISingleton2), (adapter) => adapter.container.GetExportedValue<ISingleton2>() },
                { typeof(ISingleton3), (adapter) => adapter.container.GetExportedValue<ISingleton3>() },
                { typeof(ICombined1), (adapter) => adapter.container.GetExportedValue<ICombined1>() },
                { typeof(ICombined2), (adapter) => adapter.container.GetExportedValue<ICombined2>() },
                { typeof(ICombined3), (adapter) => adapter.container.GetExportedValue<ICombined3>() },
                { typeof(IComplex1), (adapter) => adapter.container.GetExportedValue<IComplex1>() },
                { typeof(IComplex2), (adapter) => adapter.container.GetExportedValue<IComplex2>() },
                { typeof(IComplex3), (adapter) => adapter.container.GetExportedValue<IComplex3>() },
                { typeof(IComplexPropertyObject1), (adapter) => adapter.container.GetExportedValue<IComplexPropertyObject1>() },
                { typeof(IComplexPropertyObject2), (adapter) => adapter.container.GetExportedValue<IComplexPropertyObject2>() },
                { typeof(IComplexPropertyObject3), (adapter) => adapter.container.GetExportedValue<IComplexPropertyObject3>() },
                { typeof(IDummyOne), (adapter) => adapter.container.GetExportedValue<IDummyOne>() },
                { typeof(IDummyTwo), (adapter) => adapter.container.GetExportedValue<IDummyTwo>() },
                { typeof(IDummyThree), (adapter) => adapter.container.GetExportedValue<IDummyThree>() },
                { typeof(IDummyFour), (adapter) => adapter.container.GetExportedValue<IDummyFour>() },
                { typeof(IDummyFive), (adapter) => adapter.container.GetExportedValue<IDummyFive>() },
                { typeof(IDummySix), (adapter) => adapter.container.GetExportedValue<IDummySix>() },
                { typeof(IDummySeven), (adapter) => adapter.container.GetExportedValue<IDummySeven>() },
                { typeof(IDummyEight), (adapter) => adapter.container.GetExportedValue<IDummyEight>() },
                { typeof(IDummyNine), (adapter) => adapter.container.GetExportedValue<IDummyNine>() },
                { typeof(IDummyTen), (adapter) => adapter.container.GetExportedValue<IDummyTen>() },
                { typeof(ImportMultiple1), (adapter) => adapter.container.GetExportedValue<ImportMultiple1>() },
                { typeof(ImportMultiple2), (adapter) => adapter.container.GetExportedValue<ImportMultiple2>() },
                { typeof(ImportMultiple3), (adapter) => adapter.container.GetExportedValue<ImportMultiple3>() },
                { typeof(ISimpleAdapter), (adapter) => adapter.container.GetExportedValues<ISimpleAdapter>() }
            };

        public override string PackageName => "VSMef";

        public override string Url => "https://blogs.msdn.com/b/bclteam/p/composition.aspx";

        public override bool SupportsPropertyInjection => true;

        public override bool SupportsMultiple => true;

        public override bool SupportGeneric => false;

        public override string Version
        {
            get => (string)typeof(ComposableCatalog).Assembly.CustomAttributes.Single(a => a.AttributeType == typeof(AssemblyInformationalVersionAttribute)).ConstructorArguments[0].Value;
        }

        public override object Resolve(Type type)
        {
            if (resolverLookup.ContainsKey(type))
                return resolverLookup[type].Invoke(this);

            throw new ArgumentException("Non-injectable type requested: " + type.FullName, nameof(type));
        }

        public override void Dispose()
        {
            this.container?.Dispose();
            this.container = null;
        }

        public override void Prepare()
        {
            var epf = ExportProviderFactory.LoadDefaultAsync().GetAwaiter().GetResult();
            container = epf.CreateExportProvider();
        }

        public override void PrepareBasic()
        {
            var epf = ExportProviderFactory.LoadDefaultAsync().GetAwaiter().GetResult();
            container = epf.CreateExportProvider();
        }
        
    }
}
