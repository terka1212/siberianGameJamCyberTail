using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class AllItems : MonoBehaviour
    {
        [SerializeField] public List<Item> items;

        public static AllItems instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
                Storage.Init();
            }
        }

        public static Item GetItemById(int id)
        {
            return instance.items.Find(x => id == x.ItemInfo.id);
        }
    }
}