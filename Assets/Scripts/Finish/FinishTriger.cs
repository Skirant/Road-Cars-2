using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTriger : MonoBehaviour
{
    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour playerBehaviour = other.attachedRigidbody.GetComponent<PlayerBehaviour>();

        if (playerBehaviour)
        {
            playerBehaviour.StartFinishBehavior();
            FindObjectOfType<GameManager>().ShowFinishWindow();
        }
    }
}
