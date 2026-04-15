using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelResultTime : MonoBehaviour, IDependency<LevelStateTracker>, IDependency<LevelTimeTracker>, IDependency<LevelCompletionTime>
{
    public const string SaveMark = "_player_best_time";

    public event UnityAction ResultUpdated;

    private float playerRecordTime;
    private float currentTime;
    private float requiredTime;

    public float RequiredTime => requiredTime;
    public float PlayerRecordTime => playerRecordTime;
    public float CurrentTime => currentTime;

    public bool RecordWasSet => playerRecordTime != 0;

    private LevelStateTracker levelStateTracker;
    public void Construct(LevelStateTracker obj) => levelStateTracker = obj;

    private LevelTimeTracker levelTimeTracker;
    public void Construct(LevelTimeTracker obj) => levelTimeTracker = obj;

    private LevelCompletionTime levelCompletionTime;
    public void Construct(LevelCompletionTime obj)
    {
        levelCompletionTime = obj;
        // ѕолучаем требуемое врем€ из услови€ победы
        requiredTime = obj.GetRequiredTime();
    }

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        levelStateTracker.Completed += OnLevelCompleted;
    }

    private void OnDestroy()
    {
        levelStateTracker.Completed -= OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {
        currentTime = levelTimeTracker.CurrentTime;

        // —охран€ем рекорд, если врем€ >= требуемого времени
        if (currentTime >= requiredTime && playerRecordTime == 0)
        {
            playerRecordTime = currentTime;
            Save();
        }

        ResultUpdated?.Invoke();
    }

    public bool IsSurvivalTimeAchieved()
    {
        return playerRecordTime >= requiredTime;
    }

    private void Load()
    {
        playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
    }
}
