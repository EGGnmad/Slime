using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackClass : MonoBehaviour
{
    [SerializeField] protected Transform transform;
    [SerializeField] protected LayerMask layer;

    public virtual void Attack(int damage, float radius)
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, layer);
        if (!collider) return;

        Model enemy = collider.GetComponent<Model>();
        enemy.TakeDamage(damage);
    }
}
