using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TJ
{
    /// <summary>
    /// ���״̬UI������
    /// </summary>
    public class PlayerStatsUI : MonoBehaviour
    {
        public TMP_Text healthDisplayText;  // ��ʾ����ֵ���ı�����
        public TMP_Text moneyAmountText;    // ��ʾ����������ı�����
        public TMP_Text floorText;          // ��ʾ¥����ı�����
        public Transform relicParent;       // ���︸������
        public GameObject relicPrefab;      // ����Ԥ�������
        public GameObject playerStatsUIObject;  // ���״̬UI����
        GameManager gameManager;           // ��Ϸ����������

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();  // ��ȡ��Ϸ����������
        }

        /// <summary>
        /// ��ʾ���ӵ�е�����
        /// </summary>
        public void DisplayRelics()
        {
            // ������︸�������µ������Ӷ���
            foreach (Transform c in relicParent)
                Destroy(c.gameObject);

            // �������ӵ�е������б���ʵ��������Ԥ������������ʾ
            foreach (RelicTj r in gameManager.relics)
                Instantiate(relicPrefab, relicParent).GetComponent<Image>().sprite = r.relicIcon;
        }
    }
}
