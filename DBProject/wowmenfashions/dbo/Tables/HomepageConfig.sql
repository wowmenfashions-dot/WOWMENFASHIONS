CREATE TABLE [dbo].[HomepageConfig] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [ConfigKey]   NVARCHAR (100) NOT NULL,
    [ConfigValue] NVARCHAR (MAX) NOT NULL,
    [UpdatedAt]   DATETIME2 (7)  DEFAULT (sysutcdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([ConfigKey] ASC)
);

