CREATE TABLE [dbo].[StoreSettings] (
    [Id]                    INT             NOT NULL,
    [FreeShippingThreshold] DECIMAL (18, 2) DEFAULT ((1000.00)) NOT NULL,
    [ShippingChargeAmount]  DECIMAL (18, 2) DEFAULT ((100.00)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

