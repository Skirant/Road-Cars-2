using TMPro;
using UnityEngine;

public class ZoneIndicator : MonoBehaviour
{
    public RectTransform pointer;
    public TextMeshProUGUI zoneText;
    public RectTransform[] zones;

    public float speed = 5f;
    public float minPositionX;
    public float maxPositionX;
    private float startTime;

    public CoinManager coinManager;
    public float[] zoneMultipliers;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float minDistance = float.MaxValue;
        int closestZoneIndex = -1;

        for (int i = 0; i < zones.Length; i++)
        {
            float distance = Mathf.Abs(pointer.anchoredPosition.x - zones[i].anchoredPosition.x);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestZoneIndex = i;
            }
        }

        if (closestZoneIndex != -1)
        {
            zoneText.text = zones[closestZoneIndex].name;
        }

        Pointer();
    }

    private void Pointer()
    {
        float t = (Time.time - startTime) * speed;
        float newXPosition = Mathf.Lerp(minPositionX, maxPositionX, Mathf.PingPong(t, 1));
        pointer.anchoredPosition = new Vector2(newXPosition, pointer.anchoredPosition.y);
    }

    public void OnButtonClick()
    {
        float minDistance = float.MaxValue;
        int closestZoneIndex = -1;

        for (int i = 0; i < zones.Length; i++)
        {
            float distance = Mathf.Abs(pointer.anchoredPosition.x - zones[i].anchoredPosition.x);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestZoneIndex = i;
            }
        }

        if (closestZoneIndex != -1)
        {
            coinManager.MultiplyMoney(zoneMultipliers[closestZoneIndex]);
        }


    }
}
