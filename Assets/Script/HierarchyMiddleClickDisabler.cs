#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class HierarchyMiddleClickDabler
{
    static HierarchyMiddleClickDabler()
    {
        // Mendaftarkan callback kita agar dipanggil saat menggambar Hierarchy
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
    }

    private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        Event currentEvent = Event.current;

        // Pastikan event adalah MouseDown dan tombol yang ditekan adalah middle mouse (button == 2)
        if (currentEvent.type == EventType.MouseDown && currentEvent.button == 2)
        {
            // Cek apakah kursor berada di dalam area item Hierarchy
            if (selectionRect.Contains(currentEvent.mousePosition))
            {
                // Dapatkan objek dari instanceID
                GameObject clickedObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

                if (clickedObject != null)
                {
                    // Mencatat state agar bisa di-undo
                    Undo.RecordObject(clickedObject, "Toggle GameObject Active");

                    // Toggle setActive
                    clickedObject.SetActive(!clickedObject.activeSelf);

                    // Gunakan event ini agar Unity tidak memprosesnya lebih lanjut
                    currentEvent.Use();
                }
            }
        }
    }
}
#endif
