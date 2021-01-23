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
    /*Buckle your belts or something, we are doing Transpilers!*/

    [HarmonyPatch(typeof(WildAnimalSpawner))]
    [HarmonyPatch("SpawnRandomWildAnimalAt")]
    public static class MagicalMenagerie_WildAnimalSpawner_SpawnRandomWildAnimalAt_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilg)
        {
            var codes = new List<CodeInstruction>(instructions);
            Label label = ilg.DefineLabel();
            int i = 0;
            foreach (CodeInstruction instruction in codes)
            {
                if (instruction.opcode == OpCodes.Stloc_0)
                {
                    codes[i + 1].labels.Add(label);
                    yield return new CodeInstruction(OpCodes.Stloc_0);
                    yield return new CodeInstruction(OpCodes.Ldloc_0);//Load "PawnKindDef" local variable. 
                    yield return new CodeInstruction(OpCodes.Call, typeof(MagicalMenagerie_WildAnimalSpawner_SpawnRandomWildAnimalAt_Patch).GetMethod("DetectMMCreatureAndOptions"));
                    yield return new CodeInstruction(OpCodes.Brfalse, label);
                    yield return new CodeInstruction(OpCodes.Ret);
                }
                else
                {
                    yield return instruction;
                }

                i++;
            }

        }

        public static bool DetectMMCreatureAndOptions(PawnKindDef theCreature)
        {
            if (theCreature != null)
            {
               



                if (MagicalMenagerie_Mod.settings.pawnSpawnStates != null && MagicalMenagerie_Mod.settings.pawnSpawnStates.Keys.Contains(theCreature.defName))
                {
                    if (MagicalMenagerie_Mod.settings.pawnSpawnStates[theCreature.defName])
                    {

                        return true;
                    }
                    else return false;
                }
                else return false;


            }
            else return false;



        }
    }

}
