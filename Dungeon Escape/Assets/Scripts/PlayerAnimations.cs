﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Animator _anim;
    private Animator _swordAnimation;


    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _swordAnimation = transform.GetChild(1).GetComponent<Animator>();
    }

   
   public void Move(float move)
    {
        _anim.SetFloat("Move", Mathf.Abs(move));
    }


    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }

    public void AttackAnimation()
    {
        _anim.SetTrigger("Attack");
        _swordAnimation.SetTrigger("SwordAnimation");
    }

    public void Death()
    {
        _anim.SetTrigger("Death");
    }

}