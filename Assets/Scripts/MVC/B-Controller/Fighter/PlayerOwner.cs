using QFramework;
using TJ;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Frag
{

    public class PlayerOwner : MonoBehaviour,IController
    {
        public Player owner;

        public FighterHealthBarCell healthBar;

        public Transform buffParent;


        private void Start()
        {
            owner=this.GetModel<Player>();

            EventCenter.GetInstance().AddEventListener("Battle", OnUpdate);

            EventCenter.GetInstance().AddEventListener<BuffInfo>("Battle", DisplayBuffPool);
        }

        public void OnUpdate()
        {
            //更新防御显示
            healthBar.DisplayBlock(owner.currentBlock);

            //更新血条显示
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

        //指定架构
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }

    }



}


