using UnityEditor;

public class MenuItems
{
    [MenuItem("MyEditorWindow/������� ���� ��������������")]
    private static void OpenMyEditorWindow()
    {
        EditorWindow.GetWindow(typeof(MyWindow), false, "MyEditorWindow");
    }
}
