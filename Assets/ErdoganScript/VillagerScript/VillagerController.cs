using UnityEngine;

public class VillagerController : MonoBehaviour
{


    [Header("Items")]
    [SerializeField] private GameObject glassItems;
    [SerializeField] private GameObject woodItems;
    [SerializeField] private GameObject rockItems;
    [SerializeField] private GameObject potionItem;

    [Header("Points")]
    [SerializeField] private Transform throwPoint;

    [Header("Timers")]

    [SerializeField] private float baseCooldown;
    private float throwCooldown;
    private float _timer;

    [Header("SFX")]
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        baseCooldown = PlayerPrefs.GetFloat("baseCooldown", 16);
        throwCooldown = Random.Range(baseCooldown, baseCooldown + 3);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= throwCooldown)
        {
            ThrowItem();
        }
    }

    void ThrowItem()
    {
        _timer = 0;
        throwCooldown = Random.Range(baseCooldown, baseCooldown + 3);

        int randomElement = Random.Range(0, 3);

        int potionLuck = Random.Range(0, 100);

        if(potionLuck < 7)
            randomElement = 3;

        switch (randomElement)
        {
            case 0:
                Instantiate(glassItems, throwPoint.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(woodItems, throwPoint.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(rockItems, throwPoint.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(potionItem, throwPoint.position, Quaternion.identity);
                break;
        }
    }

    public void playAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
