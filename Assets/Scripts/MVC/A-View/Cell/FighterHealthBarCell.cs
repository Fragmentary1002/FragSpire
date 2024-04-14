using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using XFramework.Extend;

namespace Frag
{
    // ս������������
    public class FighterHealthBarCell : MonoBehaviour   
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


        private void Start()
        {
            blockBackground = gameObject.FindChildGameObject("BlockBackground").GetComponent<Image>();
            blockIcon = gameObject.FindChildGameObject("BlockIcon").GetComponent<Image>();

        }

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
        public void DisplayHealth(int healthAmount,int maxHealthAmount)
        {
            // ��������ֵ�ı�������ֵ��������ֵ
            //healthText.text = $"{healthAmount}/{healthSlider.maxValue}";
            healthText.text = $"{healthAmount}/{maxHealthAmount}";
            healthSlider.maxValue = maxHealthAmount;
            healthSlider.value = healthAmount;
        }

      
    }
}
