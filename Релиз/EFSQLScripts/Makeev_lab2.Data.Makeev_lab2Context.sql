IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405192358_InitialCreate')
BEGIN
    CREATE TABLE [Specialities] (
        [Id] bigint NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Specialities] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405192358_InitialCreate')
BEGIN
    CREATE TABLE [Doctors] (
        [Id] bigint NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [SpecialityId] bigint NOT NULL,
        CONSTRAINT [PK_Doctors] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Doctors_Specialities_SpecialityId] FOREIGN KEY ([SpecialityId]) REFERENCES [Specialities] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405192358_InitialCreate')
BEGIN
    CREATE TABLE [Services] (
        [Id] bigint NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Price] int NOT NULL,
        [DoctorId] bigint NOT NULL,
        CONSTRAINT [PK_Services] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Services_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [Doctors] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405192358_InitialCreate')
BEGIN
    CREATE INDEX [IX_Doctors_SpecialityId] ON [Doctors] ([SpecialityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405192358_InitialCreate')
BEGIN
    CREATE INDEX [IX_Services_DoctorId] ON [Services] ([DoctorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220405192358_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220405192358_InitialCreate', N'6.0.3');
END;
GO

COMMIT;
GO

