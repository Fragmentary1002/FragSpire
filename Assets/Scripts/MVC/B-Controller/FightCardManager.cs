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
    /// ս�����ƹ����� ���ڶ��ڿ���Ĵ���
    /// </summary>
    public class FightCardManager : MonoSingleton<FightCardManager>, IController
    {

        public Transform CardCellParent;

        private BattleInfo battleInfo;

        public Player player;

        //public Card TestCard;
        #region ��ʼ��
        public override void Init()
        {
            battleInfo = this.GetModel<BattleInfo>();


        }


        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="isFrist">�Ƿ��ǵ�һ�γ�ʼ��</param>
        public void Init(Character character)
        {
            battleInfo = this.GetModel<BattleInfo>();


            CardCellParent = transform.GetOrAddComponentInChildren<Transform>("Canvas/FightPanel/Cards");
            ////��ֵ��ҿ���
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

        #region �ƵĻ
        /// <summary>
        /// ������ҳ�һ�ſ�Ƭ
        /// </summary>
        public bool PlayCardAndIsSuccess(BaseCard card, GameObject obj)
        {

            if (card == null)
            {
                Debug.Log("�Ҳ���Card����");
                return false;
            }


            if (battleInfo.enegry.cur < card.CardCost)
            {
                Debug.Log("�����������");
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

                    // ִ�п�Ƭ�Ķ�����  
                    card.Apply(creator, target);
                }

                battleInfo.enegry.cur -= card.CardCost;

                // �������б����Ƴ��ÿ�Ƭ��
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
        /// ������������
        /// </summary>
        public void DisCardHandAll()
        {
            Debug.Log("�������е�ÿһ�ſ��ƣ��������������ƶ�  ");
            // �������е�ÿһ�ſ��ƣ��������������ƶ�  
            foreach (BaseCard card in battleInfo.cardsInHand)
            {
                DiscardCard(card);
            }

            battleInfo.cardsInHand = new List<BaseCard>();

            PushPoolAll();
        }


        /// <summary>
        /// ���ڽ�һ�ſ�Ƭ�������ƶ� 
        /// </summary>
        public void DiscardCard(BaseCard card)
        {
            if (card == null) { return; }
            // ����Ƭ��ӵ����ƶ���

            battleInfo.discardPile.Add(card);

        }

        /// <summary>
        /// ���Ʋ��� Ĭ��Ϊ��5��
        /// </summary>
        public void DrawCards(int amountToDraw)
        {

            // ����Ҫ��ȡ�Ŀ�Ƭ������δ���㣬������������������10��ʱ����ѭ��

            for (int i = 1; i <= amountToDraw; i++)
            {

                if (battleInfo.cardsInHand.Count >= 10)
                {
                    Debug.Log("�����Ѿ�����");
                    return;
                }
                if (battleInfo.drawPile.Count == 0)// ������ƶ���û�п�Ƭ�ɹ���ȡ  
                {
                    ShuffleCards();// ��ִ��ϴ�Ʋ��������������ƶ�
                }
                if (battleInfo.drawPile == null)
                {
                    Debug.LogWarning(" drawPile is null");
                    return;
                }
                //Debug.Log($"���Ʋ���  ��{i}�� �鵽�� id={ drawPile[0].cardId}�Ŀ� ");

                battleInfo.cardsInHand.Add(battleInfo.drawPile[0]);// �����ƶ��еĵ�һ�ſ�Ƭ���뵽������  

                LoadPool(battleInfo.drawPile[0]);

                battleInfo.drawPile.Remove(battleInfo.drawPile[0]);// �ӳ��ƶ����Ƴ��ѳ�ȡ�Ŀ�Ƭ��Ϊ�´γ�ȡ��׼��  

            }
            if (amountToDraw == 1)
            {
                this.SendCommand(new ApplyTimeCommand(ApplyTime.DrawCard));
            }
        }

        /// <summary>
        /// ϴ�Ʋ���
        /// </summary>
        public void ShuffleCards()
        {

            battleInfo.discardPile.Shuffle(); // ϴ�Ʋ����������ƶ��еĿ�Ƭ�������  

            battleInfo.drawPile = battleInfo.discardPile; // ��ϴ�õ����ƶѸ�ֵ�����ƶѣ�׼����һ�γ���  

            battleInfo.discardPile = new List<BaseCard>();  // �������ƶѣ�Ϊ��һ��ϴ����׼��

            Debug.Log("ϴ��");

        }
        #endregion

        #region ��UI�Ŀ���,��Pool

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
        /// ȫ�����������
        /// </summary>        
        private void PushPoolAll()
        {

            // ��ȡ�����Ӷ����ϵ�CardCell���  
            CardCell[] cardCells = GetComponentsInChildren<CardCell>();

            // ������Щ������������ǵ�PushCardPool����  
            foreach (CardCell cardCell in cardCells)
            {
                cardCell.PushCardPool();
            }

        }

        #endregion


        //ָ���ܹ�
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}