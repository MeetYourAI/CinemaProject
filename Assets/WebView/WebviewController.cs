using UnityEngine;
using UnityEngine.UI;
using Vuplex.WebView;

public class WebviewController : SingleBehaviour<WebviewController>
{
    [SerializeField] private LoadingBar loadingBar;
    [SerializeField] CanvasGroup cg;
    [SerializeField] private Button btnClose;
    [SerializeField] private CanvasWebViewPrefab desktopWebview;
    [SerializeField] bool isTest = false;
    [SerializeField] string testUrl;

    private bool initialized = false;

    protected override void InternalAwake() { }

    protected override void InternalOnDestroy() { }

    private void Start()
    {
        cg.alpha = 0;
        btnClose.onClick.AddListener(() => Close());
        btnClose.gameObject.SetActive(false);

        desktopWebview.Initialized += DesktopWebview_Initialized;
        desktopWebview.Visible = false;
        if (isTest)
            OpenLink(testUrl);
    }

    private void DesktopWebview_Initialized(object sender, System.EventArgs e)
    {
        initialized = true;
        desktopWebview.WebView.LoadProgressChanged += WebView_LoadProgressChanged;
        desktopWebview.WebView.PageLoadFailed += WebView_PageLoadFailed;

#if UNITY_ANDROID || UNITY_IOS
        desktopWebview.Resolution = 3f;
#else
        desktopWebview.Resolution = 1.5f;
#endif
    }

    private void WebView_PageLoadFailed(object sender, System.EventArgs e)
    {
        Close();
    }

    private void WebView_LoadProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        Debug.LogError(e.Type);
        if (e.Type == ProgressChangeType.Started)
            loadingBar?.Open();

        if (e.Type == ProgressChangeType.Failed)
        {
            Close();
        }

        if (e.Type == ProgressChangeType.Finished)
        {
            loadingBar?.Close();
            desktopWebview.Visible = true;
            btnClose.gameObject.SetActive(true);
        }

        loadingBar?.SetValue(e.Progress * 100f);
    }

    public void OpenLink(string url)
    {
        //if (url.IsNullOrEmpty() && !initialized)
        Debug.Log($"isInitialize: <color=cyan>{initialized}</color>");
        if (string.IsNullOrEmpty(url) && !initialized)
            return;
        cg.alpha = 1;
        //loadingBar?.Open();
        desktopWebview.WebView.LoadUrl(url);
    }

    public void Close()
    {
        cg.alpha = 0;
        //loadingBar?.Close();
        btnClose.gameObject.SetActive(false);
        //MusicManager.Instance.UnmuteMusicMixer();
        desktopWebview.Visible = false;
    }
}
