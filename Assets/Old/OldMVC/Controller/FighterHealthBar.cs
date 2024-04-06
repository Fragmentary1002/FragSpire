using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TJ
{
    // ս������������
    public class FighterHealthBar : MonoBehaviour
    {
        // �赲����ͼ��
        public Image blockBackground;
        // �赲ͼ��
        public Image blockIcon;
        // �赲�����ı�
        public TMP_Text blockNumberText;
        // ����ֵ�ı�
        public TMP_Text healthText;
        // ����ֵ������
        public Slider healthSlider;

        // ��ʾ�赲ֵ�ķ���
        public void DisplayBlock(int blockAmount)
        {
            // ����赲ֵ����0
            if (blockAmount > 0)
            {
                // ��ʾ�赲�������赲ͼ����赲�����ı�
                blockBackground.enabled = true;
                blockIcon.enabled = true;
                blockNumberText.text = blockAmount.ToString();
                blockNumberText.enabled = true;
            }
            else
            {
                // ���������赲�������赲ͼ����赲�����ı�
                blockBackground.enabled = false;
                blockIcon.enabled = false;
                blockNumberText.enabled = false;
            }
        }

        // ��ʾ����ֵ�ķ���
        public void DisplayHealth(int healthAmount)
        {
            // ��������ֵ�ı�������ֵ��������ֵ
            healthText.text = $"{healthAmount}/{healthSlider.maxValue}";
            healthSlider.value = healthAmount;
        }
    }
}
