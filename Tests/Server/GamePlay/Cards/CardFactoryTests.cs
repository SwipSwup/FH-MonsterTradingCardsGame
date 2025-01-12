using Server.Gameplay.Cards;
using Shared.Card;
using Shared.Card.Types;
using Shared.DTOs;

namespace Tests.Server.GamePlay.Cards
{
    [TestFixture]
    public class CardFactoryTests
    {
        [Test]
        public void GetCardPack_ShouldReturnCorrectNumberOfCards()
        {
            int packSize = CardFactory.PackSize;

            Card[] cardPack = CardFactory.GetCardPack(packSize);

            Assert.That(cardPack.Length, Is.EqualTo(packSize),
                "The number of cards in the pack should match the expected pack size.");
        }

        [Test]
        public void GetCardFromCardDto_ShouldCreateCorrectCard()
        {
            CardDto cardDto = new CardDto
            {
                CardId = 1,
                CardType = CardType.Monster,
                Damage = 50f,
                Element = Element.Fire,
                Species = Species.Goblin,
                Rarity = Rarity.Rare
            };

            Card card = CardFactory.GetCardFromCardDto(cardDto);

            Assert.IsNotNull(card, "The card should not be null.");
            Assert.That(card.Id, Is.EqualTo(cardDto.CardId), "The card ID should match.");
            Assert.That(card.Damage, Is.EqualTo(cardDto.Damage), "The card damage should match.");
            Assert.That(card.Element, Is.EqualTo(cardDto.Element), "The card element should match.");
            Assert.That(card.Rarity, Is.EqualTo(cardDto.Rarity), "The card rarity should match.");
        }

        [Test]
        public void GetSpellCard_ShouldCreateSpellCardWithCorrectAttributes()
        {
            float damage = 100f;
            Element element = Element.Water;
            Rarity rarity = Rarity.Legendary;

            SpellCard spellCard = CardFactory.GetSpellCard(damage, element, rarity);

            Assert.IsNotNull(spellCard, "The spell card should not be null.");
            Assert.That(spellCard.Damage, Is.EqualTo(damage), "The spell card damage should match.");
            Assert.That(spellCard.Element, Is.EqualTo(element), "The spell card element should match.");
            Assert.That(spellCard.Rarity, Is.EqualTo(rarity), "The spell card rarity should match.");
        }

        [Test]
        public void GetMonsterCard_ShouldCreateMonsterCardWithCorrectAttributes()
        {
            float damage = 75f;
            Element element = Element.Fire;
            Species species = Species.Goblin;
            Rarity rarity = Rarity.Rare;

            MonsterCard monsterCard = CardFactory.GetMonsterCard(damage, element, species, rarity);

            Assert.That(monsterCard, Is.Not.Null, "The monster card should not be null.");
            Assert.Multiple(() =>
            {
                Assert.That(monsterCard.Damage, Is.EqualTo(damage), "The monster card damage should match.");
                Assert.That(monsterCard.Element, Is.EqualTo(element), "The monster card element should match.");
                Assert.That(monsterCard.Species, Is.EqualTo(species), "The monster card species should match.");
                Assert.That(monsterCard.Rarity, Is.EqualTo(rarity), "The monster card rarity should match.");
            });
        }
    }
}