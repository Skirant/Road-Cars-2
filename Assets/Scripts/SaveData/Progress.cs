using UnityEngine;

public class Progress : MonoBehaviour
{
    public int Coins;
    public int Weight;

    public static Progress Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }        
    }
}
