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
    private float _playerSaveSpeed;
    private float _cubeSpeedBonusTime;

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

        // Загрузка префабов из ресурсов
        LoadResources();
        // Инициализация префабов, моделей, вью и контроллеров
        InitFloor();
        InitPlayer();
        InitPlayerHUD();
        InitCubeSpeedBonus();

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
        IsPlayerMoveSpeedChanged();
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
        _playerController.Enable();

        _playerView.GameOverDeathLine = _gameOverDeathLine;
        _playerSaveSpeed = _playerModel.MoveSpeed;
        _cubeSpeedBonusTime = _playerModel.MoveSpeedBonusTime;
    }

    private void InitCubeSpeedBonus()
    {
        _cubeSpeedBonusGameObject = Instantiate(_cubeSpeedBonusPrefab, new Vector3(Random.Range((int)-9f, (int)9f), 0.5f, Random.Range((int)-9f, (int)9f)), Quaternion.identity);
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
        _endGame = false;
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
        if (!_endGame)
        {
            float time = _playerHUDView.TimerSeconds;
            int currentCubeBonusCount = _playerHUDView.CurrentCubeBonusCount;
            int maxCubeBonusCount = _playerHUDView.MaxCubeBonusCount;
            bool isGameOver = _playerView.IsGameOver;

            if (time <= 0 || currentCubeBonusCount == maxCubeBonusCount || isGameOver)
            {
                _endGame = true;
                Time.timeScale = 0;
                _playerController.Dispose();

                _playerHUDCanvas.SetActive(false);
                _endGameCanvas.SetActive(true);
                _endGameView.SetWinOrLoseText(currentCubeBonusCount, maxCubeBonusCount, isGameOver);
                Debug.Log("ВРЕМЯ ВЫШЛО!");
            }
        }
    }

    private void IsPlayerMoveSpeedChanged()
    {
        // Если скорость игрока изменилась, то
        if (_playerModel.MoveSpeed != _playerSaveSpeed)
        {
            // Пока не прошло _playerModel.MoveSpeedBonusTime секунд
            _playerModel.MoveSpeedBonusTime -= Time.deltaTime;
            
            if (_playerModel.MoveSpeedBonusTime <= 0)
            {
                // Когда прошло _playerModel.MoveSpeedBonusTime секунд, то
                // Восстанавливаем изначальную скорость
                _playerModel.MoveSpeed = _playerSaveSpeed;
                // Устанавливаем время снова на прежднее значение
                _playerModel.MoveSpeedBonusTime = _cubeSpeedBonusTime;
                
            }
        }
    }
}
