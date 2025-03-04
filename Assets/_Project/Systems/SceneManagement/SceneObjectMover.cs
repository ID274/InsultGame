using Unity.VisualScripting;
using UnityEngine;

public class SceneObjectMover : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToMove;
    private GameObject[] clones;

    private bool startPassed = false;

    private void Start()
    {
        EventManager.Instance.SubscribeToEvent(MySceneManager.sceneChangeEventName, MoveObjects);
        startPassed = true;
    }

    private void OnEnable()
    {
        if (startPassed)
        {
            EventManager.Instance.SubscribeToEvent(MySceneManager.sceneChangeEventName, MoveObjects);
        }
    }

    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeFromEvent(MySceneManager.sceneChangeEventName, MoveObjects);
    }

    public void MoveObjects()
    {
        foreach (GameObject obj in objectsToMove)
        {
            DontDestroyOnLoad(obj);
        }
    }
}
