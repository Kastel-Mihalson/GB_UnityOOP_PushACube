using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    #region Fields

    private GameObject _floorPrefab;
    private GameObject _floorGameObject;
    private GameObject _gameOverDeathLine;

    private GameObject _playerPrefab;
    private GameObject _playerGameObject;
    private PlayerController _playerController;
    private PlayerModel _playerModel;
    private PlayerView _playerView;
    private Rigidbody _playerRigidBody;

    private GameObject _cubeSpeedBonusPrefab;
    private GameObject _cubeSpeedBonusGameObject;

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

    private int _levelSizeX;
    private int _levelSizeZ;

    #endregion

    void Start()
    {
        // Вывод результата по практическому заданию
        List<int> list = new List<int> { 1, 3, 1, 2, 5, 3, 2, 2, 7, 6, 7 };
        Debug.Log($"String Extension HelloWorld.CharCount('l'): {"HelloWorld".CharCount('l')}");
        Debug.Log("Elements occurrences number:");
        foreach (var item in list.ElementsOccNumber())
        {
            Debug.Log($"{item.Key} : {item.Value}");
        }

        Time.timeScale = 1;
        _levelSizeX = 20;
        _levelSizeZ = 30;

        // Загрузка префабов из ресурсов
        LoadResources();
        // Инициализация префабов, моделей, вью и контроллеров
        InitFloor();
        InitPlayer();
        InitPlayerHUD();
        InitCubeSpeedBonus();

        int cubeBonusCount = _playerHUDController.MaxCubeBonusCount;

        for (int i = 0; i < cubeBonusCount; i++)
        {
            float x = Random.Range((int)-10f, (int)9f) + 0.5f;
            float z = Random.Range((int)-10f, (int)9f) + 0.5f;

            InitCubeBonus(x, z);
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
        _floorPrefab = Resources.Load("Floor") as GameObject;
        _playerPrefab = Resources.Load("Player") as GameObject;
        _cubeBonusPrefab = Resources.Load("CubeBonus") as GameObject;
        _cubeSpeedBonusPrefab = Resources.Load("CubeSpeedBonus") as GameObject;
        _playerHUDPrefab = Resources.Load("PlayerHUDCanvas") as GameObject;
        _endGamePrefab = Resources.Load("EndGameCanvas") as GameObject;
    }
    
    #region INIT METHODS

    private void InitFloor()
    {
        _floorGameObject = Instantiate(_floorPrefab, new Vector3(0f, -0.25f, 0f), Quaternion.identity);
        _gameOverDeathLine = new GameObject("GameOverDeathLine");

        _gameOverDeathLine.AddComponent<BoxCollider>().isTrigger = true;
        _gameOverDeathLine.transform.localScale = new Vector3(30f, 2f, 30f);
        _gameOverDeathLine.transform.position = new Vector3(0f, -5f, 0f);
    }

    private void InitPlayer()
    {
        _playerModel = new PlayerModel();
        _playerGameObject = Instantiate(_playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        _playerView = _playerGameObject.GetComponent<PlayerView>();
        _playerRigidBody = _playerGameObject.GetComponent<Rigidbody>();
        _playerController = new PlayerController(_playerModel, _playerView);
        _playerController.EnableEvents();

        _playerView.GameOverDeathLine = _gameOverDeathLine;
    }

    private void InitCubeSpeedBonus()
    {
        _cubeSpeedBonusGameObject = Instantiate(_cubeSpeedBonusPrefab, new Vector3(Random.Range((int)-9f, (int)9f), 0.5f, Random.Range((int)-9f, (int)9f)), Quaternion.identity);
    }

    private void InitCubeBonus(float X, float Z)
    {
        _cubeBonusModel = new CubeBonusModel();
        _cubeBonusGameObject = Instantiate(_cubeBonusPrefab, new Vector3(X, 0.5f, Z), Quaternion.identity);
        _cubeBonusView = _cubeBonusGameObject.GetComponent<CubeBonusView>();
        _cubeBonusController = new CubeBonusController(_cubeBonusModel, _cubeBonusView);
    }

    private void InitPlayerHUD()
    {
        _playerHUDCanvas = Instantiate(_playerHUDPrefab, Vector3.zero, Quaternion.identity);
        _playerHUDView = _playerHUDCanvas.GetComponent<PlayerHUDView>();
        _playerHUDModel = new PlayerHUDModel(_playerHUDView);
        _playerHUDController = new PlayerHUDController(_playerHUDModel, _playerHUDView);
        _playerHUDController.EnableEvent();
    }

    private void InitEndGame()
    {
        _endGameCanvas = Instantiate(_endGamePrefab, Vector3.zero, Quaternion.identity);
        _endGameView = _endGameCanvas.GetComponent<EndGameView>();
        _endGameCanvas.SetActive(false);
        _endGame = false;
    }

    #endregion

    #region UPDATE METHODS

    private void PlayerUpdate()
    {
        _playerController.PlayerMove(_playerRigidBody, _playerGameObject.transform);
        _playerController.SetPlayerDefaultSpeedAfterBonusEffect();
    }

    private void PlayerHUDUpdate()
    {
        _playerHUDController.SetCubeBonusCount();
        _playerHUDController.Timer();
    }

    private void EndGameUpdate()
    {
        if (!_endGame)
        {
            float time = _playerHUDController.TimerSeconds;
            int currentCubeBonusCount = _playerHUDController.CurrentCubeBonusCount;
            int maxCubeBonusCount = _playerHUDController.MaxCubeBonusCount;
            bool isGameOver = _playerView.IsGameOver;

            if (time <= 0 || currentCubeBonusCount == maxCubeBonusCount || isGameOver)
            {
                _endGame = true;
                Time.timeScale = 0;

                _playerHUDCanvas.SetActive(false);
                _endGameCanvas.SetActive(true);
                _endGameView.SetWinOrLoseText(currentCubeBonusCount, maxCubeBonusCount, isGameOver);

                _playerHUDController.DisposeEvent();
                _playerController.DisposeEvents();
            }
        }
    }

    #endregion
}
