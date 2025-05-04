using Game.SceneManagement;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SceneLoader.LoadScene(SceneName.Menu));
    }
}
