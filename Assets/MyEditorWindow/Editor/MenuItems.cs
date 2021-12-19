using UnityEditor;

public class MenuItems
{
    [MenuItem("MyEditorWindow/Открыть окно редактирования")]
    private static void OpenMyEditorWindow()
    {
        EditorWindow.GetWindow(typeof(MyWindow), false, "MyEditorWindow");
    }
}
