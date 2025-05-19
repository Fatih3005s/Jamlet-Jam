using System.Collections;
using UnityEngine;

public class SuperBall : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Power")]
    [SerializeField] private float forcePower;

    [Header("Destroy")]
    private float dieTime;

    public float radius;
    void Start()
    {
        dieTime = (radius - 11f) * 0.08f + 0.6f;

        Debug.Log(dieTime);

        StartCoroutine(superBallIE());
    }

    IEnumerator superBallIE()
    {
        yield return new WaitForSeconds(.1f);

        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.right * forcePower, ForceMode2D.Force);

        Destroy(this.gameObject, dieTime);
    }
}
