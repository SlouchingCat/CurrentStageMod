using BepInEx;
using RoR2;
using UnityEngine;
using System;

namespace CurrentStageMod
{
    //This is an example plugin that can be put in BepInEx/plugins/ExamplePlugin/ExamplePlugin.dll to test out.
    //It's a very simple plugin that adds Bandit to the game, and gives you a tier 3 item whenever you press F2.
    //Lets examine what each line of code is for:

    //This attribute specifies that we have a dependency on R2API, as we're using it to add Bandit to the game.
    //You don't need this if you're not using R2API in your plugin, it's just to tell BepInEx to initialize R2API before this plugin so it's safe to use R2API.
    [BepInDependency("com.bepis.r2api")]

    //This attribute is required, and lists metadata for your plugin.
    //The GUID should be a unique ID for this plugin, which is human readable (as it is used in places like the config). I like to use the java package notation, which is "com.[your name here].[your plugin name here]"
    //The name is the name of the plugin that's displayed on load, and the version number just specifies what version the plugin is.
    [BepInPlugin("https://github.com/SlouchingCat/CurrentStageMod", "CurrentStageMod", "0.1")]

    public class CurrentStageMod : BaseUnityPlugin
    {
      //public event Action<Stage> onServerStageBegin = delegate { };
      float time = 15;
      float timeout = 15; // Wait in between rounds to prevent too many messages triggering.

      int count = 0;

      public void Update()
      {
         time += Time.deltaTime;
         RoR2.Stage.onServerStageBegin += (_) =>
         {           
            if (isTimeoutComplete())
            {
               count++;
               Chat.AddMessage($"Welcome to Stage {count}.");
            }               
         };

         if (Input.GetKeyDown(KeyCode.F3))
         {
            Chat.AddMessage($"The current time is {time}");
         }
      }

      private bool isTimeoutComplete()
      {         
         if (time >= timeout)
         {
            time = 0;
            return true;
         }
         return false;
      }
    }
}