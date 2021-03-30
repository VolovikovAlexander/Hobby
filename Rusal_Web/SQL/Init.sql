SET NOCOUNT ON;

-- Очистка данных
Delete from refColors
Delete from refDrinks
Delete from tblMainForm


-- Добавляем цвета
if not exists (Select 1 from [refColors] where Description = 'Синий')
	Insert into refColors (Description) Values ('Синий')

if not exists (Select 1 from [refColors] where Description = 'Желтый')
	Insert into refColors (Description) Values ('Желтый')

if not exists (Select 1 from [refColors] where Description = 'Красный')
	Insert into refColors (Description) Values ('Красный')

-- Добавляем напитки
if not exists (Select 1 from refDrinks where Description = 'Чай')
	Insert into refDrinks (Description) Values ('Чай')

if not exists (Select 1 from refDrinks where Description = 'Кофе')
	Insert into refDrinks (Description) Values ('Кофе')

if not exists (Select 1 from refDrinks where Description = 'Сок')
	Insert into refDrinks (Description) Values ('Сок')

if not exists (Select 1 from refDrinks where Description = 'Вода')
	Insert into refDrinks (Description) Values ('Вода')

-- Проверка
if not exists (Select 1 from refDrinks)
	RAISERROR('Ошибка скрипта!', 16,1)

if not exists (Select 1 from refColors)
	RAISERROR('Ошибка скрипта!', 16,1)

-- Формируем анкеты
Declare @nCountItems as int = 100
Declare @nCurrentItem as int = 0

if exists (Select 1 from TempDB.INFORMATION_SCHEMA.Tables where Table_Name like '#tmpFirstNames%')
	Drop table #tmpFirstNames

if exists (Select 1 from TempDB.INFORMATION_SCHEMA.Tables where Table_Name like '#tmpLastNames%')
	Drop table #tmpLastNames

Create table #tmpFirstNames (Description varchar(max))
Insert into #tmpFirstNames(Description) Values('Ира'),
('Петя'),('Иван'),('Артен'),('Анна'),('Евгений')

Create table #tmpLastNames (Description varchar(max))
Insert into #tmpLastNames(Description) Values('Иванов'),
('Петров'),('Васичкин'),('Воловиков'),('Цой')

While(@nCountItems > @nCurrentItem)
Begin
	Declare @nCountFirstNames as int = (Select Count(*) from #tmpFirstNames) - 1
	Declare @nCountLastNames as int = (Select Count(*) from #tmpLastNames) - 1

	Declare @nRandomValue as int = 0 

	-- Определяем случайно какое будет имя
	Set @nRandomValue = CAST( RAND() * @nCountFirstNames as int)
	Declare @strFirstName as varchar(max) = ''
	Select Top 1 @strFirstName = Description from
		( Select Top (@nRandomValue) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) as rNumber , Description from #tmpFirstNames ) as tt
	where 
		rNumber = @nRandomValue

	-- Определяем случайно какое будет фамилия
	Set @nRandomValue = CAST( RAND() * @nCountLastNames as int)
	Declare @strLastName as varchar(max) = ''
	Select Top 1 @strLastName = Description from
		( Select Top (@nRandomValue) ROW_NUMBER() OVER(ORDER BY (SELECT NULL))  as rNumber , Description from #tmpLastNames ) as tt
	where
		rNumber = @nRandomValue

	if( not (LTrim(RTrim(@strLastName)) = '' or LTrim(RTrim(@strFirstName)) = '' ))
	Begin

		Set @nRandomValue = ABS(CHECKSUM(NewId())) % 500

		-- Вставка тестовых данных
		if not exists (Select 1 from [tblMainForm] where LastName = @strLastName and FirstName = @strFirstName)
		Begin
			Insert into [dbo].[tblMainForm]([FirstName], [LastName], [Bithday], [Phone])
			Values( @strFirstName, @strLastName, DateAdd(DAY, @nRandomValue * (-1), GetDate()), '+7' + CAST(@nRandomValue as varchar(255)))
		End

		Set @nCurrentItem = @nCurrentItem + 1
	End
End

-- Проверка
if not exists(Select 1 from [dbo].[tblMainForm])
	RAISERROR('Данные не сформирован!', 16, 1)

