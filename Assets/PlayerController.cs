using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 1.5f;

    [SerializeField] private float speedRotate = 50.0f;
    void Start()
    {
        transform.position = new Vector3(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * -transform.right;

        var axis = Input.GetAxis("Horizontal");
        
        transform.Rotate(axis * speedRotate * Time.deltaTime * Vector3.forward);
    }
}
