using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject m_BackgroundPrefab;

    [Header("Dependencies")]
    [SerializeField] private PlayerSpawner m_PlayerSpawner;
    [SerializeField] private LevelBoundary m_LevelBoundary;
    [SerializeField] private SceneDependenciesContainer m_SceneDependenciesContainer;

    private void Awake()
    {
        if (m_SceneDependenciesContainer == null)
        {
            Debug.LogError("SceneDependenciesContainer не назначен в LevelBuilder!");
            return;
        }

        m_PlayerSpawner.Construct(m_SceneDependenciesContainer);

        Player player = m_PlayerSpawner.Spawn();

        if (player == null)
        {
            Debug.LogError("Не удалось спаунить Player!");
            return;
        }

        m_SceneDependenciesContainer.SetPlayer(player);

        GameObject background = Instantiate(m_BackgroundPrefab);
        SyncTransform syncTransform = background.AddComponent<SyncTransform>();
        syncTransform.SetTarget(player.FollowCamera.transform);
    }
}
