using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 1.5f;

    [SerializeField] private float speedRotate = 15.0f;
    void Start()
    {
        transform.position = new Vector3(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * -transform.up);
        transform.Rotate(speedRotate * Time.deltaTime * Vector3.forward);
    }
}
