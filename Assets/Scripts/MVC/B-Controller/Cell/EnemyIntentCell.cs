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

        // ��ʾ���˵���ͼ  
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

            // ����Buff���ֵĶ���  
            //intentUI.animator.Play("IntentSpawn");
        }

    }
}
