using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            GetComponent<AudioSource>().Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
