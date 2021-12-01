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

    private Dictionary<GameColor, string> _endGameResultTextColor;

    private enum GameColor
    {
        GreenWinner = 1,
        TomatoLoser,
        YellowTitle
    }

    private void Awake()
    {
        _endGameResultTextColor = new Dictionary<GameColor, string>
        {
            { GameColor.GreenWinner, "9FFF00" },
            { GameColor.TomatoLoser, "FF4000" },
            { GameColor.YellowTitle, "FFF600" }
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
            "Вы были далеко от победы",
            "Эй?! Ты куда?"
        };
    }

    public void SetWinOrLoseText(int cubeBonusCount, int maxCubeBonusCount, bool isGameOver)
    {
        string endGameResultText = "";

        if (isGameOver)
        {
            _isWin = !isGameOver;
            endGameResultText = _endGameResultTexts[5];
        }
        else
        {
            _isWin = cubeBonusCount > 13;

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
        }
        
        _winOrLoseText.text = _isWin ? 
            $"<color=#{_endGameResultTextColor[GameColor.GreenWinner]}>Победа!</color>" : 
            $"<color=#{_endGameResultTextColor[GameColor.TomatoLoser]}>Поражение!</color>";

        _endGameResultText.text =  $"<color=#{_endGameResultTextColor[GameColor.YellowTitle]}>{endGameResultText}.</color>\n<color=white>Собрано {cubeBonusCount} из {maxCubeBonusCount} кубиков.</color>";
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
