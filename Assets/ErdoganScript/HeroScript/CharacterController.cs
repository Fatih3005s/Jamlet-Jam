using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;

    private int Gold;
    void Start()
    {
        currentHp = maxHp;

        Gold = PlayerPrefs.GetInt("goldValue");
    }

    void Update()
    {
        
    }

    public void takeDamage(int damageAmount)
    {
        currentHp -= damageAmount;

        if (currentHp < 0)
            return;

        GameUIManager.instance.heartBroke(currentHp);

        if(currentHp <= 0)
        {
            GameOver();
        }
    }

    public void addGold(int Amount)
    {
        Gold += Amount;

        PlayerPrefs.SetInt("goldValue", Gold);

        GameUIManager.instance.ShowGold();
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        GameUIManager.instance.GameOverText();

        StartCoroutine(gameoverIE());
    }

    IEnumerator gameoverIE()
    {
        yield return new WaitForSecondsRealtime(1.4f);

        SceneManager.LoadScene(3);
    }
}
