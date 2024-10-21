using MonsterTradingCardsGame.Gameplay.Card;
using MonsterTradingCardsGame.Gameplay.Card.MonsterCards;
using MonsterTradingCardsGame.Gameplay.Card.SpellCards;

namespace MonsterTradingCardsGameTests;

[TestFixture]
public class CardFactoryTest
{
    [TestFixture]
    public class CardFactoryTests
    {
        [Test]
        public void WaterSpellCard_AgainstKraken_ShouldDealZeroDamage()
        {
            SpellCard waterSpellCard = CardFactory.GetSpellCard("Water Spell", 50f, Element.Water);
            MonsterCard krakenMonsterCard = CardFactory.GetMonsterCard("Kraken", 50f, Element.Normal, Species.Kraken);

            float damage = waterSpellCard.CalculateDamage(krakenMonsterCard);

            Assert.That(damage, Is.EqualTo(0f));
        }

        [Test]
        public void WaterSpellCard_AgainstKnight_ShouldDealHighDamage()
        {
            SpellCard waterSpellCard = CardFactory.GetSpellCard("Water Spell", 50f, Element.Water);
            MonsterCard knightMonsterCard = CardFactory.GetMonsterCard("Knight", 50f, Element.Normal, Species.Knight);

            float damage = waterSpellCard.CalculateDamage(knightMonsterCard);

            Assert.That(damage > 50f, Is.True); // Assuming 999999x multiplier
        }

        [Test]
        public void GoblinCard_AgainstDragon_ShouldDealZeroDamage()
        {
            MonsterCard goblinMonsterCard = CardFactory.GetMonsterCard("Goblin", 20f, Element.Normal, Species.Goblin);
            MonsterCard dragonMonsterCard = CardFactory.GetMonsterCard("Dragon", 20f, Element.Normal, Species.Dragon);

            float damage = goblinMonsterCard.CalculateDamage(dragonMonsterCard);

            Assert.That(damage, Is.EqualTo(0f));
        }

        [Test]
        public void FireElvCard_AgainstDragon_ShouldDealZeroDamage()
        {
            MonsterCard fireElvMonsterCard = CardFactory.GetMonsterCard("Fire Elv", 40f, Element.Fire, Species.FireElv);
            MonsterCard dragonMonsterCard = CardFactory.GetMonsterCard("Dragon", 40f, Element.Normal, Species.Dragon);
            
            float damage = fireElvMonsterCard.CalculateDamage(dragonMonsterCard);

            Assert.That(damage, Is.EqualTo(0f));
        }

        [Test]
        public void OrkCard_AgainstWizard_ShouldDealZeroDamage()
        {
            MonsterCard orcMonsterCard = CardFactory.GetMonsterCard("Orc", 30f, Element.Normal, Species.Orc);
            MonsterCard wizardMonsterCard = CardFactory.GetMonsterCard("Wizard", 30f, Element.Normal, Species.Wizard);

            float damage = orcMonsterCard.CalculateDamage(wizardMonsterCard);

            Assert.That(damage, Is.EqualTo(0f));
        }

        [Test]
        public void KrakenCard_AgainstAnySpecies_ShouldDealFullDamage()
        {
            MonsterCard krakenMonsterCard = CardFactory.GetMonsterCard("Kraken", 80f, Element.Water, Species.Kraken);
            MonsterCard goblinMonsterCard = CardFactory.GetMonsterCard("Goblin", 80f, Element.Normal, Species.Kraken);

            float damage = krakenMonsterCard.CalculateDamage(goblinMonsterCard);

            Assert.That(damage, Is.EqualTo(80f));
        }
    }
}