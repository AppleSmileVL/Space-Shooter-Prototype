using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesIndicators : MonoBehaviour, IDependency<Player>
{
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private Image m_Icon;

    private Player player;
    private int lastLives;

    public void Construct(Player playerInstance) => player = playerInstance;

    private void Start()
    {
        if (player != null && player.ActiveShip != null)
        {
            m_Icon.sprite = player.ActiveShip.PreviewImage;
        }
    }

    private void Update()
    {
        if (player == null) return;

        int lives = player.NumLives;

        if (lastLives != lives)
        {
            m_Text.text = lives.ToString();
            lastLives = lives;
        }
    }
}