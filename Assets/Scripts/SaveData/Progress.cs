using UnityEngine;

public class Progress : MonoBehaviour
{
    public int Coins;
    public int Weight;
    public int HealthBarrir;
    public int LevelNumber;

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
