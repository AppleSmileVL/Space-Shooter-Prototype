using UnityEngine;

public class LevelCompletionScore : LevelCondition, IDependency<Player>
{
    [SerializeField] private int m_Score;
    private Player player;

    public void Construct(Player playerInstance) => player = playerInstance;

    public override bool IsCompleted
    {
        get
        {
            if (player == null || player.ActiveShip == null) 
                return false;

            if (player.Score >= m_Score)
            {
                return true;
            }

            return false;
        }
    }
}
