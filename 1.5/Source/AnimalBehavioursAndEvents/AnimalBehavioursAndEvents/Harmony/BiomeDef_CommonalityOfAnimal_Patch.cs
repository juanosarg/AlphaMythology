using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Reflection.Emit;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;

namespace AnimalBehaviours
{
    /*This Harmony Postfix multiplies commonality of animals in the biome
    */
    [HarmonyPatch(typeof(BiomeDef))]
    [HarmonyPatch("CommonalityOfAnimal")]
    public static class AlphaMythology_BiomeDef_CommonalityOfAnimal_Patch
    {
        [HarmonyPostfix]
        public static void MultiplyMythAnimalCommonality(PawnKindDef animalDef, ref float __result)

        {

            if (animalDef.defName.Contains("MM_"))
            {
                float TotalMultiplier = MagicalMenagerie_Mod.settings.mythAnimalSpawnMultiplier;
                __result *= TotalMultiplier;
                 
            }


        }
    }
}
