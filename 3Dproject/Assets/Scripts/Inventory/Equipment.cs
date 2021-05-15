using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace InventoryNS
{
    [CreateAssetMenu(fileName = "Equipment", menuName = "Item/Equipment")]
    public class Equipment : StatItemBase
    {
        [SerializeField] private EquipmentType _type;

        public EquipmentType Type => _type;

        private void Awake()
        {
            EditorUtility.SetDirty(this);
        }
    }
}
