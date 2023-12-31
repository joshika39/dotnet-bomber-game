using Bomber.BL.Entities;
using Bomber.BL.Impl.Entities.Factories;
using Bomber.BL.Impl.MapGenerator;
using Bomber.BL.MapGenerator;
using Implementation.Module;
using Infrastructure.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.BL.Impl
{
    public class BusinessLogicModule : AModule, IBaseModule
    {
        public BusinessLogicModule(IServiceCollection collection) : base(collection)
        { }
        
        public override IModule RegisterServices(IServiceCollection collection)
        {
            collection.AddSingleton<IMapGeneratorSettings, MapGeneratorSettings>();
            collection.AddSingleton<IEntityFactory, EntityFactory>();
            return this;
        }
    }
}
