using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScript : MonoBehaviour
{
    [SerializeField]
    private GameManager _gm;
    private IEnumerator enumerator;
    private bool _isCoroutine = false;

    private int eventNumber = 0;

    private void Start()
    {
        enumerator = _gm.StartDialogue();

        if (!_isCoroutine)
        {
            StartCoroutine(enumerator);
            _isCoroutine = true;
        }
    }

    private void Update()
    {
        if (_isCoroutine)
        {
            Invoke("nextDialogue", 1f);
        }
    }

    private void nextDialogue()
    {
        if (_gm.currentDialogue() && eventNumber == 0)
        {
            _isCoroutine = false;
            eventNumber++;

            changebackground();
        }

        if (_gm.endDialogue() && eventNumber == 1)
        {
            eventNumber++; 
            endIntro();
        }    
    }

    private void changebackground()
    {
        Debug.Log("change backgoround ����");
        //�̹��� ���� ��ũ��Ʈ �߰�
        if (!_isCoroutine)
        {
            StartCoroutine(_gm.StartDialogue());

            _isCoroutine = true;
        }
    }

    private void endIntro()
    {
        Debug.Log("end Intro ����");

        _gm.sceneChange();
    }
}
