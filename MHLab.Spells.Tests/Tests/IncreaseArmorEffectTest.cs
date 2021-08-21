﻿using System.Collections.Generic;
using MHLab.Spells.Costs;
using MHLab.Spells.Definitions;
using MHLab.Spells.Effects;
using MHLab.Spells.Requirements;
using MHLab.Spells.Tests.Spells.Effects;
using NUnit.Framework;

namespace MHLab.Spells.Tests.Tests
{
    public class IncreaseArmorEffectTest
    {
        private class MySpellsContext : SpellsContext
        {
            protected override void RegisterSpells(SpellDefinitionContainer container)
            {
                container.Add<SimpleSpellDefinition>();
            }
        }
        
        private class SimpleSpellDefinition : SpellDefinition
        {
            protected override void SetMetadata()
            {
            }

            protected override void AddRequirements(SpellRequirementContainer container)
            {
            }

            protected override void AddCosts(SpellCostContainer container)
            {
            }

            protected override void AddEffects(SpellEffectContainer container)
            {
                container.Add(new IncreaseArmorSpellEffect(3));
            }
        }
        
        private MySpellsContext           _context;
        private IEnumerable<ISpellTarget> _targets;
        private SpellDefinition           _spell;

        [SetUp]
        public void Setup()
        {
            _context = new MySpellsContext();
            _targets = new[]
            {
                new MyTarget() { Armor = 0 }
            };
            _spell = _context.Definitions.Get<SimpleSpellDefinition>();
        }

        [Test]
        public void Correctly_Increase_Armor()
        {
            var caster = new MyPlayer();

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            Assert.AreEqual(SpellCastState.Success, castResult.State);

            _context.Update(1);

            foreach (var spellTarget in _targets)
            {
                var target = (MyTarget)spellTarget;
                Assert.AreEqual(3, target.Armor);
            }
        }

        [Test]
        public void Correctly_Clean_Up()
        {
            var caster = new MyPlayer();

            var castResult = _context.CasterSystem.Cast(caster, _targets, _spell, out _);
            Assert.AreEqual(SpellCastState.Success, castResult.State);

            _context.Update(3);
            _context.Update(1);

            foreach (var spellTarget in _targets)
            {
                var target = (MyTarget)spellTarget;
                Assert.AreEqual(0, target.Armor);
            }
        }
    }
}