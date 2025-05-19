using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    KeyCode pressedKey;
    private bool waitingForKey = false;
    private TMP_Text bindingText;
    private string keybindName;
    [SerializeField] private GameObject settingsCanvas;

    [SerializeField] private GameObject backButton;

    [SerializeField] private TMP_Text[] bindTextList;
    private string previousKeyText;

    private void Start()
    {
        Screen.SetResolution(840, 480, false);

        bindTextList[0].text = PlayerPrefs.GetString("StoneKey","J");
        bindTextList[1].text = PlayerPrefs.GetString("WoodKey","K");
        bindTextList[2].text = PlayerPrefs.GetString("SkeletonKey","L");
        bindTextList[3].text = PlayerPrefs.GetString("AbilityKey","Q");
        bindTextList[4].text = PlayerPrefs.GetString("PotionKey","W");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public  void BackButton()
    {
        settingsCanvas.SetActive(false);

        PlayerPrefs.SetString("StoneKey", bindTextList[0].text);
        PlayerPrefs.SetString("WoodKey", bindTextList[1].text);
        PlayerPrefs.SetString("SkeletonKey", bindTextList[2].text);
        PlayerPrefs.SetString("AbilityKey", bindTextList[3].text);
        PlayerPrefs.SetString("PotionKey", bindTextList[4].text);
    }

    public void ResetButton()
    {
        PlayerPrefs.SetString("StoneKey","J");
        PlayerPrefs.SetString("WoodKey","K");
        PlayerPrefs.SetString("SkeletonKey", "L");
        PlayerPrefs.SetString("AbilityKey", "Q");
        PlayerPrefs.SetString("PotionKey","W");

        bindTextList[0].text = PlayerPrefs.GetString("StoneKey");
        bindTextList[1].text = PlayerPrefs.GetString("WoodKey");
        bindTextList[2].text = PlayerPrefs.GetString("SkeletonKey");
        bindTextList[3].text = PlayerPrefs.GetString("AbilityKey");
        bindTextList[4].text = PlayerPrefs.GetString("PotionKey");
    }

    public void KeybindButtonClick(TMP_Text BindText)
    {
        previousKeyText = BindText.text;
        BindText.text = "Press...";

        bindingText = BindText;
        waitingForKey = true;
    }

    private void Update()
    {
        KeyListener(bindingText,keybindName);
    }

    private void KeyListener(TMP_Text bindText , string keybindName)
    {
        if (bindText == null) return;

        if (waitingForKey)
        {
            backButton.SetActive(false);
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    string newKey = keyCode.ToString();

                    if (CheckDuplicateKey(newKey))
                    {
                        bindText.text = previousKeyText;
                        waitingForKey = false;
                        backButton.SetActive(true);
                        return;
                    }

                    backButton.SetActive(true);
                    pressedKey = keyCode;
                    waitingForKey = false;
                    bindText.text = pressedKey.ToString();
                    return;
                }
            }
        }
    }

    private bool CheckDuplicateKey(string key)
    {
        foreach (var text in bindTextList)
        {
            if (text != bindingText && text.text == key)
            {
                return true;
            }
        }
        return false;
    }
}
