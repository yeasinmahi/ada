CREATE TABLE [dbo].[LeaveTypes] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [LeaveTypeName]          NVARCHAR (50) NOT NULL,
    [CompanyPolicy]          INT           CONSTRAINT [DF__LeaveType__Compa__47DBAE45] DEFAULT ((0)) NOT NULL,
    [MaximumAllowedAtATime]  INT           CONSTRAINT [DF__LeaveType__Maxim__48CFD27E] DEFAULT ((0)) NOT NULL,
    [IsHalfDayAllowed]       BIT           CONSTRAINT [DF__LeaveType__IsHal__49C3F6B7] DEFAULT ((0)) NOT NULL,
    [IsBalanceChecked]       BIT           CONSTRAINT [DF__LeaveType__IsBal__4AB81AF0] DEFAULT ((0)) NOT NULL,
    [IsOnlyOneTime]          BIT           CONSTRAINT [DF__LeaveType__IsOnl__4BAC3F29] DEFAULT ((0)) NOT NULL,
    [MaxApplicationAtAMonth] INT           CONSTRAINT [DF__LeaveType__MaxAp__4CA06362] DEFAULT ((0)) NOT NULL,
    [IsRestricted]           BIT           CONSTRAINT [DF__LeaveType__IsRes__4D94879B] DEFAULT ((0)) NOT NULL,
    [ApplicableFor]          NVARCHAR (1)  NULL,
    CONSTRAINT [PK_dbo.LeaveTypes] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 80)
);

