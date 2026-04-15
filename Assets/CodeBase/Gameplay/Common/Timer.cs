using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Использование 1 (MonoBehaviour):
/// - Поместите на GameObject
/// - Используйте event Finished
/// 
/// Использование 2 (Utility класс):
/// - var timer = new Timer(5f);
/// - timer.Tick(Time.deltaTime);
/// - if (timer.IsFinished) { ... }
/// </summary>
public class Timer : MonoBehaviour
{
    public event UnityAction Finished;

    [SerializeField] private float initialTime;
    private float currentTime;
    private bool isRunning;

    /// <summary>Текущее значение времени</summary>
    public float Value => currentTime;

    /// <summary>Проверка: закончилось ли время</summary>
    public bool IsFinished => currentTime <= 0f;

    /// <summary>Конструктор для использования как обычный класс (без MonoBehaviour)</summary>
    public Timer(float startTime)
    {
        initialTime = startTime;
        currentTime = startTime;
        isRunning = false;
    }

    private void Start()
    {
        // Инициализация для MonoBehaviour режима
        if (initialTime > 0)
        {
            currentTime = initialTime;
        }
    }

    private void Update()
    {
        // Автоматический отсчет только если разрешено
        if (isRunning && currentTime > 0)
        {
            Tick(Time.deltaTime);
        }
    }

    /// <summary>Запустить таймер с начальным значением (для MonoBehaviour режима)</summary>
    public void StartTimer()
    {
        isRunning = true;
        currentTime = initialTime;
    }

    /// <summary>Инициализировать таймер с новым временем и запустить</summary>
    public void Initialize(float startTime)
    {
        initialTime = startTime;
        currentTime = startTime;
        isRunning = true;
    }

    /// <summary>Уменьшить время на deltaTime (Tick - универсальный метод обновления)</summary>
    public void Tick(float deltaTime)
    {
        if (currentTime <= 0f) 
            return;

        currentTime -= deltaTime;

        if (currentTime <= 0f && isRunning)
        {
            currentTime = 0f;
            isRunning = false;
            Finished?.Invoke();
        }
    }

    /// <summary>Остановить таймер</summary>
    public void Stop()
    {
        isRunning = false;
    }

    /// <summary>Сбросить таймер</summary>
    public void Reset()
    {
        currentTime = initialTime;
        isRunning = false;
    }

    /// <summary>Остановить и вернуться в начало</summary>
    public void StopAndReset()
    {
        Stop();
        Reset();
    }
}