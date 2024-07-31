using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���� �̵��ص� �����ؾ��� �׸�, ���, hp, ������ etc
public class GameManager : MonoBehaviour
{
    private static GameManager gameManager = null;

    public GameObject[] HP = new GameObject[5];
    public HP_animation[] hp_Ani = new HP_animation[5];
    private int HP_number = 5;

    public Image img_element;
    public Sprite[] element = new Sprite[5];

    public GameObject img_script;
    public Text titleText;
    public Text contentText;

    //[SerializeField]
    //private DialogueManager dialogue;
    //private int currentLineIndex = 0;

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
        //dialogue = GetComponent<DialogueManager>();

        img_script.SetActive(false);
        //StartCoroutine(StartDialogue());
    }

    private void Update()
    {

    }

    //IEnumerator StartDialogue()
    //{
    //    yield return new WaitForSeconds(1f);

    //    img_script.SetActive(true);

    //    while (dialogue.DialogueToString(currentLineIndex, 0, SceneState.Intro, 3) != null) //���� �ʿ�
    //    {
    //        titleText.text = dialogue.DialogueToString(currentLineIndex, 0, SceneState.Intro, 3); //���� �ʿ�
    //        Debug.Log("get title text");
    //        contentText.text = dialogue.DialogueToString(currentLineIndex, 0, SceneState.Intro, 4); //���� �ʿ�
    //        Debug.Log("get content text");

    //        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

    //        if (Input.GetKeyDown(KeyCode.Space))
    //            currentLineIndex++;
    //    }

    //    if (dialogue.DialogueToString(currentLineIndex, 0, SceneState.Intro, 3) == null)
    //        img_script.SetActive(false);

    //}

    public void UI_player_hp_minus(int p_hp)
    {
        //HP ���Ұ� 1�� ���
        if (HP_number - p_hp == 1)
        {
            HP_number = p_hp;

            Debug.Log("�ִϸ��̼� ���");
            hp_Ani[p_hp].HP_minus_animaition();

            Invoke("anim_delay", 1.0f);
        }
    }

    private void anim_delay()
    {
        if (HP_number < 4 && HP[HP_number + 1].activeInHierarchy)
        {
            Debug.Log("ü�� ���� - �迭 + 1�� ��Ȱ��ȭ ���� ����");
            HP[HP_number + 1].SetActive(false);
        }

        else
        {
            Debug.Log("ü�� ����");
            HP[HP_number].SetActive(false);
        }
    }
}
