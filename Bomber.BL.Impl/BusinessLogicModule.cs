using Bomber.BL.Entities;
using Bomber.BL.Impl.Entities.Factories;
using Bomber.BL.Impl.MapGenerator;
using Bomber.BL.MapGenerator;
using Infrastructure.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.BL.Impl
{
    public class BusinessLogicModule : IModule
    {
        public void LoadModules(IServiceCollection collection)
        {
            collection.AddSingleton<IMapGeneratorSettings, MapGeneratorSettings>();
            collection.AddSingleton<IEntityFactory, EntityFactory>();
        }
    }
}
