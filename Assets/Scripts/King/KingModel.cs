using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingModel : Model
{
    [SerializeField] private float godModeCooldown = 1.5f;
    public bool canTakeDamage = true;

    private SpriteRenderer sprite;
    private Animator animator;
    [SerializeField] private GameDirector gameDirector;
    [SerializeField] GameObject hpGauge;

    void Start()
    {
        hp = maxHp;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public override void TakeDamage(int decrease)
    {
        if (hp <= 0) return;
        if (!canTakeDamage) return;
        canTakeDamage = false;

        hp -= decrease;

        if (hp <= 0)
        {
            animator.SetBool("run", false);
            animator.SetTrigger("death");

            gameDirector.BossDied("King");
        }

        cameraController.Shake(2f, 0.1f, false);

        hpGauge.GetComponent<Slider>().value = (float)hp / maxHp;

        StartCoroutine(GodModeCooldown());
        StartCoroutine(GodModeIndicator(godModeCooldown));
    }

    IEnumerator GodModeCooldown()
    {
        yield return new WaitForSeconds(godModeCooldown);
        canTakeDamage = true;
    }

    IEnumerator GodModeIndicator(float time)
    {
        sprite.color = Color.gray;
        yield return new WaitForSeconds(time);
        sprite.color = Color.white;
    }
}
