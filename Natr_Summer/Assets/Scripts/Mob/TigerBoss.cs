using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TigerBoss : Mob
{
    private float   _hp;
    [SerializeField]
    private float   _maxHp = 15f;
    [SerializeField]
    private float   _getDamage = 2f;
    private float   _attackRange;
    [SerializeField]
    private float   _pase1SkillSelectRandom;
    [SerializeField]
    private float   _pase2SkillSelectRandom;
    [SerializeField]
    private float   _moveSpeed = 5f;
    private float   _skillCoolTime;
    private float   _playerDirection;
    private bool    _isMove = true;
    private bool    _pase2 = false;
    private float   _currentTime = 0.0f;
    private float   _stopTime = 4.0f;
    private float   _stopTime_current = 0.0f;
    private float   _movestopTime = 0.0f;

    private Transform   _targetPos;
    private Rigidbody2D _rb;
    private GameObject  _hitBox_tail;
    private GameObject  _hitBox_jump;
    private GameObject  _hitBox_dash;
    private Animator    _animator;

    void Start()
    {
        _rb          = GetComponent<Rigidbody2D>();
        _animator    = GetComponent<Animator>();  
        _targetPos   = GameObject.FindGameObjectWithTag("Player").transform;
        _hitBox_tail = GameObject.FindGameObjectWithTag("BossWeapon_tail");
        _hitBox_jump = GameObject.FindGameObjectWithTag("BossWeapon_jump");
        _hitBox_dash = GameObject.FindGameObjectWithTag("BossWeapon_dash");
        _hp          = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        _stopTime_current += Time.deltaTime;

        if (_hp <= 5f)
        {
            _pase2 = true;
        }

        if (_stopTime_current >= _stopTime)
            stoptime();

        if (!_isMove)
            _movestopTime += Time.deltaTime;

        if (!_isMove && _movestopTime >= 1.5f)
        {
            _isMove = true;
            _movestopTime = 0.0f;
        }
        if (_isMove)
            move();
    }
    private void FixedUpdate()
    {
    }

    private void stoptime()
    {
        //_stopTime ���� ������
        _stopTime = Random.Range(3, 4);
        
        //_isMove�� ��Ȱ��ȭ (IDLE ���·� ��ȯ)
        _isMove = false;
        _stopTime_current = 0.0f;
    }

    public void think()
    {
        if (_pase2 == false)
        {
            _pase1SkillSelectRandom = Random.Range(1, 4);
        }
        else if (_pase2 == true)
        {
            _pase2SkillSelectRandom = Random.Range(1, 5);
        }
    }
    public override void move()
    {
        _playerDirection = Vector3.Distance(this.transform.position, _targetPos.transform.position);
        Vector3 _mobFollow = _targetPos.position - this.transform.position;
        _mobFollow.Normalize();

        if (_mobFollow.x > 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        else if (_mobFollow.x < 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        //�÷��̾�� ������ ��ġ�� �������� �ʵ��� �ؾ� ��
            transform.position += _mobFollow * _moveSpeed * Time.deltaTime;

        if(_playerDirection <= 3 && _currentTime >= 5f)
        {
            think();
            attack();
            _currentTime = 0;
        }
    }
    public override void attack()
    {
        if (_targetPos == null)
        {
            Debug.Log("Load End Scene");
        }

        if(_pase2 == false)
        {
            switch(_pase1SkillSelectRandom)
            {
                case 1:
                    //skill 1
                    StartCoroutine(TailAttack());
                    break;
                case 2:
                    //skill 2
                    StartCoroutine(JumpAttack());
                    break;
                case 3:
                    //skill 3
                    StartCoroutine(DashAttack());
                    break;
                default:
                    break;
            }
        }
        else if(_pase2 == true)
        {
            switch(_pase2SkillSelectRandom)
            {
                case 1:
                    //skill 2
                    StartCoroutine(JumpAttack());
                    break;
                case 2:
                    //skill 3
                    StartCoroutine(DashAttack());
                    break;
                case 3:
                    //skill 4
                    StartCoroutine(BarkAttack());
                    break;
                case 4:
                    //skill 5
                    StartCoroutine(Envasion());
                    break;
                default:
                    break;
            }
        }

    }

    IEnumerator TailAttack()
    {
        yield return new WaitForSeconds(1.0f);
        _isMove = false;

        //�÷��̾ �ִ� ���� �ݴ�� ���Ƽ� �ִϸ��̼� ���
        Debug.Log("TailAttack");
        _animator.SetTrigger("tailAttack");

        //�ִϸ��̼� ��� �� ���� ��Ʈ�ڽ� Ȱ��ȭ
        yield return new WaitForSeconds(5*Time.deltaTime);

        _isMove = true;
    }
    IEnumerator JumpAttack()
    {
        yield return new WaitForSeconds(1.0f);
        _isMove = false;

        //���� �ִϸ��̼� ��� �� y ��ǥ ���, ���� �÷��̾� �Ӹ� ������ ����
        Debug.Log("JumpAttack");
        _animator.SetTrigger("jumpAttack");

        yield return new WaitForSeconds(8*Time.deltaTime);

        _isMove = true;
    }
    IEnumerator DashAttack()
    {
        yield return new WaitForSeconds(1.0f);
        _isMove = false;

        //�÷��̾� ������ ����
        Debug.Log("DashAttack");
        _animator.SetTrigger("dashAttack");

        yield return new WaitForSeconds(8 * Time.deltaTime);

        _isMove = true;
    }
    IEnumerator BarkAttack()
    {
        Debug.Log("BarkAttack");
        yield return new WaitForSeconds(8 * Time.deltaTime);
    }
    IEnumerator Envasion()
    {
        Debug.Log("Envasion");
        yield return new WaitForSeconds(8 * Time.deltaTime);
    }
    public override void hit()
    {
        _hp -= _getDamage;
        if (_hp <= 0)
        {
            StartCoroutine(DropItem());
            _hitBox_tail.GetComponent<BoxCollider2D>().enabled = false;
            _hitBox_jump.GetComponent<BoxCollider2D>().enabled = false;
            _hitBox_dash.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator DropItem()
    {
        // mana +10
        /*for (int i = 0; i < spawnCont; i++)
        {
            Instantiate(manaBall, transform.position, transform.rotation);
        }*/
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("PlayerWeapon"))
        {
            hit();
        }
    }
}
