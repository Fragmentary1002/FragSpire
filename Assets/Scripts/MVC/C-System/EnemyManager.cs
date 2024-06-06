using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人管理器
/// </summary>
namespace Frag
{
    public class EnemyManager : AbstractSystem
    {
        private List<GameObject> enemyGoList;
        private List<Enemy> enmeyList;//储存战斗中的敌人
        protected override void OnInit()
        {

        }

       
        public void LoadRes(Transform tran)
        {


        }

        //移除敌人
        public void DeleteEnemy(Enemy enmey)
        {
            enmeyList.Remove(enmey);

            //后续做是否击杀所有怪物判断
            if (enmeyList.Count == 0)
            {
                FightFSM.Instance.ChangeType(FightType.Win);
            }
        }

        //执行活着的怪物的行为
        public IEnumerator DoAllEnemyAction()
        {
            //行动完成后 更新所有敌人
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