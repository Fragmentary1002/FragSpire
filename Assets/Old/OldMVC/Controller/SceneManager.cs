using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//������
namespace TJ
{
    /// <summary>
    /// ���������������������Ϸ�еĳ����л���UI��ʾ
    /// </summary>
    ///
    public class SceneManager : MonoBehaviour
    { // ��������
        public GameObject titleScene;                   // ���ⳡ������
        public GameObject classSelectionScreen;        // ��ɫѡ��������
        public GameObject battleScene;                 // ս����������
        public GameObject chestScene;                  // ���䳡������
        public GameObject restScene;                   // ��Ϣ��������
        public GameObject idleScene;                   // ���г�������
        public GameObject mapScene;                    // ��ͼ��������

        [Header("UI")]
        public Image splashArt;                        // UI�е�չʾͼ
        public GameObject classSelectionObjects;       // ��ɫѡ�����UI����

        [Header("Character Select")]
        public List<CharacterTj> characters;             // ��ѡ��Ľ�ɫ�б�
        public CharacterTj selectedCharacter;            // ��ǰѡ��Ľ�ɫ
        public GameObject playerIcon;                  // ��ɫͼ�����

        // ���������������
        GameManager gameManager;                        // ��Ϸ������
        BattleSceneManager battleSceneManager;          // ս������������
        EndScreen endScreen;                            // ��������
        SceneFader sceneFader;                          // �������뵭��Ч��������
        public enum Encounter { enemy, boss, restSite }  // ��������������

        private void Awake()
        {
            // ��ȡ�������������
            gameManager = GetComponent<GameManager>();
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            endScreen = FindObjectOfType<EndScreen>();
            sceneFader = FindObjectOfType<SceneFader>();
        }

        /// <summary>
        /// ��ʼ��Ϸ��ť�ĵ���¼�����
        /// </summary>
        public void PlayButton()
        {
            titleScene.SetActive(false);   // ���ñ��ⳡ��
        }

        /// <summary>
        /// ѡ���ɫ
        /// </summary>
        /// <param name="i">��ɫ����</param>
        public void SelectClass(int i)
        {
            selectedCharacter = characters[i];             // ����ѡ��Ľ�ɫ
            splashArt.sprite = selectedCharacter.splashArt; // ����չʾͼ
        }

        /// <summary>
        /// ������ť�ĵ���¼�����
        /// </summary>
        public void Embark()
        {
            //gameManager.character = selectedCharacter;

            StartCoroutine(LoadScene("Map"));  // ���ص�ͼ����
            gameManager.LoadCharacterStats();   // ���ؽ�ɫ����
        }

        /// <summary>
        /// ѡ�񳡾�
        /// </summary>
        /// <param name="sceneName">��������</param>
        public void SelectScreen(string sceneName)
        {
            StartCoroutine(LoadScene(sceneName));  // ����ָ���ĳ���
        }

        /// <summary>
        /// ѡ��ս������
        /// </summary>
        /// <param name="e">ս������</param>
        public void SelectBattleType(string e)
        {
            StartCoroutine(LoadBattle(e)); // ����ս������
        }

        /// <summary>
        /// ����ս������
        /// </summary>
        /// <param name="e">ս������</param>
        public IEnumerator LoadBattle(string e)
        {
            Cursor.lockState = CursorLockMode.Locked;  // ���������
            StartCoroutine(sceneFader.UI_Fade());      // ��ʼUI���뵭��Ч��
            yield return new WaitForSeconds(1);        // �ȴ�һ��ʱ��

            // ���������������������ͼ��
            mapScene.SetActive(false);
            chestScene.SetActive(false);
            restScene.SetActive(false);
            playerIcon.SetActive(true);

            // ����ս�����Ϳ�ʼ��Ӧ��ս��
            if (e == "enemy")
                battleSceneManager.StartHallwayFight();
            else if (e == "elite")
                battleSceneManager.StartEliteFight();

            // ����UIЧ��
            yield return new WaitForSeconds(1);
            Cursor.lockState = CursorLockMode.None;    // ���������
        }

        /// <summary>
        /// ���س���
        /// </summary>
        /// <param name="sceneToLoad">Ҫ���صĳ�������</param>
        public IEnumerator LoadScene(string sceneToLoad)
        {

            //Cursor.lockState=CursorLockMode.Locked;

            StartCoroutine(sceneFader.UI_Fade());      // ��ʼUI���뵭��Ч��

            //fade to black

            yield return new WaitForSeconds(1);        // �ȴ�һ��ʱ��
            endScreen.gameObject.SetActive(false);     // ���ý�������
            playerIcon.SetActive(true);                // �������ͼ��

            // ���ݳ�������������Ӧ�ĳ���
            if (sceneToLoad == "Map")
            {
                // �����������������õ�ͼ����
                playerIcon.SetActive(false);
                classSelectionScreen.SetActive(false);
                mapScene.SetActive(true);
                chestScene.SetActive(false);
                restScene.SetActive(false);
            }
            else if (sceneToLoad == "Battle")
            {
                // ������������������ս������
                mapScene.SetActive(false);
                chestScene.SetActive(false);
                restScene.SetActive(false);
            }
            else if (sceneToLoad == "Chest")
            {
                // �����������������ñ��䳡��
                restScene.SetActive(false);
                mapScene.SetActive(false);
                chestScene.SetActive(true);
            }
            else if (sceneToLoad == "Rest")
            {
                // ��������������������Ϣ����
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
