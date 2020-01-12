using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    //[SerializeField]
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        animator.Play("attack");
    }
    public void PlayHitAnimation()
    {
        animator.Play("hit");
    }
    public void PlayDeadAnimation()
    {
        animator.Play("dead");
    }
    public void PlayWalkAnimation()
    {
        animator.Play("walk");
    }


    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        AnimationAttack();
    //    }
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        PlayHitAnimation();
    //    }
    //    if(Input.GetKeyDown(KeyCode.W))
    //    {
    //        PlayDeadAnimation();
    //    }
    //}
}
