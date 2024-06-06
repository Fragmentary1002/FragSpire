using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace Frag
{
    //��һغ�
    public class Fight_PlayerTurn : FightUnit
    {
       
        public override void Init()
        {

            base.Init();
            Debug.Log("��ʾ���˵���ͼ����");

           
            FightCardManager.Instance.InitTrun();
            Debug.Log("���ƿ�ʼ");
            this.SendCommand(new ApplyTimeCommand(ApplyTime.TurnStart));


            FightCardManager.Instance.DrawCards(5);


        }

        public override void OnUpdate()
        {
       
        }
    
        public override void OnDestroy()
        {

            //EnemyManager.Instance.DisplayReset();
         

            //�غϽ�����������
            FightCardManager.Instance.DisCardHandAll();

            this.SendCommand(new ApplyTimeCommand(ApplyTime.TurnEnd));

            //�л������˻غ�
            FightFSM.Instance.ChangeType(FightType.Enemy);

        }

    }
}