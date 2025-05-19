using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    private bool isDead = false;

    [Header("Player")]
    private GameObject Player;

    [Header("Speed")]
    [SerializeField] private float baseMoveSpeed = 4.2f; 

    private float moveSpeed;
    private bool canRun = true;

    [Header("Attack")]
    [SerializeField] private float attackSpeed;
    private bool canAttack = true;

    [Header("Distance")]
    [SerializeField] float distanceFloat;
    private float Dis;

    [Header("Die")]
    [SerializeField] float dieTime;
    CapsuleCollider2D collider;

    [Header("Gold")]
    [SerializeField] private int goldValue;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();

        Player = GameObject.FindGameObjectWithTag("Player");

        float slowAmount = PlayerPrefs.GetFloat("enemySlowValue", 0f);
        moveSpeed = baseMoveSpeed - slowAmount;
    }

    void FixedUpdate()
    {
        MoveToPlayer();
    }

    private void Update()
    {
        CheckDistanceForAttack();
        checkPlayerPos();
    }

    void checkPlayerPos()
    {
        if(Player.transform.position.x >= transform.position.x - 1f)
        {
            Die();
        }
    }

    void MoveToPlayer()
    {
        if (canRun)
        {
            Vector2 dir = (Player.transform.position - transform.position).normalized;

            rb.linearVelocity = dir * moveSpeed;
        }
        else
            return;
       
    }

    void CheckDistanceForAttack()
    {
        Dis = Vector2.Distance(transform.position, Player.transform.position);
        if (Dis <= distanceFloat && canAttack)
        {
            if (isDead) return;

            canRun = false;
            Attack();
            return;
        }
    }
    
    void Attack()
    {
        StartCoroutine(AttackIE());
    }
    IEnumerator AttackIE()
    {
        canAttack = false;

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);

        Player.GetComponent<CharacterController>().takeDamage(1);
        Die();
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        int currentGold = PlayerPrefs.GetInt("goldValue", 0);
        currentGold += 30;
        PlayerPrefs.SetInt("goldValue", currentGold);

        GameUIManager.instance.ShowGold();

        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.gravityScale = 0;
        Destroy(collider);

        animator.SetTrigger("isDie");

        Destroy(this.gameObject,dieTime);
    }
}
