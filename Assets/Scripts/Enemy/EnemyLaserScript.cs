using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserScript : MonoBehaviour
{


    void Update()
    {
        if (transform.position.y <= -9f)
        {
            Destroy(gameObject);
            if (transform.parent != null) Destroy(transform.parent.gameObject);

        }
    }
}
