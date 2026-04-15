using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static SpaceShip SelectedSpaceShip;

    [SerializeField] private int m_NumLives;
    [SerializeField] private SpaceShip m_PlayerShipPrefab;
    [SerializeField] private float m_RespawnDelay;
    
    public SpaceShip ActiveShip => m_Ship;
    public FollowCamera FollowCamera => m_FollowCamera;

    private ShipInputController m_ShipInputController;
    private FollowCamera m_FollowCamera;
    private Transform m_SpawnPoint;

    private SpaceShip m_Ship;
    private int m_Score;
    private int m_NumKills;

    public int Score => m_Score;
    public int NumKills => m_NumKills;
    public int NumLives => m_NumLives;

    public SpaceShip ShipPrefab
    {
        get => SelectedSpaceShip != null ? SelectedSpaceShip : m_PlayerShipPrefab;
    }

    public void Construct(FollowCamera followCamera, ShipInputController shipInputController, Transform spawnPoint)
    {
        m_FollowCamera = followCamera;
        m_ShipInputController = shipInputController;
        m_SpawnPoint = spawnPoint;
    }

    private void Start()
    {
        Respawn();
        m_Ship.EventOnDeath.AddListener(OnShipDeath);
    }

    private void OnShipDeath()
    {
        m_NumLives--;

        if (m_NumLives > 0)
        {
            StartCoroutine(RespawnWithDelay());
        }
    }

    private IEnumerator RespawnWithDelay()
    {
        yield return new WaitForSeconds(m_RespawnDelay);
        Respawn();
        m_Ship.EventOnDeath.AddListener(OnShipDeath);
    }

    private void Respawn()
    {
        var newPlayerShip = Instantiate(ShipPrefab, m_SpawnPoint.position, m_SpawnPoint.rotation);
        m_Ship = newPlayerShip.GetComponent<SpaceShip>();

        m_ShipInputController.SetTargetShip(m_Ship);
        m_FollowCamera.SetTarget(m_Ship.transform);
    }

    public void AddKill()
    {
        m_NumKills += 1;
    }

    public void AddScore(int num)
    {
        m_Score += num;
    }
}
