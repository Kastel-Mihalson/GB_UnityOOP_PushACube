using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class MyWindow : EditorWindow
{
    public static GameObject ObjectInstantiate;
    public string _nameObject = "Hello World";
    public bool _groupEnabled;
    public bool _randomColor = true;
    public int _countObject = 1;
    public float _radius = 10;
    public float _gameObjectPositionY;
    public List<GameObject> _roots;
    public List<GameObject> _temps;
    public int instanceCount = 0;

    private void OnGUI()
    {
        GUILayout.Label("������� ���������", EditorStyles.boldLabel);
        ObjectInstantiate = EditorGUILayout.ObjectField("������ ������� ����� ��������", ObjectInstantiate, typeof(GameObject), true) as GameObject;

        if (ObjectInstantiate) _nameObject = ObjectInstantiate.name;

        _nameObject = EditorGUILayout.TextField("��� �������", _nameObject);

        _groupEnabled = EditorGUILayout.BeginToggleGroup("�������������� ���������", _groupEnabled);
        _randomColor = EditorGUILayout.Toggle("��������� ����", _randomColor);
        _countObject = EditorGUILayout.IntSlider("���������� ��������", _countObject, 1, 100);
        _radius = EditorGUILayout.Slider("������ ����������", _radius, 10, 50);
        _gameObjectPositionY = EditorGUILayout.Slider("��������� �� ��� Y: ", _gameObjectPositionY, -5, 5);
        EditorGUILayout.EndToggleGroup();

        var instantiateButton = GUILayout.Button("������� �������");
        var deleteButton = GUILayout.Button("������� ��� �������");
        
        InstantiateObjectsButton(instantiateButton);
        DeleteObjectsButton(deleteButton);
    }

    private void InstantiateObjectsButton(bool button)
    {
        if (button)
        {
            if (ObjectInstantiate)
            {
                instanceCount++;
                GameObject root = new GameObject($"Root_{instanceCount}");
                for (int i = 0; i < _countObject; i++)
                {
                    float angle = i * Mathf.PI * 2 / _countObject;
                    Vector3 pos = new Vector3(Mathf.Cos(angle), _gameObjectPositionY, Mathf.Sin(angle)) * _radius;
                    GameObject temp = Instantiate(ObjectInstantiate, pos, Quaternion.identity);
                    temp.name = _nameObject + "(" + i + ")";
                    temp.transform.parent = root.transform;
                    var tempRenderer = temp.GetComponent<Renderer>();
                    if (tempRenderer && _randomColor)
                    {
                        tempRenderer.sharedMaterial.color = Random.ColorHSV();
                    }
                    _temps.Add(temp);
                }
                _roots.Add(root);
            }
        }
    }

    private void DeleteObjectsButton(bool deleteButton)
    {
        if (deleteButton)
        {
            int objectCount = _temps.Count;
            for (int i = 0; i < objectCount; i++)
            {
                DestroyImmediate(_temps[i]);
            }

            int rootsCount = _roots.Count;
            for (int i = 0; i < rootsCount; i++)
            {
                DestroyImmediate(_roots[i]);
            }
            instanceCount = 0;
        }
    }
}
