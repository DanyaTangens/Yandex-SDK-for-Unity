using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace YandexSDK
{
    [AttributeUsage(AttributeTargets.Method)]
    public class YaInitAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class YaStartAttribute : Attribute { }
    
    public partial class YandexGame
    {
        private static List<Action> methodsInitToCall = new();
        private static List<Action> methodsStartToCall = new();
        
        private static void CallYaInitAttribute()
        {
            var type = typeof(YandexGame);
            var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(YaInitAttribute), true);
                if (attributes.Length > 0)
                {
                    methodsInitToCall.Add(() => method.Invoke(type, null));
                }
            }
            foreach (var action in methodsInitToCall)
            {
                action.Invoke();
            }
        }

        private static void CallYaStartAttribute()
        {
            var type = typeof(YandexGame);
            var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(YaStartAttribute), true);
                if (attributes.Length > 0)
                {
                    methodsStartToCall.Add(() => method.Invoke(type, null));
                }
            }
            foreach (var action in methodsStartToCall)
            {
                action.Invoke();
            }
        }
    }
}