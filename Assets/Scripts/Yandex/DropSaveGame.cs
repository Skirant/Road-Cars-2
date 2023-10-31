using UnityEngine;
using YG;

public class DropSaveGame : MonoBehaviour
{
    private string inputString = "";

    void Update()
    {
        foreach (char c in Input.inputString)
        {
            // append each character to the inputString
            inputString += c;

            // when inputString is long enough, check if it matches "2488"
            if (inputString.Length == 4)
            {
                if (inputString == "2488")
                {
                    YandexGame.ResetSaveProgress();
                }

                print("---Save DALITE---");

                // clear the inputString for the next input
                inputString = "";
            }
        }
    }
}
