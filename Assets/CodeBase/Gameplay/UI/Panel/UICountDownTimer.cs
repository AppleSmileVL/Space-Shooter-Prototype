using UnityEngine;
using TMPro;

public class UICountDownTimer : MonoBehaviour, IDependency<LevelStateTracker>
{
    [SerializeField] private TextMeshProUGUI text;
    private Timer countdowmTimer;

    private LevelStateTracker levelStateTracker;
    public void Construct(LevelStateTracker obj) => levelStateTracker = obj;

    private void Start()
    {
        levelStateTracker.PreparationStarted += OnPreparationStarted;
        levelStateTracker.Started += OnLevelStarted;

        text.enabled = false;
    }

    private void OnDestroy()
    {
        levelStateTracker.PreparationStarted -= OnPreparationStarted;
        levelStateTracker.Started -= OnLevelStarted;
    }

    private void OnPreparationStarted()
    {
        text.enabled = true;
        enabled = true;
    }

    private void OnLevelStarted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void Update()
    {
        text.text = levelStateTracker.CountDownTimer.Value.ToString("F0");

        if (text.text == "0")
            text.text = "ACTION!";
    }
}
