using AddressablesPlugin;
using PreparationState;
using SpaceGameState;
using StateMachine;
using UnityEngine;
using Zenject;

public class ZenjectMonoinstaller : MonoInstaller
{
    [SerializeField] private AddressablesController _addressablesController;
    [SerializeField] private InteractiveObjectsPool _interactiveObjectsPool;
    [SerializeField] private CharactersManager _charactersManager;
    [SerializeField] private RocketManager _rocketManager;
    [SerializeField] private TaskManager _taskManager;
    [SerializeField] private GameStateMachine _gameStateMachine;
    [SerializeField] private SpaceGameManager _spaceGameManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private BackgroundManager _backgroundManager;


    public override void InstallBindings()
    {
        AddControllers();
        BindFactory();
        InitializeFactory();
    }

    private void BindFactory()
    {
        Container.Bind<ZenjectFactory>().To<ZenjectFactory>().AsSingle();
    }

    private void AddControllers()
    {
        Container
            .Bind<AddressablesController>()
            .FromInstance(_addressablesController)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<InteractiveObjectsPool>()
            .FromInstance(_interactiveObjectsPool)
            .AsSingle()
            .NonLazy();

        Container
            .Bind<CharactersManager>()
            .FromInstance(_charactersManager)
            .AsSingle()
            .NonLazy();

        Container
            .Bind<RocketManager>()
            .FromInstance(_rocketManager)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<TaskManager>()
            .FromInstance(_taskManager)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<GameStateMachine>()
            .FromInstance(_gameStateMachine)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<SpaceGameManager>()
            .FromInstance(_spaceGameManager)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<EnemyManager>()
            .FromInstance(_enemyManager)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<BackgroundManager>()
            .FromInstance(_backgroundManager)
            .AsSingle()
            .NonLazy();
    }

    private void InitializeFactory()
    {
        var interactiveObjectFactory = Container.Resolve<ZenjectFactory>();
        interactiveObjectFactory.LoadAll();
    }
}