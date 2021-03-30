
-- Проверка данных
Select * from [dbo].[tblMainForm_SelectionResult] as t1
left join [dbo].[refColors] as t2 on t1.ColorID = t2.ID
left join [dbo].[refDrinks] as t3 on t3.ID = t1.DrinkID
inner join [dbo].[tblMainForm] as t4 on t4.ID = t1.MainID

