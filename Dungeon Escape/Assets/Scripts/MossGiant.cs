﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy,IDamageable
{
    public int Health { get; set; }

    private Diamonds _gems;
  
    public override void Init()
    {
        base.Init();
        Health = base.health;
        _gems = GetComponent<Diamonds>();
    }

    public override void Movement()
    {
        base.Movement();
        
    }

    public void Damage()
    {
        if(isDead == true || player.playerIsDead == true)
        {
            return;
        }

        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        if (Health <= 0)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamonds>().gems = base.gems;

        }
    }

}