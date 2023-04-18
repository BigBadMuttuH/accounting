CREATE TABLE IF NOT EXISTS device (
    id INTEGER PRIMARY KEY,
    model VARCHAR(255) NOT NULL,
    vid VARCHAR(8) NOT NULL,
    pid VARCHAR(8) NOT NULL,
    serial_number VARCHAR(255) UNIQUE NOT NULL,
    inventory_number VARCHAR(255) UNIQUE NOT NULL
);