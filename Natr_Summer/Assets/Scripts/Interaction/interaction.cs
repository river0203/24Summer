using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour
{
    [SerializeField]
    private GameObject _interaction;
    [SerializeField]
    private DialoguePrint _dp;

    public bool _isCoroutine = false;

    private void Start()
    {
        _interaction.SetActive(false);
    }

    private void Update()
    {
        if (!_isCoroutine)
        {
            if (_interaction.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(_dp.StartDialogue());

                    _isCoroutine = true;
                }
            }
        }

       else
        {
            if (_dp.img_script.activeInHierarchy == false)
            {
                StopCoroutine(_dp.StartDialogue());
                _isCoroutine = false;
            }
        }
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
