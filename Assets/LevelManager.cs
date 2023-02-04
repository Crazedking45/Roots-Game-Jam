using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int enemyCount;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private String sceneExit;
    private bool canExit = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kill"))
        {
            enemyCount--;
            Destroy(collision.gameObject);

            if(enemyCount==0) {
                canExit = true;
            }
        }

        if (collision.gameObject.CompareTag("Exit") && canExit)
        {
            SceneManager.LoadScene(sceneExit);
        }
    }
}
