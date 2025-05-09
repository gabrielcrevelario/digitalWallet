CREATE TABLE tbl_user (
    id UUID PRIMARY KEY,
    name VARCHAR(150) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password_hash VARCHAR(16) NOT NULL
);

CREATE TABLE tbl_wallet (
    id UUID PRIMARY KEY,
    user_id UUID NOT NULL,
    balance DECIMAL(18,2) NOT NULL,
    CONSTRAINT fk_wallet_user FOREIGN KEY (user_id) REFERENCES tbl_user(id) ON DELETE CASCADE
);

CREATE TABLE tbl_transaction (
    id UUID PRIMARY KEY,
    from_wallet_id UUID NOT NULL,
    to_wallet_id UUID NOT NULL,
    amount DECIMAL(18,2) NOT NULL,
    created_at TIMESTAMP NOT NULL,
    CONSTRAINT fk_transaction_from_wallet FOREIGN KEY (from_wallet_id) REFERENCES tbl_wallet(id) ON DELETE RESTRICT,
    CONSTRAINT fk_transaction_to_wallet FOREIGN KEY (to_wallet_id) REFERENCES tbl_wallet(id) ON DELETE RESTRICT
);
