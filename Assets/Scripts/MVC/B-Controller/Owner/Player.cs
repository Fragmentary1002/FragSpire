using QFramework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Frag
{

    public class Player : Fighter
    {
        //model
        public Character character;

        //public Player owner;

        /// <summary>
        /// ����
        /// </summary>
        /// 

        public string playerName;           // �������

        public int gold;                    // �������

        public List<Potion> potions = new List<Potion>();       // ��ҳ��е�ҩˮ�б�

        public int curFloor;            // ��ǰ����¥��

        /// <summary>
        /// ����
        /// </summary>
        public List<BaseCard> cards = new List<BaseCard>();         // ��ҳ��еĿ����б�

        public List<BaseRelic> relics = new List<BaseRelic>();        // ��ҳ��е������б�

        public BattleInfo battleInfo;

        private void Start()
        {
            hp.max = character.startHealth;
            hp.cur = character.startHealth;
            playerName = "Frag";
            gold = 99;
            cards = character.startingDeck;
            relics.Add(character.startingRelic);

            battleInfo=this.GetModel<BattleInfo>();
            battleInfo.player = this;
        }



    }



}


