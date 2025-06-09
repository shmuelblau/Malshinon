USE Malshinon;

CREATE TABLE People (
    id INT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    secret_code VARCHAR(100) UNIQUE NOT NULL,
    type ENUM('reporter', 'target', 'both', 'potential_agent') NOT NULL,
    num_reports INT DEFAULT 0,
    num_mentions INT DEFAULT 0
);

CREATE TABLE IntelReports (
    id INT AUTO_INCREMENT PRIMARY KEY,
    reporter_id INT,
    target_id INT,
    text TEXT NOT NULL,
    timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (reporter_id) REFERENCES People(id) ON DELETE SET NULL,
    FOREIGN KEY (target_id) REFERENCES People(id) ON DELETE SET NULL
);

INSERT INTO People (first_name, last_name, secret_code, type)
VALUES 
('John', 'Doe', 'X1A9C2', 'reporter'),
('Jane', 'Smith', 'B7Z3Q8', 'target');

INSERT INTO IntelReports (reporter_id, target_id, text)
VALUES (1, 2, 'Target was seen near the border.');
