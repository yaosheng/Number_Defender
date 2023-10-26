using System;
using com.shephertz.app42.paas.sdk.csharp;

public class UnityCallBack : App42CallBack
{
    public void OnSuccess( object response )
    {
        com.shephertz.app42.paas.sdk.csharp.social.Social social = (com.shephertz.app42.paas.sdk.csharp.social.Social)response;
        App42Log.Console("userName is" + social.GetUserName( ));
        App42Log.Console("fb Access Token is" + social.GetFacebookAccessToken( ));
        //fbUserName = social.GetUserName( );
        //fbAccessToken = social.GetFacebookAccessToken( );
    }

    public void OnException( Exception e )
    {
        App42Log.Console("Exception : " + e);
    }
}
