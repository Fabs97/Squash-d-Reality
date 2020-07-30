using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPuzzle : Challenge
{
    [SerializeField] private Platform[] platforms;

   protected override void Start()
   {
      base.Start();
   }

    private void Update()
    {
        if (IsAllPressed())
            endChallenge(IsAllPressed());
    }

    protected override void setDifficulty()
   {
      base.setDifficulty();
   }

   public override void endChallenge(bool successful)
   {
      base.endChallenge(successful);
   }

    private bool IsAllPressed()
    {
        for(int i= 0; i < platforms.Length; i++)
        {
            if(platforms[i].isPressed == false)
            {
                return false;
            }
        }
        return true;
    }
}
