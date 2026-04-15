using UnityEngine;

public enum TurretMode
{
    Primary,
    Secondary
}

[CreateAssetMenu]
public sealed class TurretProperties : ScriptableObject 
{
    [SerializeField] private TurretMode m_Mode;
    public TurretMode Mode => m_Mode;

    [SerializeField] private  Projectile m_ProjectilePrefab;
    public Projectile ProjectilePrefab => m_ProjectilePrefab;

    [SerializeField] private float m_RateOfFire;
    public float RateOfFire => m_RateOfFire;

    [SerializeField] private float m_EnergyUsage;
    public float EnergyUsage => m_EnergyUsage;

    [SerializeField] private float m_AmmoUsage;
    public float AmmoUsage => m_AmmoUsage;

    [SerializeField] private AudioClip m_LaunchSFX;
    public AudioClip LaunchSFX => m_LaunchSFX;
}
