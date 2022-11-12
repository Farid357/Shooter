using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Shooter.GameLogic.Settings
{
    public class DropdownFromInspectorData<TType> : SerializedMonoBehaviour, IDropdown<TType>
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private List<(TType Type, DropdownData Self)> _data;

        public event Action<TType> OnSelected;
        
        public IEnumerable<TType> Data => _data.Select(data => data.Type);

        public void Create()
        {
            for (var i = 0; i < _data.Count; i++)
            {
                var data = _data[i];
                _dropdown.options.Add(new TMP_Dropdown.OptionData(data.Self.Name, data.Self.Sprite));
            }

            _dropdown.onValueChanged.AddListener(Select);
        }

        private void Select(int index) => OnSelected?.Invoke(_data[index].Type);

        private void OnDestroy() => _dropdown.onValueChanged.RemoveListener(Select);

    }
}