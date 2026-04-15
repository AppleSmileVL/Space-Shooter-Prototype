using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResultLevel : MonoBehaviour, IDependency<LevelResultTime>, IDependency<LevelController>, IDependency<Player>
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private UISelectableButtonContainer buttonContainer;
    [SerializeField] private UIButton previousButton;
    [SerializeField] private UIButton nextButton;
    [SerializeField] private TextMeshProUGUI playerTime;
    [SerializeField] private TextMeshProUGUI kills;
    [SerializeField] private TextMeshProUGUI score;

    private LevelResultTime levelResultTime;
    public void Construct(LevelResultTime obj) => levelResultTime = obj;

    private LevelController levelController;
    public void Construct(LevelController obj) => levelController = obj;

    private Player player;
    public void Construct(Player playerInstance) => player = playerInstance;

    private void Start()
    {
        if (levelResultTime != null)
        {
            levelResultTime.ResultUpdated += OnUpdateResults;
        }

        resultPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        if (levelResultTime != null)
        {
            levelResultTime.ResultUpdated -= OnUpdateResults;
        }
    }

    private void OnUpdateResults()
    {
        resultPanel.SetActive(true);

        if (buttonContainer != null)
            buttonContainer.gameObject.SetActive(true);

        float currentTime = levelResultTime.CurrentTime;

        playerTime.text = StringTime.SecondToTimeString(currentTime);

        if (player != null)
        {
            if (kills != null)
            {
                kills.text = player.NumKills.ToString();
            }

            if (score != null)
            {
                score.text = player.Score.ToString();
            }
        }

        if (previousButton != null && levelController != null)
        {
            bool hasPreviousLevel = levelController.LevelProperties != null &&
                                    levelController.LevelProperties.PreviousLevel != null;

            previousButton.Interactable = hasPreviousLevel;

            var image = previousButton.GetComponent<Image>();
            if (image != null)
            {
                image.color = hasPreviousLevel ? Color.white : new Color(0.5f, 0.5f, 0.5f, 0.5f);
            }
        }

        if (nextButton != null && levelController != null)
        {
            bool isTimeAchieved = currentTime >= levelResultTime.RequiredTime;
            bool hasNextLevel = levelController.HasNextLevel;

            nextButton.Interactable = isTimeAchieved && hasNextLevel;

            var image = nextButton.GetComponent<Image>();
            if (image != null)
            {
                image.color = (isTimeAchieved && hasNextLevel) ? Color.white : new Color(0.5f, 0.5f, 0.5f, 0.5f);
            }
        }
    }
}