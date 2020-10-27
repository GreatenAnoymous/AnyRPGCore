using AnyRPG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AnyRPG {
    public class CharacterPetManager {

        private List<UnitProfile> unitProfiles = new List<UnitProfile>();

        protected Dictionary<UnitProfile, UnitController> activeUnitProfiles = new Dictionary<UnitProfile, UnitController>();

        protected BaseCharacter baseCharacter;

        protected bool eventSubscriptionsInitialized = false;

        public BaseCharacter MyBaseCharacter {
            get => baseCharacter;
            set => baseCharacter = value;
        }

        public List<UnitProfile> MyUnitProfiles { get => unitProfiles; set => unitProfiles = value; }
        public Dictionary<UnitProfile, UnitController> MyActiveUnitProfiles { get => activeUnitProfiles; set => activeUnitProfiles = value; }

        public CharacterPetManager(BaseCharacter baseCharacter) {
            this.baseCharacter = baseCharacter;
        }

        public virtual void AddPet(UnitProfile unitProfile) {
            // need more logic in here about whether this class or spec is allowed to capture this type of pet
            if (unitProfile != null && unitProfiles != null && unitProfiles.Contains(unitProfile) == false && unitProfile.IsPet == true) {
                unitProfiles.Add(unitProfile);
            }
        }

        public virtual void AddPet(string unitProfileName) {
            UnitProfile unitProfile = SystemUnitProfileManager.MyInstance.GetResource(unitProfileName);
            if (unitProfile != null) {
                AddPet(unitProfile);
            } else {
                Debug.Log("CharacterPetManager.AddPet: COULD NOT FIND unitProfile: " + unitProfileName + " WHILE LOADING");
            }
        }

        public virtual void DespawnPet(UnitProfile unitProfile) {
            if (activeUnitProfiles.ContainsKey(unitProfile)) {
                UnityEngine.Object.Destroy(activeUnitProfiles[unitProfile]);
            }
            activeUnitProfiles.Remove(unitProfile);
        }

        public virtual void SpawnPet(UnitProfile unitProfile) {
            if (activeUnitProfiles.ContainsKey(unitProfile)) {
                // can't add the same dictionary key twice
                return;
            }
            UnitController unitController = unitProfile.SpawnUnitPrefab(baseCharacter.UnitController.transform.parent, baseCharacter.UnitController.transform.position, baseCharacter.UnitController.transform.forward);
            if (unitController != null) {
                unitController.SetPetMode(baseCharacter);
            }
            activeUnitProfiles.Add(unitProfile, unitController);
        }


    }

}