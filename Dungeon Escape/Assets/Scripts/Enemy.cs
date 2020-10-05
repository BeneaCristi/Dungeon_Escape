using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    public GameObject diamondPrefab;

    protected bool isHit = false;
    protected bool isDead = false;


    protected UnityEngine.Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected Player player;


    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
        {
            return;
        }
        if (isDead == false)
            Movement();


    }



    public virtual void Movement()
    {
        if (transform.position == pointA.position)
        {
            anim.SetTrigger("Idle");
            currentTarget = pointB.position;

        }
        else if (transform.position == pointB.position)
        {
            anim.SetTrigger("Idle");
            currentTarget = pointA.position;
        }
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        if(isHit == false)
        {
            transform.position = UnityEngine.Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        float distance = UnityEngine.Vector3.Distance(transform.localPosition, player.transform.localPosition);

        if(distance > 2.0f)
        {
            isHit = false;
           anim.SetBool("InCombat", false);
        }

       

        UnityEngine.Vector3 direction = player.transform.localPosition - transform.localPosition;

        if (anim.GetBool("InCombat") == true)
        {
            if (direction.x < 0)
            {
                sprite.flipX = true;
            }
            else if (direction.x > 0)
            {
                sprite.flipX = false;
            }
        }


    }
    

}
