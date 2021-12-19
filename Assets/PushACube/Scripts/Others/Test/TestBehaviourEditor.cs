using UnityEditor;
using UnityEngine;

namespace MyEditorWindow
{
    [CustomEditor(typeof(TestBehaviour))]
    public class TestBehaviourEditor : UnityEditor.Editor
    {
        private bool _isPressButtonOk;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            TestBehaviour testTarget = (TestBehaviour)target;

            testTarget.count = EditorGUILayout.IntSlider(testTarget.count, 10, 50);
            testTarget.offset = EditorGUILayout.IntSlider(testTarget.offset, 1, 5);

            testTarget.obj =
               EditorGUILayout.ObjectField("Объект который хотим вставить",
                     testTarget.obj, typeof(GameObject), false)
                  as GameObject;

            var isPressButton = GUILayout.Button("Создание объектов по кнопке",
               EditorStyles.miniButtonLeft);

            _isPressButtonOk = GUILayout.Toggle(_isPressButtonOk, "Ok");

            if (isPressButton)
            {
                testTarget.CreateObj();
                _isPressButtonOk = true;
            }

            if (_isPressButtonOk)
            {
                testTarget.Test = EditorGUILayout.Slider(testTarget.Test, 10, 50);
                EditorGUILayout.HelpBox("Вы нажали на кнопку", MessageType.Warning);

                var isPressAddButton = GUILayout.Button("Add Component",
                   EditorStyles.miniButtonLeft);
                var isPressRemoveButton = GUILayout.Button("Remove Component",
                   EditorStyles.miniButtonLeft);
                if (isPressAddButton)
                {
                    testTarget.AddComponent();
                }
                if (isPressRemoveButton)
                {
                    testTarget.RemoveComponent();
                }
            }
        }
    }
}