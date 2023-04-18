using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerModel : Model
{
    [SerializeField] private float godModeCooldown = 3f;

    public bool canTakeDamage = true;

    private Animator animator;
    private PlayerMovement movement;
    private SpriteRenderer sprite;
    private PlayerCameraController cameraController;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        sprite = GetComponent<SpriteRenderer>();
        cameraController = GetComponent<PlayerCameraController>();
    }

    public override void TakeDamage(int decrease)
    {
        if (!canTakeDamage) return;
        if (movement.isDashing) return;

        canTakeDamage = false;

        hp -= decrease;

        if(hp == 0)
        {
            //TODO: player die
        }

        cameraController.Shake(5f*decrease, 0.1f);

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
        while (time >= 0f)
        {
            sprite.color = Color.gray;

            yield return new WaitForSeconds(0.2f);

            sprite.color = Color.white;

            yield return new WaitForSeconds(0.2f);

            time -= 0.4f;
        }
    }
}
