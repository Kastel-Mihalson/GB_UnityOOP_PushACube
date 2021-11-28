using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDController
{
    private GameObject _playerHUDPrefab;
    private PlayerHUDModel _playerHUDModel;
    private PlayerHUDView _playerHUDView;

    public PlayerHUDController(PlayerHUDModel playerHUDModel, PlayerHUDView playerHUDView)
    {
        _playerHUDModel = playerHUDModel;
        _playerHUDView = playerHUDView;
    }
}
