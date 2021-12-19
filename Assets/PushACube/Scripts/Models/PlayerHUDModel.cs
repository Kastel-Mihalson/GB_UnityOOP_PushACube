using UnityEngine.UI;

public class PlayerHUDModel
{
    public Text timer;
    public Text cubeBonusCounter;
    public Button restartLevelButton;
    public float timerSeconds;
    public int maxCubeBonusCount;
    public int currentCubeBonusCount;
    public int isCurrentCubeBonusCountChanged;

    public int CurrentCubeBonusCount
    {
        get => currentCubeBonusCount;
        set => currentCubeBonusCount = value;
    }

    public float TimerSeconds
    {
        get => timerSeconds;
        set => timerSeconds = value;
    }

    public PlayerHUDModel(PlayerHUDView playerHUDView)
    {
        timer = playerHUDView.transform.GetChild(1).GetComponent<Text>();
        cubeBonusCounter = playerHUDView.transform.GetChild(3).GetComponent<Text>();
        restartLevelButton = playerHUDView.transform.GetChild(4).GetComponent<Button>();

        currentCubeBonusCount = 0;
        maxCubeBonusCount = 20;
        timerSeconds = 60f;
        cubeBonusCounter.text = $"0 / {maxCubeBonusCount}";
    }
}
