
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/02/2020 17:52:59
-- Generated from EDMX file: C:\Users\Adrien\Documents\ETNA\Master\IDV-NET4\MovieNet\MovieNet.DB\DataModel.edmx
-- --------------------------------------------------
CREATE DATABASE MovieDb;
SET QUOTED_IDENTIFIER OFF;
GO
USE [MovieDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_GenreMovie_Genre]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GenreMovie] DROP CONSTRAINT [FK_GenreMovie_Genre];
GO
IF OBJECT_ID(N'[dbo].[FK_GenreMovie_Movie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GenreMovie] DROP CONSTRAINT [FK_GenreMovie_Movie];
GO
IF OBJECT_ID(N'[dbo].[FK_UserNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentSet] DROP CONSTRAINT [FK_UserNote];
GO
IF OBJECT_ID(N'[dbo].[FK_NoteMovie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentSet] DROP CONSTRAINT [FK_NoteMovie];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[MovieSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MovieSet];
GO
IF OBJECT_ID(N'[dbo].[CommentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentSet];
GO
IF OBJECT_ID(N'[dbo].[GenreSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GenreSet];
GO
IF OBJECT_ID(N'[dbo].[GenreMovie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GenreMovie];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Admin] int 
);
GO

-- Creating table 'MovieSet'
CREATE TABLE [dbo].[MovieSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Synopsis] nvarchar(max)  NOT NULL,
    [Image] nvarchar(max)
);
GO

-- Creating table 'CommentSet'
CREATE TABLE [dbo].[CommentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] smallint  NOT NULL,
    [Like] bigint  NOT NULL,
    [Dislike] bigint  NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [User_Id] int  NOT NULL,
    [Movie_Id] int  NOT NULL
);
GO

-- Creating table 'GenreSet'
CREATE TABLE [dbo].[GenreSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'GenreMovie'
CREATE TABLE [dbo].[GenreMovie] (
    [Genre_Id] int  NOT NULL,
    [Movie_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MovieSet'
ALTER TABLE [dbo].[MovieSet]
ADD CONSTRAINT [PK_MovieSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [PK_CommentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GenreSet'
ALTER TABLE [dbo].[GenreSet]
ADD CONSTRAINT [PK_GenreSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Genre_Id], [Movie_Id] in table 'GenreMovie'
ALTER TABLE [dbo].[GenreMovie]
ADD CONSTRAINT [PK_GenreMovie]
    PRIMARY KEY CLUSTERED ([Genre_Id], [Movie_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Genre_Id] in table 'GenreMovie'
ALTER TABLE [dbo].[GenreMovie]
ADD CONSTRAINT [FK_GenreMovie_Genre]
    FOREIGN KEY ([Genre_Id])
    REFERENCES [dbo].[GenreSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Movie_Id] in table 'GenreMovie'
ALTER TABLE [dbo].[GenreMovie]
ADD CONSTRAINT [FK_GenreMovie_Movie]
    FOREIGN KEY ([Movie_Id])
    REFERENCES [dbo].[MovieSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GenreMovie_Movie'
CREATE INDEX [IX_FK_GenreMovie_Movie]
ON [dbo].[GenreMovie]
    ([Movie_Id]);
GO

-- Creating foreign key on [User_Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_UserNote]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserNote'
CREATE INDEX [IX_FK_UserNote]
ON [dbo].[CommentSet]
    ([User_Id]);
GO

-- Creating foreign key on [Movie_Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_NoteMovie]
    FOREIGN KEY ([Movie_Id])
    REFERENCES [dbo].[MovieSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NoteMovie'
CREATE INDEX [IX_FK_NoteMovie]
ON [dbo].[CommentSet]
    ([Movie_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------