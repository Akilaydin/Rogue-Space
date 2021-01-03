using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayAuthentification : MonoBehaviour
{

    public static PlayGamesPlatform gpPlatformInstance;
   
    void Awake(){
        if (gpPlatformInstance == null){
            PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
            PlayGamesPlatform.InitializeInstance(configuration);
            PlayGamesPlatform.DebugLogEnabled = true;

            gpPlatformInstance = PlayGamesPlatform.Activate();

        }

        Social.Active.localUser.Authenticate(success => 
        {
            if (success){
                Debug.Log("Logged in successfully");

            } else {
                Debug.Log("Failed logging in");
            }
        });
    }


}
