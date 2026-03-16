using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
