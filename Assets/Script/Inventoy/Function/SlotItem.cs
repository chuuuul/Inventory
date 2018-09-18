using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// - 슬롯 표시를 위한 슬롯 아이템
/// - 필요시 상속하여 다른 아이템 구현
/// </summary>

public class SlotItem : I_SlotItem, IItemProperty
{
    public InventoryTab Tab { get; set; }
    public int Index { get; set; }

    public int Count { get; set; }
    public int MaxCount { get; set; }

    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }

    public Sprite Icon { get; set; }

    public void SetCount (int count = 1 ,int maxCount = 1)
    {
        Count = count;
        MaxCount = maxCount;
    }

    public void SetProperty(string id, string name, string displayName, string description, string type)
    {
        Id = id;
        Name = name;
        DisplayName = displayName;
        Type = type;
        Description = description;
    }
}
