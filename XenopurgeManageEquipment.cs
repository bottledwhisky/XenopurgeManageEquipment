using HarmonyLib;
using MelonLoader;
using SpaceCommander;
using SpaceCommander.PartyCustomization;
using SpaceCommander.UI;
using SpaceCommander.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;

[assembly: MelonInfo(typeof(XenopurgeManageEquipment.XenopurgeManageEquipmentMod), "Xenopurge Manage Equipment", "1.0.0", "Felix Hao")]
[assembly: MelonGame("Traptics", "Xenopurge")]

namespace XenopurgeManageEquipment
{
    public class XenopurgeManageEquipmentMod : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Xenopurge Manage Equipment Mod Loaded!");
        }
    }
    [HarmonyPatch(typeof(InspectSoldierDetails_SquadManagementDirectory))]
    public class InspectSoldierDetails_Patches
    {
        private static Dictionary<InspectSoldierDetails_SquadManagementDirectory, Action<UpgradableUnit, SpaceCommander.Enumerations.EquipmentType>> _swapEquipmentEvents =
            [];

        [HarmonyPatch("Initialize")]
        [HarmonyPostfix]
        public static void Initialize_Postfix(
            InspectSoldierDetails_SquadManagementDirectory __instance,
            DirectoryData __result)
        {
            try
            {
                ((List<ButtonData>)__result.ButtonData).Add(new ButtonData
                {
                    MainText = ModLocalization.Get("swap_ranged_button"),
                    Tooltip = ModLocalization.Get("swap_ranged_tooltip"),
                    onSelectCallback = new Action(() => ShowEquipmentDetails(__instance, SpaceCommander.Enumerations.EquipmentType.Ranged)),
                    onClickCallback = new Action(() => OnSwapEquipment(__instance, SpaceCommander.Enumerations.EquipmentType.Ranged)),
                });
                ((List<ButtonData>)__result.ButtonData).Add(new ButtonData
                {
                    MainText = ModLocalization.Get("swap_melee_button"),
                    Tooltip = ModLocalization.Get("swap_melee_tooltip"),
                    onSelectCallback = new Action(() => ShowEquipmentDetails(__instance, SpaceCommander.Enumerations.EquipmentType.Melee)),
                    onClickCallback = new Action(() => OnSwapEquipment(__instance, SpaceCommander.Enumerations.EquipmentType.Melee)),
                });
                ((List<ButtonData>)__result.ButtonData).Add(new ButtonData
                {
                    MainText = ModLocalization.Get("swap_gear_button"),
                    Tooltip = ModLocalization.Get("swap_gear_tooltip"),
                    onSelectCallback = new Action(() => ShowEquipmentDetails(__instance, SpaceCommander.Enumerations.EquipmentType.Gear)),
                    onClickCallback = new Action(() => OnSwapEquipment(__instance, SpaceCommander.Enumerations.EquipmentType.Gear)),
                });

                MelonLogger.Msg("Added 'Swap Ranged Weapon' button");
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error in Initialize patch: {ex}");
            }
        }

        private static void ShowEquipmentDetails(
            InspectSoldierDetails_SquadManagementDirectory instance,
            SpaceCommander.Enumerations.EquipmentType equipmentType
        )
        {
            var unitDataField = AccessTools.Field(
                typeof(InspectSoldierDetails_SquadManagementDirectory),
                "_unitData"
            );
            var unitData = (UpgradableUnit)unitDataField.GetValue(instance);
            var tmpUnitData = unitData.GetCopyOfUnitData();
            switch (equipmentType)
            {
                case SpaceCommander.Enumerations.EquipmentType.Melee:
                    {
                        tmpUnitData.UnitEquipmentManager.RangedWeaponDataSO = null;
                        tmpUnitData.UnitEquipmentManager.GearDataSO = null;
                        break;
                    }
                case SpaceCommander.Enumerations.EquipmentType.Ranged:
                    {
                        tmpUnitData.UnitEquipmentManager.MeleeWeaponDataSO = null;
                        tmpUnitData.UnitEquipmentManager.GearDataSO = null;
                        break;
                    }
                case SpaceCommander.Enumerations.EquipmentType.Gear:
                    {
                        tmpUnitData.UnitEquipmentManager.RangedWeaponDataSO = null;
                        tmpUnitData.UnitEquipmentManager.MeleeWeaponDataSO = null;
                        break;
                    }
            }
            unitDataField.SetValue(instance, new UpgradableUnit(tmpUnitData));
            AccessTools.Method(typeof(InspectSoldierDetails_SquadManagementDirectory), "ShowWeaponDetails").Invoke(instance, null);
            unitDataField.SetValue(instance, unitData);
        }

        private static void OnSwapEquipment(
            InspectSoldierDetails_SquadManagementDirectory instance,
            SpaceCommander.Enumerations.EquipmentType equipmentType
        )
        {
            try
            {
                var unitDataField = AccessTools.Field(
                    typeof(InspectSoldierDetails_SquadManagementDirectory),
                    "_unitData"
                );
                var unitData = (UpgradableUnit)unitDataField.GetValue(instance);

                if (_swapEquipmentEvents.ContainsKey(instance) && _swapEquipmentEvents[instance] != null)
                {
                    _swapEquipmentEvents[instance].Invoke(unitData, equipmentType);
                }

                InspectSquadList_Patches._directoriesFlowController.GoBack();

                MelonLogger.Msg($"Swap initiated for {unitData.UnitName}");
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error in OnSwapRangedWeapon: {ex}");
            }
        }

        public static void RegisterSwapEquipmentHandler(
            InspectSoldierDetails_SquadManagementDirectory instance,
            Action<UpgradableUnit, SpaceCommander.Enumerations.EquipmentType> handler)
        {
            if (!_swapEquipmentEvents.ContainsKey(instance))
            {
                _swapEquipmentEvents[instance] = null;
            }

            _swapEquipmentEvents[instance] -= handler;
            _swapEquipmentEvents[instance] += handler;
        }
    }

    [HarmonyPatch(typeof(InspectSquadList_SquadManagementDirectory))]
    public class InspectSquadList_Patches
    {
        private static UpgradableUnit _unitPendingWeaponSwap;
        private static SpaceCommander.Enumerations.EquipmentType _equipmentTypeToSwap;
        public static DirectoriesFlowController _directoriesFlowController;

        [HarmonyPatch("SetReferencesToSquadList")]
        [HarmonyPostfix]
        public static void SetReferencesToSquadList_Postfix(
            InspectSquadList_SquadManagementDirectory __instance,
            SquadData squadData,
            DirectoriesFlowController directoriesFlowController
        )
        {
            try
            {
                MelonLogger.Msg($"InspectSquadList_SquadManagementDirectory SetReferencesToSquadList Postfix");
                var inspectSoldierField = AccessTools.Field(
                    typeof(InspectSquadList_SquadManagementDirectory),
                    "_inspectSoldier_SquadManagementDirectory"
                );

                var inspectSoldier = (InspectSoldierDetails_SquadManagementDirectory)inspectSoldierField.GetValue(__instance);

                if (inspectSoldier != null)
                {
                    InspectSoldierDetails_Patches.RegisterSwapEquipmentHandler(
                        inspectSoldier,
                        (unit, equipType) => RegisterSwapEquipment(__instance, unit, equipType)
                    );
                }

                _directoriesFlowController = directoriesFlowController;
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error in SetReferencesToSquadList patch: {ex}");
            }
        }

        [HarmonyPatch("SoldierClicked")]
        [HarmonyPrefix]
        public static bool SoldierClicked_Prefix(
            InspectSquadList_SquadManagementDirectory __instance,
            UpgradableUnit soldier)
        {
            try
            {
                MelonLogger.Msg($"SoldierClicked");
                if (_unitPendingWeaponSwap != null)
                {
                    if (_unitPendingWeaponSwap != soldier)
                    {
                        PerformWeaponSwap(_unitPendingWeaponSwap, soldier, _equipmentTypeToSwap);
                    }

                    _unitPendingWeaponSwap = null;

                    var directoryTextDataField = AccessTools.Field(
                        typeof(InspectSquadList_SquadManagementDirectory),
                        "_directoryTextData"
                    );
                    var directoryTextData = directoryTextDataField.GetValue(__instance);
                    AccessTools.Method(directoryTextData.GetType(), "Reset").Invoke(directoryTextData, null);

                    MelonLogger.Msg($"SoldierClicked patch skip");
                    _directoriesFlowController.ShowDirectory();
                    return false;
                }
                MelonLogger.Msg($"SoldierClicked non-patch pass through");
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error in SoldierClicked patch: {ex}");
            }

            return true;
        }

        [HarmonyPatch("OnBackClicked")]
        [HarmonyPrefix]
        public static void OnBackClicked_Prefix()
        {
            _unitPendingWeaponSwap = null;
            _equipmentTypeToSwap = default(SpaceCommander.Enumerations.EquipmentType);
        }

        private static void RegisterSwapEquipment(
            InspectSquadList_SquadManagementDirectory instance,
            UpgradableUnit sourceUnit,
            SpaceCommander.Enumerations.EquipmentType equipmentType)
        {
            try
            {
                _unitPendingWeaponSwap = sourceUnit;
                _equipmentTypeToSwap = equipmentType;

                MelonLogger.Msg($"Swap mode activated for {sourceUnit.UnitName}");
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error in RegisterSwapEquipment: {ex}");
            }
        }

        private static void PerformWeaponSwap(
            UpgradableUnit unit1,
            UpgradableUnit unit2,
            SpaceCommander.Enumerations.EquipmentType equipmentType)
        {
            try
            {
                EquipmentDataSO equipment1 = unit1.GetCurrentEquipmentOfUnit(equipmentType);
                EquipmentDataSO equipment2 = unit2.GetCurrentEquipmentOfUnit(equipmentType);

                unit1.ReplaceEquipment(equipment2);
                unit2.ReplaceEquipment(equipment1);

                MelonLogger.Msg($"Swapped {equipmentType} between {unit1.UnitName} and {unit2.UnitName}");
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error in PerformWeaponSwap: {ex}");
            }
        }

        [HarmonyPatch(typeof(InspectSquadList_SquadManagementDirectory), "SoldierSelected")]
        [HarmonyPrefix]
        public static bool SoldierSelected_Prefix(
            InspectSquadList_SquadManagementDirectory __instance,
            UpgradableUnit soldier)
        {
            try
            {
                if (_unitPendingWeaponSwap != null)
                {
                    // We're in swap mode - handle custom UI
                    var directoryTextDataField = AccessTools.Field(
                        typeof(InspectSquadList_SquadManagementDirectory),
                        "_directoryTextData"
                    );
                    var directoryTextData = directoryTextDataField.GetValue(__instance);
                    AccessTools.Method(directoryTextData.GetType(), "SetText", new Type[] { typeof(string) })
                        .Invoke(directoryTextData, new object[] { soldier.UnitName });

                    // Get the details area
                    var detailsAreaField = AccessTools.Field(
                        typeof(InspectSquadList_SquadManagementDirectory),
                        "_detailsArea_LogicModifications"
                    );
                    var detailsArea = detailsAreaField.GetValue(__instance);

                    // Clear existing info
                    var infoPerLogicListField = AccessTools.Field(
                        detailsArea.GetType(),
                        "_infoPerLogicList"
                    );
                    var infoPerLogicList = (System.Collections.IList)infoPerLogicListField.GetValue(detailsArea);

                    foreach (var item in infoPerLogicList)
                    {
                        UnityEngine.Object.Destroy(((MonoBehaviour)item).gameObject);
                    }
                    infoPerLogicList.Clear();

                    // Create new info display
                    var prefabField = AccessTools.Field(detailsArea.GetType(), "_infoPerLogicPrefab");
                    var containerField = AccessTools.Field(detailsArea.GetType(), "_infoPerLogicContainer");

                    var prefab = prefabField.GetValue(detailsArea);
                    var container = (Transform)containerField.GetValue(detailsArea);

                    var newInfo = UnityEngine.Object.Instantiate(
                        prefab as MonoBehaviour,
                        container
                    );

                    string displayText = ModLocalization.Get("swap_target_info",
                        _unitPendingWeaponSwap.UnitName,
                        soldier.UnitName,
                        ModLocalization.Get(_equipmentTypeToSwap.ToString()));

                    AccessTools.Method(newInfo.GetType(), "SetInfo", new Type[] { typeof(string), typeof(DetailsArea_LogicModifications_InfoPerLogic.ModificationType) })
                        .Invoke(newInfo, new object[] { displayText, DetailsArea_LogicModifications_InfoPerLogic.ModificationType.None });

                    newInfo.gameObject.SetActive(true);
                    infoPerLogicList.Add(newInfo);

                    // Show the details area
                    ((MonoBehaviour)detailsArea).gameObject.SetActive(true);

                    return false; // Skip original method
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error in SoldierSelected patch: {ex}");
            }

            return true; // Execute original method for normal selection
        }
    }
}
