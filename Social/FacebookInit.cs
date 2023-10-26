using UnityEngine;
using System.Collections;
using Facebook.MiniJSON;
using Facebook.Unity;

public class FacebookInit : MonoBehaviour
{
    public string fbID;
    public string fbSrecet;

    void Awake( )
    {
        if(!FB.IsInitialized) {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp( );
        }
    }

    void Start( )
    {
        //FB.API("me?fields=name", Facebook.Unity.HttpMethod.GET, UserCallBack);
    }

    //void UserCallBack( FBResult result )
    //{
    //    string get_data = "";
    //    if(result.Error != null) {
    //        get_data = result.Text;
    //    }
    //    else {
    //        get_data = result.Text;
    //    }
    //    var dict = Json.Deserialize(get_data) as IDictionary;
    //    fbID = dict["name"].ToString( );
    //}
     
    void Update( )
    {

    }

    private void InitCallback( )
    {
        if(FB.IsInitialized) {
            // Signal an app activation App Event
            FB.ActivateApp( );
            // Continue with Facebook SDK
            // ...
        }
        else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity( bool isGameShown )
    {
        if(!isGameShown) {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
}
