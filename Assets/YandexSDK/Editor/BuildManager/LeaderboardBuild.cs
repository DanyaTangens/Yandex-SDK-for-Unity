using System.IO;
using UnityEngine;
using YandexSDK.Enum;

namespace YandexSDK.Editor.BuildManager
{
    public partial class ModifyBuildManager
    {
        public static void Leaderboard()
        {
            Debug.Log("Inject Leaderboard start");
            
            var initFunction = "InitLeaderboards();\nconsole.log('Init Leaderboards');";
            AddIndexCode(initFunction, CodeTypeEnum.Init);

            var donorPatch = Application.dataPath + "/YandexSDK/Leaderboard/Content/leaderboard.js";
            var donorText = File.ReadAllText(donorPatch);

            AddIndexCode(donorText, CodeTypeEnum.Js);
            
            Debug.Log("Inject Leaderboard finish");
        }
    }
}