SET NOCOUNT ON;

-- ������� ������
Delete from refColors
Delete from refDrinks
Delete from tblMainForm


-- ��������� �����
if not exists (Select 1 from [refColors] where Description = '�����')
	Insert into refColors (Description) Values ('�����')

if not exists (Select 1 from [refColors] where Description = '������')
	Insert into refColors (Description) Values ('������')

if not exists (Select 1 from [refColors] where Description = '�������')
	Insert into refColors (Description) Values ('�������')

-- ��������� �������
if not exists (Select 1 from refDrinks where Description = '���')
	Insert into refDrinks (Description) Values ('���')

if not exists (Select 1 from refDrinks where Description = '����')
	Insert into refDrinks (Description) Values ('����')

if not exists (Select 1 from refDrinks where Description = '���')
	Insert into refDrinks (Description) Values ('���')

if not exists (Select 1 from refDrinks where Description = '����')
	Insert into refDrinks (Description) Values ('����')

-- ��������
if not exists (Select 1 from refDrinks)
	RAISERROR('������ �������!', 16,1)

if not exists (Select 1 from refColors)
	RAISERROR('������ �������!', 16,1)

-- ��������� ������
Declare @nCountItems as int = 100
Declare @nCurrentItem as int = 0

if exists (Select 1 from TempDB.INFORMATION_SCHEMA.Tables where Table_Name like '#tmpFirstNames%')
	Drop table #tmpFirstNames

if exists (Select 1 from TempDB.INFORMATION_SCHEMA.Tables where Table_Name like '#tmpLastNames%')
	Drop table #tmpLastNames

Create table #tmpFirstNames (Description varchar(max))
Insert into #tmpFirstNames(Description) Values('���'),
('����'),('����'),('�����'),('����'),('�������')

Create table #tmpLastNames (Description varchar(max))
Insert into #tmpLastNames(Description) Values('������'),
('������'),('��������'),('���������'),('���')

While(@nCountItems > @nCurrentItem)
Begin
	Declare @nCountFirstNames as int = (Select Count(*) from #tmpFirstNames) - 1
	Declare @nCountLastNames as int = (Select Count(*) from #tmpLastNames) - 1

	Declare @nRandomValue as int = 0 

	-- ���������� �������� ����� ����� ���
	Set @nRandomValue = CAST( RAND() * @nCountFirstNames as int)
	Declare @strFirstName as varchar(max) = ''
	Select Top 1 @strFirstName = Description from
		( Select Top (@nRandomValue) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) as rNumber , Description from #tmpFirstNames ) as tt
	where 
		rNumber = @nRandomValue

	-- ���������� �������� ����� ����� �������
	Set @nRandomValue = CAST( RAND() * @nCountLastNames as int)
	Declare @strLastName as varchar(max) = ''
	Select Top 1 @strLastName = Description from
		( Select Top (@nRandomValue) ROW_NUMBER() OVER(ORDER BY (SELECT NULL))  as rNumber , Description from #tmpLastNames ) as tt
	where
		rNumber = @nRandomValue

	if( not (LTrim(RTrim(@strLastName)) = '' or LTrim(RTrim(@strFirstName)) = '' ))
	Begin

		Set @nRandomValue = ABS(CHECKSUM(NewId())) % 500

		-- ������� �������� ������
		if not exists (Select 1 from [tblMainForm] where LastName = @strLastName and FirstName = @strFirstName)
		Begin
			Insert into [dbo].[tblMainForm]([FirstName], [LastName], [Bithday], [Phone])
			Values( @strFirstName, @strLastName, DateAdd(DAY, @nRandomValue * (-1), GetDate()), '+7' + CAST(@nRandomValue as varchar(255)))
		End

		Set @nCurrentItem = @nCurrentItem + 1
	End
End

-- ��������
if not exists(Select 1 from [dbo].[tblMainForm])
	RAISERROR('������ �� �����������!', 16, 1)

