using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LiftOffAnimator : MonoBehaviour
{
    private Animator animator;
    private EnemyController controller;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = transform.parent.GetComponent<EnemyController>();
        player = controller.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 2.1f)
        {
            // turn to face the player
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(targetPosition);

            // ...then punch
            animator.SetInteger("AnimationState", 2); // punch if close
        }
        else
        {
            animator.SetInteger("AnimationState", 1); // walk if not close
        }
    }
}
