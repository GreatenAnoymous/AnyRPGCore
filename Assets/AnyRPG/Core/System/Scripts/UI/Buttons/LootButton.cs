using AnyRPG;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AnyRPG {
    public class LootButton : HighlightButton {

        [Header("Loot")]

        [SerializeField]
        protected Image lootBackGroundImage;

        [SerializeField]
        protected Image icon = null;

        [SerializeField]
        protected TextMeshProUGUI title = null;

        protected LootUI lootWindow = null;

        protected LootDrop lootDrop = null;

        /*
        // game manager references
        protected LootManager lootManager = null;
        */

        public TextMeshProUGUI Title { get => title; }
        public Image Icon { get => icon; }
        public LootDrop LootDrop { get => lootDrop; }

        /*
        public override void Configure(SystemGameManager systemGameManager) {
            //Debug.Log(gameObject.name + ".LootButton.Configure(): " + GetInstanceID());
            base.Configure(systemGameManager);
        }

        public override void SetGameManagerReferences() {
            base.SetGameManagerReferences();
            lootManager = systemGameManager.LootManager;
        }
        */

        /*
        public void SetLootUI(LootUI lootUI) {
            lootWindow = lootUI;
        }
        */

        public void SetLootDrop(LootDrop lootDrop) {
            this.lootDrop = lootDrop;
            Icon.sprite = lootDrop.Icon;
            lootDrop.SetBackgroundImage(lootBackGroundImage);

            string colorString = "white";
            if (lootDrop.ItemQuality != null) {
                colorString = "#" + ColorUtility.ToHtmlStringRGB(lootDrop.ItemQuality.QualityColor);
            }
            string title = string.Format("<color={0}>{1}</color>", colorString, lootDrop.DisplayName);
            // set the title
            Title.text = title;
        }

        public void ClearLootDrop() {
            lootDrop = null;
            icon.sprite = null;
            lootBackGroundImage.sprite = null;
            title.text = "";
        }

        public void TakeLoot() {
            Debug.Log("LootButton.TakeLoot()");
            if (LootDrop == null) {
                return;
            }

            LootDrop.TakeLoot();
        }

        public override void OnPointerEnter(PointerEventData eventData) {
            //Debug.Log("LootButton.OnPointerEnter(): " + GetInstanceID());
            base.OnPointerEnter(eventData);
            if (LootDrop == null) {
                return;
            }

            uIManager.ShowToolTip(transform.position, LootDrop);
        }

        public override void OnPointerExit(PointerEventData eventData) {
            base.OnPointerExit(eventData);
            uIManager.HideToolTip();
        }

        public override void OnPointerClick(PointerEventData eventData) {
            base.OnPointerClick(eventData);
            if (LootDrop == null) {
                return;
            }

            // loot the item
            TakeLoot();
        }

        public override void Accept() {
            base.Accept();
            TakeLoot();
        }

    }

}