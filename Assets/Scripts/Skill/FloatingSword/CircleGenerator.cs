using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject sword;


    private void Generate(Vector2 pos, float angle)
    {
        GameObject swordObject = Instantiate(sword, pos, Quaternion.identity);

        swordObject.transform.eulerAngles = new Vector3(0, 0, angle);
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
