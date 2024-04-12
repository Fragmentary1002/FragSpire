using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Frag
{
    // ��ɫְҵö��
    public enum CharacterClass
    {
        ironChad,
        silent
    }   

    /// <summary>
    /// ��ɫ�࣬�̳���ScriptableObject�����ڴ�����ɫ��Դ
    /// </summary>
    /// 
    [CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character")]
    public class Character : ScriptableObject
    {
        public string characterId;
        public CharacterClass characterClass;   // ��ɫְҵ
        public GameObject characterPrefab;      // ��ɫԤ�������
        public BaseRelic startingRelic;             // ��ɫ��ʼ����
        public Sprite splashArt;                // ��ɫͷ��
        public List<BaseCard> startingDeck;         // ��ɫ��ʼ����
        public int startHealth;                 //��ʼ�������ֵ
    }
}