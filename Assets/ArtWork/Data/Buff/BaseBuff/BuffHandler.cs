using System.Collections;
using System.Collections.Generic;
using UnityEditor;


namespace Frag
{
    public class BuffHandler
    {
        public LinkedList<BuffInfo> buffList = new LinkedList<BuffInfo>();

        //此部分逻辑为对于tick反应的处理

        //private void Update()
        //{
        //    BuffTickAndRemove();
        //}


        //private void BuffLife()
        //{
        //    foreach (BuffInfo info in buffList)
        //    {

        //    }
        //}
        //private void BuffTickAndRemove()
        //{
        //    IEnumerator enumerator = buffList.GetEnumerator();

        //    while (enumerator.MoveNext())
        //    {
        //        BuffInfo info=(BuffInfo)enumerator.Current;

        //        if (info.buffData.OnTick != null)
        //        {
        //            if (info.tickTimer < 0)
        //            {
        //                //info.buffData.OnTick.Apply(info);
        //                //CallBack(info, CallBackPoint.OnRemoved);

        //                info.tickTimer = info.buffData.tickTime;
        //            }
        //            else
        //            {
        //                info.tickTimer-=Time.deltaTime;
        //            }
        //        }


        //        if (info.durationTimer < 0)
        //        {
        //            //这边如果直接用foreach遍历的话删除链表会报错
        //            //我这边使用了迭代器
        //            //还可以添加个cache链表 遍历后进行删除
        //            RemoveBuff(info);
        //        }
        //        else
        //        {
        //            info.durationTimer -= Time.deltaTime;
        //        }
        //    }
        //}


        public void AddBuff(BuffInfo info)
        {
            BuffInfo findInfo = FindBuff(info.buffData.id);

            Tool.Log($"AddBuff + {info.buffData.name}");

            if (findInfo != null)
            {
                //buff 存在
                if (findInfo.curStack < findInfo.buffData.maxStack)
                {
                    findInfo.curStack++;

                    switch (findInfo.buffData.buffUpdateTime)
                    {
                        //如果堆叠类型是添加的话就直接叠加时间
                        case BuffUpdateTimeEnum.Add:
                            findInfo.durationTimer += findInfo.buffData.duration;
                            break;
                        //如果是重置类型就重置时间
                        case BuffUpdateTimeEnum.Replace:
                            findInfo.durationTimer = findInfo.buffData.duration;
                            break;
                    }
                    //添加buff回调点
                    // findInfo.buffData.OnCreate.Apply(findInfo);
                    CallBack(findInfo, CallBackPoint.OnCreate);
                }

            }
            else
            {
                info.durationTimer = info.buffData.duration;

                //info.tickTimer=info.buffData.tickTime; 

                buffList.AddLast(info);

                //sort 插排 
                //sort(buffList);
            }
        }

        /// <summary>
        /// 删除buff,减少buff层数方法
        /// </summary>
        /// <param name="info"></param>
        public void RemoveBuff(BuffInfo info)
        {
            switch (info.buffData.buffRemoveStackUpdate)
            {
                //直接调用删除回调点
                case BuffRemoveStackUpdateEnum.Clear:
                    //info.buffData.OnRemove.Apply(info);
                    CallBack(info, CallBackPoint.OnRemoved);
                    break;
                // 减少buff层数 调用删除回调点
                case BuffRemoveStackUpdateEnum.Reduce:
                    info.curStack--;
                    //info.buffData.OnRemove.Apply(info);
                    CallBack(info, CallBackPoint.OnRemoved);
                    if (info.curStack <= 0)
                    {
                        buffList.Remove(info);
                    }
                    else
                    {
                        info.durationTimer = info.buffData.duration;
                    }
                    break;
            }
        }
        private BuffInfo FindBuff(int id)
        {
            foreach (BuffInfo info in buffList)
            {
                if (info.buffData.id == id)
                {
                    return info;
                }

            }
            return default;
        }

        /// <summary>
        /// 没有实现
        /// </summary>
        public void Clear()
        {
            //foreach(BuffInfo info in buffList)
            //{
            //    RemoveBuff(info);
            //}
        }

        private void CallBack(BuffInfo info, CallBackPoint callBackPoint)
        {

            EventCenter.GetInstance().EventTrigger<BuffInfo>($"{callBackPoint}", info);

        }

    }
}