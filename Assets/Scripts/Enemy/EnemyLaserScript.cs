using UnityEngine;

public class EnemyLaserScript : MonoBehaviour
{

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 4f);

        if (transform.position.y <= -9f)
        {
            Destroy(gameObject);
            if (transform.parent != null) Destroy(transform.parent.gameObject);

        }
    }
}
