﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
   //GENERAL MALUSES
   public int death;
   public int timeOut;
   public int friendlyKill;
   
   //GENERAL BONUSES
   public int powerUp;
   public int collectible;
   
   //PIPELINE
   public int electrocution;
   public int cableManagement;
   
   //COOKING TIME
   public int notOrdered;
   public int greetChef;

   //TRENCH TIME
   public int trenchTimeFriendlyKill;
   public int antivirusKilled;
   public int professionalSniper;

   
   //TOTAL
   public int totalPoints;
   public string bonusPrize;
   
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

   private void setTotalPoints()
   {
      //TODO: aggiungere conti total points
   }

   private void setBonusPrize()
   {
      //TODO: aggiungere conti bonus prize
   }
   
   
}
