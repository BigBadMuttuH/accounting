CREATE TABLE IF NOT EXISTS ad_user (
    sid INTEGER PRIMARY KEY,
    display_name VARCHAR(255) NOT NULL,
    department VARCHAR(255) NOT NULL,
    sam_account_name VARCHAR(125) NOT NULL);