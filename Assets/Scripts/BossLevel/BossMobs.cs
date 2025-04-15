using UnityEngine;

public class BossMobs : MonoBehaviour
{
    //[SerializeField]
    //private GameObject _enemyDestroyPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Instantiate(_enemyDestroyPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.tag == "laser")
        {
            //Instantiate(_enemyDestroyPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
