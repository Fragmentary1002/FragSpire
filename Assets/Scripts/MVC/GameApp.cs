using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using XFramework;

namespace Frag
{
    /// <summary>
    /// 游戏管理类 对于其他管理类的管理
    /// </summary>
    public class GameApp : MonoSingleton<GameApp>, IController
    {
        // Start is called before the first frame update

        public Character character;
        public BaseMonster monster;

        //protected override void Awake()
        //{
        //    base.Awake();
        //    OnInit();
        //}

        private void Start()
        {
            OnInit();

        }
        private void Update()
        {
        }

        public void OnInit()
        {
            Debug.Log("GameManager start");

            //管理类
            AudioManager.Instance.Init();

            SceneManager.Instance.Init(new FightScene());

            //逻辑层 卡牌逻辑
            FightCardManager.Instance.Init(character);

            FightFSM.Instance.Init(FightType.BattleInit);


            Debug.Log("GameManager end");
        }


        //指定架构
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}