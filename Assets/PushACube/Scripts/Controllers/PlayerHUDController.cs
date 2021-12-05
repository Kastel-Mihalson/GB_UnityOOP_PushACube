using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHUDController
{
    private PlayerHUDModel _playerHUDModel;
    private PlayerHUDView _playerHUDView;

    public int MaxCubeBonusCount => _playerHUDModel.maxCubeBonusCount;
    public float TimerSeconds => _playerHUDModel.timerSeconds;
    public int CurrentCubeBonusCount
    {
        get => _playerHUDModel.currentCubeBonusCount;
        set => _playerHUDModel.currentCubeBonusCount += value;
    }

    public PlayerHUDController(PlayerHUDModel playerHUDModel, PlayerHUDView playerHUDView)
    {
        _playerHUDModel = playerHUDModel;
        _playerHUDView = playerHUDView;

        _playerHUDModel.restartLevelButton.onClick.AddListener(RestartLevel);
    }

    public void EnableEvent()
    {
        _playerHUDView.CubeBonusCountChangedEvent += CubeBonusCountChanged;
    }

    public void DisposeEvent()
    {
        _playerHUDView.CubeBonusCountChangedEvent -= CubeBonusCountChanged;
    }

    public void Timer()
    {
        if (_playerHUDModel.timerSeconds > 0)
        {
            _playerHUDModel.timerSeconds -= Time.deltaTime;
            _playerHUDModel.timer.text = Mathf.Round(_playerHUDModel.timerSeconds).ToString();
        }
    }

    public void SetCubeBonusCount()
    {
        if (_playerHUDModel.currentCubeBonusCount != _playerHUDModel.isCurrentCubeBonusCountChanged)
        {
            _playerHUDModel.cubeBonusCounter.text = $"{_playerHUDModel.currentCubeBonusCount} / {_playerHUDModel.maxCubeBonusCount}";
            _playerHUDModel.isCurrentCubeBonusCountChanged = _playerHUDModel.currentCubeBonusCount;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void CubeBonusCountChanged(int cubeCount)
    {
        _playerHUDModel.CurrentCubeBonusCount = cubeCount;
    }
}
