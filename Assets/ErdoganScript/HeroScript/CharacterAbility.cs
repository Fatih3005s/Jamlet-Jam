using System.Collections;
using System.Linq;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{
    Animator animator;

    [Header("Keycode")]
    private KeyCode abilityKey;
    private KeyCode potionKey;

    [Header("AbilityPoint")]
    [SerializeField] private Transform abilityPoint;
    [SerializeField] private float abilityRadius;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Potion")]
    [SerializeField] private Transform collectPotionPoint;
    [SerializeField] private float collectPotionRadius;
    [SerializeField] private LayerMask potionLayer;

    [HideInInspector] public bool useAbility = false;

    [Header("Superball")]
    [SerializeField] private GameObject superBall;
    void Start()
    {
        animator = GetComponent<Animator>();

        abilityRadius = PlayerPrefs.GetFloat("abilityRadius", 11f);

        abilityKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AbilityKey", "Q"));
        potionKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PotionKey", "W"));
    }

    void Update()
    {
        if(useAbility)
        {
            StartCoroutine(AbilityTimer());
        }
        setAbility();
        takePotion();
    }

    IEnumerator AbilityTimer()
    {
        yield return new WaitForSeconds(1);
        useAbility = false;
    }

    void setAbility()
    {
        if (useAbility && Input.GetKeyDown(abilityKey))
        {
            animator.SetTrigger("SuperBall");

            var spawnedSuperBall = Instantiate(superBall, transform.position, Quaternion.identity);

            spawnedSuperBall.GetComponent<SuperBall>().radius = abilityRadius;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(abilityPoint.position, abilityRadius, enemyLayer);

            hitEnemies = hitEnemies.OrderBy(e => e.transform.position.x).ToArray();



            StartCoroutine(KillEnemiesOneByOne(hitEnemies));
            useAbility = false;
        }
    }

    IEnumerator KillEnemiesOneByOne(Collider2D[] enemies)
    {
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyMovement>().Die();
            yield return new WaitForSeconds(0.3f);
        }
    }

    void takePotion()
    {
        if(Input.GetKeyDown(potionKey))
        {
            Collider2D[] potions = Physics2D.OverlapCircleAll(collectPotionPoint.position, collectPotionRadius,potionLayer);
            foreach(var potion in potions)
            {
                Destroy(potion.gameObject);
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(abilityPoint.position, abilityRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(collectPotionPoint.position, collectPotionRadius);
    }
}
