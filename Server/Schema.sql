-- Drop existing tables if they exist
DROP TABLE IF EXISTS battles, trades, decks, user_cards, cards, card_types, trade_statuses, elements, rarities, species, users CASCADE;

-- Create the users table
CREATE TABLE users
(
    user_id       SERIAL PRIMARY KEY,
    username      VARCHAR(50) UNIQUE NOT NULL,
    password_hash VARCHAR(256)       NOT NULL,
    mmr           INT DEFAULT 1200   NOT NULL,
    currency      INT DEFAULT 200    NOT NULL
);

-- Create the species table
CREATE TABLE species
(
    species_id   SERIAL PRIMARY KEY,
    species_name VARCHAR(50) UNIQUE NOT NULL
);

-- Insert default species data
INSERT INTO species (species_name)
VALUES ('Goblin'),
       ('Dragon'),
       ('Wizard'),
       ('Orc'),
       ('Knight'),
       ('Kraken'),
       ('FireElv');

-- Create the elements table
CREATE TABLE elements
(
    element_id   SERIAL PRIMARY KEY,
    element_name VARCHAR(50) UNIQUE NOT NULL
);

-- Insert default elements data
INSERT INTO elements (element_name)
VALUES ('Water'),
       ('Fire'),
       ('Normal');

-- Create the rarities table
CREATE TABLE rarities
(
    rarity_id   SERIAL PRIMARY KEY,
    rarity_name VARCHAR(20) UNIQUE NOT NULL
);

-- Insert default rarities data
INSERT INTO rarities (rarity_name)
VALUES ('Uncommon'),
       ('Common'),
       ('Rare'),
       ('Epic'),
       ('Legendary');

-- Create the card_types table
CREATE TABLE card_types
(
    type_id   SERIAL PRIMARY KEY,
    type_name VARCHAR(50) UNIQUE NOT NULL
);

-- Insert default card types data
INSERT INTO card_types (type_name)
VALUES ('Monster'),
       ('Spell');

-- Create the cards table
CREATE TABLE cards
(
    card_id      SERIAL PRIMARY KEY,
    card_name    VARCHAR(100) NOT NULL,
    species_id   INT REFERENCES species (species_id),
    damage       FLOAT          NOT NULL,
    rarity_id    INT          NOT NULL REFERENCES rarities (rarity_id),
    element_id   INT          NOT NULL REFERENCES elements (element_id),
    card_type_id INT          NOT NULL REFERENCES card_types (type_id)
);

-- Create the trade_statuses table
CREATE TABLE trade_statuses
(
    status_id   SERIAL PRIMARY KEY,
    status_name VARCHAR(50) UNIQUE NOT NULL
);

-- Insert default trade statuses data
INSERT INTO trade_statuses (status_name)
VALUES ('Pending'),
       ('Accepted'),
       ('Rejected');

-- Create the user_cards table
CREATE TABLE user_cards
(
    user_id     INT NOT NULL,
    card_id     INT NOT NULL,
    is_in_deck  BOOLEAN DEFAULT FALSE,
    is_in_trade BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (user_id, card_id),
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES users (user_id) ON DELETE CASCADE,
    CONSTRAINT fk_card FOREIGN KEY (card_id) REFERENCES cards (card_id) ON DELETE CASCADE
);


-- Create the trades table
CREATE TABLE trades
(
    trade_id          SERIAL PRIMARY KEY,
    user_id           INT NOT NULL REFERENCES users (user_id) ON DELETE CASCADE,
    offered_card_id   INT NOT NULL REFERENCES cards (card_id) ON DELETE CASCADE,
    requested_card_id INT NOT NULL REFERENCES cards (card_id) ON DELETE CASCADE,
    trade_status_id   INT NOT NULL REFERENCES trade_statuses (status_id) ON DELETE CASCADE
);

-- Create the battles table
CREATE TABLE battles (
                         battle_id SERIAL PRIMARY KEY,
                         player_1_id INT,          
                         player_2_id INT,           
                         winner_id INT,            
                         FOREIGN KEY (player_1_id) REFERENCES users(user_id),
                         FOREIGN KEY (player_2_id) REFERENCES users(user_id),
                         FOREIGN KEY (winner_id) REFERENCES users(user_id)
);

CREATE TABLE battle_rounds (
                               round_id SERIAL PRIMARY KEY,
                               battle_id INT,           
                               round_number INT,        
                               card_1_id INT,           
                               card_2_id INT,           
                               winner_card_id INT,      
                               result TEXT,              
                               FOREIGN KEY (card_1_id) REFERENCES cards(card_id),
                               FOREIGN KEY (card_2_id) REFERENCES cards(card_id),
                               FOREIGN KEY (winner_card_id) REFERENCES cards(card_id)
);
