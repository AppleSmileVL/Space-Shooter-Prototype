using UnityEngine;

public class SceneDependenciesContainer : Dependency, IDependency<SceneDependenciesContainer>
{
    [SerializeField] private LevelStateTracker levelStateTracker;
    [SerializeField] private LevelTimeTracker levelTimeTracker;
    [SerializeField] private LevelResultTime levelResultTime;
    [SerializeField] private LevelController levelController;
    [SerializeField] private LevelBoundary levelBoundary;
    [SerializeField] private ShipInputController shipInputController;
    [SerializeField] private LevelCompletionTime levelCompletionTime;

    private Player player;

    public void Construct(SceneDependenciesContainer container) { }

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        Bind<LevelStateTracker>(levelStateTracker, monoBehaviourInScene);
        Bind<LevelTimeTracker>(levelTimeTracker, monoBehaviourInScene);
        Bind<LevelResultTime>(levelResultTime, monoBehaviourInScene);
        Bind<LevelController>(levelController, monoBehaviourInScene);
        Bind<LevelBoundary>(levelBoundary, monoBehaviourInScene);
        Bind<ShipInputController>(shipInputController, monoBehaviourInScene);
        Bind<Player>(player, monoBehaviourInScene);
        Bind<LevelCompletionTime>(levelCompletionTime, monoBehaviourInScene);
    }

    private void Awake()
    {
        FindAllObjectToBind();
    }

    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer;
        
        FindAllObjectToBind();
    }
}
