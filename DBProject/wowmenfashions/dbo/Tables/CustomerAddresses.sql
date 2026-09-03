CREATE TABLE [dbo].[CustomerAddresses] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [CustomerId]        INT            NOT NULL,
    [FullName]          NVARCHAR (100) NOT NULL,
    [AddressLine]       NVARCHAR (255) NOT NULL,
    [City]              NVARCHAR (100) NOT NULL,
    [PostalCode]        NVARCHAR (20)  NOT NULL,
    [Country]           NVARCHAR (100) NOT NULL,
    [IsDefaultShipping] BIT            DEFAULT ((0)) NOT NULL,
    [IsDefaultBilling]  BIT            DEFAULT ((0)) NOT NULL,
    [CreatedAt]         DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedAt]         DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [AddressLine2]      NVARCHAR (500) NULL,
    [State]             NVARCHAR (100) NULL,
    [ContactNumber]     NVARCHAR (50)  NULL,
    [Landmark]          NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CustomerAddresses_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]) ON DELETE CASCADE
);

