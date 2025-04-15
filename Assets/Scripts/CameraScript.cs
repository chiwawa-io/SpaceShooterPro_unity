using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    private void Start()
    {
        if (animator == null) Debug.Log("Animator on Camera is NULL");
    }

    public void CameraShake ()
    {
        animator.SetTrigger("Shake");
    }
}
