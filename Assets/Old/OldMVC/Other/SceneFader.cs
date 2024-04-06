using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace TJ
{
    /// <summary>
    /// 控制场景淡入淡出效果的脚本
    /// </summary>
    public class SceneFader : MonoBehaviour
    {
        public Image img;           // 用于淡入淡出效果的图片对象
        public AnimationCurve curve;    // 控制淡入淡出曲线

        // 在脚本被加载时执行
        private void Awake()
        {
            img.enabled = true; // 启用图片对象
        }

        // 在脚本启用时执行
        void Start()
        {
            StartCoroutine(FadeIn());   // 开始淡入效果
        }

        // 淡出到指定场景
        public void FadeTo(string scene)
        {
            StartCoroutine(FadeOut(scene));    // 开始淡出效果到指定场景
        }

        // 执行淡入效果的协程
        IEnumerator FadeIn()
        {
            float t = 1f;

            while (t > 0f)
            {
                t -= Time.deltaTime;    // 减少淡入时间
                float a = curve.Evaluate(t);    // 根据曲线计算透明度
                img.color = new Color(0f, 0f, 0f, a);  // 更新图片的透明度
                yield return 0;
            }
        }

        // 执行淡出效果到指定场景的协程
        public IEnumerator FadeOut(string scene)
        {
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime;    // 增加淡出时间
                float a = curve.Evaluate(t);    // 根据曲线计算透明度
                img.color = new Color(0f, 0f, 0f, a);  // 更新图片的透明度
                yield return 0;
            }

            // 加载指定的场景
            //SceneManager.LoadScene(scene);
        }

        // 执行UI淡入淡出效果的协程
        public IEnumerator UI_Fade()
        {
            float t2 = 0f;
            while (t2 < 1f)
            {
                t2 += Time.deltaTime;    // 增加淡入时间
                float a = curve.Evaluate(t2);    // 根据曲线计算透明度
                img.color = new Color(0f, 0f, 0f, a);  // 更新图片的透明度
                yield return 0;
            }

            float t = 1f;
            while (t > 0f)
            {
                t -= Time.deltaTime;    // 减少淡出时间
                float a = curve.Evaluate(t);    // 根据曲线计算透明度
                img.color = new Color(0f, 0f, 0f, a);  // 更新图片的透明度
                yield return 0;
            }
        }
    }
}
