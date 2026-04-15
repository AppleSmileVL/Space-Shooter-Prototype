using UnityEngine;

public class LevelCompletionTime : LevelCondition, IDependency<LevelTimeTracker>
{
    [SerializeField] private float requiredTime;
    private LevelTimeTracker levelTimeTracker;

    public void Construct(LevelTimeTracker timeTracker) => levelTimeTracker = timeTracker;

    public override bool IsCompleted
    {
        get
        {
            if (levelTimeTracker == null)
                return false;

            return levelTimeTracker.CurrentTime >= requiredTime;
        }
    }

    public float GetRequiredTime() => requiredTime;
}