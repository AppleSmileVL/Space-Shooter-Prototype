using TMPro;
using UnityEngine;

public class UILevelRecordTime : MonoBehaviour, IDependency<LevelResultTime>, IDependency<LevelStateTracker>, IDependency<LevelCompletionTime>
{
    [SerializeField] private GameObject requiredRecordObject;
    [SerializeField] private GameObject playerRecordObject;
    [SerializeField] TextMeshProUGUI requiredRecordTime;
    [SerializeField] TextMeshProUGUI playerRecordTime;

    private LevelResultTime levelResultTime;
    public void Construct(LevelResultTime obj) => levelResultTime = obj;

    private LevelStateTracker levelStateTracker;
    public void Construct(LevelStateTracker obj) => levelStateTracker = obj;

    private LevelCompletionTime levelCompletionTime;
    public void Construct(LevelCompletionTime obj) => levelCompletionTime = obj;

    private void Start()
    {
        levelStateTracker.Started += OnLevelStart;
        levelStateTracker.Completed += OnLevelComplete;

        requiredRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);
    }

    private void OnDestroy()
    {
        levelStateTracker.Started -= OnLevelStart;
        levelStateTracker.Completed -= OnLevelComplete;
    }

    private void OnLevelStart()
    {
        float requiredTimeValue = levelCompletionTime.GetRequiredTime();

        if (levelResultTime.PlayerRecordTime > requiredTimeValue || levelResultTime.RecordWasSet == false)
        {
            requiredRecordObject.SetActive(true);
            requiredRecordTime.text = StringTime.SecondToTimeString(requiredTimeValue);
        }

        if (levelResultTime.RecordWasSet == true)
        {
            playerRecordObject.SetActive(true);
            playerRecordTime.text = StringTime.SecondToTimeString(levelResultTime.PlayerRecordTime);
        }
    }

    private void OnLevelComplete()
    {
        requiredRecordObject?.SetActive(false);
        playerRecordObject?.SetActive(false);
    }
}
