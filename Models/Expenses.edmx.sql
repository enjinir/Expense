
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/17/2016 17:48:39
-- Generated from EDMX file: C:\Users\erdem\Documents\GitHub\Expense\Models\Expenses.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Expense];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Expense_Form]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Expense] DROP CONSTRAINT [FK_Expense_Form];
GO
IF OBJECT_ID(N'[dbo].[FK_Expense_State]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Expense] DROP CONSTRAINT [FK_Expense_State];
GO
IF OBJECT_ID(N'[dbo].[FK_Form_State]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Form] DROP CONSTRAINT [FK_Form_State];
GO
IF OBJECT_ID(N'[dbo].[FK_Form_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Form] DROP CONSTRAINT [FK_Form_User];
GO
IF OBJECT_ID(N'[dbo].[FK_User_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_User_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Expense]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Expense];
GO
IF OBJECT_ID(N'[dbo].[Form]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Form];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[State]', 'U') IS NOT NULL
    DROP TABLE [dbo].[State];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Expenses'
CREATE TABLE [dbo].[Expenses] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [StateId] uniqueidentifier  NOT NULL,
    [Cost] int  NOT NULL,
    [FormId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Forms'
CREATE TABLE [dbo].[Forms] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Date] datetime  NOT NULL,
    [OwnerId] uniqueidentifier  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ManagerDescription] nvarchar(max)  NULL,
    [Total] int  NOT NULL,
    [StateId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [AuthorizeLevel] int  NOT NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] uniqueidentifier  NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [RoleId] uniqueidentifier  NULL,
    [ManagerId] uniqueidentifier  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Expenses'
ALTER TABLE [dbo].[Expenses]
ADD CONSTRAINT [PK_Expenses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [PK_Forms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FormId] in table 'Expenses'
ALTER TABLE [dbo].[Expenses]
ADD CONSTRAINT [FK_Expense_Form]
    FOREIGN KEY ([FormId])
    REFERENCES [dbo].[Forms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Expense_Form'
CREATE INDEX [IX_FK_Expense_Form]
ON [dbo].[Expenses]
    ([FormId]);
GO

-- Creating foreign key on [StateId] in table 'Expenses'
ALTER TABLE [dbo].[Expenses]
ADD CONSTRAINT [FK_Expense_State]
    FOREIGN KEY ([StateId])
    REFERENCES [dbo].[States]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Expense_State'
CREATE INDEX [IX_FK_Expense_State]
ON [dbo].[Expenses]
    ([StateId]);
GO

-- Creating foreign key on [StateId] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [FK_Form_State]
    FOREIGN KEY ([StateId])
    REFERENCES [dbo].[States]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Form_State'
CREATE INDEX [IX_FK_Form_State]
ON [dbo].[Forms]
    ([StateId]);
GO

-- Creating foreign key on [OwnerId] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [FK_Form_User]
    FOREIGN KEY ([OwnerId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Form_User'
CREATE INDEX [IX_FK_Form_User]
ON [dbo].[Forms]
    ([OwnerId]);
GO

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_User_Role]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_User_Role'
CREATE INDEX [IX_FK_User_Role]
ON [dbo].[Users]
    ([RoleId]);
GO

-- Creating foreign key on [ManagerId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_User_User]
    FOREIGN KEY ([ManagerId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_User_User'
CREATE INDEX [IX_FK_User_User]
ON [dbo].[Users]
    ([ManagerId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------