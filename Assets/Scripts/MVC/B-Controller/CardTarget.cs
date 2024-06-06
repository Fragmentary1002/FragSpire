using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Frag
{
    public class CardTarget : MonoBehaviour, ICanGetModel { 
        // ս������������������
        // �з�ս���ߵ�����
        Enemy enemyFighter;
        BattleInfo battleInfo;

        private void Awake()
        {
            // ���Ҳ��洢ս�������������͵з�ս����
            enemyFighter = GetComponent<EnemyOwner>().owner;
            battleInfo = this.GetModel<BattleInfo>();

        }

        // �����ָ����뿨��Ŀ��ʱ�����ķ���
        public void OnPointerEnter()
        {
            // ����з�ս����Ϊ�գ����²���
            if (enemyFighter == null)
            {
                Debug.Log("fighter is null");

                enemyFighter = GetComponent<EnemyOwner>().owner;
            }

            // ��Ŀ������Ϊ�з�ս����
            battleInfo.target = this.enemyFighter;
            Debug.Log("set target");

        }

        // �����ָ���˳�����Ŀ��ʱ�����ķ���
        public void OnPointerExit()
        {
            // ������Ŀ������Ϊ��
            battleInfo.target = null;
            Debug.Log("drop target");
        }

        //ָ���ܹ�
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}
