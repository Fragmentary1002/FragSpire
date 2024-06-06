using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Frag
{
    public class BuffCell : MonoBehaviour
    {
        // ���幫��ͼ��������ã��������ú͸���Buff��ͼ�ꡣ  
        public Image buffImage;
        // ���幫���ı�������ã�������ʾBuff��ֵ��  
        public TMP_Text buffAmountText;
        // ���幫��������������ã����ڿ���Buff��ʾʱ�Ķ���Ч���� 
        public Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        public void LoadBuff(BuffInfo b)
        {
            Tool.Log("load");
            // ����"IntentSpawn"����״̬������ͨ��animator��������Ƶġ�
            animator.Play("IntentSpawn");
            // ����buffImage��sprite����Ϊ�����Buff�����buffIcon���ԣ��Ը�����ʾͼ�ꡣ  
            buffImage.sprite = b.buffData.icon;
            // ����buffAmountText��text����Ϊ�����Buff�����buffValue���Ե��ַ�����ʽ���Ը�����ʾֵ
            buffAmountText.text = b.durationTimer.ToString();
          
        }

        public void PushBuffPool()
        {
            PoolMgr.GetInstance().PushObj("Prefabs/UI/Cell/BuffCell", this.gameObject);
        }

    }
}
