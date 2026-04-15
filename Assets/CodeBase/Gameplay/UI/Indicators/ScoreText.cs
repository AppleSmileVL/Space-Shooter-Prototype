using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour, IDependency<Player>
{
    [SerializeField] private TextMeshProUGUI m_Text;
    private Player player;
    private int lastScoreText;

    public void Construct(Player playerInstance) => player = playerInstance;

    private void Update()
    {
        if (player == null) return;

        int score = player.Score;

        if (lastScoreText != score)
        {
            m_Text.text = "Score: " + score.ToString();
            lastScoreText = score;
        }
    }
}