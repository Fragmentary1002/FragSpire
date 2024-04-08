using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using XFramework.Extend;
using QFramework;
using static UnityEngine.GraphicsBuffer;

namespace Frag
{
    /// <summary>
    /// 战斗卡牌管理类 用于对于卡组的处理
    /// </summary>
    public class FightCardManager : MonoSingleton<FightCardManager>, IController
    {

        public Transform CardCellParent;

        private BattleInfo battleInfo;

        public Player player;

        //public Card TestCard;
        #region 初始化
        public override void Init()
        {
            battleInfo = this.GetModel<BattleInfo>();


        }


        /// <summary>
        /// 初始化卡组
        /// </summary>
        /// <param name="isFrist">是否是第一次初始化</param>
        public void Init(Character character)
        {
            battleInfo = this.GetModel<BattleInfo>();


            CardCellParent = transform.GetOrAddComponentInChildren<Transform>("Canvas/FightPanel/Cards");
            ////赋值玩家卡组
            if (character != null)
            {
                //   OnInit(startCards, 3);

                battleInfo.deck = character.startingDeck;
                battleInfo.enegry.max = 3;
                battleInfo.enegry.cur = 3;
            }
            battleInfo.discardPile.AddRange(battleInfo.deck);
            ShuffleCards();
        }

        public void InitTrun()
        {
            battleInfo.enegry.cur = battleInfo.enegry.max;
        }
        #endregion

        #region 牌的活动
        /// <summary>
        /// 用于玩家出一张卡片
        /// </summary>
        public bool PlayCardAndIsSuccess(BaseCard card, GameObject obj)
        {

            if (card == null)
            {
                Debug.Log("找不到Card数据");
                return false;
            }


            if (battleInfo.enegry.cur < card.CardCost)
            {
                Debug.Log("你的能量不够");
                return false;
            }

            // if (cardUI.card.cardType != CardTj.CardType.Attack && enemies[0].GetComponent<Fighter>().enrage.buffValue > 0)
            //   enemies[0].GetComponent<Fighter>().AddBuff(Buff.Type.strength, enemies[0].GetComponent<Fighter>().enrage.buffValue);

            try
            {

                Player creator = player;

                Enemy target = obj.transform.GetComponent<EnemyOwner>().owner;

                if (creator != null && target!=null)
                {

                    // 执行卡片的动作。  
                    card.Apply(creator, target);
                }

                battleInfo.enegry.cur -= card.CardCost;

                // 从手牌列表中移除该卡片。
                battleInfo.cardsInHand.Remove(card);

                DiscardCard(card);
            }
            catch
            {
                return false;
            }


            return true;

        }


        /// <summary>
        /// 丢弃所有手牌
        /// </summary>
        public void DisCardHandAll()
        {
            Debug.Log("遍历手中的每一张卡牌，并将其置入弃牌堆  ");
            // 遍历手中的每一张卡牌，并将其置入弃牌堆  
            foreach (BaseCard card in battleInfo.cardsInHand)
            {
                DiscardCard(card);
            }

            battleInfo.cardsInHand = new List<BaseCard>();

            PushPoolAll();
        }


        /// <summary>
        /// 用于将一张卡片放入弃牌堆 
        /// </summary>
        public void DiscardCard(BaseCard card)
        {
            if (card == null) { return; }
            // 将卡片添加到弃牌堆中

            battleInfo.discardPile.Add(card);

        }

        /// <summary>
        /// 抽牌操作 默认为抽5张
        /// </summary>
        public void DrawCards(int amountToDraw)
        {

            // 当需要抽取的卡片数量还未满足，并且手牌数量不超过10张时继续循环

            for (int i = 1; i <= amountToDraw; i++)
            {

                if (battleInfo.cardsInHand.Count >= 10)
                {
                    Debug.Log("手牌已经满了");
                    return;
                }
                if (battleInfo.drawPile.Count == 0)// 如果抽牌堆中没有卡片可供抽取  
                {
                    ShuffleCards();// 则执行洗牌操作，重新填充抽牌堆
                }
                if (battleInfo.drawPile == null)
                {
                    Debug.LogWarning(" drawPile is null");
                    return;
                }
                //Debug.Log($"抽牌操作  第{i}张 抽到了 id={ drawPile[0].cardId}的卡 ");

                battleInfo.cardsInHand.Add(battleInfo.drawPile[0]);// 将抽牌堆中的第一张卡片加入到手牌中  

                LoadPool(battleInfo.drawPile[0]);

                battleInfo.drawPile.Remove(battleInfo.drawPile[0]);// 从抽牌堆中移除已抽取的卡片，为下次抽取做准备  

            }
            if (amountToDraw == 1)
            {
                this.SendCommand(new ApplyTimeCommand(ApplyTime.DrawCard));
            }
        }

        /// <summary>
        /// 洗牌操作
        /// </summary>
        public void ShuffleCards()
        {

            battleInfo.discardPile.Shuffle(); // 洗牌操作，将弃牌堆中的卡片随机排序  

            battleInfo.drawPile = battleInfo.discardPile; // 将洗好的弃牌堆赋值给抽牌堆，准备下一次抽牌  

            battleInfo.discardPile = new List<BaseCard>();  // 重置弃牌堆，为下一次洗牌做准备

            Debug.Log("洗牌");

        }
        #endregion

        #region 牌UI的控制,用Pool

        private void LoadPool(BaseCard card)
        {

            PoolMgr.GetInstance().GetObj("Prefabs/UI/Cell/CardCell", (go) =>
            {
                if (CardCellParent != null)
                {
                    go.transform.SetParent(CardCellParent);

                    go.GetOrAddComponent<CardCell>().LoadCard(card);
                }
            });
        }
        /// <summary>
        /// 全部卡进入池子
        /// </summary>        
        private void PushPoolAll()
        {

            // 获取所有子对象上的CardCell组件  
            CardCell[] cardCells = GetComponentsInChildren<CardCell>();

            // 遍历这些组件并调用它们的PushCardPool方法  
            foreach (CardCell cardCell in cardCells)
            {
                cardCell.PushCardPool();
            }

        }

        #endregion


        //指定架构
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}