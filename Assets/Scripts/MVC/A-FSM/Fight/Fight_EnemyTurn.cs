using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Frag
{
    //���˻غ�
    public class Fight_EnemyTurn : FightUnit
    {
        public override void Init()
        {
            base.Init();

            //// ��ʼһ���µ�Э����������˵Ļغϡ�
            //StartCoroutine(HandleEnemyTurn());
          //  MonoMgr.GetInstance().StartCoroutine(this.enemyManager.DoAllEnemyAction());

        }

        public override void OnUpdate()
        {
            base.OnUpdate();
           
        }

        public override void OnDestroy() {

            //������ҷ���
            //PlayerActionManager.Instance.ResetCurrentBlock();
          //  MonoMgr.GetInstance().StartCoroutine(this.enemyManager.DoAllUpdateIntent());
            FightFSM.Instance.ChangeType(FightType.Player);

        }
    }
}