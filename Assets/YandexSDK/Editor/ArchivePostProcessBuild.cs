using System;
using System.IO.Compression;
using UnityEngine;
using YandexSDK.Helper;

namespace YandexSDK.Editor
{
    public class ArchivePostProcessBuild
    {
        public static void Archiving(string pathToBuiltProject)
        {
            var config = ConfigSearcher.GetConfig();

            if (config.isNeedArchivingBuild)
            {
                var currentDate = DateTime.Now;
                var formattedDate = currentDate.ToString("yyyyMMddHHmmss");
                
                Debug.Log("Create archive with name: " + pathToBuiltProject + formattedDate + ".zip");
                ZipFile.CreateFromDirectory(pathToBuiltProject, pathToBuiltProject + formattedDate + ".zip");
            }
        }
    }
}