using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float speedGainPerSecond = 0.1f;
    [SerializeField] float turnSpeed = 200f;

    int steerValue;
    void Update()
    {
        speed += speedGainPerSecond * Time.deltaTime;

        transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Obstacle")
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Steer(int value)
    {
        steerValue = value;
    }
}
