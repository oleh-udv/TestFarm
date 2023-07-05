using BuilderGame.Gameplay.Plants;
using BuilderGame.Gameplay.Pools;
using Zenject;

namespace BuilderGame
{
    public class BuilderGameInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BuilderGameInstaller>().FromInstance(this);
            
            //AndriiCodeReview: Changed binding logic
            //Pools
            // var plantPoolManager = Container.InstantiatePrefabResourceForComponent<PlantPoolManager>("PlantPoolManager");
            // Container.Bind<PlantPoolManager>().FromInstance(plantPoolManager).AsSingle();
            Container.Bind<PlantCollectablesPool>().FromComponentInNewPrefabResource(nameof(PlantCollectablesPool)).AsSingle();

            Container.Bind<PlantsSettings>().FromResources(nameof(PlantsSettings)).AsSingle();
            Container.BindInterfacesAndSelfTo<PlantsFactory>().AsSingle();
        }

        public void Initialize()
        {
            
        }
    }
}