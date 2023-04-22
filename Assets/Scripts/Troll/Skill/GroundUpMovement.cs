using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUpMovement : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] float time = 5f;
    [SerializeField] private string EnemyTag;

    public Vector2 direction;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = direction * speed;

        StartCoroutine(DestroyTimer());
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(EnemyTag))
        {
            collision.GetComponent<Model>().TakeDamage(1);
        }
    }
}
