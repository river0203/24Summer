using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonster : Mob
{
    private int     _nextMoveDir = 1;
    private int     _beforeMoveDir;
    private float   _thintTime = 0f;
    private float   _damage = 1; // mob Weapon으로 전환
    private float   _hp = 2;
    private float   _moveSpeed = 1f;
    private float   _playerDirection;
    private float   _mobDetectionArea = 7;
    private float   _attackTimer;

    private MobState    _presentMobState;

    private Transform   _playerPosition;
    private Rigidbody2D _rigid;
    private GameObject  _hitBox;

    // Start is called before the first frame update
    private void Awake()
    {
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        _hitBox = GameObject.FindGameObjectWithTag("EnemyWeapon");

    }

    void Start()
    {
        
        _rigid = GetComponent<Rigidbody2D>();   
        think();
    }
    private void Update()
    {
        if(_attackTimer > 2)
        {
            _hitBox.GetComponent<BoxCollider2D>().enabled = false;
        }

        if(_playerPosition == null)
        {
            think();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_hp <= 0)
        {
            dead(this.gameObject);
        }
        if(_presentMobState != MobState.ATTACK)
        {
            move();
        }
        attack();
    }
    public override void attack()
    {
        _playerDirection = Vector3.Distance(this.transform.position, _playerPosition.transform.position);

        if(_playerDirection <= _mobDetectionArea)
        {
            _presentMobState = MobState.ATTACK;
            Vector3 _mobFollow = _playerPosition.position - this.transform.position;
            _mobFollow.Normalize();
            transform.position += _mobFollow * _moveSpeed * Time.deltaTime;
            _hitBox.GetComponent<BoxCollider2D>().enabled = true;
            _attackTimer += Time.deltaTime;
        }
        else
        {
            _presentMobState = MobState.RUN;
        }
    }
    public override void hit()
    {
        Debug.Log("Mob : Hit");
        if(_hp <= 0 )
        {
            dead(this.gameObject);
        }
        _hp -= 1; // 플레이어의 공격 정보를 받아와야함
    }
    public override void move()
    {
        _nextMoveDir = think();
        _rigid.velocity = new Vector2(_nextMoveDir, _rigid.velocity.y * _moveSpeed);
    }
    public int think()
    {
        _thintTime += Time.deltaTime;  

        if(_thintTime > 5f)
        {
            _nextMoveDir = Random.Range(-1, 2);
            _beforeMoveDir = _nextMoveDir;
            _thintTime = 0;
        }
        return _nextMoveDir;
    }

    public void dropItem()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Mob : Attack");
        }

        if (collision.CompareTag("PlayerWeapon"))
        {
            hit();
        }
    }
}
