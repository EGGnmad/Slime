using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUpGenerator : MonoBehaviour
{
    [SerializeField] private GameObject ground;


    private void Generate(Vector2 pos, float angle)
    {
        GameObject groundObject = Instantiate(ground, pos, Quaternion.identity);

        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        groundObject.GetComponent<GroundUpMovement>().direction = direction;
    }

    public void Circle(int swordCnt)
    {
        float anglePerSword = 360 / swordCnt;

        for(int i = 0; i < swordCnt; i++)
        {
            Generate(transform.position, anglePerSword * i);
        }
    }
}
