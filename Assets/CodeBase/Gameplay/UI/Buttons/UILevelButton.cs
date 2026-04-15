using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILevelButton : UISelectableButton, IScriptableObjectProperty
{
    [SerializeField] private LevelInfo levelInfo;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;

    private void Start()
    {
        ApplyProperty(levelInfo);
        onClick.AddListener(OnSubmit);
    }

    private void OnDestroy()
    {
        onClick.RemoveListener(OnSubmit);
    }

    public override void Submit()
    {
        base.Submit();
        OnSubmit();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }

    private void OnSubmit()
    {
        if (interactable == false || levelInfo == null) return;

        var container = GetComponentInParent<UISelectableButtonContainer>();
        if (container != null) container.Interactable = false;

        SceneManager.LoadScene(levelInfo.SceneName);
    }

    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null || property is LevelInfo == false) return;

        levelInfo = property as LevelInfo;
        icon.sprite = levelInfo.Icon;
        title.text = levelInfo.Title;

        interactable = levelInfo.IsUnlocked();

        if (icon != null)
            icon.color = interactable ? Color.white : new Color(0.3f, 0.3f, 0.3f, 1f);

        if (title != null)
            title.color = interactable ? Color.white : Color.gray;
    }
}
