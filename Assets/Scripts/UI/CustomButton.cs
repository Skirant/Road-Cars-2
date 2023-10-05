using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler
{
    [System.Obsolete]
    public void OnPointerDown(PointerEventData eventData)
    {
        // Ваша функция Play()
        PlayerBehaviour playerBehaviour = Object.FindObjectOfType<PlayerBehaviour>();

        if (playerBehaviour != null)
        {
            playerBehaviour.Play();
            Destroy(gameObject);
        }

        // Disable the parent object
        if (transform.parent != null)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
