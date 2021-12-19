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

    public void Save(SavedData saveData)
    {
        if (!Directory.Exists(Path.Combine(_path)))
        {
            Directory.CreateDirectory(_path);
        }

        _data.Save(saveData, Path.Combine(_path, _fileName));
        Debug.Log("Save player");
    }

    public SavedData Load()
    {
        var file = Path.Combine(_path, _fileName);

        if (!File.Exists(file)) return new SavedData();

        var model = _data.Load(file);
        Debug.Log("Load player");

        return model;
    }
}
