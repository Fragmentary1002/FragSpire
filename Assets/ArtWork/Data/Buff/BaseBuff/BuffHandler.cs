using System.Collections;
using System.Collections.Generic;
using UnityEditor;


namespace Frag
{
    public class BuffHandler
    {
        public LinkedList<BuffInfo> buffList = new LinkedList<BuffInfo>();

        //�˲����߼�Ϊ����tick��Ӧ�Ĵ���

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
        //            //������ֱ����foreach�����Ļ�ɾ������ᱨ��
        //            //�����ʹ���˵�����
        //            //��������Ӹ�cache���� ���������ɾ��
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
                //buff ����
                if (findInfo.curStack < findInfo.buffData.maxStack)
                {
                    findInfo.curStack++;

                    switch (findInfo.buffData.buffUpdateTime)
                    {
                        //����ѵ���������ӵĻ���ֱ�ӵ���ʱ��
                        case BuffUpdateTimeEnum.Add:
                            findInfo.durationTimer += findInfo.buffData.duration;
                            break;
                        //������������;�����ʱ��
                        case BuffUpdateTimeEnum.Replace:
                            findInfo.durationTimer = findInfo.buffData.duration;
                            break;
                    }
                    //���buff�ص���
                    // findInfo.buffData.OnCreate.Apply(findInfo);
                    CallBack(findInfo, CallBackPoint.OnCreate);
                }

            }
            else
            {
                info.durationTimer = info.buffData.duration;

                //info.tickTimer=info.buffData.tickTime; 

                buffList.AddLast(info);

                //sort ���� 
                //sort(buffList);
            }
        }

        /// <summary>
        /// ɾ��buff,����buff��������
        /// </summary>
        /// <param name="info"></param>
        public void RemoveBuff(BuffInfo info)
        {
            switch (info.buffData.buffRemoveStackUpdate)
            {
                //ֱ�ӵ���ɾ���ص���
                case BuffRemoveStackUpdateEnum.Clear:
                    //info.buffData.OnRemove.Apply(info);
                    CallBack(info, CallBackPoint.OnRemoved);
                    break;
                // ����buff���� ����ɾ���ص���
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
        /// û��ʵ��
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