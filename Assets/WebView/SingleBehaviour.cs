#region Using

using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

/// <summary>
///     Generic singleton Class. Extend this class to make singleton component.
///     Example:
///     <code>
/// public class Foo : GenericSingleton<Foo>
/// </code>
///     . To get the instance of Foo class, use <code>Foo.instance</code>
///     Override <code>Init()</code> method instead of using <code>Awake()</code>
///     from this class.
/// </summary>
public abstract class SingleBehaviour<T> : MonoBehaviour where T : SingleBehaviour<T>
{
    private static T instance;

    [SerializeField, Tooltip("If set to true, the singleton will be marked as \"don't destroy on load\"")] private bool _dontDestroyOnLoad;

    private bool isInitialized;

    public static T Instance
    {
        get
        {
            // Instance required for the first time, we look for it
            if (instance != null)
            {
                return instance;
            }

            var instances = Resources.FindObjectsOfTypeAll<T>();
            if (instances == null || instances.Length == 0)
            {
                return null;
            }

            instance = instances.FirstOrDefault(i => i.gameObject.scene.buildIndex != -1);
            if (Application.isPlaying)
            {
                instance?.Init();
            }
            return instance;
        }
    }

    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    protected virtual void Awake()
    {
        if (instance == null || !instance || !instance.gameObject)
        {
            instance = (T)this;
        }
        else if (instance != this)
        {
            //Destroy(this);
            Destroy(gameObject);
            return;
        }
        instance.Init();
    }

    /// <summary>
    ///     This function is called when the instance is used the first time
    ///     Put all the initializations you need here, as you would do in Awake
    /// </summary>
    public void Init()
    {
        if (isInitialized)
        {
            return;
        }

        if (_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;

        InternalAwake();
        isInitialized = true;
    }

    private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene scene)
    {
        if (!Instance || gameObject == null)
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            instance = null;
            return;
        }

        if (_dontDestroyOnLoad)
        {
            return;
        }

        SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
        instance = null;
    }

    protected abstract void InternalAwake();

    /// Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit()
    {
        instance = null;
    }

    private void OnDestroy()
    {
        // Clear static listener OnDestroy
        SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;

        StopAllCoroutines();
        InternalOnDestroy();
        if (instance != this)
        {
            return;
        }
        instance = null;
        isInitialized = false;
    }

    protected abstract void InternalOnDestroy();
}