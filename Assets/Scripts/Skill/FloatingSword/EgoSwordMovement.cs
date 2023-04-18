using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoSwordMovement : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] float time;
    [SerializeField] float speed;
    [SerializeField] float shootDistance;


    private bool isShooting = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distance = (target.transform.position - transform.position).magnitude;
        if (distance >= shootDistance)
        {
            rb.velocity = Vector2.zero;
            isShooting = false;
        }

        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0f, 0f, angle-90);

        yield return new WaitForSeconds(time);

        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            collision.GetComponent<PlayerModel>().TakeDamage(1);
        }
    }
}
