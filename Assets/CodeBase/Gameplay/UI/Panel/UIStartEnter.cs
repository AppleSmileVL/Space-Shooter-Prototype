using TMPro;
using UnityEngine;

public class UIStartEnter : MonoBehaviour, IDependency<LevelStateTracker>
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] GameObject title;

    private LevelStateTracker levelStateTracker;
    public void Construct(LevelStateTracker obj) => levelStateTracker = obj;

    private void Start()
    {
        levelStateTracker.PreparationStarted += OnLevelStarted;

        text.enabled = true;
        title.SetActive(true);
        enabled = true;
    }
    private void OnDestroy()
    {
        levelStateTracker.PreparationStarted -= OnLevelStarted;
    }
    private void OnLevelStarted()
    {
        text.enabled = false;
        title.SetActive(false);
        enabled = false;
    }
}