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

    private GameObject _mainCamera;

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

    private float _levelSizeX;
    private float _levelSizeZ;

    private readonly KeyCode _saveGame = KeyCode.F5;
    private readonly KeyCode _loadGame = KeyCode.F9;
    private SaveDataRepository _saveDataRepository;
    private SavedData _savedData;

    #endregion

    void Awake()
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
        _saveDataRepository = new SaveDataRepository();
        _savedData = new SavedData();

        LoadResources();
        InitFloor();

        _levelSizeX = _floorGameObject.transform.localScale.x;
        _levelSizeZ = _floorGameObject.transform.localScale.z;

        InitPlayer();
        InitCamera();
        InitPlayerHUD();

        float cubeSpeedBonusX = Random.Range((int)-(_levelSizeX / 2), (int)((_levelSizeX - 2) / 2)) + 0.5f;
        float cubeSpeedBonusZ = Random.Range((int)-(_levelSizeZ / 2), (int)((_levelSizeZ - 2) / 2)) + 0.5f;
        InitCubeSpeedBonus(cubeSpeedBonusX, 0.5f, cubeSpeedBonusZ);

        // int cubeBonusCount = _playerHUDController.MaxCubeBonusCount;
        int cubeBonusCount = (int)_levelSizeX;

        for (int i = 0; i < cubeBonusCount; i++)
        {
            float x = Random.Range((int)-(_levelSizeX / 2), (int)((_levelSizeX - 2) / 2)) + 0.5f;
            float z = Random.Range((int)-(_levelSizeZ / 2), (int)((_levelSizeZ - 2) / 2)) + 0.5f;
            InitCubeBonus(x, z);
        }

        InitEndGame();

        _interactiveObjects = FindObjectsOfType<InteractiveObjects>().ToList();
    }

    void Update()
    {
        PlayerUpdate();
        InteractiveObjectsUpdate();
        PlayerHUDUpdate();
        EndGameUpdate();
        ExecuteSaveAndLoad();
    }

    #region EXECUTE ONE TINE METHODS

    public void ExecuteSaveAndLoad()
    {
        if (Input.GetKeyDown(_saveGame))
        {
            _savedData = InitSavedData();
            _saveDataRepository.Save(_savedData);
            Debug.Log("Press F5");
        }
        if (Input.GetKeyDown(_loadGame))
        {
            var loadingData = _saveDataRepository.Load();
            InitLoadingSavedData(loadingData);
            Debug.Log("Press F9");
        }
    }

    #endregion

    #region INIT METHODS

    private void LoadResources()
    {
        _floorPrefab = Resources.Load("Floor") as GameObject;
        _playerPrefab = Resources.Load("Player") as GameObject;
        _cubeBonusPrefab = Resources.Load("CubeBonus") as GameObject;
        _cubeSpeedBonusPrefab = Resources.Load("CubeSpeedBonus") as GameObject;
        _playerHUDPrefab = Resources.Load("PlayerHUDCanvas") as GameObject;
        _endGamePrefab = Resources.Load("EndGameCanvas") as GameObject;
    }

    private void InteractiveObjectsUpdate()
    {
        if (_interactiveObjects == null)
        {
            _interactiveObjects = FindObjectsOfType<InteractiveObjects>().ToList();
        }
        else
        {
            foreach (var _interactiveObject in _interactiveObjects)
            {
                if (_interactiveObject == null)
                {
                    continue;
                }
                if (_interactiveObject is CubeBonusView interactiveObject)
                {
                    interactiveObject.CubeBonusPingPongFlyAnim();
                }
            }
        }
    }

    private SavedData InitSavedData()
    {
        return new SavedData
        {
            Name = _playerView.gameObject.name,
            IsEnabled = _playerView.gameObject.activeSelf,
            Position = _playerView.transform.position,
            Rotation = _playerView.transform.rotation,
            Time = _playerHUDModel.TimerSeconds,
            CubeBonusCount = _playerHUDModel.CurrentCubeBonusCount,
            CubeBonusPositions = _interactiveObjects.Where(i => i != null && (i is CubeBonusView))
                    .Select(i => i.transform.position).ToList(),
            CubeSpeedBonusPositions = _interactiveObjects.Where(i => i != null && (i is CubeSpeedBonusView))
                    .Select(i => i.transform.position).ToList()
        };
    }

    private void InitLoadingSavedData(SavedData loadingSavedData)
    {
        _playerView.gameObject.name = loadingSavedData.Name;
        _playerView.transform.position = loadingSavedData.Position;
        _playerView.transform.rotation = loadingSavedData.Rotation;
        _playerView.gameObject.SetActive(loadingSavedData.IsEnabled);
        _playerHUDModel.TimerSeconds = loadingSavedData.Time;
        _playerHUDModel.CurrentCubeBonusCount = loadingSavedData.CubeBonusCount;

        // delete all interactiveObjects
        var index = 0;
        var interactObjectCount = _interactiveObjects.Where(i => i != null).ToList();

        for (int i = 0; i < interactObjectCount.Count(); i++)
        {
            Destroy(interactObjectCount[index].gameObject);
            index++;
        }
        _interactiveObjects = null;

        // instantiate interactiveObject by their position from loadingData
        foreach (var cubeBonusPosition in loadingSavedData.CubeBonusPositions)
        {
            InitCubeBonus(cubeBonusPosition.x, cubeBonusPosition.z);
        }

        // instantiate cubeSpeedBonus by their position from loadingData
        foreach (var item in loadingSavedData.CubeSpeedBonusPositions)
        {
            InitCubeSpeedBonus(item.x, item.y, item.z);
        }

        _interactiveObjects = FindObjectsOfType<InteractiveObjects>().ToList();
    }

    private void InitFloor()
    {
        _floorGameObject = Instantiate(_floorPrefab, new Vector3(0f, -0.25f, 0f), Quaternion.identity);
        _gameOverDeathLine = new GameObject("GameOverDeathLine");

        _gameOverDeathLine.AddComponent<BoxCollider>().isTrigger = true;
        _gameOverDeathLine.transform.localScale = new Vector3(
            _floorGameObject.transform.localScale.x + 15f, 
            2f, 
            _floorGameObject.transform.localScale.z + 15f);
        _gameOverDeathLine.transform.position = new Vector3(
            _floorGameObject.transform.position.x, 
            _floorGameObject.transform.position.y - 4.75f, 
            _floorGameObject.transform.position.z);
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

    private void InitCamera()
    {
        _mainCamera = new GameObject("mainCamera");
        _mainCamera.AddComponent<Camera>();
        _mainCamera.AddComponent<DungeonCamera>();
        _mainCamera.transform.position = _playerGameObject.transform.position + new Vector3(0, 10f, 0);
        _mainCamera.transform.rotation = Quaternion.identity;

        var dungeonCamera = _mainCamera.gameObject.GetComponent<DungeonCamera>();
        dungeonCamera.Target = _playerGameObject;
    }

    private void InitCubeSpeedBonus(float X, float Y, float Z)
    {
        _cubeSpeedBonusGameObject = Instantiate(
            _cubeSpeedBonusPrefab, new Vector3(X, Y, Z), Quaternion.identity);
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
