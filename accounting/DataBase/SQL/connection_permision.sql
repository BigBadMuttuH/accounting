CREATE TABLE IF NOT EXISTS connection_permission (
    id INTEGER PRIMARY KEY,
    permission_number VARCHAR(22) UNIQUE NOT NULL,
    permission_date DATE NOT NULL,
    registration_number VARCHAR(125) NOT NULL,
    url TEXT NOT NULL
);