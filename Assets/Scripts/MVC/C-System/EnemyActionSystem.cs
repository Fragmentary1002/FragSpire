using System.Collections.Generic;
using UnityEngine;



namespace Frag
{
    public class EnemyActionSystem : MonoSingleton<EnemyActionSystem>
    {
     

        public List<Enemy> enemies = new List<Enemy>();

        public List<EnemyAction> enemyActionList = new List<EnemyAction>();

        //private int trunCnt = 0;

        //        // ��ʾEnemyAction����ͼ���͡�  
        //        public EnemyActionType enemyActionType;

        //        public EnemyIntentCell cell;

        //        private void Start()
        //        {
        //            cell = FindObjectOfType<EnemyIntentCell>();
        //        }

        //        public void ChangeType(EnemyActionType type)
        //        {
        //            EnemyAction neweNemyAction = enemyActionList[trunCnt];
        //            DisplayIntent(neweNemyAction);
        //            Debug.Log($"EnemyActionType ChangeType");
        //            switch (type)
        //            {
        //                case EnemyActionType.Attack:
        //                    // ������ҵ�Э��  
        //                    AttackPlayer(ToolManager.Instance.TestAmount);
        //                    // StartCoroutine(AttackPlayer());
        //                    break;
        //                case EnemyActionType.Block:
        //                    // ִ���赲����  
        //                    PerformBlock(ToolManager.Instance.TestAmount);
        //                    // StartCoroutine(ApplyBuff());
        //                    break;
        //                case EnemyActionType.StrategicBuff:
        //                    // ���Լ�Ӧ������Ч��  
        //                    ApplyBuffToSelf(neweNemyAction.buffType);
        //                    // Ӧ������Ч����Э��  
        //                    //StartCoroutine(ApplyBuff());
        //                    break;
        //                case EnemyActionType.StrategicDebuff:
        //                    // �����Ӧ�ü���Ч��  
        //                    ApplyDebuffToPlayer(neweNemyAction.buffType);
        //                    // Ӧ������Ч����Э��
        //                    // StartCoroutine(ApplyBuff());
        //                    break;
        //                case EnemyActionType.AttackDebuff:
        //                    // �����Ӧ�ü���Ч��  
        //                    ApplyDebuffToPlayer(neweNemyAction.buffType);

        //                    //StartCoroutine(AttackPlayer());
        //                    // StartCoroutine(ApplyBuff());
        //                    break;
        //                default:
        //                    Debug.Log("EnemyActionType û���ҵ�״̬");
        //                    break;
        //            }
        //            WrapUpTurn();

        //        }

        //        /// <summary>
        //        /// ��ʾ���˵���ͼ  
        //        /// </summary>
        //        private void DisplayIntent(EnemyAction newEnemyAction)
        //        {
        //            if (newEnemyAction != null) { return; }

        //            newEnemyAction.Damage
        //            ();

        //            cell.DisplayIntent(newEnemyAction);

        //        }


        //        /// <summary>
        //        /// ���˻غϵĺ���  
        //        /// </summary>
        //        private void GenerateTurns()
        //        {

        //            // DisplayIntent();

        //            print("���˻غϵĺ���");
        //        }

        //        /// <summary>
        //        /// ������ҵ�Э�̺���  
        //        /// </summary>
        //        /// <returns></returns>
        //        //private IEnumerator AttackPlayer()
        //        //{
        //        //    print("������ҵ�Э�̺���");

        //        //    yield return new WaitForSeconds(0.5f);
        //        //}
        //        private void AttackPlayer(int amount)
        //        {
        //            RoleManager.Instance.DoTakeDamage(amount,RoleType.Play);
        //            print("������ҵ�Э�̺���");

        //        }

        //        /// <summary>
        //        /// ����һ��Э�̷�����������һ��ʱ���Ӧ������Ч��  
        //        /// </summary>
        //        /// <returns></returns>
        //        //private IEnumerator ApplyBuff()
        //        //{
        //        //    print("����һ��Э�̷�����������һ��ʱ���Ӧ������Ч�� ");
        //        //    yield return new WaitForSeconds(0.5f);
        //        //}


        //        // ������Ӧ������Ч��  
        //        private void ApplyBuffToSelf(BuffType t)
        //        {
        //           RoleManager.Instance.DoAddBuff(t, RoleType.Enemy);

        //            print(" ������Ӧ������Ч��  ");
        //        }
        //        // �����ʩ�Ӽ���Ч��  
        //        private void ApplyDebuffToPlayer(BuffType t)
        //        {
        //            RoleManager.Instance.DoAddBuff(t, RoleType.Play);
        //            print(" �����ʩ�Ӽ���Ч��  ");
        //        }

        //        // ִ���赲����  
        //        private void PerformBlock(int amount)
        //        {
        //            RoleManager.Instance.DoAddBlock(amount, RoleType.Enemy);
        //            print(" ִ���赲����  ");
        //        }


        //        /// �����غϵĴ�����  
        //        private void WrapUpTurn()
        //        {
        //            trunCnt++;
        //            print(" �����غϵĴ�����");
        //        }

    }

}