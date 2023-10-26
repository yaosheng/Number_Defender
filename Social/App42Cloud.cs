using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
//using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp.social;

public class App42Cloud : MonoBehaviour {

    public string API_KEY;
    public string SECRET_KEY;

    public string fbUserName;
    public string fbAccessToken;
    public string fbAppID;
    public string fbAppSecret;

    //public UserService userService;
    public SocialService socialService;
    //public UnityCallBack unitycallBack;

    void Start( )
    {
        App42API.Initialize(API_KEY, SECRET_KEY);
        //userService = App42API.BuildUserService( );
        socialService = App42API.BuildSocialService( );
    }

    public void FacebookLogIn( )
    {
        socialService.LinkUserFacebookAccount(fbUserName, fbAccessToken, fbAppID, fbAppSecret, new UnityCallBack());
    }
}






