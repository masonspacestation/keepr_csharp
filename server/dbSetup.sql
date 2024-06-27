-- Active: 1717712650110@@52.43.166.8@3306@portfolio
CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

SELECT * FROM accounts;

ALTER TABLE accounts DROP COLUMN coverImg;

ALTER TABLE accounts
ADD coverImg VARCHAR(255) NOT NULL default 'https://images.unsplash.com/photo-1528722828814-77b9b83aafb2?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Njh8fGFzdHJvfGVufDB8fDB8fHww';

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

INSERT INTO
    keeps (
        name,
        description,
        img,
        creatorId
    )
VALUES (
        "Bowling",
        "shoes for bowling",
        "https://images.unsplash.com/photo-1628139483293-eea5a6b2e0a5?q=80&w=3328&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "662818ab0bd0398d8bf3cd62"
    );

SELECT keeps.*, accounts.*
FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
WHERE
    keeps.id = LAST_INSERT_ID();

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
    -- FIXME add foreign key for keeps to this table
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE
);

SELECT * FROM vaultKeeps;

-- DROP TABLE vaultKeeps;