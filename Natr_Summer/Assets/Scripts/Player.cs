using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Mob
{
    private int     _currentHp;
    private int     _maxMana = 100;
    private int     _currentMana = 0;
    private string  _moveDir;
    //move
    private float   _attackRange = 10f; 
    private float   _basicSpeed = 5;
    //jump
    private float   _basicJumpForce = 4;
    private float   _jumpTime = 0f;
    private float   _jumpTimeLimit = 0.1f;
    //knock back
    private float   _knockBackForce = 5f;
    private float   _knockBackDuration = 0.5f;
    private float   _knockBackTimer = 0f;
    //attack
    private float   _coolTime = 0.3f;
    private float   curTime;

    private bool    _gateOpen = false;
    private bool    _isKnockedBack = false;
    private bool    _isJump;
    private bool    _interObj = true;

    [SerializeField]
    public GameObject    bullet;
    [SerializeField]
    public Transform        pos;
    private Vector3         mPosition;
    private Rigidbody2D     _rigid;
    public Animator         _animator;
    private SpriteRenderer  _spriteRenderer;

    private GameManager _gameManager;
    private changeScene _changeScene;

    [SerializeField]
    private HP_UI       _UI_HP;

    void Start()
    {
        _changeScene = new changeScene();
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        _currentHp = _gameManager.get_playercurrentHP();
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
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
    public bool getGateOpen() { return _gateOpen; }
    public bool getIsJump() { return _isJump; }

    public int getplayerhp() { return _currentHp; }
    /*public IEnumerator DelayTimer()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer > 2)
        {
            yield return null;

        }
    }*/
    public override void attack()
    {
        if (curTime <= 0)
        {
            if (Input.GetKey(KeyCode.X))
            {
                _animator.SetTrigger("Attack");

                Instantiate(bullet, pos.position, this.transform.rotation);
            }
            curTime = _coolTime;

        }
        curTime -= Time.deltaTime;
    }
    public override void hit()
    {
        _currentHp -= 1; // 상대 데미지를 받아와야함
        _animator.SetTrigger("Damage");

        if (!_isKnockedBack)
        {
            _rigid.velocity = Vector2.zero;
            _rigid.AddForce(mPosition.normalized * _knockBackForce, ForceMode2D.Impulse);

            _isKnockedBack = true;
            _knockBackTimer = _knockBackDuration;
        }

        _UI_HP.UI_player_hp_minus(_currentHp);
    }
    public override void move()
    {
        float moveSpeed;
        mPosition = Vector3.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _animator.SetBool("Walk", true);
            _moveDir = "right";
            moveSpeed = _basicSpeed;
            mPosition += Vector3.right;
            transform.position += mPosition * moveSpeed * Time.deltaTime;
            _spriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _animator.SetBool("Walk", true);
            _moveDir = "left";
            moveSpeed = _basicSpeed;
            mPosition += Vector3.left;
            transform.position += mPosition * moveSpeed * Time.deltaTime;
            _spriteRenderer.flipX = true;
        }
        else
        {
            _animator.SetBool("Walk", false);
            //_moveDir = "None";
            moveSpeed = 0;
            transform.position += mPosition * moveSpeed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.C))
        {
            _moveDir = "up";
            if (_jumpTime == 0)
            {
                _animator.SetTrigger("Jump");
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

    public void DeadAnim()
    {
        dead(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("DropItem"))
        {
            _currentMana += 10;
            Debug.Log($"current Mana : {_currentMana}");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _jumpTime = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Gate"))
        {
            Debug.Log("Gate Enter");

            int currentScene = _changeScene.getcurrentScene();

            if (currentScene == 2)
            {
                transform.position = new Vector3(23, -1.23f, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("player onTriggerEnter");

        if (collision.CompareTag("EnemyWeapon") || collision.CompareTag("BossWeapon_tail") || collision.CompareTag("BossWeapon_jump") || collision.CompareTag("BossWeapon_dash"))
        {
            Debug.Log("Player : Hit\n");
            Debug.Log($"{_currentHp}");

            if (_currentHp <= 1)
            {
                _animator.SetTrigger("Death");
                Invoke("DeadAnim", 1f);
            }
            else
            {
                hit();

            }
        }
    }
}
