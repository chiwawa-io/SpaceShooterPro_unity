using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    
    void Start()
    {
        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    IEnumerator Destroy ()
    {
        yield return new WaitForSeconds(2.37f);
        Destroy(gameObject);
    }
}
