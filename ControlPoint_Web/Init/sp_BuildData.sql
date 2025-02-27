USE [ControlPoint]
GO
/****** Object:  StoredProcedure [dbo].[sp_BuildData]    Script Date: 22.09.2020 4:29:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_BuildData]
AS
BEGIN
	SET NOCOUNT ON;

	Declare @nCountRows bigint = 99999
	Declare @nCurrentRow bigint = 0

	While(@nCountRows >= @nCurrentRow)
	Begin
		Declare @nMode as bigint = 10000000
		Declare @nRandomValue as varchar(max) =  CAST( CAST( RAND() * @nMode as bigint) as varchar(max))
		
		-- Вставка данных
		Insert into Table1([Data]) Values('T1_' + @nRandomValue)
		Set @nRandomValue =  CAST( CAST( RAND() * @nMode as bigint) as varchar(max))
		Insert into Table2([Data]) Values('T2_' + @nRandomValue)
		Set @nRandomValue =  CAST( CAST( RAND() * @nMode as bigint) as varchar(max))
		Insert into Table3([Data]) Values('T3_' + @nRandomValue)

		Set @nCurrentRow = @nCurrentRow + 1
	End

END
