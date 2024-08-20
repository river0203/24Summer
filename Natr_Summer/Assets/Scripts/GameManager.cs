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
    private Player _player;

    private bool isPause          = false;
    private bool gameloop         = true;
    private int _maxHp            = 5;
    private int _playercurrentHP  = 5;
    private int currentScene;

    private changeScene _scene;

    public int get_playercurrentHP() { return _playercurrentHP; }
    public bool get_gameloop() { return gameloop; }

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
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _scene = new changeScene();

        gameloop = true;
        currentScene = _scene.getcurrentScene();
        //_playercurrentHP = _maxHp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                isPause = false;
                Time.timeScale = 0.0f;
            }

            else
            {
                isPause = true;
                Time.timeScale = 1.0f;
            }
        }

        _playercurrentHP = _player.getplayerhp();

        if (_playercurrentHP <= 1)
            gameloop = false;
    }
}
