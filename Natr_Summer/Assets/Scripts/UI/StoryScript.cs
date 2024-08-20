using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScript : MonoBehaviour
{
    [SerializeField]
    private DialoguePrint _dp;
    private IEnumerator enumerator;
    private bool _isCoroutine = false;

    private int eventNumber = 0;

    private changeScene scene;
    //private GameManager _gm;

    private void Start()
    {
        //_gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        scene = new changeScene();

        enumerator = _dp.StartDialogue();

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
        if (_dp.currentDialogue() && eventNumber == 0)
        {
            _isCoroutine = false;
            eventNumber++;

            changebackground();
        }

        if (_dp.endDialogue() && eventNumber == 1)
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
            StartCoroutine(_dp.StartDialogue());

            _isCoroutine = true;
        }
    }

    private void endIntro()
    {
        Debug.Log("end Intro ����");

        scene.changescene(SceneState.STAGE1);
    }
}
