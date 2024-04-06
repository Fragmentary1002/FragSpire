using QFramework;
using UnityEngine;

namespace Frag
{
    /// <summary>
    /// 保存信息用PlayerPrefs
    /// </summary>
    public class Storage : IUtility
    {
        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
    }
}