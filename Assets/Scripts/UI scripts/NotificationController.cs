using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NotificationController : MonoBehaviour
{
    private int isNotificationOn; //0 stands for off, 1 stands for on;
    private string NOTIFICATIONS = "notifications";
    public int isNotifOn
    {
        get
        {
            return isNotificationOn;
        }
    }
    [SerializeField]
    private Button notifOnButton;
    [SerializeField]
    private Button notifOffButton;

    [SerializeField]
    private Sprite onSprite;
    [SerializeField]
    private Sprite offSprite;

    void Start()
    {
        if (PlayerPrefs.HasKey(NOTIFICATIONS))
        {
            isNotificationOn = PlayerPrefs.GetInt(NOTIFICATIONS, 1);
            
        }
        setImagesToButtons();
    }

    public void SetNotificationsOn()
    {
        isNotificationOn = 1;
        PlayerPrefs.SetInt(NOTIFICATIONS, isNotificationOn);

        //TODO: LOGIC FOR NOTIFICATIONS FROM GOOGLE PLAY GAMES
        
        setImagesToButtons();
    }

    public void SetNotificationsOff()
    {
        isNotificationOn = 0;
        PlayerPrefs.SetInt(NOTIFICATIONS, isNotificationOn);

        //TODO: LOGIC FOR NOTIFICATIONS FROM GOOGLE PLAY GAMES

        setImagesToButtons();
    }

    private void setImagesToButtons()
    {
        if (isNotificationOn == 0)
        {
            notifOffButton.GetComponent<Image>().sprite = onSprite;
            notifOnButton.GetComponent<Image>().sprite = offSprite;
        }
        else
        {
            notifOnButton.GetComponent<Image>().sprite = onSprite;
            notifOffButton.GetComponent<Image>().sprite = offSprite;
        }
    }
}
