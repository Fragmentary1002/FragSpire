using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TJ
{
    public class BuffUI : MonoBehaviour
    {

        // 定义公共图像组件引用，用于设置和更新Buff的图标。  
        public Image buffImage;
        // 定义公共文本组件引用，用于显示Buff的值。  
        public TMP_Text buffAmountText;
        // 定义公共动画器组件引用，用于控制Buff显示时的动画效果。 
        public Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        public void DisplayBuff(Buff b)
        {
            // 播放"IntentSpawn"动画状态。这是通过animator组件来控制的。
            animator.Play("IntentSpawn");
            // 设置buffImage的sprite属性为传入的Buff对象的buffIcon属性，以更新显示图标。  
            buffImage.sprite = b.buffIcon;
            // 设置buffAmountText的text属性为传入的Buff对象的buffValue属性的字符串形式，以更新显示值
            buffAmountText.text = b.buffValue.ToString();
        }
    }
}
