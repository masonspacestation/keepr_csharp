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

-- #SECTION 🖼️ keeps -
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
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
    vaultKeepId int NOT NULL DEFAULT 0
    -- FOREIGN KEY (vaultKeepId) REFERENCES vaultKeeps (id) ON DELETE CASCADE
);

SELECT * FROM keeps;

SELECT keeps.*, accounts.*
FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
WHERE
    keeps.id = 2;

SELECT keeps.*, vaults.*, vaultKeeps.*, accounts.*
FROM
    keeps
    JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
    JOIN vaults ON vaultKeeps.vaultId = vaults.id
    JOIN accounts ON vaultKeeps.creatorId = accounts.id
WHERE
    vaults.id = 2;

-- DROP TABLE keeps;

-- #SECTION 📦 vaults
CREATE TABLE vaults (
    id INT NOT NULL AUTO_INCREMENT primary key,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) NOT NULL,
    description varchar(1000) NOT NULL,
    img varchar(1000) NOT NULL,
    isPrivate BOOLEAN NOT NULL DEFAULT false,
    creatorId varchar(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);

SELECT * FROM vaults;

SELECT vaults.*, accounts.*
FROM vaults
    JOIN accounts ON vaults.creatorId = accounts.id
WHERE
    vaults.id = 2;

SELECT vaults.*, accounts.*
FROM vaults
    JOIN accounts ON vaults.creatorId = accounts.id
WHERE
    vaults.creatorId = "662818ab0bd0398d8bf3cd62";

-- DROP TABLE vaults;

-- #SECTION 🔗 vaultKeeps
CREATE TABLE vaultKeeps (
    id INT NOT NULL AUTO_INCREMENT primary key,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    keepId int NOT NULL,
    vaultId int NOT NULL,
    creatorId varchar(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE
);

SELECT * FROM vaultKeeps;

SELECT vaultKeeps.*, keeps.*, vaults.*, accounts.*
FROM
    vaultKeeps
    JOIN keeps ON vaultKeeps.keepId = keeps.id
    JOIN vaults ON vaultKeeps.vaultId = vaults.id
    JOIN accounts ON vaultKeeps.creatorId = accounts.id
WHERE
    vaultKeeps.id = 1

DROP TABLE vaultKeeps;