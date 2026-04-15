using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour, IDependency<LevelStateTracker>, IDependency<LevelTimeTracker>, IDependency<Player>
{
    private const string MainMenuSceneName = "main_menu";

    public event UnityAction LevelPassed;
    public event UnityAction LevelLost;

    [SerializeField] private LevelInfo levelProperties;
    [SerializeField] private LevelCondition[] conditions;

    private bool isLevelFailed;
    private LevelStateTracker levelStateTracker;
    private LevelTimeTracker levelTimeTracker;
    private Player player;

    public bool HasNextLevel => levelProperties != null && levelProperties.NextLevel != null;
    public bool IsLevelFailed => isLevelFailed;
    public LevelInfo LevelProperties => levelProperties;

    public void Construct(LevelStateTracker stateTracker) => levelStateTracker = stateTracker;
    public void Construct(LevelTimeTracker timeTracker) => levelTimeTracker = timeTracker;
    public void Construct(Player playerInstance) => player = playerInstance;

    private void OnEnable()
    {
        if (levelStateTracker != null)
        {
            levelStateTracker.Started += OnLevelStarted;
        }
    }

    private void OnDisable()
    {
        if (levelStateTracker != null)
        {
            levelStateTracker.Started -= OnLevelStarted;
        }
    }

    private void Update()
    {
        if (isLevelFailed || levelStateTracker == null || levelStateTracker.State != LevelState.Action)
            return;

        CheckLevelConditions();
        CheckPlayerLives();
    }

    private void OnLevelStarted()
    {
        isLevelFailed = false;
        Time.timeScale = 1.0f;
    }

    private void CheckLevelConditions()
    {
        if (conditions == null || conditions.Length == 0)
            return;

        int completedCount = 0;

        for (int i = 0; i < conditions.Length; i++)
        {
            if (conditions[i].IsCompleted)
            {
                completedCount++;
            }
        }

        if (completedCount == conditions.Length && !isLevelFailed)
        {
            Pass();
        }
    }

    private void CheckPlayerLives()
    {
        if (player != null && player.NumLives <= 0 && !isLevelFailed)
        {
            Lose();
        }
    }

    private void Lose()
    {
        isLevelFailed = true;
        LevelLost?.Invoke();
        Time.timeScale = 1.0f;

        if (levelStateTracker != null)
        {
            levelStateTracker.CompleteLevel();
        }
    }

    private void Pass()
    {
        isLevelFailed = true;
        LevelPassed?.Invoke();
        Time.timeScale = 0;

        if (levelStateTracker != null)
        {
            levelStateTracker.CompleteLevel();
        }
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1.0f;

        if (HasNextLevel)
        {
            SceneManager.LoadScene(levelProperties.NextLevel.SceneName);
        }
        else
        {
            LoadMainMenu();
        }
    }

    public void LoadPreviousLevel()
    {
        Time.timeScale = 1.0f;

        if (levelProperties != null && levelProperties.PreviousLevel != null)
        {
            SceneManager.LoadScene(levelProperties.PreviousLevel.SceneName);
        }
        else
        {
            LoadMainMenu();
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;

        if (levelProperties != null)
        {
            SceneManager.LoadScene(levelProperties.SceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(MainMenuSceneName);
    }
}