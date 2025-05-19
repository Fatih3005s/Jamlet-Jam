using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private Animator animator;

    [Header("Audio")]
    [SerializeField] private AudioClip audioClip;
    AudioSource audioSource;
    [Header("Timers")]
    [SerializeField] private float attackSpeed;
    private float _timer;

    [Header("Keycodes")]
    private KeyCode clabAttackKeycode;
    private KeyCode axeAttackKeycode;
    private KeyCode mazeAttackKeycode;

    [Header("Hit")]
    [SerializeField] private Transform hitPoint;
    [SerializeField] private float hitRadius;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Durability")]
    CharacterInventory characterInventory;


    void Start()
    {
        characterInventory = GetComponent<CharacterInventory>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        clabAttackKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SkeletonKey", "L"));
        axeAttackKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("WoodKey", "K"));
        mazeAttackKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("StoneKey", "J"));

        PlayerPrefs.SetInt("Durability",PlayerPrefs.GetInt("maxDurability",3));
    }

    void Update()
    {
        _timer += Time.deltaTime;

        Inputs();
    }

    void Attack(string Weapon)
    {
        if(_timer >= attackSpeed)
        {
            _timer = 0;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitPoint.position, hitRadius, enemyLayer);
            foreach (var enemy in hitEnemies)
            {
                if (enemy.CompareTag(Weapon))
                {
                    audioSource.PlayOneShot(audioClip);
                    enemy.GetComponent<EnemyMovement>().Die();
                    break;
                }
                else
                {
                    int newDurability = PlayerPrefs.GetInt("Durability");

                    if(newDurability <= 0)
                    {
                        GetComponent<CharacterController>().GameOver();
                    }

                    newDurability--;
                    PlayerPrefs.SetInt("Durability", newDurability);

                    GameUIManager.instance.changeDurability();
                    
                    break;
                }
            }
        }
    }

    void Inputs()
    {
        if(Input.GetKeyDown(clabAttackKeycode))
        {
            Attack("Glass");
            animator.SetTrigger("Club");
        }

        if (Input.GetKeyDown(axeAttackKeycode))
        {
            Attack("Wood");
            animator.SetTrigger("AXE");
        }

        if (Input.GetKeyDown(mazeAttackKeycode))
        {
            Attack("Rock");
            animator.SetTrigger("Mace");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPoint.position, hitRadius);
    }
}
