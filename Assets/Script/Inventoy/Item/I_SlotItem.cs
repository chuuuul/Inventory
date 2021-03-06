﻿using System;
using UnityEngine;
using Inventory;

namespace Item
{ 
    //ISlotItem : 해당 아이템을 가지고 있는 인벤토리 테이블과 순서의 정보를 가진 인터페이스
    public interface I_SlotItem
    {
        InventoryTab Tab { get; set; } //아이템을 가진 탭 이름
        int Index { get; set; } //인벤토리 슬롯 순서
    }


    //IItemProperty : 아이템에 관한 기본적인 정보들을 가진 인터페이스
    public interface IItemProperty
    {
        int Count { get; set; }     // 아이템 현재 개수
        int MaxCount { get; set; }  // 아이템 최대 개수

        string Id { get; set; }     // 아이템 아이디
        string Name { get; set; }   // 아이템 이름
        string DisplayName { get; set; } // 화면 표시용 이름
        string Type { get; set; }   // 아이템 타입
        string Description { get; set; } // 아이템 설명

        Sprite Icon { get; set; }   // 표시 아이콘

        void SetCount(int count, int maxCount); // 최대 개수 및 현재 개수 설정 메서드
        void SetProperty(string id, string name, string displayName, string type, string description); // 아이템 속성 설정 함수
    }

    //ITradable : 구매 및 판매에 관한 정보를 가진 인터페이스 (추후에 Shop Helper 기능에 사용된다.)
    public interface ITradable
    {
        int BuyPrice { get; set; }  // 구매가격
        int SellPrice { get; set; } // 판매가격
        void SetPrice(int buyPrice, int sellPrice); // 구매 및 판매 가격 설정 메서드
    }

    //ICommon, IEquipment, IConsumable : 아이템의 사용에 관한 기능과 정보를 가진 인터페이스
    public interface ICommon
    {
        bool Usable { get; set; }
        Action<SlotItem> UseEvent { get; set; }
    }

    public interface IEquipment
    {
        bool Usable { get; set; }
        InventorySlot TargetSlot { get; set; }      
        Action<SlotItem> UseEvent { get; set; }   
    }

    public interface IConsum
    {
        bool Usable { get; set; }
        Action<SlotItem> UseEvent { get; set; }
    }

    public interface IImmediate
    {
        string Id { get; set; }
        string Name { get; set; }
        string DisplayName { get; set; }
        string Description { get; set; }
        string Type { get; set; }

        bool Usable { get; set; }
        Action <int> UseEvent { get; set; }
    }
}