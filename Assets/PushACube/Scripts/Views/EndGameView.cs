using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameView : MonoBehaviour
{
    private Text _winOrLoseText;
    private Text _endGameResultText;
    private Button _restartGameButton;
    private bool _isWin;
    private string[] _endGameResultTexts;

    private void Awake()
    {
        _winOrLoseText = transform.GetChild(0).GetComponent<Text>();
        _endGameResultText = transform.GetChild(1).GetComponent<Text>();
        _restartGameButton = transform.GetChild(2).GetComponent<Button>();

        _restartGameButton.onClick.AddListener(RestartGame);

        _endGameResultTexts = new string[]
        { 
            "Превосходный результат",
            "Вы хорошо справились",
            "Не плохой результат",
            "Эх, мда-а. Чуть-чуть не хватило",
            "Вы были далеко от победы"
        };
    }

    public void SetWinOrLoseText(int cubeBonusCount, int maxCubeBonusCount)
    {
        _isWin = cubeBonusCount > 13;

        _winOrLoseText.color = _isWin ? new Color(116, 255, 0) : new Color(255, 64, 0);
        _winOrLoseText.text = _isWin ? "Победа!" : "Поражение!";

        string endGameResultText = "";
        
        if (cubeBonusCount == maxCubeBonusCount)
        {
            endGameResultText = _endGameResultTexts[0];
        }
        else if (cubeBonusCount >= 17 && cubeBonusCount <= 19)
        {
            endGameResultText = _endGameResultTexts[1];
        }
        else if (cubeBonusCount >= 14 && cubeBonusCount <= 16)
        {
            endGameResultText = _endGameResultTexts[2];
        }
        else if (cubeBonusCount >= 11 && cubeBonusCount <= 13)
        {
            endGameResultText = _endGameResultTexts[3];
        }
        else if (cubeBonusCount <= 10)
        {
            endGameResultText = _endGameResultTexts[4];
        }
        _endGameResultText.text =  $"{endGameResultText}. Собрано {cubeBonusCount} из {maxCubeBonusCount} кубиков.";
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
