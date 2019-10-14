using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Linear
{
    public class Inventory : MonoBehaviour
    {
        #region Variables
        //General:
        public List<Item> inv = new List<Item>();
        public Item selectedItem;
        public bool isShown;
        public Vector2 scrt;
        public Vector2 scrollPos;
        public int money;
        public string sortType = "";

        //References:
        public Transform dropLocation;

        [System.Serializable]
        public struct equipment
        {
            public string name;
            public Transform location;
            public GameObject curItem;
        }

        public equipment[] equipmentSlots;
        #endregion

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            inv.Add(ItemData.CreateItem(0));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (isShown)
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    isShown = false;
                }
                else
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    isShown = true;
                }
            }
        }

        private void OnGUI()
        {
            scrt.x = Screen.width / 16;
            scrt.y = Screen.height / 9;

            GUI.Box(new Rect(0, 0, scrt.x * 8, Screen.height), "");
        }
    }
}
