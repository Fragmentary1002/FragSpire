using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Frag
{

    public class Player : Fighter
    {

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

        public Character character;



        protected override void OnInit()
        {
            this.character = GameApp.Instance.character??(new Character());

            hp.max = character.startHealth;
            hp.cur = character.startHealth;
            playerName = "Frag";
            gold = 99;
            cards = character.startingDeck;
            relics.Add(character.startingRelic);
            return;

        }

        /// <summary>
        /// 添加卡牌
        /// </summary>
        /// <param name="newCard">新的卡牌</param>
        /// <param name="amount">卡牌数量默认为1</param>
        public void AddCards(BaseCard newCard, int amount = 1)
        {
            if (newCard == null)
            {
                return;
            }

            for (int i = 0; i < amount; i++)
            {
                this.cards.Add(newCard);
            }

        }

        /// <summary>
        /// 添加遗物
        /// </summary>
        /// <param name="newRelic"></param>
        public void AddRelice(BaseRelic newRelic)
        {

            if (newRelic == null)
            {
                return;
            }
            this.relics.Add(newRelic);
        }

        public void UpdateFloor()
        {
            this.curFloor++;

            return;
        }

    }
}
