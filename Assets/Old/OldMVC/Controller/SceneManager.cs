using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//以弃用
namespace TJ
{
    /// <summary>
    /// 场景管理器，负责管理游戏中的场景切换和UI显示
    /// </summary>
    ///
    public class SceneManager : MonoBehaviour
    { // 场景对象
        public GameObject titleScene;                   // 标题场景对象
        public GameObject classSelectionScreen;        // 角色选择界面对象
        public GameObject battleScene;                 // 战斗场景对象
        public GameObject chestScene;                  // 宝箱场景对象
        public GameObject restScene;                   // 休息场景对象
        public GameObject idleScene;                   // 空闲场景对象
        public GameObject mapScene;                    // 地图场景对象

        [Header("UI")]
        public Image splashArt;                        // UI中的展示图
        public GameObject classSelectionObjects;       // 角色选择相关UI对象

        [Header("Character Select")]
        public List<CharacterTj> characters;             // 可选择的角色列表
        public CharacterTj selectedCharacter;            // 当前选择的角色
        public GameObject playerIcon;                  // 角色图标对象

        // 其他管理器和组件
        GameManager gameManager;                        // 游戏管理器
        BattleSceneManager battleSceneManager;          // 战斗场景管理器
        EndScreen endScreen;                            // 结束界面
        SceneFader sceneFader;                          // 场景淡入淡出效果控制器
        public enum Encounter { enemy, boss, restSite }  // 定义遭遇的类型

        private void Awake()
        {
            // 获取其他组件的引用
            gameManager = GetComponent<GameManager>();
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            endScreen = FindObjectOfType<EndScreen>();
            sceneFader = FindObjectOfType<SceneFader>();
        }

        /// <summary>
        /// 开始游戏按钮的点击事件处理
        /// </summary>
        public void PlayButton()
        {
            titleScene.SetActive(false);   // 禁用标题场景
        }

        /// <summary>
        /// 选择角色
        /// </summary>
        /// <param name="i">角色索引</param>
        public void SelectClass(int i)
        {
            selectedCharacter = characters[i];             // 设置选择的角色
            splashArt.sprite = selectedCharacter.splashArt; // 更新展示图
        }

        /// <summary>
        /// 出发按钮的点击事件处理
        /// </summary>
        public void Embark()
        {
            //gameManager.character = selectedCharacter;

            StartCoroutine(LoadScene("Map"));  // 加载地图场景
            gameManager.LoadCharacterStats();   // 加载角色数据
        }

        /// <summary>
        /// 选择场景
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        public void SelectScreen(string sceneName)
        {
            StartCoroutine(LoadScene(sceneName));  // 加载指定的场景
        }

        /// <summary>
        /// 选择战斗类型
        /// </summary>
        /// <param name="e">战斗类型</param>
        public void SelectBattleType(string e)
        {
            StartCoroutine(LoadBattle(e)); // 加载战斗场景
        }

        /// <summary>
        /// 加载战斗场景
        /// </summary>
        /// <param name="e">战斗类型</param>
        public IEnumerator LoadBattle(string e)
        {
            Cursor.lockState = CursorLockMode.Locked;  // 锁定鼠标光标
            StartCoroutine(sceneFader.UI_Fade());      // 开始UI淡入淡出效果
            yield return new WaitForSeconds(1);        // 等待一段时间

            // 禁用其他场景，启用玩家图标
            mapScene.SetActive(false);
            chestScene.SetActive(false);
            restScene.SetActive(false);
            playerIcon.SetActive(true);

            // 根据战斗类型开始对应的战斗
            if (e == "enemy")
                battleSceneManager.StartHallwayFight();
            else if (e == "elite")
                battleSceneManager.StartEliteFight();

            // 淡出UI效果
            yield return new WaitForSeconds(1);
            Cursor.lockState = CursorLockMode.None;    // 解锁鼠标光标
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="sceneToLoad">要加载的场景名称</param>
        public IEnumerator LoadScene(string sceneToLoad)
        {

            //Cursor.lockState=CursorLockMode.Locked;

            StartCoroutine(sceneFader.UI_Fade());      // 开始UI淡入淡出效果

            //fade to black

            yield return new WaitForSeconds(1);        // 等待一段时间
            endScreen.gameObject.SetActive(false);     // 禁用结束界面
            playerIcon.SetActive(true);                // 启用玩家图标

            // 根据场景名称启用相应的场景
            if (sceneToLoad == "Map")
            {
                // 禁用其他场景，启用地图场景
                playerIcon.SetActive(false);
                classSelectionScreen.SetActive(false);
                mapScene.SetActive(true);
                chestScene.SetActive(false);
                restScene.SetActive(false);
            }
            else if (sceneToLoad == "Battle")
            {
                // 禁用其他场景，启用战斗场景
                mapScene.SetActive(false);
                chestScene.SetActive(false);
                restScene.SetActive(false);
            }
            else if (sceneToLoad == "Chest")
            {
                // 禁用其他场景，启用宝箱场景
                restScene.SetActive(false);
                mapScene.SetActive(false);
                chestScene.SetActive(true);
            }
            else if (sceneToLoad == "Rest")
            {
                // 禁用其他场景，启用休息场景
                chestScene.SetActive(false);
                mapScene.SetActive(false);
                restScene.SetActive(true);
            }

            //fade from black
            yield return new WaitForSeconds(1);
            //Cursor.lockState=CursorLockMode.None;
        }
    }
       
}
