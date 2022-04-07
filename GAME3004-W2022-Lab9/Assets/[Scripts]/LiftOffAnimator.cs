using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LiftOffAnimator : MonoBehaviour
{
    private Animator animator;
    private EnemyController enemyController;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = transform.parent.GetComponent<EnemyController>();
        player = enemyController.player;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Vector3.Distance(transform.position, enemyController.player.position) < 2.1f)
        {

            Vector3 targetPosition =
                new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(targetPosition);
            animator.SetInteger("AnimationState", 2);
        }
        else
        {
            animator.SetInteger("AnimationState", 1);
        }
            
    }
}

