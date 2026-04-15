using UnityEngine;
using TMPro;

public class KillText : MonoBehaviour, IDependency<Player>
{
    [SerializeField] private TextMeshProUGUI m_Text;
    private Player player;
    private int lastNumKills;

    public void Construct(Player playerInstance) => player = playerInstance;

    private void Update()
    {
        if (player == null) return;

        int numKills = player.NumKills;

        if (lastNumKills != numKills)
        {
            m_Text.text = "Kills: " + numKills.ToString();
            lastNumKills = numKills;
        }
    }
}
