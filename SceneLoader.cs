using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadShopScene()
    {
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
    }

    public void UnloadShopScene()
    {
        SceneManager.UnloadSceneAsync("Shop");
    }
}
