using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SkillTreeUI : MonoBehaviour
{
    private int goldValue;
    [SerializeField] private TMP_Text goldText;

    [Header("Durability")]
    [SerializeField] private TMP_Text durabilityValueText;
    [SerializeField] private TMP_Text DurabilityCostText;
    private int durabilityCost;

    [Header("Slow")]
    [SerializeField] private TMP_Text slowValueText;
    [SerializeField] private TMP_Text slowCostText;
    private int slowCost;

    [Header("Ability Radius")]
    [SerializeField] private TMP_Text abilityRadiusValueText;
    [SerializeField] private TMP_Text abilityRadiusCostText;
    private int abilityRadiusCost;

    [Header("Villager Ability")]
    [SerializeField] private TMP_Text villagerAbilityText;
    [SerializeField] private TMP_Text villagerCooldownCostText;
    private int villagerCooldownCost;


    void Start()
    {
        goldValue = PlayerPrefs.GetInt("goldValue");
        durabilityCost = PlayerPrefs.GetInt("durabilityCost",100);
        slowCost = PlayerPrefs.GetInt("slowCost",100);
        abilityRadiusCost = PlayerPrefs.GetInt("abilityRadiusCost", 100);
        villagerCooldownCost = PlayerPrefs.GetInt("villagerCooldownCost",100);


        setNewGoldText();
        setNewDurabilityMarket();
        setNewSlowMarket();
        setNewAbilityRadiusMarket();
        setNewCooldownMarket();

    }

    public void ShowDescription(GameObject descriptionArea)
    {
        descriptionArea.SetActive(true);
    }

    public void CloseDescription(GameObject descriptionArea)
    {
        descriptionArea.SetActive(false);
    }

    public void BuyWeaponDurability()
    {
        if (goldValue >= durabilityCost)
        {
            // Durability arttýr
            int currentDurability = PlayerPrefs.GetInt("maxDurability", 3);
            currentDurability += 1;
            PlayerPrefs.SetInt("maxDurability", currentDurability);

            // Para azalt
            goldValue -= durabilityCost;
            PlayerPrefs.SetInt("goldValue", goldValue);

            // Yeni maliyeti hesapla
            durabilityCost = (int)(durabilityCost * 1.5f);
            PlayerPrefs.SetInt("durabilityCost", durabilityCost);

            PlayerPrefs.Save();

            setNewGoldText();
            setNewDurabilityMarket();
        }
        else
        {
            Debug.Log("Para Yeterli Deðil");
        }
    }

    public void BuyEnemySlow()
    {
        if (goldValue >= slowCost)
        {
            float currentSlow = PlayerPrefs.GetFloat("enemySlowValue", 0f);
            currentSlow += 0.1f;
            PlayerPrefs.SetFloat("enemySlowValue", currentSlow);

            goldValue -= slowCost;
            PlayerPrefs.SetInt("goldValue", goldValue);

            slowCost = (int)(slowCost * 1.5f);
            PlayerPrefs.SetInt("slowCost", slowCost);

            PlayerPrefs.Save();

            setNewGoldText();
            setNewSlowMarket();
        }
        else
        {
            Debug.Log("Yetersiz Altýn (Slow)");
        }
    }
    public void BuyAbilityRadius()
    {
        if (goldValue >= abilityRadiusCost)
        {
            float currentRadius = PlayerPrefs.GetFloat("abilityRadius", 11);
            currentRadius += 1; 
            PlayerPrefs.SetFloat("abilityRadius", currentRadius);

            goldValue -= abilityRadiusCost;
            PlayerPrefs.SetInt("goldValue", goldValue);

            abilityRadiusCost = (int)(abilityRadiusCost * 1.5f);
            PlayerPrefs.SetInt("abilityRadiusCost", abilityRadiusCost);

            PlayerPrefs.Save();

            setNewGoldText();
            setNewAbilityRadiusMarket();
        }
        else
        {
            Debug.Log("Yetersiz altýn.");
        }
    }

    public void BuyCooldown()
    {
        if (goldValue >= villagerCooldownCost)
        {
            float currentCooldown = PlayerPrefs.GetFloat("baseCooldown",16);
            currentCooldown -= 1f;

            PlayerPrefs.SetFloat("baseCooldown", currentCooldown);

            goldValue -= villagerCooldownCost;
            PlayerPrefs.SetInt("goldValue", goldValue);

            villagerCooldownCost = (int)(villagerCooldownCost * 1.5f);
            PlayerPrefs.SetInt("villagerCooldownCost", villagerCooldownCost);

            PlayerPrefs.Save();

            setNewGoldText();
            setNewCooldownMarket();
        }
        else
        {
            Debug.Log("Yetersiz altýn.");
        }
    }

    

    void setNewGoldText()
    {
        goldText.text = goldValue.ToString();
    }

    void setNewDurabilityMarket()
    {
        int plusValue = PlayerPrefs.GetInt("maxDurability");
        plusValue++;
        durabilityValueText.text = plusValue.ToString();

        DurabilityCostText.text = PlayerPrefs.GetInt("durabilityCost").ToString();
    }

    void setNewSlowMarket()
    {
        float plusValue = PlayerPrefs.GetFloat("enemySlowValue", 0f) + 0.1f;
        slowValueText.text = plusValue.ToString("0.0");
        slowCostText.text = PlayerPrefs.GetInt("slowCost",100).ToString();
    }
    
    void setNewAbilityRadiusMarket()
    {
        float plusValue = PlayerPrefs.GetFloat("abilityRadius", 11) + 1f; 
        abilityRadiusValueText.text = plusValue.ToString();
        abilityRadiusCostText.text = PlayerPrefs.GetInt("abilityRadiusCost", 100).ToString();
    }

    void setNewCooldownMarket()
    {
        float baseCooldown = PlayerPrefs.GetFloat("baseCooldown" , 16);
        float avgCooldown = baseCooldown - 1f;
        villagerAbilityText.text = avgCooldown.ToString();
        villagerCooldownCostText.text = PlayerPrefs.GetInt("villagerCooldownCost").ToString();
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}

