using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int enemyCount;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private String sceneExit;
    [SerializeField] private TMP_Text enemyText;
    private bool canExit = false;

    // sounds
    [SerializeField] private AudioSource killSoundEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint;
        enemyText.text = "Enemies Remaining: " + enemyCount;
        if (enemyCount == 0)
        {
            canExit = true;
        }
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
            enemyText.text = "Enemies Remaining: " + enemyCount;
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
