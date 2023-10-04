using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DefrmationType
{
    Weight
}

public class GateAppearaence : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    [SerializeField] Image _topImage;
    [SerializeField] Image _glassImage;

    [SerializeField] Color _colorPositive;
    [SerializeField] Color _colorNegative;


    [SerializeField] GameObject _upLable;
    [SerializeField] GameObject _downLable;

    public void UpdateVisual(DefrmationType defrmationType, int value)
    {
        string prefix = "";
        string formattedValue = FormatValue(value);

        if (value > 0)
        {
            prefix = "+";
            SetColor(_colorPositive);
        }
        else if (value == 0)
        {
            SetColor(Color.grey);
        }
        else
        {
            SetColor(_colorNegative);
        }

        _text.text = prefix + formattedValue;

        _upLable.SetActive(false);
        _downLable.SetActive(false);

        if (defrmationType == DefrmationType.Weight)
        {
            if (value > 0)
            {
                _upLable.SetActive(true);
            }
            else
            {
                _downLable.SetActive(true);
            }
        }
    }

    string FormatValue(int value)
    {
        bool isNegative = value < 0;
        int absValue = Mathf.Abs(value);
        float floatValue = (float)absValue;

        string formattedValue;

        if (absValue >= 1000000000)
        {
            floatValue /= 1000000;
            formattedValue = floatValue.ToString("F1") + "B";
        }
        else if (absValue >= 1000000)
        {
            floatValue /= 1000000;
            formattedValue = floatValue.ToString("F1") + "M";
        }
        else if (absValue >= 100000)
        {
            floatValue /= 100000;
            formattedValue = floatValue.ToString("F1") + "KK";
        }
        else if (absValue >= 1000)
        {
            floatValue /= 1000;
            formattedValue = floatValue.ToString("F1") + "K";
        }
        else
        {
            formattedValue = floatValue.ToString("F1");
        }

        if (isNegative)
        {
            formattedValue = "-" + formattedValue;
        }

        return formattedValue;
    }

    void SetColor(Color color)
    {
        _topImage.color = color;
        _glassImage.color = new Color(color.r, color.g, color.b, 0.5f);
    }
}
