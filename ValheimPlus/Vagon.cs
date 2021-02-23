﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ValheimPlus.Configurations;

namespace ValheimPlus {
    class VagonModifications {
        [HarmonyPatch(typeof(Vagon), "UpdateMass")]
        public static class ModifyVagonMass {
            private static Boolean Prefix(ref Vagon __instance) {
				if (!__instance.m_nview.IsOwner())
				{
					return false;
				}
				if (__instance.m_container == null)
				{
					return false;
				}



				float totalWeight;

				if (Configuration.Current.Vagon.IsEnabled)
					totalWeight = Helper.applyModifierValue(__instance.m_container.GetInventory().GetTotalWeight(), Configuration.Current.Vagon.wagonItemWeight);
				else
					totalWeight = __instance.m_container.GetInventory().GetTotalWeight();

				if (Configuration.Current.Vagon.IsEnabled)
					__instance.m_baseMass = Configuration.Current.Vagon.vagonBaseMass;
				else
					__instance.m_baseMass = 20;

				float mass = __instance.m_baseMass + totalWeight * __instance.m_itemWeightMassFactor;

				__instance.SetMass(mass);

				return false;
			}
        }
    }
}