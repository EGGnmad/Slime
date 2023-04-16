using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float time;
    private Vector2 direction;

    private GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");


        direction = (boss.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, angle);

        StartCoroutine("Remove");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }


    IEnumerator Remove()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
