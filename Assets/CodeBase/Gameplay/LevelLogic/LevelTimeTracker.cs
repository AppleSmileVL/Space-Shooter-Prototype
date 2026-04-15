using UnityEngine;

public class LevelTimeTracker : MonoBehaviour, IDependency<LevelStateTracker>
{
    private LevelStateTracker levelStateTracker;
    public void Construct(LevelStateTracker obj) => levelStateTracker = obj;

    private float currentTime;

    public float CurrentTime => currentTime;

    private void Start()
    {
        levelStateTracker.Started += OnLevelStarted;
        levelStateTracker.Completed += OnLevelCompleted;

        enabled = false;
    }

    private void OnDestroy()
    {
        levelStateTracker.Started -= OnLevelStarted;
        levelStateTracker.Completed -= OnLevelCompleted;
    }

    private void OnLevelStarted()
    {
        enabled = true;
        currentTime = 0;
    }

    private void OnLevelCompleted()
    {
        enabled = false;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }
}
