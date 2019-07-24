using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using aAppli.Views;

namespace aAppli
{
    public class ModuleInit : IModule
    {
        private IRegionManager region;
        private IUnityContainer container;

        public ModuleInit(IRegionManager region, IUnityContainer container)
        {
            this.region = region;
            this.container = container;
        }

        public void Initialize()
        {
            region.RegisterViewWithRegion("MainRegion", typeof(StockView));
        }
    }
}
