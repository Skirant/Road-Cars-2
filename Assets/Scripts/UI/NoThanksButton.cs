using UnityEngine;
using UnityEngine.UI;

public class NoThanksButton : MonoBehaviour
{
    public Button noThanksButton;
    public Button BonussButton;

    [Space]   
    public CarMass CarMassInstance;
    public ResellPrice ResellPriceInstance;
    public PlayerModifier PlayerModifierInstance;
    public ZoneIndicator ZoneIndicator;

    public void ButtonOff()
    {
        noThanksButton.interactable = false;
        BonussButton.interactable = false;
        ZoneIndicator.update = false;
        FindObjectOfType<AudioManager>().Play("Getting—oins");
    }
}
