using UnityEngine;

public class PlayerSpawner : MonoBehaviour, IDependency<SceneDependenciesContainer>
{
    [Header("Prefab")]
    [SerializeField] private FollowCamera m_FollowCameraPrefab;
    [SerializeField] private Player m_PlayerPrefab;
    [SerializeField] private ShipInputController m_ShipInputControllerPrefab;
    [SerializeField] private VirtualGamepad m_VirtualGamepadPrefab;
    [SerializeField] private Transform m_SpawnPoint;

    private SceneDependenciesContainer sceneDependenciesContainer;
    public void Construct(SceneDependenciesContainer container) => sceneDependenciesContainer = container;

    public Player Spawn()
    {
        FollowCamera followCamera = Instantiate(m_FollowCameraPrefab);
        VirtualGamepad virtualGamepad = Instantiate(m_VirtualGamepadPrefab);
        
        ShipInputController shipInputController = Instantiate(m_ShipInputControllerPrefab);
        shipInputController.Construct(virtualGamepad);

        Player player = Instantiate(m_PlayerPrefab);
        player.Construct(followCamera, shipInputController, m_SpawnPoint);

        return player;
    }
}
