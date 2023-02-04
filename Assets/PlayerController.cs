using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed;

    [SerializeField] private float speedRotate = 50.0f;

    // acceleration variable
    public float accelerationTime = 60; 
    float maxSpeed = 10f;
    private float minSpeed = 1.5f;
    private float time;

    void Start()
    {
        transform.position = new Vector3(0, 0);
        minSpeed = speed;
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
    }
}
