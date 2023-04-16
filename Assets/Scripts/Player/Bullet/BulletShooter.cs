using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float time = 0.3f;

    public void StartFire()
    {
        StartCoroutine("Shoot");
    }

    public void StopFire()
    {
        StopCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
    }
}
