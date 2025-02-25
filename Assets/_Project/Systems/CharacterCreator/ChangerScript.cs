using TMPro;
using UnityEngine;

public class ChangerScript : MonoBehaviour
{
    [SerializeField] public CharacterPart part { get; private set; }

    [SerializeField] private TextMeshProUGUI countText;

    [SerializeField] private GameObject categoryParent;
    private GameObject[] optionObjects;

    [SerializeField] private bool canBeEmpty;
    private int currentOptionIndex = 0;

    public GameObject selectedObject;

    private bool startPassed = false;

    private void OnEnable()
    {
        if (startPassed)
        {
            EventManager.Instance.SubscribeToEvent(CharacterCustomisationEventHub.randomiseEventName, RandomiseChanger);
            EventManager.Instance.SubscribeToEvent(CharacterCustomisationEventHub.resetEventName, ResetChanger);
        }
    }
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeFromEvent(CharacterCustomisationEventHub.randomiseEventName, RandomiseChanger);
        EventManager.Instance.UnsubscribeFromEvent(CharacterCustomisationEventHub.resetEventName, ResetChanger);
    }

    private void Start()
    {
        startPassed = true;
        OnEnable();

        if (categoryParent != null)
        {
            if (canBeEmpty)
            {
                optionObjects = new GameObject[categoryParent.transform.childCount + 1];
            }
            else
            {
                optionObjects = new GameObject[categoryParent.transform.childCount];
            }

            for (int i = 0; i < optionObjects.Length; i++)
            {
                if (i == 0 && canBeEmpty)
                {
                    optionObjects[i] = null;
                }
                else
                {
                    int childIndex = canBeEmpty ? i - 1 : i;
                    optionObjects[i] = categoryParent.transform.GetChild(childIndex).gameObject;
                }
            }
        }

        countText.text = "0";
        selectedObject = optionObjects[currentOptionIndex];
    }

    // Example of method overloading and polymorphism. Here we have two methods that essentially do the same thing, but slightly differently. It makes sense for
    // these to share a name as they are a means-to-an-end.

    public void Change(bool left) // change by 1 to left or right
    {
        if (left)
        {
            if (currentOptionIndex == 0)
            {
                currentOptionIndex = optionObjects.Length - 1;
            }
            else
            {
                currentOptionIndex--;
            }
        }
        else
        {
            if (currentOptionIndex == optionObjects.Length - 1)
            {
                currentOptionIndex = 0;
            }
            else
            {
                currentOptionIndex++;
            }
        }

        if (selectedObject != null)
        {
            selectedObject.SetActive(false);
        }

        selectedObject = optionObjects[currentOptionIndex];

        if (selectedObject != null)
        {
            selectedObject.SetActive(true);
        }

        countText.text = currentOptionIndex.ToString();
    }

    private void Change(int index) // change to specific index
    {
        currentOptionIndex = index;

        if (selectedObject != null)
        {
            selectedObject.SetActive(false);
        }

        selectedObject = optionObjects[currentOptionIndex];

        if (selectedObject != null)
        {
            selectedObject.SetActive(true);
        }

        countText.text = currentOptionIndex.ToString();
    }

    private void RandomiseChanger()
    {
        int randomIndex = Random.Range(0, optionObjects.Length);
        Change(randomIndex);
    }

    private void ResetChanger()
    {
        Change(0);
    }
}