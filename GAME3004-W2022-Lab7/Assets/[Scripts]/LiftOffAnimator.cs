using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LiftOffAnimator : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetInteger("AnimationState", 0); // idle test
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetInteger("AnimationState", 1); // Walk test
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.SetInteger("AnimationState", 2); // Punch test
        }
    }
}
