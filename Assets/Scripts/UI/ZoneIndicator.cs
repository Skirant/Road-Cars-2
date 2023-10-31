using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using static Progress;

public class ZoneIndicator : MonoBehaviour
{
    public RectTransform pointer;
    public RectTransform[] zones;

    public TextMeshProUGUI zoneText;

    public float speed = 5f;
    public float minPositionX;
    public float maxPositionX;

    private float startTime;

    public float[] zoneMultipliers;

    public CoinManager coinManager;   

    public bool update = true;

    [SerializeField] Button _buttonAd;
    [SerializeField] Button _buttonNoThanks;

    public CarMass CarMassInstance;
    public ResellPrice ResellPriceInstance;
    public PlayerModifier PlayerModifierInstance;

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
            if (update)
            {
                coinManager.UpdateDisplayedCoins(zoneMultipliers[closestZoneIndex]); // Add this line
            }            
        }

        if (update)
        {
            Pointer();
        }       
    }

    private void Pointer()
    {
        float t = (Time.time - startTime) * speed;
        float newXPosition = Mathf.Lerp(minPositionX, maxPositionX, Mathf.PingPong(t, 1));
        pointer.anchoredPosition = new Vector2(newXPosition, pointer.anchoredPosition.y);
    }

    public void OnButtonClick()
    {
        if (!update) return;

        YandexGame.RewVideoShow(0);

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

        coinManager.MultiplyMoney(zoneMultipliers[closestZoneIndex]);
        _buttonAd.interactable = false;

        // After running the button logic, set update to false
        update = false;

        DataHolder.ProgressInstance.MySave();
    }
}
