using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
   //
   public string playerName = "default";
   
   //GENERAL MALUSES
   public int death; //OK
   public int timeOut; //OK
   public int friendlyKill; //Ok
   
   //GENERAL BONUSES
   public int powerUp; //OK
   public int collectible; //OK
   
   //PIPELINE
   public int electrocution;
   public int cableManagement; //OK
   
   //COOKING TIME
   public int notOrdered; //OK
   public int greetChef; //Ok

   //TRENCH TIME
   public int trenchTimeFriendlyKill;
   public int antivirusKilled; //OK
   public int professionalSniper;

   
   //TOTAL
   public int totalPoints; //OK
   public string bonusPrize; //OK
   
   private void Start()
   {
      resetValues();
   }

   private void resetValues()
   {
      death = 0;
      timeOut = 0;
      friendlyKill = 0;
      powerUp = 0;
      collectible = 0;
      electrocution = 0;
      cableManagement = 0;
      notOrdered = 0;
      greetChef = 0;
      trenchTimeFriendlyKill = 0;
      antivirusKilled = 0;
      professionalSniper = 0;
      totalPoints = 0;
      bonusPrize = "default";
   }

   public void setTotalPoints()
   {
      Debug.LogError("POWERUP: " +powerUp );
      Debug.LogError("COLLECTIBLE: " + collectible);
      Debug.LogError("CABLE MANAGEMENT: " + cableManagement);
      Debug.LogError("GREET CHEF: " + greetChef);
      Debug.LogError("ANTIVIRUS KILLED: " + antivirusKilled);
      Debug.LogError("PROFESSIONAL SNIPER: " + professionalSniper);
      Debug.LogError("FRIENDLY KILL: " + friendlyKill);
      Debug.LogError("TIMEOUT: " + timeOut);
      Debug.LogError("ELECTROCUTION: " + electrocution);
      Debug.LogError("NOT ORDERED: " + notOrdered);
      totalPoints = powerUp + collectible + cableManagement + greetChef + antivirusKilled + professionalSniper - friendlyKill - timeOut - electrocution -notOrdered -trenchTimeFriendlyKill;
   }

   public void setBonusPrize(string value)
   {
      //TODO: aggiungere conti bonus prize

      bonusPrize = value;
   }
   
}
