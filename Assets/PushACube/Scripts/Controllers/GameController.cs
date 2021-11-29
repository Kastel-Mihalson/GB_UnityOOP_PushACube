using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameObject _playerPrefab;
    private GameObject _playerGameObject;
    private PlayerController _playerController;
    private PlayerModel _playerModel;
    private PlayerView _playerView;
    private Rigidbody _playerRigidBody;

    private GameObject _cubeBonusPrefab;
    private GameObject _cubeBonusGameObject;
    private CubeBonusModel _cubeBonusModel;
    private CubeBonusView _cubeBonusView;
    private CubeBonusController _cubeBonusController;
    private List<InteractiveObjects> _interactiveObjects;

    private GameObject _playerHUDPrefab;
    private GameObject _playerHUDCanvas;
    private PlayerHUDModel _playerHUDModel;
    private PlayerHUDView _playerHUDView;
    private PlayerHUDController _playerHUDController;

    private GameObject _endGamePrefab;
    private GameObject _endGameCanvas;
    private EndGameView _endGameView;
    private bool _endGame;

    void Start()
    {
        Time.timeScale = 1;

        LoadResources();
        InitPlayer();
        InitPlayerHUD();

        int cubeBonusCount = _playerHUDView.MaxCubeBonusCount;

        for (int i = 0; i < cubeBonusCount; i++)
        {
            InitCubeBonus();
        }

        InitEndGame();

        _interactiveObjects = FindObjectsOfType<InteractiveObjects>().ToList();
    }

    void Update()
    {
        PlayerUpdate();
        InteractiveObjects();
        PlayerHUDUpdate();
        EndGameUpdate();
    }

    private void InteractiveObjects()
    {
        foreach (var _interactiveObject in _interactiveObjects)
        {
            if (_interactiveObject == null)
            {
                continue;
            }
            if (_interactiveObject is InteractiveObjects interactiveObject)
            {
                interactiveObject.CubeBonusPingPongFlyAnim();
            }
        }
    }

    private void LoadResources()
    {
        _playerPrefab = Resources.Load("Player") as GameObject;
        _cubeBonusPrefab = Resources.Load("CubeBonus") as GameObject;
        _playerHUDPrefab = Resources.Load("PlayerHUDCanvas") as GameObject;
        _endGamePrefab = Resources.Load("EndGameCanvas") as GameObject;
    }

    private void InitPlayer()
    {
        _playerModel = new PlayerModel();
        _playerGameObject = Instantiate(_playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        _playerView = _playerGameObject.GetComponent<PlayerView>();
        _playerRigidBody = _playerGameObject.GetComponent<Rigidbody>();
        _playerController = new PlayerController(_playerModel, _playerView);
        _playerController.Enable();
    }

    private void InitCubeBonus()
    {
        _cubeBonusModel = new CubeBonusModel();
        _cubeBonusGameObject = Instantiate(_cubeBonusPrefab, new Vector3(Random.Range((int)-9f, (int)9f), 0.5f, Random.Range((int)-9f, (int)9f)), Quaternion.identity);
        _cubeBonusView = _cubeBonusGameObject.GetComponent<CubeBonusView>();
        _cubeBonusController = new CubeBonusController(_cubeBonusModel, _cubeBonusView);
    }

    private void InitPlayerHUD()
    {
        _playerHUDCanvas = Instantiate(_playerHUDPrefab, Vector3.zero, Quaternion.identity);
        _playerHUDModel = new PlayerHUDModel();
        _playerHUDView = _playerHUDCanvas.GetComponent<PlayerHUDView>();
        _playerHUDController = new PlayerHUDController(_playerHUDModel, _playerHUDView);
    }

    private void InitEndGame()
    {
        _endGameCanvas = Instantiate(_endGamePrefab, Vector3.zero, Quaternion.identity);
        _endGameView = _endGameCanvas.GetComponent<EndGameView>();
        _endGameCanvas.SetActive(false);
        _endGame = true;
    }

    private void PlayerUpdate()
    {
        _playerController.PlayerMove(_playerRigidBody, _playerGameObject.transform);
    }

    private void PlayerHUDUpdate()
    {
        _playerHUDView.SetCubeBonusCount();
        _playerHUDView.Timer();
    }

    private void EndGameUpdate()
    {
        if (_endGame)
        {
            float time = _playerHUDView.TimerSeconds;
            int currentCubeBonusCount = _playerHUDView.CurrentCubeBonusCount;
            int maxCubeBonusCount = _playerHUDView.MaxCubeBonusCount;

            if (time <= 0 || currentCubeBonusCount == maxCubeBonusCount)
            {
                _endGame = false;
                Time.timeScale = 0;

                _playerHUDCanvas.SetActive(false);
                _endGameCanvas.SetActive(true);
                _endGameView.SetWinOrLoseText(currentCubeBonusCount, maxCubeBonusCount);
                Debug.Log("ВРЕМЯ ВЫШЛО!");
            }
        }
    }
}
