using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private Button playButton, characterButton, settingsButton, exitButton;

    public const string playButtonEvent = "playButtonEvent";
    public const string characterButtonEvent = "characterButtonEvent";
    public const string settingsButtonEvent = "settingsButtonEvent";
    public const string exitButtonEvent = "exitButtonEvent";

    private bool startPassed = false;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayButton);
        characterButton.onClick.AddListener(CharacterButton);
        settingsButton.onClick.AddListener(SettingsButton);
        exitButton.onClick.AddListener(ExitButton);
    }

    private void Start()
    {
        EventManager.Instance.AddEvent(playButtonEvent, new CustomEvent());
        EventManager.Instance.AddEvent(characterButtonEvent, new CustomEvent());
        EventManager.Instance.AddEvent(settingsButtonEvent, new CustomEvent());
        EventManager.Instance.AddEvent(exitButtonEvent, new CustomEvent());

        EventManager.Instance.SubscribeToEvent(playButtonEvent, PlayButton);
        EventManager.Instance.SubscribeToEvent(characterButtonEvent, CharacterButton);
        EventManager.Instance.SubscribeToEvent(settingsButtonEvent, SettingsButton);
        EventManager.Instance.SubscribeToEvent(exitButtonEvent, ExitButton);

        startPassed = true;
    }

    public void PlayButton() 
    {
        Debug.Log("Play button pressed");
        MySceneManager.Instance.ChangeScene(2);
    }

    public void CharacterButton()
    {
        Debug.Log("Character button pressed");
        MySceneManager.Instance.ChangeScene(1);
    }

    public void SettingsButton()
    {
        Debug.Log("Settings button pressed");
    }

    public void ExitButton() 
    {
        Debug.Log("Exit button pressed");
        Application.Quit();
    }


    private void OnEnable()
    {
        if (startPassed)
        {
            EventManager.Instance.SubscribeToEvent(playButtonEvent, PlayButton);
            EventManager.Instance.SubscribeToEvent(characterButtonEvent, CharacterButton);
            EventManager.Instance.SubscribeToEvent(settingsButtonEvent, SettingsButton);
            EventManager.Instance.SubscribeToEvent(exitButtonEvent, ExitButton);
        }
    }

    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeFromEvent(playButtonEvent, PlayButton);
        EventManager.Instance.UnsubscribeFromEvent(characterButtonEvent, CharacterButton);
        EventManager.Instance.UnsubscribeFromEvent(settingsButtonEvent, SettingsButton);
        EventManager.Instance.UnsubscribeFromEvent(exitButtonEvent, ExitButton);
    }
}
