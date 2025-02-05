using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{

    [SerializeField] public Slider loadingBar = default;

    public void Open()
    {
        gameObject.SetActive(true);
        this.SetValue(0);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        this.SetValue(0);
    }

    public void SetValue(float pval)
    {
        loadingBar.value = (float)System.Math.Round(pval, 2);// limit to 2 decimal ,
        loadingBar.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = loadingBar.value.ToString() + " %";
    }

    public void SetValueWithCustomText(float pval, string text)
    {
        loadingBar.value = (float)System.Math.Round(pval, 2);// limit to 2 decimal ,
        loadingBar.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public void SetValueInSeconds(int pval)
    {
        loadingBar.value = pval;// limit to 2 decimal ,
        loadingBar.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = loadingBar.value.ToString() + " s";
    }

    public float GetValue()
    {
        return loadingBar.value;

    }
}
