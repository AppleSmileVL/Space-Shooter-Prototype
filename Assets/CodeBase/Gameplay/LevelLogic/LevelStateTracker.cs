using UnityEngine;
using UnityEngine.Events;

public enum LevelState
{
    Preparation,
    CountDown,
    Action,
    Passed
}

public class LevelStateTracker : MonoBehaviour
{
    public event UnityAction PreparationStarted;
    public event UnityAction Started;
    public event UnityAction Completed;

    [SerializeField] private Timer countdownTimer;

    public Timer CountDownTimer => countdownTimer;

    private LevelState state;
    public LevelState State => state;

    private void StartState(LevelState newState)
    {
        state = newState;
    }

    private void Start()
    {
        StartState(LevelState.Preparation);

        if (countdownTimer != null)
        {
            countdownTimer.enabled = false;
            countdownTimer.Finished += OnCountdownTimerFinished;
        }
    }

    private void OnDestroy()
    {
        if (countdownTimer != null)
        {
            countdownTimer.Finished -= OnCountdownTimerFinished;
        }
    }

    private void OnCountdownTimerFinished()
    {
        StartLevel();
    }

    public void LaunchPreparationStarted()
    {
        if (state != LevelState.Preparation)
            return;

        StartState(LevelState.CountDown);

        if (countdownTimer != null)
        {
            countdownTimer.enabled = true;
            countdownTimer.StartTimer();
            PreparationStarted?.Invoke();
        }
    }

    private void StartLevel()
    {
        if (state != LevelState.CountDown)
            return;

        StartState(LevelState.Action);

        if (countdownTimer != null)
        {
            countdownTimer.enabled = false;
            countdownTimer.Stop();
        }

        Started?.Invoke();
    }

    public void CompleteLevel()
    {
        if (state != LevelState.Action)
            return;

        StartState(LevelState.Passed);

        if (countdownTimer != null)
        {
            countdownTimer.StopAndReset();
        }

        Completed?.Invoke();
    }
}
