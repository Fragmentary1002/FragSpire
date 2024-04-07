using QFramework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Frag
{


    public class PoolBuffCommand : AbstractCommand
    {
        Transform buffParent;
        BuffInfo buff;

        public PoolBuffCommand(Transform buffParent, BuffInfo buff)
        {
            this.buffParent = buffParent;
            this.buff = buff;
        }

        protected override void OnExecute()
        {
            DisplayBuffPool();
        }

        private void DisplayBuffPool()
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



    }




}

