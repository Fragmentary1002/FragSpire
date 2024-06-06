using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˹�����
/// </summary>
namespace Frag
{
    public class EnemyManager : AbstractSystem
    {
        private List<GameObject> enemyGoList;
        private List<Enemy> enmeyList;//����ս���еĵ���
        protected override void OnInit()
        {

        }

       
        public void LoadRes(Transform tran)
        {


        }

        //�Ƴ�����
        public void DeleteEnemy(Enemy enmey)
        {
            enmeyList.Remove(enmey);

            //�������Ƿ��ɱ���й����ж�
            if (enmeyList.Count == 0)
            {
                FightFSM.Instance.ChangeType(FightType.Win);
            }
        }

        //ִ�л��ŵĹ������Ϊ
        public IEnumerator DoAllEnemyAction()
        {
            //�ж���ɺ� �������е���
            for (int i = 0; i < enmeyList.Count; i++)
            {
                enmeyList[i].DoAction();
                yield return null;
            }
        }

        public IEnumerator DoAllUpdateIntent()
        {
            for (int i = 0; i < enmeyList.Count; i++)
            {
                enmeyList[i].UpdateIntent();
                yield return null;
            }
        }
    }

}