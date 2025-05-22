using Game.Data;
using Game.SceneManagement;
using Game.Utils;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SceneLoader.LoadScene(SceneName.Menu));
    }
}
