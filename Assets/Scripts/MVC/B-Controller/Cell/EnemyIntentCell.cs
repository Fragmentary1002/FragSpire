using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Frag
{
    public class EnemyIntentCell : MonoBehaviour
    {
        [Header("UI")]
        public Image intentIcon;
        public TMP_Text intentAmount;

        // 显示敌人的意图  
        public void DisplayIntent(BaseIntent e)
        {
            if (e == null) return;
            intentIcon.sprite = e.icon;
            if (e.intentAttack != 0)
            {
                intentAmount.text = e.intentAttack.ToString();
            }
            else
            {
                intentAmount.text = "";
            }

            // 播放Buff出现的动画  
            //intentUI.animator.Play("IntentSpawn");
        }

    }
}
