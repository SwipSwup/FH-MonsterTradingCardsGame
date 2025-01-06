-- Drop existing tables if they exist
DROP TABLE IF EXISTS battles, trades, decks, user_cards, cards, user_credentials, card_types, trade_statuses, elements, rarities, species, users CASCADE;

-- Create the users table
CREATE TABLE users
(
    user_id    SERIAL PRIMARY KEY,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create the user_credentials table
CREATE TABLE user_credentials
(
    credential_id SERIAL PRIMARY KEY,
    user_id       INT NOT NULL REFERENCES users (user_id) ON DELETE CASCADE,
    username      VARCHAR(50) UNIQUE NOT NULL,
    password_hash VARCHAR(256) NOT NULL,
    created_at    TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at    TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create the species table
CREATE TABLE species
(
    species_id   SERIAL PRIMARY KEY,
    species_name VARCHAR(50) UNIQUE NOT NULL
);

-- Insert default species data
INSERT INTO species (species_name)
VALUES ('Goblin'), ('Dragon'), ('Wizard'), ('Orc'), ('Knight'), ('Kraken'), ('FireElv');

-- Create the elements table
CREATE TABLE elements
(
    element_id   SERIAL PRIMARY KEY,
    element_name VARCHAR(50) UNIQUE NOT NULL
);

-- Insert default elements data
INSERT INTO elements (element_name)
VALUES ('Water'), ('Fire'), ('Normal');

-- Create the rarities table
CREATE TABLE rarities
(
    rarity_id   SERIAL PRIMARY KEY,
    rarity_name VARCHAR(20) UNIQUE NOT NULL
);

-- Insert default rarities data
INSERT INTO rarities (rarity_name)
VALUES ('Uncommon'), ('Common'), ('Rare'), ('Epic'), ('Legendary');

-- Create the card_types table
CREATE TABLE card_types
(
    type_id   SERIAL PRIMARY KEY,
    type_name VARCHAR(50) UNIQUE NOT NULL
);

-- Insert default card types data
INSERT INTO card_types (type_name)
VALUES ('Monster'), ('Spell');

-- Create the cards table
CREATE TABLE cards
(
    card_id     SERIAL PRIMARY KEY,
    card_name   VARCHAR(100) NOT NULL,
    species_id  INT NOT NULL REFERENCES species (species_id),
    attack      INT NOT NULL,
    defense     INT NOT NULL,
    rarity_id   INT NOT NULL REFERENCES rarities (rarity_id),
    element_id  INT NOT NULL REFERENCES elements (element_id),
    card_type_id INT NOT NULL REFERENCES card_types (type_id),
    created_at  TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at  TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create the trade_statuses table
CREATE TABLE trade_statuses
(
    status_id   SERIAL PRIMARY KEY,
    status_name VARCHAR(50) UNIQUE NOT NULL
);

-- Insert default trade statuses data
INSERT INTO trade_statuses (status_name)
VALUES ('Pending'), ('Accepted'), ('Rejected');

-- Create the user_cards table
CREATE TABLE user_cards
(
    user_id     INT NOT NULL,
    card_id     INT NOT NULL,
    is_in_deck  BOOLEAN DEFAULT FALSE,
    is_in_trade BOOLEAN DEFAULT FALSE,
    created_at  TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id, card_id),
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    CONSTRAINT fk_card FOREIGN KEY (card_id) REFERENCES cards(card_id) ON DELETE CASCADE
);

-- Create the decks table
CREATE TABLE decks
(
    deck_id    SERIAL PRIMARY KEY,
    user_id    INT NOT NULL REFERENCES users (user_id) ON DELETE CASCADE,
    card_id    INT NOT NULL REFERENCES cards (card_id) ON DELETE CASCADE,
    CONSTRAINT unique_deck UNIQUE (user_id, card_id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create the trades table
CREATE TABLE trades
(
    trade_id          SERIAL PRIMARY KEY,
    user_id           INT NOT NULL REFERENCES users (user_id) ON DELETE CASCADE,
    offered_card_id   INT NOT NULL REFERENCES cards (card_id) ON DELETE CASCADE,
    requested_card_id INT NOT NULL REFERENCES cards (card_id) ON DELETE CASCADE,
    trade_status_id   INT NOT NULL REFERENCES trade_statuses (status_id) ON DELETE CASCADE,
    created_at        TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at        TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create the battles table
CREATE TABLE battles
(
    battle_id  SERIAL PRIMARY KEY,
    user1_id   INT NOT NULL REFERENCES users (user_id) ON DELETE CASCADE,
    user2_id   INT NOT NULL REFERENCES users (user_id) ON DELETE CASCADE,
    winner_id  INT REFERENCES users (user_id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
