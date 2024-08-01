using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SceneState
{
    INTRO, STAGE1, BOSS
}

//���� �̵��ص� �����ؾ��� �׸�, ���, hp, ������ etc
public class GameManager : MonoBehaviour
{
    private static GameManager gameManager = null;

    public GameObject[] HP = new GameObject[5];
    public HP_animation[] hp_Ani = new HP_animation[5];
    private int HP_number = 4;

    public Image img_element;
    public Sprite[] element = new Sprite[5];

    public GameObject img_script;
    public Text titleText;
    public Text contentText;

    [SerializeField]
    private DialogueManager dialogue;
    private int currentLineIndex = 0;
    private int eventNumber = 0;
    private int currentScene;

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(gameManager != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Start()
    {
        currentScene = (int)SceneState.INTRO;
        dialogue.readCSV(SceneState.INTRO);

        img_script.SetActive(false);
    }

    private void Update()
    {

    }

    public IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1f);

        img_script.SetActive(true);

        while (dialogue.DialogueToString(currentLineIndex, eventNumber, 4) != null)
        {
            titleText.text = dialogue.DialogueToString(currentLineIndex, eventNumber, 3);
            Debug.Log("get title text " + currentLineIndex);
            contentText.text = dialogue.DialogueToString(currentLineIndex, eventNumber, 4);
            Debug.Log("get content text " + currentLineIndex);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            if (Input.GetKeyDown(KeyCode.Space))
                currentLineIndex++;
        }

        if (dialogue.DialogueToString(currentLineIndex, eventNumber, 3) == null)
        {
            eventNumber++;
            img_script.SetActive(false);
        }
    }
     
    public void UI_player_hp_minus(int p_hp)
    {
        //HP ���Ұ� 1�� ���
        if (HP_number - p_hp == 1)
        {
            HP_number = p_hp;

            Debug.Log("�ִϸ��̼� ���");
            hp_Ani[HP_number + 1].HP_minus_animaition();

            Invoke("anim_delay", 1.0f);
        }
    }

    private void anim_delay()
    {
        //if (HP_number < 4 && HP[HP_number + 1].activeInHierarchy)
        //{
        //    Debug.Log("ü�� ���� - �迭 + 1�� ��Ȱ��ȭ ���� ����");
        //    HP[HP_number + 1].SetActive(false);
        //}

        Debug.Log("ü�� ����");
        HP[HP_number + 1].SetActive(false);
    }
}
