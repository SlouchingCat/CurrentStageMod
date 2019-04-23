using BepInEx;
using RoR2;
using UnityEngine;

namespace CurrentStage
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("https://github.com/SlouchingCat/CurrentStageMod", "CurrentStageMod", "1.1.1")]

    public class CurrentStage : BaseUnityPlugin
    {
      float time = 15;
      float timeout = 15;

      int stageCount = 0;

      public void Update()
      {
         time += Time.deltaTime;
         RoR2.Stage.onServerStageBegin += (_) =>
         {           
            if (isTimeoutComplete())
            {
               if (isStage())
               {
                  stageCount++;
                  Chat.AddMessage($"Welcome to Stage {stageCount}.");
               }
               else
               {
                  Chat.AddMessage($"Intermission Time! Next Stage: {stageCount + 1}.");
               }
            }       
         };
      }

      // Wait in between rounds to prevent too many messages triggering.
      private bool isTimeoutComplete()
      {         
         if (time >= timeout)
         {
            time = 0;
            return true;
         }
         return false;
      }

      private bool isStage()
      {
         return RoR2.SceneInfo.instance.sceneDef.sceneType == RoR2.SceneType.Stage;
      }
   }    
}