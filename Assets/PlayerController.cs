using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

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
    [SerializeField] private GameObject trailObject;

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
        
        if (Vector3.Distance(transform.position, lastTrailUpdate) >= trailUpdateThreshold)
        {
            CreateTrail();
        }
    }

    void CreateTrail()
    {
        Instantiate(trailObject, transform.position, transform.rotation);
        lastTrailUpdate = transform.position;
    }
}
