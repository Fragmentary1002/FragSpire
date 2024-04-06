using QFramework;
using System;
using UnityEngine;


namespace Frag
{
    public enum ApplyTime
    {
        None,
        BattleBegin, //��ʼս��
        BattleEnd,   //ս������
        DoDamage,    //����
        BeDamaged,  //������
        TurnStart, //�غϿ�ʼ
        TurnEnd,   //�غϽ���
        DrawCard,    //����
        PlayHand     //����
    }

    public class ApplyTimeCommand : AbstractCommand
    {
        ApplyTime applyTime;

        public ApplyTimeCommand(ApplyTime applyTime)
        {
            this.applyTime = applyTime;
        }   

        protected override void OnExecute()
        {
            PerformAction();

            //return this;
        }

        private void PerformAction()
        {
            switch (applyTime)
            {
                case ApplyTime.BattleBegin:
                    // ��ս����ʼʱִ�в���
                    Tool.Log("ս���ѿ�ʼ��");
                    break;
                case ApplyTime.BattleEnd:
                    // ��ս������ʱִ�в���
                    Tool.Log("ս���ѽ�����");
                    break;
                case ApplyTime.DoDamage:
                    // �ڹ���ʱִ�в���
                    Tool.Log("ִ�й�����");
                    break;
                case ApplyTime.BeDamaged:
                    // �ڱ�����ʱִ�в���
                    Tool.Log("�ܵ�������");
                    break;
                case ApplyTime.TurnStart:
                    // �ڻغϿ�ʼʱִ�в���
                    Tool.Log("�غϿ�ʼ��");
                    break;
                case ApplyTime.TurnEnd:
                    // �ڻغϽ���ʱִ�в���
                    Tool.Log("�غϽ�����");
                    break;
                case ApplyTime.DrawCard:
                    // �ڳ���ʱִ�в���
                    Tool.Log("���ƣ�");
                    break;
                case ApplyTime.PlayHand:
                    Tool.Log("���ƣ�");
                    break;
                default:
                    // Ĭ�����
                    Tool.Log("δ֪������");
                    break;
            }
            ApplyEvent(applyTime);

        }


        private void ApplyEvent(ApplyTime applyTime)
        {
            EventCenter.GetInstance().EventTrigger(applyTime.ToString());
        }

    }
}
