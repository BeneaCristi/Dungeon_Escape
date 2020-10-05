using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour,IDamageable
{

    private Rigidbody2D _rigid;
    [SerializeField]
    public float _jumpForce = 5.0f;
    private bool resetJump = false;
    [SerializeField]
    private float _speed = 2.5f;
    private PlayerAnimations _playerAnim;
    private SpriteRenderer _playerSprite;
    private bool _grounded = false;
    private SpriteRenderer _swordArcSprite;



    public bool playerIsDead = false;


   

    public int diamonds;

    public int Health { get; set; }

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimations>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
      
        Health = 4;
    }


    void Update()
    {
        if(playerIsDead == false)
        {
            Movement();
            Attack();
        }
        else if(playerIsDead == true)
        {
            return;
        }
        
       
    }

    void Movement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal");

        _grounded = isGrounded();

        Flip(move);
        
        if((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && isGrounded() == true)
        {
            _rigid.velocity = new UnityEngine.Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }
       
        _rigid.velocity = new UnityEngine.Vector2(move * _speed, _rigid.velocity.y);

        _playerAnim.Move(move);
    }

  bool isGrounded()
    {
       RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, UnityEngine.Vector2.down, 0.6f , 1 << 8);
        if(hitInfo.collider != null)
        {
            if(resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }

        }
        return false;

    }

    void Attack()
    {
        if(CrossPlatformInputManager.GetButtonDown("A_Button") && isGrounded()== true)
        {
            _playerAnim.AttackAnimation();
        }
    }

    void Flip(float move)
    {
        if (move < 0)
        {
            _playerSprite.flipX = true;
            FlipSwordArc();
        }
        else
        {
            if (move > 0)
            {
                _playerSprite.flipX = false;
                FlipSwordArc();
            }
        }
    }

    private void FlipSwordArc()
    {
        if (_rigid.velocity.x < 0)
        {
            _playerSprite.flipX = false;
            transform.localScale = new UnityEngine.Vector2(-1, 1);
        }
        else if (_rigid.velocity.x > 0)
        {
            _playerSprite.flipX = false;
            transform.localScale = new UnityEngine.Vector2(1, 1);
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }

    public void Damage()
    {
        if(Health < 1)
        {
            return;
        }

        Health--;
        UIManager.Instance.UpdateLives(Health);

        if (Health < 1)
        {
            
            playerIsDead = true;
            _playerAnim.Death();
        }
      
    }


    public void AddGems(int amaount)
    {
        diamonds += amaount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }



}
