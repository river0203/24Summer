using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float   cameraSpeed = 10.0f;
    [SerializeField]
    private GameObject  player;
    private Player      _strDir;
    private int         _dirNumX;
    private int         _dirNumY;
    private string      _playerDir;
    private string      _befDir;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _strDir = player.GetComponent<Player>();
    }

    private void Update()
    {
        _playerDir = _strDir.getMoveDir();

        switch(_playerDir)
        {
            case "up":
                if(_befDir == "right")
                {
                    _dirNumX = 3;
                }
                
                if(_befDir == "right")
                {
                    _dirNumX = -3;
                }

                if(_befDir == "up")
                {
                    _dirNumX = 0;
                }
                _dirNumY = 0;
                break;

            case "left":
                _dirNumX = -5;
                _dirNumY = 0;
                break;

            case "right":
                _dirNumX = 5;
                _dirNumY = 0;
                break;
        }

        Vector3 dir = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3((dir.x + _dirNumX) * cameraSpeed * Time.deltaTime, (dir.y + _dirNumY) * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
        _befDir = _playerDir;
    }
}