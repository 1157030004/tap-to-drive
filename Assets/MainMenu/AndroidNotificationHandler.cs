using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
public class AndroidNotificationHandler : MonoBehaviour
{
#if UNITY_ANDROID
   const string ChannelId = "notification_channel";

   public void ScheduleNotification(DateTime dateTime)
   {
      AndroidNotificationChannel notificatonChannel = new AndroidNotificationChannel{
         Id = ChannelId,
         Name = "Notification Channel",
         Importance = Importance.High,
         Description = "Notification Channel Description"
      };

      AndroidNotificationCenter.RegisterNotificationChannel(notificatonChannel);

      AndroidNotification notification = new AndroidNotification
      {
         Title = "Energy Rechagred!",
         Text = "Your energy has been recharged!",
         SmallIcon = "default",
         LargeIcon = "default",
         FireTime = dateTime,
      };

      AndroidNotificationCenter.SendNotification(notification, ChannelId);
   }
#endif
}
