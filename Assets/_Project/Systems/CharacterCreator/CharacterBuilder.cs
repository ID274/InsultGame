using UnityEngine;

public class CharacterBuilder : MonoBehaviour
{
    public static CharacterBuilder Instance { get; private set; }

    [SerializeField] private GameObject prefab;
    private GameObject character;

    [SerializeField] private CharacterPart[] partsToFind;

    private CharacterCustomisationUI characterCustomisationUI;

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

        characterCustomisationUI = FindObjectOfType<CharacterCustomisationUI>();
    }

    private void OnEnable()
    {
        EventManager.Instance.SubscribeToEvent(characterCustomisationUI.saveEventName, SaveCharacter);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeFromEvent(characterCustomisationUI.saveEventName, SaveCharacter);
    }

    private GameObject BuildCharacter()
    {
        if (character != null)
        {
            Destroy(character);
        }

        Debug.Log($"Building character with {partsToFind.Length} parts.");
        character = Instantiate(prefab, new Vector3 ((float)Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)), transform.rotation); // instantiate off screen
        
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
        return character;
    }

    private void SaveCharacter()
    {
        BuildCharacter();
    }
}