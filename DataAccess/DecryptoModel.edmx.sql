
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/09/2023 16:34:58
-- Generated from EDMX file: C:\Users\danse\Desktop\DeCrypto\DeCrypto\DataAccess\DecryptoModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Decrypto];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AccountAccount_Account]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FriendList] DROP CONSTRAINT [FK_AccountAccount_Account];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountAccount_Account1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FriendList] DROP CONSTRAINT [FK_AccountAccount_Account1];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet] DROP CONSTRAINT [FK_UserAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_BlueTeamGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameSet] DROP CONSTRAINT [FK_BlueTeamGame];
GO
IF OBJECT_ID(N'[dbo].[FK_RedTeamGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameSet] DROP CONSTRAINT [FK_RedTeamGame];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountBlueTeam_AccountSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountBlueTeam] DROP CONSTRAINT [FK_AccountBlueTeam_AccountSet];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountBlueTeam_BlueTeamSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountBlueTeam] DROP CONSTRAINT [FK_AccountBlueTeam_BlueTeamSet];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountRedTeam_AccountSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountRedTeam] DROP CONSTRAINT [FK_AccountRedTeam_AccountSet];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountRedTeam_RedTeamSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountRedTeam] DROP CONSTRAINT [FK_AccountRedTeam_RedTeamSet];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AccountSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountSet];
GO
IF OBJECT_ID(N'[dbo].[BlueTeamSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlueTeamSet];
GO
IF OBJECT_ID(N'[dbo].[FriendList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FriendList];
GO
IF OBJECT_ID(N'[dbo].[GameSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameSet];
GO
IF OBJECT_ID(N'[dbo].[RedTeamSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RedTeamSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[AccountBlueTeam]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountBlueTeam];
GO
IF OBJECT_ID(N'[dbo].[AccountRedTeam]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountRedTeam];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AccountSet'
CREATE TABLE [dbo].[AccountSet] (
    [Nickname] nvarchar(20)  NOT NULL,
    [Email] nvarchar(45)  NOT NULL,
    [EmailVerify] bit  NOT NULL,
    [Password] nvarchar(45)  NOT NULL,
    [FriendRequest_FriendRequestId] int  NOT NULL,
    [FriendRequest1_FriendRequestId] int  NOT NULL
);
GO

-- Creating table 'BlueTeamSet'
CREATE TABLE [dbo].[BlueTeamSet] (
    [BlueTeamId] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'FriendList'
CREATE TABLE [dbo].[FriendList] (
    [Account2_Nickname] nvarchar(20)  NOT NULL,
    [Account1_Nickname] nvarchar(20)  NOT NULL,
    [IsBlocked] bit  NULL,
    [StartDate] datetime  NULL
);
GO

-- Creating table 'GameSet'
CREATE TABLE [dbo].[GameSet] (
    [GamesId] int IDENTITY(1,1) NOT NULL,
    [WinnerTeam] nvarchar(max)  NOT NULL,
    [RoundNumber] nvarchar(max)  NOT NULL,
    [BlueTeam_BlueTeamId] int  NOT NULL,
    [RedTeam_RedTeamId] int  NOT NULL
);
GO

-- Creating table 'RedTeamSet'
CREATE TABLE [dbo].[RedTeamSet] (
    [RedTeamId] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [BirthDay] nvarchar(max)  NOT NULL,
    [Account_Nickname] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'AccountBlueTeam'
CREATE TABLE [dbo].[AccountBlueTeam] (
    [AccountSet_Nickname] nvarchar(20)  NOT NULL,
    [BlueTeamSet_BlueTeamId] int  NOT NULL
);
GO

-- Creating table 'AccountRedTeam'
CREATE TABLE [dbo].[AccountRedTeam] (
    [AccountSet_Nickname] nvarchar(20)  NOT NULL,
    [RedTeamSet_RedTeamId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Nickname] in table 'AccountSet'
ALTER TABLE [dbo].[AccountSet]
ADD CONSTRAINT [PK_AccountSet]
    PRIMARY KEY CLUSTERED ([Nickname] ASC);
GO

-- Creating primary key on [BlueTeamId] in table 'BlueTeamSet'
ALTER TABLE [dbo].[BlueTeamSet]
ADD CONSTRAINT [PK_BlueTeamSet]
    PRIMARY KEY CLUSTERED ([BlueTeamId] ASC);
GO

-- Creating primary key on [Account2_Nickname], [Account1_Nickname] in table 'FriendList'
ALTER TABLE [dbo].[FriendList]
ADD CONSTRAINT [PK_FriendList]
    PRIMARY KEY CLUSTERED ([Account2_Nickname], [Account1_Nickname] ASC);
GO

-- Creating primary key on [GamesId] in table 'GameSet'
ALTER TABLE [dbo].[GameSet]
ADD CONSTRAINT [PK_GameSet]
    PRIMARY KEY CLUSTERED ([GamesId] ASC);
GO

-- Creating primary key on [RedTeamId] in table 'RedTeamSet'
ALTER TABLE [dbo].[RedTeamSet]
ADD CONSTRAINT [PK_RedTeamSet]
    PRIMARY KEY CLUSTERED ([RedTeamId] ASC);
GO

-- Creating primary key on [UserId] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [AccountSet_Nickname], [BlueTeamSet_BlueTeamId] in table 'AccountBlueTeam'
ALTER TABLE [dbo].[AccountBlueTeam]
ADD CONSTRAINT [PK_AccountBlueTeam]
    PRIMARY KEY CLUSTERED ([AccountSet_Nickname], [BlueTeamSet_BlueTeamId] ASC);
GO

-- Creating primary key on [AccountSet_Nickname], [RedTeamSet_RedTeamId] in table 'AccountRedTeam'
ALTER TABLE [dbo].[AccountRedTeam]
ADD CONSTRAINT [PK_AccountRedTeam]
    PRIMARY KEY CLUSTERED ([AccountSet_Nickname], [RedTeamSet_RedTeamId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Account2_Nickname] in table 'FriendList'
ALTER TABLE [dbo].[FriendList]
ADD CONSTRAINT [FK_AccountAccount_Account]
    FOREIGN KEY ([Account2_Nickname])
    REFERENCES [dbo].[AccountSet]
        ([Nickname])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Account1_Nickname] in table 'FriendList'
ALTER TABLE [dbo].[FriendList]
ADD CONSTRAINT [FK_AccountAccount_Account1]
    FOREIGN KEY ([Account1_Nickname])
    REFERENCES [dbo].[AccountSet]
        ([Nickname])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountAccount_Account1'
CREATE INDEX [IX_FK_AccountAccount_Account1]
ON [dbo].[FriendList]
    ([Account1_Nickname]);
GO

-- Creating foreign key on [Account_Nickname] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [FK_UserAccount]
    FOREIGN KEY ([Account_Nickname])
    REFERENCES [dbo].[AccountSet]
        ([Nickname])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAccount'
CREATE INDEX [IX_FK_UserAccount]
ON [dbo].[UserSet]
    ([Account_Nickname]);
GO

-- Creating foreign key on [BlueTeam_BlueTeamId] in table 'GameSet'
ALTER TABLE [dbo].[GameSet]
ADD CONSTRAINT [FK_BlueTeamGame]
    FOREIGN KEY ([BlueTeam_BlueTeamId])
    REFERENCES [dbo].[BlueTeamSet]
        ([BlueTeamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BlueTeamGame'
CREATE INDEX [IX_FK_BlueTeamGame]
ON [dbo].[GameSet]
    ([BlueTeam_BlueTeamId]);
GO

-- Creating foreign key on [RedTeam_RedTeamId] in table 'GameSet'
ALTER TABLE [dbo].[GameSet]
ADD CONSTRAINT [FK_RedTeamGame]
    FOREIGN KEY ([RedTeam_RedTeamId])
    REFERENCES [dbo].[RedTeamSet]
        ([RedTeamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RedTeamGame'
CREATE INDEX [IX_FK_RedTeamGame]
ON [dbo].[GameSet]
    ([RedTeam_RedTeamId]);
GO

-- Creating foreign key on [AccountSet_Nickname] in table 'AccountBlueTeam'
ALTER TABLE [dbo].[AccountBlueTeam]
ADD CONSTRAINT [FK_AccountBlueTeam_AccountSet]
    FOREIGN KEY ([AccountSet_Nickname])
    REFERENCES [dbo].[AccountSet]
        ([Nickname])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BlueTeamSet_BlueTeamId] in table 'AccountBlueTeam'
ALTER TABLE [dbo].[AccountBlueTeam]
ADD CONSTRAINT [FK_AccountBlueTeam_BlueTeamSet]
    FOREIGN KEY ([BlueTeamSet_BlueTeamId])
    REFERENCES [dbo].[BlueTeamSet]
        ([BlueTeamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountBlueTeam_BlueTeamSet'
CREATE INDEX [IX_FK_AccountBlueTeam_BlueTeamSet]
ON [dbo].[AccountBlueTeam]
    ([BlueTeamSet_BlueTeamId]);
GO

-- Creating foreign key on [AccountSet_Nickname] in table 'AccountRedTeam'
ALTER TABLE [dbo].[AccountRedTeam]
ADD CONSTRAINT [FK_AccountRedTeam_AccountSet]
    FOREIGN KEY ([AccountSet_Nickname])
    REFERENCES [dbo].[AccountSet]
        ([Nickname])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RedTeamSet_RedTeamId] in table 'AccountRedTeam'
ALTER TABLE [dbo].[AccountRedTeam]
ADD CONSTRAINT [FK_AccountRedTeam_RedTeamSet]
    FOREIGN KEY ([RedTeamSet_RedTeamId])
    REFERENCES [dbo].[RedTeamSet]
        ([RedTeamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountRedTeam_RedTeamSet'
CREATE INDEX [IX_FK_AccountRedTeam_RedTeamSet]
ON [dbo].[AccountRedTeam]
    ([RedTeamSet_RedTeamId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------