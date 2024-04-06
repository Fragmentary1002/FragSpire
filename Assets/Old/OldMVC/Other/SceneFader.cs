using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace TJ
{
    /// <summary>
    /// ���Ƴ������뵭��Ч���Ľű�
    /// </summary>
    public class SceneFader : MonoBehaviour
    {
        public Image img;           // ���ڵ��뵭��Ч����ͼƬ����
        public AnimationCurve curve;    // ���Ƶ��뵭������

        // �ڽű�������ʱִ��
        private void Awake()
        {
            img.enabled = true; // ����ͼƬ����
        }

        // �ڽű�����ʱִ��
        void Start()
        {
            StartCoroutine(FadeIn());   // ��ʼ����Ч��
        }

        // ������ָ������
        public void FadeTo(string scene)
        {
            StartCoroutine(FadeOut(scene));    // ��ʼ����Ч����ָ������
        }

        // ִ�е���Ч����Э��
        IEnumerator FadeIn()
        {
            float t = 1f;

            while (t > 0f)
            {
                t -= Time.deltaTime;    // ���ٵ���ʱ��
                float a = curve.Evaluate(t);    // �������߼���͸����
                img.color = new Color(0f, 0f, 0f, a);  // ����ͼƬ��͸����
                yield return 0;
            }
        }

        // ִ�е���Ч����ָ��������Э��
        public IEnumerator FadeOut(string scene)
        {
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime;    // ���ӵ���ʱ��
                float a = curve.Evaluate(t);    // �������߼���͸����
                img.color = new Color(0f, 0f, 0f, a);  // ����ͼƬ��͸����
                yield return 0;
            }

            // ����ָ���ĳ���
            //SceneManager.LoadScene(scene);
        }

        // ִ��UI���뵭��Ч����Э��
        public IEnumerator UI_Fade()
        {
            float t2 = 0f;
            while (t2 < 1f)
            {
                t2 += Time.deltaTime;    // ���ӵ���ʱ��
                float a = curve.Evaluate(t2);    // �������߼���͸����
                img.color = new Color(0f, 0f, 0f, a);  // ����ͼƬ��͸����
                yield return 0;
            }

            float t = 1f;
            while (t > 0f)
            {
                t -= Time.deltaTime;    // ���ٵ���ʱ��
                float a = curve.Evaluate(t);    // �������߼���͸����
                img.color = new Color(0f, 0f, 0f, a);  // ����ͼƬ��͸����
                yield return 0;
            }
        }
    }
}
