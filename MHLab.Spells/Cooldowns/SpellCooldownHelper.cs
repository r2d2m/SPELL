﻿using MHLab.Spells.Definitions;

namespace MHLab.Spells.Cooldowns
{
    public static class SpellCooldownHelper
    {
        public static bool IsInCooldown(SpellsContext context, ISpellCaster caster, Spell spell)
        {
            if (context.CastedData.TryGet(caster, out var spellCastedData) == false)
                return false;

            if (spellCastedData.SpellInstances.TryGetFirst(spell, out var spellInstance) == false)
                return false;

            return spellInstance.IsInCooldown();
        }
    }
}