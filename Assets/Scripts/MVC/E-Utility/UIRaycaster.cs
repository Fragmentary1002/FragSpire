//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEditor.UI;

//public class UIRaycaster : MonoBehaviour
//{
//    // Update is called once per frame
//    void Update()
//    {
//        // 检查鼠标左键是否按下
//        if (Input.GetMouseButtonDown(0))
//        {
//            // 创建一个射线从摄像机发射
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;

//            // 如果射线击中了UI元素
//            if (Physics.Raycast(ray, out hit))
//            {
//                // 检查击中的对象是否是UI元素
//                if (hit.collider != null && hit.collider.GetComponent<RectTransform>() != null)
//                {
//                    // 创建PointerEventData
//                    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
//                    pointerEventData.position = Input.mousePosition;

//                    // 创建RaycastResults列表以接收射线检测结果
//                    var raycastResults = new List<RaycastResult>();

//                    // 进行射线检测
//                    EventSystem.current.RaycastAll(pointerEventData, raycastResults);

//                    // 遍历射线检测结果
//                    foreach (var result in raycastResults)
//                    {
//                        // 检查是否有可点击的UI元素
//                        if (result.gameObject.GetComponent<Button>() != null)
//                        {
//                            // 在这里处理UI元素的点击
//                            Debug.Log("Clicked on UI element: " + result.gameObject.name);
//                            break; // 退出循环，只处理最上层的UI元素
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
