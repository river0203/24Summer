using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TigerBoss : Mob
{
    private float   _hp;
    [SerializeField]
    private float   _maxHp = 100f;
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
    private bool    _pase2 = false;
    
    private Transform   _targetPos;
    private Rigidbody2D _rb;
    private GameObject  _hitBox;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _targetPos = GameObject.FindGameObjectWithTag("Player").transform;
        _hitBox = GameObject.FindGameObjectWithTag("EnemyWeapon");
        _hp = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(_hp <= 30f)
        {
            _pase2 = true;
        }
        move();
    }
    private void FixedUpdate()
    {
        think();
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
        transform.position += _mobFollow * _moveSpeed * Time.deltaTime;

        if(_playerDirection <= 3)
        { 
            attack();
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
        Debug.Log("TailAttack");
        yield return null;
        attack();
    }
    IEnumerator JumpAttack()
    {
        Debug.Log("JumpAttack");
        yield return new WaitForSeconds(8*Time.deltaTime);
        attack();
    }
    IEnumerator DashAttack()
    {
        Debug.Log("DashAttack");
        yield return new WaitForSeconds(8 * Time.deltaTime);
        attack();
    }
    IEnumerator BarkAttack()
    {
        Debug.Log("BarkAttack");
        yield return new WaitForSeconds(8 * Time.deltaTime);
        attack();
    }
    IEnumerator Envasion()
    {
        Debug.Log("Envasion");
        yield return new WaitForSeconds(8 * Time.deltaTime);
        attack();
    }
    public override void hit()
    {
        _hp -= _getDamage;
        if (_hp <= 0)
        {
            StartCoroutine(DropItem());
            _hitBox.GetComponent<BoxCollider2D>().enabled = false;
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
