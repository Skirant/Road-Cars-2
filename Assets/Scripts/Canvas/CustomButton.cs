using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        // Ваша функция Play()
        PlayerBehaviour playerBehaviour = Object.FindFirstObjectByType<PlayerBehaviour>();

        if (playerBehaviour != null)
        {
            playerBehaviour.Play();
            Destroy(gameObject);
        }
    }
}
