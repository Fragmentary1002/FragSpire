using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Frag
{
    public class Fight_BattleInit : FightUnit,ICanSendCommand
    {
        //ָ���ܹ�
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }

    
        public override void Init()
        {
            // ʵ����һ������ĵ���Ԥ����  ����ʹ�ö���أ�
            // EnemyManager.Instance.CreateEnemy();

            // ������ڽ�����Ļ���ͽ�����Ϊ�ǻ״̬������Ҳ��ע�͵��ˣ�
            //if (endScreen != null)
            //    endScreen.gameObject.SetActive(false);


            // ����ҵ��ƶѼ������ƶѣ���ϴ�ƣ�Ȼ���һ���������Ƽ������еĿ����б���  
            //FightCardManager.Instance.Init();
            

            this.SendCommand(new ApplyTimeCommand(ApplyTime.BattleBegin));

            FightFSM.Instance.ChangeType(FightType.Player);

        }
    }
}