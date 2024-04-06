using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    /// <summary>
    /// ��ʾ��ҽ�ɫ����
    /// </summary>
    public class Player
    {
        public string playerName;           // �������
        public CharacterTj selectedClass;     // ��ѡ��Ľ�ɫ���
        public int currentHealth;           // ��ǰ����ֵ
        public int maxHealth;               // �������ֵ
        public int gold;                    // �������
        public List<Potion> potions;       // ��ҳ��е�ҩˮ�б�
        public int currentFloor;            // ��ǰ����¥��
        public List<CardTj> cards;           // ��ҳ��еĿ����б�
        public List<RelicTj> relics;         // ��ҳ��е������б�
    }
}
