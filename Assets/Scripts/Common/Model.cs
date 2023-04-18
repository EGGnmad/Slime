using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model : MonoBehaviour
{
    public int hp;

    public abstract void TakeDamage(int decrease);
}
