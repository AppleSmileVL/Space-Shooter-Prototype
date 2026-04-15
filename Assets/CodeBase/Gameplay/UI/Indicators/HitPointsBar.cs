using UnityEngine;
using UnityEngine.UI;

public class HitPointsBar : MonoBehaviour, IDependency<Player>
{
    [SerializeField] private Image m_Image;
    private Player player;
    private float lastHitPoints;

    public void Construct(Player playerInstance) => player = playerInstance;

    private void Update()
    {
        if (player == null || player.ActiveShip == null) return;

        float hitPoints = (float)player.ActiveShip.HitPoints / (float)player.ActiveShip.MaxHitPoints;

        if (hitPoints != lastHitPoints)
        {
            m_Image.fillAmount = hitPoints;
            lastHitPoints = hitPoints;
        }
    }
}