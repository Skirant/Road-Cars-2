using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;

    private void Update()
    {
        transform.Rotate(0, 180 * Time.deltaTime, 0);
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("Ñoin");

        FindObjectOfType<CoinManager>().AddOne();
        Destroy(gameObject);
    }
}
