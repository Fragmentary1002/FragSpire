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

        // ��ʾ���˵���ͼ  
        //public void DisplayIntent(EnemyAction enemyAction)
        //{
        //    intentIcon.sprite = enemyAction.icon;
        //    intentAmount.text = enemyAction.amount.ToString();

        //    // ����Buff���ֵĶ���  
        //    //intentUI.animator.Play("IntentSpawn");
        //}

    }
}
