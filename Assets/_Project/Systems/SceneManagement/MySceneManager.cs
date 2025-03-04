using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance { get; private set; }

    [SerializeField] private bool dontDestroyOnLoad = true;

    private bool startPassed = false;

    public const string sceneChangeEventName = "sceneChangedEvent";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        EventManager.Instance.AddEvent(sceneChangeEventName, new CustomEvent());
        startPassed = true;
    }


    public void ChangeScene()
    {
        EventManager.Instance.InvokeEvent(sceneChangeEventName);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = (currentSceneIndex + SceneManager.sceneCount + 1) % SceneManager.sceneCount;
        SceneManager.LoadScene(nextIndex);
    }

    public void ChangeScene(int sceneIndex)
    {
        EventManager.Instance.InvokeEvent(sceneChangeEventName);
        SceneManager.LoadScene(sceneIndex);
    }
}