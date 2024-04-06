using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    /// <summary>
    /// ��ɫ�࣬�̳���ScriptableObject�����ڴ�����ɫ��Դ
    /// </summary>
    //[CreateAssetMenu]
    public class CharacterTj : ScriptableObject
    {
        public CharacterClass characterClass;   // ��ɫְҵ
        public enum CharacterClass { ironChad, silent }   // ��ɫְҵö��
        public GameObject characterPrefab;      // ��ɫԤ�������
        public RelicTj startingRelic;             // ��ɫ��ʼ����
        public Sprite splashArt;                // ��ɫͷ��
        public List<CardTj> startingDeck;         // ��ɫ��ʼ����
    }
}
