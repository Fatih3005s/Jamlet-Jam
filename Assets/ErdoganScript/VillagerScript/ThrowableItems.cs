using UnityEngine;

public class ThrowableItems : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Player")]
    private GameObject Player;
    private CharacterAbility characterAbility;

    [Header("Settings")]
    [SerializeField] private float torquePower;
    private float forcePower;
    [SerializeField] private float forcePowerFloat1;
    [SerializeField] private float forcePowerFloat2;

    [Header("Potion")]
    [SerializeField] private bool isPotion;
    [SerializeField] private float potionPower;

    [Header("Points")]
    private GameObject targetTransform;
    private GameObject potionTargetTransform;

    [Header("SFX")]
    [SerializeField] AudioClip audioClip;
    private GameObject villagerController;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        villagerController = GameObject.FindGameObjectWithTag("VillagerController");

        targetTransform = GameObject.FindGameObjectWithTag("Target");
        potionTargetTransform = GameObject.FindGameObjectWithTag("PotionTarget");

        Player = GameObject.FindGameObjectWithTag("Player");
        characterAbility = Player.GetComponent<CharacterAbility>();

        ItemAddForce();
    }

    void ItemAddForce()
    {
        Vector2 dir = targetTransform.transform.position - transform.position;
        Vector2 potionDir = potionTargetTransform.transform.position - transform.position;

        forcePower = Random.Range(forcePowerFloat1, forcePowerFloat2);

        if (isPotion)
        {
            forcePower = potionPower;
            rb.AddForce(potionDir * forcePower, ForceMode2D.Impulse);
            rb.AddTorque(-torquePower, ForceMode2D.Impulse);
            return;
        }
        rb.AddForce(dir * forcePower , ForceMode2D.Impulse);
        rb.AddTorque(-torquePower, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            villagerController.GetComponent<VillagerController>().playAudio(audioClip);

            if(!isPotion)
                characterAbility.useAbility = true;

            Destroy(this.gameObject);
        }
    }
}
