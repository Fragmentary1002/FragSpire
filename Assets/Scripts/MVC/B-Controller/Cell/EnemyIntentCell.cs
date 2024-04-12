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
        public BuffCell intentUI;

        // 显示敌人的意图  
        //public void DisplayIntent(EnemyAction enemyAction)
        //{
        //    intentIcon.sprite = enemyAction.icon;
        //    intentAmount.text = enemyAction.amount.ToString();

        //    // 播放Buff出现的动画  
        //    //intentUI.animator.Play("IntentSpawn");
        //}

    }
}
