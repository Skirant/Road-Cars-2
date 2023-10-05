using UnityEngine;

public class SwipeToPlay : MonoBehaviour
{
    public RectTransform pointer;

    public float speed = 5f;
    public float minPositionX;
    public float maxPositionX;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float t = (Time.time - startTime) * speed;
        float newXPosition = Mathf.Lerp(minPositionX, maxPositionX, Mathf.PingPong(t, 1));
        pointer.anchoredPosition = new Vector2(newXPosition, pointer.anchoredPosition.y);
    }
}
