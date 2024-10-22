using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TJ
{
    // 战斗者生命条类
    public class FighterHealthBar : MonoBehaviour
    {
        // 阻挡背景图像
        public Image blockBackground;
        // 阻挡图标
        public Image blockIcon;
        // 阻挡数量文本
        public TMP_Text blockNumberText;
        // 生命值文本
        public TMP_Text healthText;
        // 生命值滑动条
        public Slider healthSlider;

        // 显示阻挡值的方法
        public void DisplayBlock(int blockAmount)
        {
            // 如果阻挡值大于0
            if (blockAmount > 0)
            {
                // 显示阻挡背景、阻挡图标和阻挡数量文本
                blockBackground.enabled = true;
                blockIcon.enabled = true;
                blockNumberText.text = blockAmount.ToString();
                blockNumberText.enabled = true;
            }
            else
            {
                // 否则隐藏阻挡背景、阻挡图标和阻挡数量文本
                blockBackground.enabled = false;
                blockIcon.enabled = false;
                blockNumberText.enabled = false;
            }
        }

        // 显示生命值的方法
        public void DisplayHealth(int healthAmount)
        {
            // 更新生命值文本和生命值滑动条的值
            healthText.text = $"{healthAmount}/{healthSlider.maxValue}";
            healthSlider.value = healthAmount;
        }
    }
}
