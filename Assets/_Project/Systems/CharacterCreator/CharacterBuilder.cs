using UnityEngine;

public class CharacterBuilder : MonoBehaviour
{
    public static CharacterBuilder Instance { get; private set; }

    [SerializeField] private GameObject prefab;
    private GameObject character;

    [SerializeField] private CharacterPart[] partsToFind;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.SubscribeToEvent(CharacterCustomisationEventHub.saveEventName, SaveCharacter);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeFromEvent(CharacterCustomisationEventHub.saveEventName, SaveCharacter);
    }

    private void BuildCharacter()
    {
        if (character != null)
        {
            Destroy(character);
        }

        Debug.Log($"Building character with {partsToFind.Length} parts.");
        character = Instantiate(prefab, new Vector3 (0, -10, 0), transform.rotation); // instantiate off screen
        
        foreach (CharacterPart part in partsToFind)
        {
            // find changer with the part variable same as the current part
            ChangerScript[] changers = FindObjectsOfType<ChangerScript>();

            foreach (ChangerScript changer in changers)
            {
                if (part == changer.part)
                {
                    if (changer.selectedObject != null)
                    {
                        GameObject partObject = Instantiate(changer.selectedObject);
                        partObject.transform.SetParent(character.transform);
                        partObject.transform.localPosition = Vector3.zero;
                        Debug.Log($"Added {partObject.name} to character.");
                    }
                    else
                    {
                        Debug.Log($"No object selected for {part}.");
                    }
                }
            }
        }

        Debug.Log($"Character {character.name} built.", character);
    }

    public GameObject GetCreatedCharacter()
    {
        return character;
    }

    private void SaveCharacter()
    {
        BuildCharacter();
        character.SetActive(false);
    }
}