using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoSwordGenerator : MonoBehaviour
{
    [SerializeField] private GameObject egoSword;
    private List<GameObject> egoSwords = new List<GameObject>();

    public void Generate(GameObject target)
    {
        GameObject ego = Instantiate(egoSword, transform.position, Quaternion.identity);
        ego.GetComponent<EgoSwordMovement>().target = target;

        egoSwords.Add(ego);
    }

    public void DestroyAll()
    {
        foreach (GameObject ego in egoSwords)
        {
            Destroy(ego);
        }
    }
}
