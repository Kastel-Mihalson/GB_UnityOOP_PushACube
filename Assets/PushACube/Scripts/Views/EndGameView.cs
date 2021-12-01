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

    private Dictionary<EndGameResult, string> _endGameResultTextColor;

    private enum EndGameResult
    {
        Win = 1,
        Lose,
        Soso
    }

    private void Awake()
    {
        _endGameResultTextColor = new Dictionary<EndGameResult, string>
        {
            { EndGameResult.Win, "9FFF00" },
            { EndGameResult.Lose, "FF4000" },
            { EndGameResult.Soso, "FFF600" }
        };

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
        _winOrLoseText.text = _isWin ? 
            $"<color=#{_endGameResultTextColor[EndGameResult.Win]}>Победа!</color>" : 
            $"<color=#{_endGameResultTextColor[EndGameResult.Lose]}>Поражение!</color>";

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
        _endGameResultText.text =  $"<color=#{_endGameResultTextColor[EndGameResult.Soso]}>{endGameResultText}.</color>\n<color=white>Собрано {cubeBonusCount} из {maxCubeBonusCount} кубиков.</color>";
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
