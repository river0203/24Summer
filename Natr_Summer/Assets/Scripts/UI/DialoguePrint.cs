using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePrint : MonoBehaviour
{
    public GameObject img_script;
    public Text titleText;
    public Text contentText;
    public Button selectButton1;
    public Button selectButton2;
    [SerializeField]
    private DialogueManager dialogue;
    private int currentLineIndex;
    private int eventNumber;

    private GameManager _gm;
    private changeScene scene;

    private int currentScene;

    private void Start()
    {
        scene = new changeScene();

        currentScene = scene.getcurrentScene();
        dialogue.readCSV((SceneState)currentScene);
        currentLineIndex = 0;

        img_script.SetActive(false);
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

                eventNumber++;
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

    public bool onClickButton()
    {
        return true;
    }
}
