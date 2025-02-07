using TMPro;
using UnityEngine;

public class MovieChooserBtn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt;
    MovieDetails md;
    public void Inits(MovieDetails m)
    {
        nameTxt.text = m.name;
        md = m;
    }

    public void OnClick()
    {
        Debug.Log($"PlayVideo: <color=yellow>{md.url}</color>");
        MovieManager.Instance.PlayVideoLink(md.url);
        UIManager_Movie.Instance.SetPanels();
    }
}
