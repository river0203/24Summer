using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour
{
    [SerializeField]
    private GameObject _interaction;

    private void Start()
    {
        _interaction.SetActive(false);
    }

    private void Update()
    {
        if (_interaction.activeInHierarchy)
        {
           if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }

        //���ͷ����� Ȱ��ȭ �Ǿ� ���� �� space�� ������ ��ũ��Ʈ ���
        //��ư Ŭ�� �� ���� ������ �̵�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.gameObject.tag == "Player")
        {
            _interaction.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _interaction.SetActive(false);
        }
    }
}
