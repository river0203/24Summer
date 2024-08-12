using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SceneState
{
    INTRO, STAGE1, BOSS
}

//씬이 이동해도 유지해야할 항목, 골드, hp, 아이템 etc
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
    public Button selectButton1;
    public Button selectButton2;

    [SerializeField]
    private DialogueManager dialogue;
    private int currentLineIndex = 0;
    private int eventNumber = 0;

    private int currentScene = (int)SceneState.INTRO;

    private changeScene scene;

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
        scene = new changeScene();

        dialogue.readCSV((SceneState)currentScene);

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
            string type;

            type = dialogue.checkDialogueType(currentLineIndex);

            if (type == "text")
            {
                contentText.gameObject.SetActive(true);
                selectButton1.gameObject.SetActive(false);
                selectButton2.gameObject.SetActive(false);

                titleText.text = dialogue.DialogueToString(currentLineIndex, eventNumber, 2);
                contentText.text = dialogue.DialogueToString(currentLineIndex, eventNumber, 3);
            }

            else if (type == "select")
            {
                contentText.gameObject.SetActive(false);
                selectButton1.gameObject.SetActive(true);
                selectButton2.gameObject.SetActive(true);

                titleText.text = dialogue.DialogueToString(currentLineIndex, eventNumber, 2);
            }

            else
            {
                img_script.SetActive(false);
                
                currentLineIndex++;
                eventNumber++;

                yield break;
            }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentLineIndex++;
            }
        }
    }

    public bool currentDialogue()
    {
        if (!img_script.activeInHierarchy)
        {
            return true;
        }

        return false;
    }

    public bool endDialogue()
    {
        if (currentLineIndex >= dialogue.count_data)
            return true;

        return false;
    }

    public void sceneChange()
    {
        currentScene++;
        scene.changescene((SceneState)currentScene);
    }

    public void UI_player_hp_minus(int p_hp)
    {
        //HP 감소가 1인 경우
        if (HP_number - p_hp == 1)
        {
            HP_number = p_hp;

            Debug.Log("애니메이션 출력");
            hp_Ani[HP_number + 1].HP_minus_animaition();

            Invoke("anim_delay", 1.0f);
        }
    }

    private void anim_delay()
    {
        //if (HP_number < 4 && HP[HP_number + 1].activeInHierarchy)
        //{
        //    Debug.Log("체력 감소 - 배열 + 1이 비활성화 되지 않음");
        //    HP[HP_number + 1].SetActive(false);
        //}

        Debug.Log("체력 감소");
        HP[HP_number + 1].SetActive(false);
    }

}
