using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAttack : MonoBehaviour
{
    [SerializeField] private Transform hitTransform;
    [SerializeField] private LayerMask playerLayer;

    public void Attack(int damage, float radius)
    {
        Collider2D collider = Physics2D.OverlapCircle(hitTransform.position, radius, playerLayer);
        if (!collider) return;

        PlayerModel player = collider.GetComponent<PlayerModel>();
        player.TakeDamage(damage);
    }
}
