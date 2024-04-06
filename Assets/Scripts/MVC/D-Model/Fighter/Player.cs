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
        /// ��ӿ���
        /// </summary>
        /// <param name="newCard">�µĿ���</param>
        /// <param name="amount">��������Ĭ��Ϊ1</param>
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
        /// �������
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
