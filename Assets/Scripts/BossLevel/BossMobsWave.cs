using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMobsWave : MonoBehaviour
{
    [SerializeField]
    private int _speed;

    void Update()
    {
        Movement();
    }
    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -4f)
        {
            Destroy(gameObject);
        }
    }
}
