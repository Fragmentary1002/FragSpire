using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework.Extend;

namespace Frag
{
    /// <summary>
    /// ���˹����� ����Ӧ�����ɵĹ����б� �͹���״̬��
    /// </summary>

    public class CrateEnemySystem : MonoSingleton<CrateEnemySystem>
    {
        private List<BaseMonster> enemiesList = new List<BaseMonster>();    // ���ܳ��ֵĵ����б�  
        private List<BaseMonster> eliteEnemyList = new List<BaseMonster>(); // ���ܳ��ֵľ�Ӣ�����б�  
        public List<BaseMonster> enemies;
        public BaseMonster enemyTest;
        public Transform enemyParent;                              //���ɵ��˸�����

        int tier = 1;
        public override void Init()
        {

        }

        /// <summary>
        /// ʵ��������
        /// </summary>
        /// <param name="isEliteFight"></param>
        public void CreateEnemy(bool isEliteFight = false, bool isWeakFight = true)
        {
            if (enemyTest != null && enemyParent != null)
            {
                //print("ʵ��������");

                Instantiate(enemyTest.MonsterClassPrefab, enemyParent);
            }
            else
            {
                // print("û���ҵ�ʵ��������");
            }

        }

        /// <summary>
        /// ��ȡ�������ĳһ�����˵��б�
        /// </summary>
        /// <param name="isEliteFight"></param>
        private List<BaseMonster> GetCreateEnemyList(bool isEliteFight = false)
        {
            return isEliteFight ? eliteEnemyList : enemiesList;

        }


        /// <summary>
        /// ��ȡӦ�����ɵĵ����б�
        /// </summary>
        /// <param name="isEliteFight"></param>
        /// <returns></returns>
        private List<Enemy> GetEnemyOrEnemyList(bool isEliteFight = false, bool isWeakFight = true)
        {

            return null;
        }

        /// <summary>
        /// ���ô������Ա����ɶ�Ӧ�¹�����
        /// </summary>
        /// <param name="tier"></param>
        private void SetTier(int tier)
        {
            this.tier = tier;
        }

        /// <summary>
        /// ��ʾ���˵���ͼ����
        /// </summary>
        public void DisplayIntent()
        {
            Debug.Log("��ʾ���˵���ͼ����");

        }

        /// <summary>
        /// �������ǵ��赲ֵ����ʾ�赲��UI
        /// </summary>
        public void DisplayReset()
        {
            Debug.Log("�������ǵ��赲ֵ����ʾ�赲��UI");

            //// �������еĵ��ˣ��������ǵ��赲ֵ����ʾ�赲��UI��  
            //foreach (Enemy e in enemies)
            //{
            //    if (e.thisEnemy == null)
            //        e.thisEnemy = e.GetComponent<Fighter>();
            //    // ���õ��˵��赲ֵ��  
            //    //reset block
            //    e.thisEnemy.currentBlock = 0;
            //    // ���µ����赲ֵ��UI��ʾ��  
            //    e.thisEnemy.fighterHealthBar.DisplayBlock(0);
            //}
            //// ��������ڵ�ǰ�غϽ���ʱ������Ч��
            //player.EvaluateBuffsAtTurnEnd();
        }
    }

}