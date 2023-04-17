using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSwordMovement : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] float time = 5f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;

        StartCoroutine(DestroyTimer());
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
