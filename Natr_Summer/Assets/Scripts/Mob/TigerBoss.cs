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
    }
    private void FixedUpdate()
    {
        if(_pase2 == false)
        {
            _pase1SkillSelectRandom = Random.Range(1, 4);
        }
        else if(_pase2 == true)
        {
            _pase2SkillSelectRandom = Random.Range(1, 5);
        }
    }
    public override void move()
    {
        Vector3 _mobFollow = _targetPos.position - this.transform.position;
        _mobFollow.Normalize();
        transform.position += _mobFollow * _moveSpeed * Time.deltaTime;
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
                    break;
                case 2:
                    //skill 2
                    break;
                case 3:
                    //skill 3
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
                    break;
                case 2:
                    //skill 3
                    break;
                case 3:
                    //skill 4
                    break;
                case 4:
                    //skill 5
                    break;
                default:
                    break;
            }
        }

    }
    public void AttackAble()
    {
        _hitBox.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void AttackEnable()
    {
        _hitBox.GetComponent<BoxCollider2D>().enabled = false;
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
