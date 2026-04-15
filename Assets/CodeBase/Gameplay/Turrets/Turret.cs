using UnityEngine;

public class Turret : MonoBehaviour, IDependency<Player>
{
    [SerializeField] private TurretMode m_Mode;
    public TurretMode Mode => m_Mode;

    [SerializeField] private TurretProperties m_TurretProperties;
    private float m_RerfireTimer;
    public bool CanFire => m_RerfireTimer <= 0f;

    private SpaceShip m_Ship;
    
    private Player player;
    private bool isConstructed = false;

    public void Construct(Player playerInstance)
    {
        player = playerInstance;
        isConstructed = true;
        Debug.Log($"[Turret] Construct вызван. Player: {(player != null ? "OK" : "NULL")}");
    }

    private void Start()
    {
        m_Ship = transform.root.GetComponent<SpaceShip>();
        Debug.Log($"[Turret] Start. Player в Turret: {(player != null ? "OK" : "NULL")}");
    }

    private void Update()
    {
        if (m_RerfireTimer > 0)
            m_RerfireTimer -= Time.deltaTime;
    }

    public void Fire()
    {
        if (m_TurretProperties == null)
            return;

        if (m_RerfireTimer > 0)
            return;

        if (m_Ship.DrawEnergy((int)m_TurretProperties.EnergyUsage) == false)
            return;

        if (m_Ship.DrawAmmo((int)m_TurretProperties.AmmoUsage) == false) 
            return;

        Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.transform.up = transform.up;

        projectile.SetParentShooter(m_Ship);

        // Ленивая инициализация Player при первом выстреле
        if (player == null && !isConstructed)
        {
            player = FindPlayerInstance();
            if (player != null)
            {
                isConstructed = true;
                Debug.Log($"[Turret.Fire] ✅ Player найден динамически");
            }
        }

        Debug.Log($"[Turret.Fire] Player перед передачей в Projectile: {(player != null ? "OK" : "NULL")}");

        if (player != null)
        {
            Debug.Log($"[Turret.Fire] ✅ Передаем Player в Projectile");
            projectile.Construct(player);
        }
        else
        {
            Debug.LogError($"[Turret.Fire] ❌ ОШИБКА! Player == null!");
        }

        m_RerfireTimer = m_TurretProperties.RateOfFire;
    }

    private Player FindPlayerInstance()
    {
        // Пытаемся получить Player через SpaceShip
        if (m_Ship != null)
        {
            Player playerComponent = m_Ship.GetComponentInParent<Player>();
            if (playerComponent != null)
                return playerComponent;
        }

        // Если не нашли, ищем в сцене (как последний вариант)
        return FindObjectOfType<Player>();
    }

    public void AssignLoadout(TurretProperties properties)
    {
        if (m_Mode != properties.Mode) 
            return;                    

        m_RerfireTimer = 0;
        m_TurretProperties = properties; 
    }
}
