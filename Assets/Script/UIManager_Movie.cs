using UnityEngine;

public class UIManager_Movie : Singleton<UIManager_Movie>
{
    [SerializeField] GameObject[] panels;
    void Start()
    {
        SetPanels();
    }
    public void SetPanels(bool v1 = true, bool v2 = false)
    {
        panels[0].SetActive(v1);
        panels[1].SetActive(v2);
    }
}
