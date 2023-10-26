using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

    public void ReStartScene( )
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("test");
    }
}
