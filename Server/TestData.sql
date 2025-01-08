DO $$
    DECLARE
        i INT;
    BEGIN
        FOR i IN 1..50 LOOP
                INSERT INTO users (created_at, updated_at)
                VALUES (CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

                INSERT INTO user_credentials (user_id, username, password_hash, created_at, updated_at)
                VALUES (i, 'user' || i, 'password_hash' || i, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
            END LOOP;
    END $$;

-- Generate 50 test cards
DO $$
    DECLARE
        i INT;
    BEGIN
        FOR i IN 1..50 LOOP
                INSERT INTO cards (card_name, species_id, attack, defense, rarity_id, element_id, card_type_id, created_at, updated_at)
                VALUES
                    (
                        'Card ' || i,
                        (i % 7) + 1, -- Random species_id
                        (i % 10) + 1, -- Random attack value
                        (i % 10) + 1, -- Random defense value
                        (i % 5) + 1,  -- Random rarity_id
                        (i % 3) + 1,  -- Random element_id
                        (i % 2) + 1,  -- Random card_type_id
                        CURRENT_TIMESTAMP,
                        CURRENT_TIMESTAMP
                    );
            END LOOP;
    END $$;

-- Generate 50 user_cards (Randomly assign cards to users)
DO $$
    DECLARE
        i INT;
    BEGIN
        FOR i IN 1..50 LOOP
                INSERT INTO user_cards (user_id, card_id, is_in_deck, is_in_trade, created_at)
                VALUES
                    (
                        (i % 50) + 1, -- Random user_id
                        (i % 50) + 1, -- Random card_id
                        (i % 2) = 0,  -- Random boolean for is_in_deck
                        (i % 2) = 1,  -- Random boolean for is_in_trade
                        CURRENT_TIMESTAMP
                    );
            END LOOP;
    END $$;

-- Generate 50 trades with random statuses
DO $$
    DECLARE
        i INT;
    BEGIN
        FOR i IN 1..50 LOOP
                INSERT INTO trades (user_id, offered_card_id, requested_card_id, trade_status_id, created_at, updated_at)
                VALUES
                    (
                        (i % 50) + 1, -- Random user_id
                        (i % 50) + 1, -- Random offered_card_id
                        (i % 50) + 1, -- Random requested_card_id
                        (i % 3) + 1,  -- Random trade_status_id
                        CURRENT_TIMESTAMP,
                        CURRENT_TIMESTAMP
                    );
            END LOOP;
    END $$;

-- Generate 50 battles between random users
DO $$
    DECLARE
        i INT;
    BEGIN
        FOR i IN 1..50 LOOP
                INSERT INTO battles (user1_id, user2_id, winner_id, created_at, updated_at)
                VALUES
                    (
                        (i % 50) + 1, -- Random user1_id
                        (i % 50) + 1, -- Random user2_id
                        (i % 50) + 1, -- Random winner_id
                        CURRENT_TIMESTAMP,
                        CURRENT_TIMESTAMP
                    );
            END LOOP;
    END $$;