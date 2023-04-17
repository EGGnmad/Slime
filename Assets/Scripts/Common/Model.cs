using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model : MonoBehaviour
{
    [SerializeField] protected int hp;

    public abstract void TakeDamage(int decrease);
}
