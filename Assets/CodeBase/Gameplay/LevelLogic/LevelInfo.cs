using UnityEngine;

[CreateAssetMenu]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private string sceneName;
    [SerializeField] private Sprite icon;
    [SerializeField] private string title;

    [SerializeField] private LevelInfo[] requiredLevelToUnlock;
    
    [SerializeField] private LevelInfo nextLevel;
    [SerializeField] private LevelInfo previousLevel;

    public string SceneName => sceneName;
    public Sprite Icon => icon;
    public string Title => title;
    public LevelInfo NextLevel => nextLevel;
    public LevelInfo PreviousLevel => previousLevel;

    public bool IsUnlocked()
    {
        if (requiredLevelToUnlock == null || requiredLevelToUnlock.Length == 0)
            return true;

        foreach (var track in requiredLevelToUnlock)
        {
            float record = PlayerPrefs.GetFloat(track.SceneName + LevelResultTime.SaveMark, 0f);

            if (record == 0f)
            {
                return false;
            }
        }

        return true;
    }
}
