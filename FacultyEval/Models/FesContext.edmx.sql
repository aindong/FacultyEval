
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/11/2014 11:09:05
-- Generated from EDMX file: C:\Users\alleo\documents\visual studio 2013\Projects\FacultyEval\FacultyEval\Models\FesContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [fes];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'StudentAccounts'
CREATE TABLE [dbo].[StudentAccounts] (
    [studentID] int IDENTITY(1,1) NOT NULL,
    [studentCOR] nvarchar(max)  NOT NULL,
    [studentFname] nvarchar(max)  NOT NULL,
    [studentMname] nvarchar(max)  NOT NULL,
    [studentLname] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [studentID] in table 'StudentAccounts'
ALTER TABLE [dbo].[StudentAccounts]
ADD CONSTRAINT [PK_StudentAccounts]
    PRIMARY KEY CLUSTERED ([studentID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------