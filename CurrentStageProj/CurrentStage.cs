using BepInEx;
using RoR2;
using UnityEngine;

namespace CurrentStage
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("https://github.com/SlouchingCat/CurrentStageMod", "CurrentStageMod", "1.1.2")]

    public class CurrentStage : BaseUnityPlugin
    {
        float time = 2;
        const float TIMEOUT = 2;

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

            Run.onRunStartGlobal += (_) =>
            {
                stageCount = 0;
            };

            if (Input.GetKeyDown(KeyCode.F3))
            {
                AdvanceStage();
            }
        }

        // Wait in between rounds to prevent too many messages triggering.
        private bool isTimeoutComplete()
        {
            if (time >= TIMEOUT)
            {
                time = 0;
                return true;
            }
            return false;
        }

        private bool isStage()
        {
            return SceneInfo.instance.sceneDef.sceneType == SceneType.Stage;
        }

        private void AdvanceStage()
        {
            Stage.instance.BeginAdvanceStage(Run.instance.nextStageScene);
            time += 2;
        }
    }
}