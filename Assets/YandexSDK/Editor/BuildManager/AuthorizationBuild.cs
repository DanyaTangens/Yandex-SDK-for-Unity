using System.IO;
using UnityEngine;
using YandexSDK.Enum;

namespace YandexSDK.Editor.BuildManager
{
    public partial class ModifyBuildManager
    {
        public static void AuthPlayer()
        {
            Debug.Log("Inject Authorization start");
            var initFunction = "playerData = await InitPlayer();\nconsole.log('Init Player ysdk');";
            AddIndexCode(initFunction, CodeTypeEnum.Init);

            var donorPatch = Application.dataPath + "/YandexSDK/Authorization/Content/authorization.js";
            var donorText = File.ReadAllText(donorPatch);
            donorText = donorText.Replace("___scopes___", config.isScopes.ToString().ToLower());
            donorText = donorText.Replace("___photoSize___", config.GetPlayerPhotoSize());

            AddIndexCode(donorText, CodeTypeEnum.Js);
            Debug.Log("Inject Authorization finish");
        }
    }
}