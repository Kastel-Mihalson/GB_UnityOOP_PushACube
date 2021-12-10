using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public sealed class SaveDataRepository
{
    private readonly IData<SavedData> _data;
    private readonly string _path;
    private const string _folderName = "GameSave";
    private const string _fileName = "saveData.bat";

    public SaveDataRepository()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            //_data = new PlayerPrefsData();
        }
        else
        {
            _data = new SerializableXMLData<SavedData>();
        }

        _path = Path.Combine(Application.dataPath, _folderName);
    }

    public void Save(PlayerView player, PlayerHUDModel playerHUDModel)
    {
        if (!Directory.Exists(Path.Combine(_path)))
        {
            Directory.CreateDirectory(_path);
        }

        var saveData = new SavedData
        {
            Name = player.transform.name,
            Position = player.transform.position,
            IsEnabled = player.gameObject.activeSelf,
            Time = playerHUDModel.TimerSeconds
        };

        _data.Save(saveData, Path.Combine(_path, _fileName));
        Debug.Log("Save player");
    }

    public void Load(PlayerView player, PlayerHUDModel playerHUDModel)
    {
        var file = Path.Combine(_path, _fileName);

        if (!File.Exists(file)) return;

        var model = _data.Load(file);

        player.transform.name = model.Name;
        player.transform.position = model.Position;
        player.gameObject.SetActive(model.IsEnabled);
        playerHUDModel.TimerSeconds = model.Time;

        Debug.Log("Load player");
    }
}
