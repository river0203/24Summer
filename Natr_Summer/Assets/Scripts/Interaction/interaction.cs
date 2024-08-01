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

        //인터렉션이 활성화 되어 있을 때 space를 누르면 스크립트 출력
        //버튼 클릭 시 다음 씬으로 이동
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
