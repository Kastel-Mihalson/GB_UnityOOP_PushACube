using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    void Start()
    {
        LoadResources();
        InitPlayer();

        for (int i = 0; i < 20; i++)
        {
            InitCubeBonus();
        }

        _interactiveObjects = FindObjectsOfType<InteractiveObjects>().ToList();
    }

    void Update()
    {
        _playerController.PlayerMove(_playerRigidBody, _playerGameObject.transform);

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
}
