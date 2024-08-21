using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_renual : Mob
{
    private float _hp;
    private float _maxhp = 15;

    private float _moveSpeed = 5f;
    private float _dashSpeed = 10f;
    private float _playerDirection;
    private float _basicJumpForce = 30;
    private float _jumpTime = 0f;

    private bool _areainPlayer = false;

    [SerializeField]
    private int _SkillSelcetRandom;
    private float _skillCooltime = 3.0f;
    private float _skillcurrenttime = 0.0f;
    private float _waittime = 0.0f;
    private BOSSSTATE _bossstate;

    private Transform _targetPos;
    private Rigidbody2D _rb;
    private GameObject _hitBox_tail;
    private GameObject _hitBox_jump;
    private GameObject _hitBox_dash;
    private BoxCollider2D boxCollider;
    private Animator _animator;

    enum BOSSSTATE
    {
        IDLE, MOVE, TAIL, JUMP, DASH, DEATH
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _targetPos = GameObject.FindGameObjectWithTag("Player").transform;
        _hitBox_tail = GameObject.FindGameObjectWithTag("BossWeapon_tail");
        _hitBox_jump = GameObject.FindGameObjectWithTag("BossWeapon_jump");
        _hitBox_dash = GameObject.FindGameObjectWithTag("BossWeapon_dash");
        boxCollider = GetComponent<BoxCollider2D>();   
        _hp = _maxhp;

        _bossstate = BOSSSTATE.IDLE;
    }

    private void Update()
    {
        if (_bossstate == BOSSSTATE.DEATH)
            return;

        if (_bossstate == BOSSSTATE.MOVE)
        {
            move();
        }

        else
        {
            _animator.SetBool("isMove", false);
        }

        if (_areainPlayer)
        {
            if (_bossstate != BOSSSTATE.TAIL || _bossstate != BOSSSTATE.DASH || _bossstate != BOSSSTATE.JUMP)
                _skillcurrenttime += Time.deltaTime;

            if (_skillcurrenttime >= _skillCooltime)
            {
                think();
            }
        }

        else
            playerDirection();
    }

    private void think()
    {
        _skillCooltime = Random.Range(4, 6);
        _SkillSelcetRandom = Random.Range(0, 6);

        _skillcurrenttime = 0;
        attack();
    }


    public override void attack()
    {
        switch (_SkillSelcetRandom)
        {
            case 0:
                _bossstate = BOSSSTATE.IDLE;
                break;

            case 1:
            case 2:
                _bossstate = BOSSSTATE.TAIL;
                StartCoroutine(attack_Tail());
                break;

            case 3:
            case 4:
                _bossstate = BOSSSTATE.DASH;
                StartCoroutine(attack_dash());
                break;

            case 5:
                _bossstate = BOSSSTATE.JUMP;
                StartCoroutine(attack_jump());
                break;

            default:
                break;
        }
    }

    IEnumerator attack_Tail()
    {
        yield return new WaitForSeconds(0.1f);

        //플레이어 위치 추적
        Vector3 _mobFollow = playerDirection();

        //플레이어의 반대 방향으로 회전
        if (_mobFollow.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        else if (_mobFollow.x < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        _hitBox_tail.GetComponent<BoxCollider2D>().enabled = true;

        _animator.SetTrigger("tailAttack");

        yield return new WaitForSeconds(0.5f);

        _hitBox_tail.GetComponent<BoxCollider2D>().enabled = false;

        _bossstate = BOSSSTATE.MOVE;
    }

    IEnumerator attack_dash()
    {
        yield return new WaitForSeconds(0.1f);

        _animator.SetTrigger("dashReady");


        yield return new WaitForSeconds(0.2f);

        Vector3 _mobFollow = playerDirection();

        _animator.SetTrigger("dashAttack");
        _hitBox_dash.GetComponent<BoxCollider2D>().enabled = true;

        if (_mobFollow.x > 0)
        {
            Vector3 targetPos = this.transform.position + new Vector3(10, 0, 0);

            transform.rotation = Quaternion.Euler(0, 180, 0);

            while (transform.position.x <= targetPos.x)
            {
                yield return new WaitForSeconds(0.001f);
                transform.position += _mobFollow * _dashSpeed * Time.deltaTime;
            }
        }
        else if (_mobFollow.x < 0)
        {
            Vector3 targetPos = this.transform.position + new Vector3(-10, 0, 0);

            transform.rotation = Quaternion.Euler(0, 0, 0);

            while (transform.position.x >= targetPos.x)
            {
                yield return new WaitForSeconds(0.01f);
                transform.position += _mobFollow * _dashSpeed * Time.deltaTime;
            }
        }

        _animator.SetTrigger("dashEnd");

        yield return new WaitForSeconds(0.2f);

        _hitBox_dash.GetComponent<BoxCollider2D>().enabled = false;
        _bossstate = BOSSSTATE.MOVE;
    }

    IEnumerator attack_jump()
    {
        yield return new WaitForSeconds(0.1f);

        _animator.SetTrigger("jumpReady");

        yield return new WaitForSeconds(0.2f);

        Vector3 targetPos = this.transform.position + new Vector3(0, 15, 0);

        boxCollider.isTrigger = true;

        while (transform.position.y < targetPos.y)
        {
            yield return new WaitForSeconds(0.1f);

            _rb.velocity = Vector2.zero;
            _rb.AddForce(Vector2.up * _basicJumpForce * ((_jumpTime * 10) + 1f), ForceMode2D.Impulse);
            _jumpTime += Time.deltaTime;
        }
        _jumpTime = 0;

        _hitBox_jump.GetComponent<BoxCollider2D>().enabled = true;

        targetPos.x = _targetPos.position.x;
        this.transform.position = targetPos;

        _animator.SetTrigger("jumpAttack");

        _hitBox_jump.GetComponent<BoxCollider2D>().enabled = false;
        boxCollider.isTrigger = false;

        _bossstate = BOSSSTATE.MOVE;

    }

    public override void hit()
    {
        _hp -= 1;

        if (_hp <= 0)
        {
            _bossstate = BOSSSTATE.DEATH;
            death();
        }
    }

    private void death()
    {
        _animator.SetBool("isDeath", true);

        _hitBox_tail.GetComponent<BoxCollider2D>().enabled = false;
        _hitBox_jump.GetComponent<BoxCollider2D>().enabled = false;
        _hitBox_dash.GetComponent<BoxCollider2D>().enabled = false;

        Invoke("DeadAnim", 1.0f);
    }

    private void DeadAnim()
    {
        dead(this.gameObject);
    }

    private Vector3 playerDirection()
    {
        _playerDirection = Vector3.Distance(this.transform.position, _targetPos.transform.position);
        Vector3 _mobFollow = _targetPos.position - this.transform.position;
        _mobFollow.Normalize();

        if (_playerDirection >= 20f)
        {
            _areainPlayer = false;
        }
        else
        {
            _areainPlayer = true;
        }

        return _mobFollow;
    }

    public override void move()
    {
        _animator.SetBool("isMove", true);

        Vector3 _mobFollow = playerDirection();

        if (_mobFollow.x > 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        else if (_mobFollow.x < 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        if (_playerDirection >= 3.5f)
        {
            transform.position += _mobFollow * _moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerWeapon"))
        {
            hit();
        }

        if (collision.CompareTag("Player"))
        {
            Debug.Log(_bossstate);
        }
    }
}


