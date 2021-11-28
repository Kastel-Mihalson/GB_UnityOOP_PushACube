using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHUDView : MonoBehaviour
{
    private Text _timer;
    private Text _cubeBonusCounter;
    private Button _restartLevelButton;
    private float _timerSeconds;
    private int _maxCubeBonusCount;
    private int _currentCubeBonusCount;
    private int _isCurrentCubeBonusCountChanged;

    private void Awake()
    {
        _timer = transform.GetChild(1).GetComponent<Text>();
        _cubeBonusCounter = transform.GetChild(3).GetComponent<Text>();
        _restartLevelButton = transform.GetChild(4).GetComponent<Button>();

        _restartLevelButton.onClick.AddListener(RestartLevel);

        _isCurrentCubeBonusCountChanged = 0;
        _currentCubeBonusCount = 0;
        _maxCubeBonusCount = 20;
        _timerSeconds = 20f;
        _cubeBonusCounter.text = $"0 / {_maxCubeBonusCount}";
    }

    public int CurrentCubeBonusCount
    { 
        get => _currentCubeBonusCount;
        set
        {
            _currentCubeBonusCount += value;
        }
    }

    public int MaxCubeBonusCount => _maxCubeBonusCount;
    public float TimerSeconds => _timerSeconds;

    public void SetCubeBonusCount()
    {
        if (_currentCubeBonusCount != _isCurrentCubeBonusCountChanged)
        {
            Debug.Log("Количество бонусов изменилось. Метод вызвался!");
            _cubeBonusCounter.text = $"{_currentCubeBonusCount} / {_maxCubeBonusCount}";
            _isCurrentCubeBonusCountChanged = _currentCubeBonusCount;
        }
    }

    public void Timer()
    {
        if (_timerSeconds > 0)
        {
            _timerSeconds -= Time.deltaTime;
            _timer.text = Mathf.Round(_timerSeconds).ToString();
        }
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
