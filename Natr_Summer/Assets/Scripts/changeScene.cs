using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void changescene(SceneState state)
    {
        switch (state)
        {
            case SceneState.INTRO: 
                SceneManager.LoadScene("Intro");
                break;

            case SceneState.STAGE1:
                SceneManager.LoadScene("Stage1");
                break;

            case SceneState.BOSS:
                //SceneManager.LoadScene("Boss");
                break;

            default:
                break;
        }
    }
}
