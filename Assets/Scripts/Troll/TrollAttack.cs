using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollAttack : AttackClass
{
    public override void Attack(int damage, float radius)
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, layer);
        if (!collider) return;

        Model enemy = collider.GetComponent<Model>();
        enemy.TakeDamage(damage);
    }

    public void Stun(float radius, float time)
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, layer);
        if (!collider) return;

        PlayerModel enemy = collider.GetComponent<PlayerModel>();
        enemy.Stunned(1f);
    }
}
