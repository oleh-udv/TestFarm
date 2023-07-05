using BuilderGame.Infrastructure.Services.Ads;
using BuilderGame.Infrastructure.Services.Ads.Fake;
using BuilderGame.Infrastructure.Services.Input;
using BuilderGame.Gameplay.Pools;
using UnityEngine;
using Zenject;

namespace BuilderGame.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this);
            
            Container.Bind<IInputProvider>().To<InputProvider>().AsSingle();

            Container.Bind<FakeAdsSettings>().FromResources(nameof(FakeAdsSettings)).AsSingle();
            Container.Bind<IAdvertiser>().To<FakeAdvertiser>().AsSingle();

            //AndriiCodeReview: Changed binding logic
            //Pools
            // var plantPoolManager = Container.InstantiatePrefabResourceForComponent<PlantPoolManager>("PlantPoolManager");
            // Container.Bind<PlantPoolManager>().FromInstance(plantPoolManager).AsSingle();
            Container.Bind<PlantPoolManager>().FromComponentInNewPrefabResource(nameof(PlantPoolManager)).AsSingle();
        }

        public void Initialize()
        {
            Application.runInBackground = true;
            Application.targetFrameRate = 60;
        }
    }
}