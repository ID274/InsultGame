using UnityEngine;

public class CharacterCustomisationUI : MonoBehaviour
{
    public string randomiseEventName { get; private set; } = "randomisecharacter";
    public string resetEventName { get; private set; } = "resetcharacter";

    public string saveEventName { get; private set; } = "savecharacter";

    public string loadEventName { get; private set; } = "loadcharacter";

    private void Start()
    {
        EventManager.Instance.AddEvent(randomiseEventName, new CustomEvent());
        EventManager.Instance.AddEvent(resetEventName, new CustomEvent());
        EventManager.Instance.AddEvent(saveEventName, new CustomEvent());
        EventManager.Instance.AddEvent(loadEventName, new CustomEvent());
    }

    public void RandomiseCharacter()
    {
        EventManager.Instance.InvokeEvent("randomisecharacter");
    }

    public void ResetCharacter()
    {
        EventManager.Instance.InvokeEvent("resetcharacter");
    }

    public void LoadCharacter()
    {
        EventManager.Instance.InvokeEvent("loadcharacter");
    }

    public void SaveCharacter()
    {
        EventManager.Instance.InvokeEvent("savecharacter");
    }
}
