using System;
using System.Reflection;
using System.IO;
using UnityEditor;
using System.Text;
using YandexSDK.Enum;
using YandexSDK.Helper;
using Debug = UnityEngine.Debug;

namespace YandexSDK.Editor.BuildManager
{
    public partial class ModifyBuildManager
    {
        private static string BUILD_PATCH;
        private static Config config;
        private static string indexFile;

        public static void ModifyIndex(string buildPatch)
        {
            config = ConfigSearcher.GetConfig();
            BUILD_PATCH = buildPatch;
            var filePath = Path.Combine(buildPatch, "index.html");
            indexFile = File.ReadAllText(filePath);

            var type = typeof(ModifyBuildManager);
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var method in methods)
            {
                if (method.Name != nameof(ModifyIndex) && method.GetParameters().Length == 0)
                {
                    var scrCopy = new ModifyBuildManager();
                    method.Invoke(scrCopy, BindingFlags.Static | BindingFlags.Public, null, null, null);
                }
            }

            File.WriteAllText(filePath, indexFile);
            Debug.Log("Modify build complete");
        }

        [MenuItem("Tools/Yandex SDK/Modify index.html build", false)]
        public static void ModifyIndex()
        {
            //string buildPatch = BuildLog.ReadProperty("Build path");

            // if (buildPatch != null)
            // {
            //     ModifyIndex(buildPatch);
            //     Process.Start("explorer.exe", buildPatch.Replace("/", "\\"));
            // }
            // else
            // {
            //     Debug.LogError("Path not found:\n" + buildPatch);
            // }
        }

        static void AddIndexCode(string code, CodeTypeEnum codeType)
        {
            var commentHelper = codeType switch
            {
                CodeTypeEnum.Head => "<!-- Additional head modules -->",
                CodeTypeEnum.Body => "<!-- Additional body modules -->",
                CodeTypeEnum.Init => "<!-- Additional init modules -->",
                CodeTypeEnum.Start => "<!-- Additional start modules -->",
                _ => "<!-- Additional script modules -->"
            };

            var sb = new StringBuilder(indexFile);
            var insertIndex = sb.ToString().IndexOf(commentHelper, StringComparison.Ordinal);
            if (insertIndex >= 0)
            {
                insertIndex += commentHelper.Length;
                sb.Insert(insertIndex, "\n" + code + "\n");
                indexFile = sb.ToString();
            }
        }
    }
}