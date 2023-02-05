using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed;
    private float time;


    [SerializeField] private float speedRotate = 50.0f;

    // acceleration variable
    [SerializeField] private float accelerationTime = 60; 
    [SerializeField] private float maxSpeed = 5.0f;
    [SerializeField] private float minSpeed = 1.0f;

    private Vector3 lastTrailUpdate;
    private float trailUpdateThreshold = 0.4f;
    [SerializeField] private GameObject trailPrefab;
    [SerializeField] private Transform trailFolder;

    [SerializeField] private float maxFuel = 20.0f;
    private float fuel;
    [SerializeField] private string sceneName;

    private bool hasStarted = false;
    private bool dead = false;

    [SerializeField] private TMP_Text uiText;
    [SerializeField] private Slider slider;

    void Start()
    {
        lastTrailUpdate = transform.position;
        fuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKey)
            {
                uiText.text = "";
                hasStarted = true;
            }
            return;
        }

        if (dead) {
            if (Input.anyKey)
            {
                SceneManager.LoadScene(sceneName);
            }
            return;
        }
        // acceleration
        speed = Mathf.SmoothStep(minSpeed, maxSpeed, time / accelerationTime);

        transform.position += speed * Time.deltaTime * -transform.right;

        var axis = Input.GetAxis("Horizontal");
        
        transform.Rotate(axis * speedRotate * Time.deltaTime * Vector3.forward);
        time += Time.deltaTime;
        fuel -= Time.deltaTime;
        slider.value = fuel / maxFuel;
        
        if (Vector3.Distance(transform.position, lastTrailUpdate) >= trailUpdateThreshold)
        {
            CreateTrail();
        }

        if (fuel <= 0.0f)
        {
            Die();
        }
    }

    private void CreateTrail()
    {
        var trail = Instantiate(trailPrefab, transform.position, transform.rotation);
        trail.transform.parent = trailFolder;
        trail.tag = "NewTrail";
        lastTrailUpdate = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("Fuel"))
        {
            fuel += 5.0f;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NewTrail"))
        {
            collision.gameObject.tag = "Obstacle";
        }
        if (collision.gameObject.CompareTag("Bounds"))
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        uiText.text = "Press any key to restart...";
    }
}