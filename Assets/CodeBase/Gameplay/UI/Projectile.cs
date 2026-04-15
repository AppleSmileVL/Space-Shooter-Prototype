using UnityEngine;

public class Projectile : Entity, IDependency<Player>
{
    [SerializeField] private float m_Velocity; 
    public float Velocity => m_Velocity;

    [SerializeField] private float m_Lifetime;
    public float Lifetime => m_Lifetime;

    [SerializeField] private int m_Damage;
    public int Damage => m_Damage;

    [SerializeField] private GameObject m_ImpactEffectPrefab;
    public GameObject ImpactEffectPrefab => m_ImpactEffectPrefab;

    protected float m_Timer;
    protected Destructible m_Parent;
    private Player player;

    public void Construct(Player playerInstance)
    {
        player = playerInstance;
        Debug.Log($"[Projectile] Construct вызван. Player: {(player != null ? "OK" : "NULL")}");
    }

    public void SetParentShooter(Destructible parent) => m_Parent = parent;
    
    private void Update()
    {
        float stepLength = Time.deltaTime * m_Velocity;
        Vector2 step = transform.up * stepLength;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

        if (hit)
        {
            HandleHit(hit);
        }

        m_Timer += Time.deltaTime;

        if (m_Timer > m_Lifetime)
        {
            Destroy(gameObject);
        }

        transform.position += (Vector3)step;
    }

    protected void HandleHit(RaycastHit2D hit)
    {
        Destructible destructible = hit.collider.transform.root.GetComponent<Destructible>();
        
        Debug.Log($"[Projectile.HandleHit] Попадание! Destructible: {destructible?.name ?? "null"}");
        
        if (destructible != null && destructible != m_Parent)
        {
            Debug.Log($"[Projectile.HandleHit] Применяем урон {m_Damage}");
            destructible.ApplyDamage(m_Damage);

            Debug.Log($"[Projectile.HandleHit] HitPoints после урона: {destructible.HitPoints}");
            Debug.Log($"[Projectile.HandleHit] Player: {(player != null ? "OK" : "NULL")}");
            Debug.Log($"[Projectile.HandleHit] m_Parent == player.ActiveShip: {(m_Parent == (player?.ActiveShip))}");

            if (destructible.HitPoints <= 0 && player != null && m_Parent == player.ActiveShip)
            {
                Debug.Log($"[Projectile.HandleHit] ✅ НАЧИСЛЯЕМ ОЧКИ! ScoreValue: {destructible.ScoreValue}");
                player.AddScore(destructible.ScoreValue);

                if (destructible is SpaceShip)
                {
                    Debug.Log($"[Projectile.HandleHit] ✅ ДОБАВЛЯЕМ KILL!");
                    player.AddKill();
                }
            }
            else
            {
                Debug.Log($"[Projectile.HandleHit] ❌ НЕ начисляем очки!");
                if (destructible.HitPoints > 0)
                    Debug.Log($"  - HitPoints еще > 0");
                if (player == null)
                    Debug.Log($"  - Player == null");
                if (m_Parent != player?.ActiveShip)
                    Debug.Log($"  - Не корабль игрока (parent != player.ActiveShip)");
            }
        }

        OnProjectileLifeEnd(hit.point);
    }

    private void OnProjectileLifeEnd(Vector2 impactPosition)
    {
        if (m_ImpactEffectPrefab != null)
        {
            Instantiate(m_ImpactEffectPrefab, impactPosition, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
