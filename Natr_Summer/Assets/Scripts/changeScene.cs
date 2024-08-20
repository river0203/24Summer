using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    Scene scene;
    public int currentScene = 0;


    public int getcurrentScene()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);

        if (scene.name == "Intro")
            currentScene = 0;

        else if (scene.name == "Stage1")
            currentScene = 1;
         
        else
            currentScene = 2;

        return currentScene;
    }

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
