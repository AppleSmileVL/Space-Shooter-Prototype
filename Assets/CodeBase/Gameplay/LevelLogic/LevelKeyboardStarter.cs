using UnityEngine;

public class LevelKeyboardStarter : MonoBehaviour, IDependency<LevelStateTracker>
{
    private LevelStateTracker levelStateTracker;
    public void Construct(LevelStateTracker obj) => levelStateTracker = obj;

    void Start()
    {
        if (UnityEngine.EventSystems.EventSystem.current != null)
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) == true)
        {
            levelStateTracker.LaunchPreparationStarted();
        }
    }
}
