using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;


namespace TJ
{
    /// <summary>
    /// ������Ϸ״̬�����ݵ���
    /// </summary>
    /// 
    public class GameManager : MonoBehaviour
    {
        public CharacterTj character;    // ��Ϸ�еĽ�ɫ
        public List<CardTj> playerDeck = new List<CardTj>();   // ��ҵĿ�����
        public List<CardTj> cardLibrary = new List<CardTj>();  // ���ƿ�
        public List<RelicTj> relics = new List<RelicTj>();     // ���ӵ�е�����
        public List<RelicTj> relicLibrary = new List<RelicTj>();   // �����
        public int floorNumber = 1; // ��ǰ¥��
        public int goldAmount;  // �������
        PlayerStatsUI playerStatsUI;    // ���״̬UI����

        private void Awake()
        {
            playerStatsUI = FindObjectOfType<PlayerStatsUI>();   // ���Ҳ���ȡ���״̬UI����
        }

        // ���ؽ�ɫ��ʼ״̬
        public void LoadCharacterStats()
        {
            relics.Add(character.startingRelic);    // ��ӽ�ɫ��ʼ����
            playerStatsUI.playerStatsUIObject.SetActive(true);  // �������״̬UI����
            playerStatsUI.DisplayRelics();  // ��ʾӵ�е�����
        }

        // �������Ƿ�ӵ��ָ�����Ƶ�����
        public bool PlayerHasRelic(string rName)
        {
            foreach (RelicTj r in relics)
            {
                if (r.relicName == rName)
                    return true;
            }
            return false;
        }

        // ���µ�ǰ¥��
        public void UpdateFloorNumber()
        {
            floorNumber += 1;

            switch (floorNumber)
            {
                case 1:
                    playerStatsUI.floorText.text = floorNumber + "st Floor";
                    break;
                case 2:
                    playerStatsUI.floorText.text = floorNumber + "nd Floor";
                    break;
                case 3:
                    playerStatsUI.floorText.text = floorNumber + "rd Floor";
                    break;
                default:
                    playerStatsUI.floorText.text = floorNumber + "th Floor";
                    break;
            }
        }

        // ���½������
        public void UpdateGoldNumber(int newGold)
        {
            goldAmount += newGold;
            playerStatsUI.moneyAmountText.text = goldAmount.ToString();
        }

        // ��ʾ��ǰ����ֵ
        public void DisplayHealth(int healthAmount, int maxHealth)
        {
            playerStatsUI.healthDisplayText.text = $"{healthAmount} / {maxHealth}";
        }
    }

}

