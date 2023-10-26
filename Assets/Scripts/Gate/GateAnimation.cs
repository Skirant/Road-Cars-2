using System.Collections;
using UnityEngine;

public class GateAnimation : MonoBehaviour
{
    public float duration = 3f;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Start()
    {
        startPosition = new Vector3(-2.4f, transform.position.y, transform.position.z);
        endPosition = new Vector3(2.4f, transform.position.y, transform.position.z);
        StartCoroutine(MoveObjectCoroutine());
    }

    private IEnumerator MoveObjectCoroutine()
    {
        while (true)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(endPosition, startPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
