-- Active: 1715633466013@@127.0.0.1@3306@cooperative_mountain_fe2c50_db
CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

SELECT * FROM accounts;

CREATE TABLE keeps (
    id INT NOT NULL AUTO_INCREMENT primary key,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) NOT NULL,
    description varchar(1000) NOT NULL,
    img varchar(1000) NOT NULL,
    views int NOT NULL DEFAULT 0,
    kept int NOT NULL DEFAULT 0,
    creatorId varchar(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);

SELECT * FROM keeps;

-- DROP TABLE keeps;