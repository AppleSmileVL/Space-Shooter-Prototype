using TMPro;
using UnityEngine;

public class UILevelTime : MonoBehaviour, IDependency<LevelStateTracker>, IDependency<LevelTimeTracker>
{
    [SerializeField] private TextMeshProUGUI text;

    private LevelStateTracker levelStateTracker;
    public void Construct(LevelStateTracker obj) => levelStateTracker = obj;

    private LevelTimeTracker levelTimeTracker;
    public void Construct(LevelTimeTracker obj) => levelTimeTracker = obj;

    private void Start()
    {
        levelStateTracker.Started += OnLevelStarted;
        levelStateTracker.Completed += OnLevelCompleted;

        text.enabled = false;
    }

    private void OnDestroy()
    {
        levelStateTracker.Started -= OnLevelStarted;
        levelStateTracker.Completed -= OnLevelCompleted;
    }

    private void OnLevelStarted()
    {
        text.enabled = true;
        enabled = true;

    }

    private void OnLevelCompleted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void Update()
    {
        text.text = StringTime.SecondToTimeString(levelTimeTracker.CurrentTime);
    }
}
