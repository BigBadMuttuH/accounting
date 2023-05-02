CREATE TABLE IF NOT EXISTS accounting (
    id INTEGER PRIMARY KEY,
    user_id INTEGER NOT NULL,
    device_id INTEGER NOT NULL,
    connection_permission_id INTEGER NOT NULL,
	disconnection_permission_id INTEGER,
    FOREIGN KEY (user_id) REFERENCES ad_user (sid),
    FOREIGN KEY (device_id) REFERENCES device (id),
    FOREIGN KEY (connection_permission_id) REFERENCES connection_permission (id)
);