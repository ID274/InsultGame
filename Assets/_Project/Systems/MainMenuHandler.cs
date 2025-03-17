using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private Button playButton, characterButton, settingsButton, exitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayButton);
        characterButton.onClick.AddListener(CharacterButton);
        settingsButton.onClick.AddListener(SettingsButton);
        exitButton.onClick.AddListener(ExitButton);
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
}
