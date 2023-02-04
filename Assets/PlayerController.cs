using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float trailUpdateThreshold = 0.2f;
    [SerializeField] private GameObject trailPrefab;
    [SerializeField] private Transform trailFolder;
    [SerializeField] private String sceneExit;

    [SerializeField] private float fuel = 20.0f;

    void Start()
    {
        transform.position = new Vector3(0, 0);
        lastTrailUpdate = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // acceleration
        speed = Mathf.SmoothStep(minSpeed, maxSpeed, time / accelerationTime);

        transform.position += speed * Time.deltaTime * -transform.right;

        var axis = Input.GetAxis("Horizontal");
        
        transform.Rotate(axis * speedRotate * Time.deltaTime * Vector3.forward);
        time += Time.deltaTime;
        fuel -= Time.deltaTime;
        
        if (Vector3.Distance(transform.position, lastTrailUpdate) >= trailUpdateThreshold)
        {
            CreateTrail();
        }

        if (fuel <= 0.0f)
        {
            Destroy(this);
        }
    }

    void CreateTrail()
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
            Destroy(this);
        }

        if (collision.gameObject.CompareTag("Fuel"))
        {
            fuel += 5.0f;
        }

        if (collision.gameObject.CompareTag("Exit"))
        {
            SceneManager.LoadScene(sceneExit);
        }

        if (collision.gameObject.CompareTag("Kill"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NewTrail"))
        {
            collision.gameObject.tag = "Obstacle";
        }
    }
}
