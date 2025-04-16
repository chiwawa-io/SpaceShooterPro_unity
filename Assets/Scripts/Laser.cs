using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private int _speed;

    void Update()
    {
        transform.Translate (Vector3.up*Time.deltaTime*_speed);

        if (transform.position.y >= 9f)
        {
            if (transform.parent != null) Destroy(transform.parent.gameObject); 
            Destroy(gameObject);
            
        }

    }

    
}
