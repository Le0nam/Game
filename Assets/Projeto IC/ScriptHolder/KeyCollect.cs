using UnityEngine;

public class GemCollect : MonoBehaviour
{
    [SerializeField] AudioSource ding;

    void OnTriggerEnter(Collider other)
    {
        ding.Play();
        Destroy(gameObject);
    }
}
