using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    [Header("Hearts")]
    [SerializeField] private Image[] Hearts;
    [SerializeField] private Sprite NormalHeart;
    [SerializeField] private Sprite BrokenHeart;

    [Header("Gold")]
    [SerializeField] private TMP_Text goldText;

    [Header("Durability")]
    [SerializeField] private TMP_Text durabilityText;

    [Header("Gameover")]
    [SerializeField] private GameObject gameOverText;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        goldText.text = PlayerPrefs.GetInt("goldValue").ToString();
        durabilityText.text = PlayerPrefs.GetInt("maxDurability").ToString();
    }

    public void heartBroke(int hp)
    {
        Hearts[hp].sprite = BrokenHeart;
    }

    public void healHeart(int hp)
    {
        Hearts[hp].sprite = NormalHeart;
    }

    public void ShowGold()
    {
        int goldCount = PlayerPrefs.GetInt("goldValue");

        goldText.text = goldCount.ToString();
    }
    public void changeDurability()
    {
        int durabilityCount = PlayerPrefs.GetInt("Durability");

        if(durabilityCount <= 0)
        {
            durabilityCount = 0;
        }

        durabilityText.text = durabilityCount.ToString();
    }

    public void GameOverText()
    {
        gameOverText.SetActive(true);
    }
}
