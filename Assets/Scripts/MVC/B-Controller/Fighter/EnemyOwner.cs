using QFramework;
using TJ;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Frag
{

    public class EnemyOwner : MonoBehaviour, IController
    {
       
        public Enemy owner;

        public FighterHealthBarCell healthBar;

        public Transform buffParent;

        private void Start()
        {
            owner = this.GetModel<Enemy>();
        }

        public void OnUpdate()
        {
            //���·�����ʾ
            healthBar.DisplayBlock(owner.currentBlock);

            //����Ѫ����ʾ
            healthBar.DisplayHealth(owner.hp.cur, owner.hp.max);

        }


        public void DisplayBuffPool(BuffInfo buff)
        {
            PoolMgr.GetInstance().GetObj("Prefabs/UI/Cell/BuffCell", (go) =>
            {
                if (buffParent != null)
                {
                    go.transform.SetParent(buffParent);
                    go.GetOrAddComponent<BuffCell>().LoadBuff(buff);
                }
                else
                {
                    Tool.Log("buffParentPlayer is null");
                }
            });
        }


        //ָ���ܹ�
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }



}


