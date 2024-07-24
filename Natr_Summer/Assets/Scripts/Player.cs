using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Mob
{
    private int     _hp = 4;
    private string  _moveDir;
    //move
    private float   _attackRange = 10f; 
    private float   _basicSpeed = 8;
    //jump
    private float   _basicJumpForce = 8;
    private float   _jumpTime = 0f;
    private float   _jumpTimeLimit = 0.1f;
    //knock back
    private float   _knockBackForce = 5f;
    private float   _knockBackDuration = 0.5f;
    private float   _knockBackTimer = 0f;
    //attack
    private float _coolTime = 0.6f;
    private float curTime;

    [SerializeField]
    public GameObject bullet;
    [SerializeField]
    public Transform pos;

    private bool    _isKnockedBack = false;
    private bool    _isJump;

    private Vector3 mPosition;
    private Rigidbody2D _rigid;
    private Animator    _animator;

    [SerializeField]
    private GameManager _gameManager;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isKnockedBack) 
        {
            _knockBackTimer -= Time.deltaTime;

            if(_knockBackTimer <= 0)
            {
                _isKnockedBack = false;
            }
        }
    }

    private void FixedUpdate()
    {
        move();
        attack();

    }

    public string getMoveDir() { return _moveDir; }
    public override void attack()
    {
        if (curTime <= 0)
        {
            if (Input.GetKey(KeyCode.X))
            {
                _animator.SetTrigger("Attack");
                Instantiate(bullet, pos.position, transform.rotation);
            }
            curTime = _coolTime;

        }
        curTime -= Time.deltaTime;
    }
    public override void hit()
    {
        _hp -= 1; // 상대 데미지를 받아와야함

        if (!_isKnockedBack)
        {
            _rigid.velocity = Vector2.zero;
            _rigid.AddForce(mPosition.normalized * _knockBackForce, ForceMode2D.Impulse);

            _isKnockedBack = true;
            _knockBackTimer = _knockBackDuration;
        }

       _gameManager.UI_player_hp_minus(_hp + 1);
    }
    public override void move()
    {
        float moveSpeed;
        mPosition = Vector3.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _animator.SetTrigger("Walk");
            _moveDir = "right";
            moveSpeed = _basicSpeed;
            mPosition += Vector3.right;
            transform.position += mPosition * moveSpeed * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _animator.SetTrigger("Walk");
            _moveDir = "left";
            moveSpeed = _basicSpeed;
            mPosition += Vector3.left;
            transform.position += mPosition * moveSpeed * Time.deltaTime;

        }
        else
        {
            _moveDir = "None";
            moveSpeed = 0;
            transform.position += mPosition * moveSpeed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.C))
        {
            _moveDir = "up";
            if (_jumpTime == 0)
            {
                Vector3 pos = transform.position;

                pos.y -= 0.5f;
                pos.z = -15;

                //jump effect
                //Instantiate()
            }

            if (!Input.GetKey(KeyCode.C) || _jumpTime >= _jumpTimeLimit) 
            {
                _isJump = false;
                return;
            }

            _rigid.velocity = Vector2.zero;
            _rigid.AddForce(Vector2.up * _basicJumpForce * ((_jumpTime * 10) + 1f), ForceMode2D.Impulse);
            _jumpTime += Time.deltaTime;
        }
        else
        {
            
            moveSpeed = 0;
            transform.position += mPosition * moveSpeed * Time.deltaTime;

        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _jumpTime = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("EnemyWeapon"))
        {
            Debug.Log("Player : Hit\n");
            Debug.Log($"{_hp}");

            if (_hp <= 0)
            {
                dead(this.gameObject);
            }
            else
            {
                hit();

            }
        }
    }
}
