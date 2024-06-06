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
        /// 局外
        /// </summary>
        /// 

        public string playerName;           // 玩家名称

        public int gold;                    // 金币数量

        public List<Potion> potions = new List<Potion>();       // 玩家持有的药水列表

        public int curFloor;            // 当前所在楼层

        /// <summary>
        /// 卡组
        /// </summary>
        public List<BaseCard> cards = new List<BaseCard>();         // 玩家持有的卡牌列表

        public List<BaseRelic> relics = new List<BaseRelic>();        // 玩家持有的遗物列表

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


