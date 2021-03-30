-- Инициализация базы данных
-- 1 Справочник пунктов приема
Insert into refReceptionPoints(Description) Values('Окно приема 1');
Insert into refReceptionPoints(Description) Values('Окно приема 2');
Insert into refReceptionPoints(Description) Values('Окно приема 3');

-- 2. Справочник услуг
Insert into refServices(Description, TimeLimit) Values('Услуга 1 (5 мин)', 5);
Insert into refServices(Description, TimeLimit) Values('Услуга 2 (7 мин)', 7);
Insert into refServices(Description, TimeLimit) Values('Услуга 3 (10 мин)', 10);
Insert into refServices(Description, TimeLimit) Values('Услуга 4 (15 мин)', 15);

-- 3. Связка пункт приема и услуга
-- Первый
Insert into tblPointsToServices(PointID, ServiceID) Values(1,1);
Insert into tblPointsToServices(PointID, ServiceID) Values(1,3);
Insert into tblPointsToServices(PointID, ServiceID) Values(1,4);
-- Второй
Insert into tblPointsToServices(PointID, ServiceID) Values(2,1);
Insert into tblPointsToServices(PointID, ServiceID) Values(2,2);
Insert into tblPointsToServices(PointID, ServiceID) Values(2,3);
-- Третий
Insert into tblPointsToServices(PointID, ServiceID) Values(3,2);
Insert into tblPointsToServices(PointID, ServiceID) Values(3,3);
Insert into tblPointsToServices(PointID, ServiceID) Values(3,4);

-- Записи в первичный журнал. Сюда фиксируем то, когда 
-- пользователь хочет получить услуги

-- Первая точка
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(1,3,'2020-11-25 09:00',0);
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(1,2,'2020-11-25 09:30',0);

-- Вторая точка
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(2,2,'2020-11-25 09:30',0);
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(2,2,'2020-11-25 09:40',0);
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(2,3,'2020-11-25 09:42',0);
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(2,1,'2020-11-25 10:20',0);

-- Третья точка
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(3,2,'2020-11-25 09:30',0);
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(3,4,'2020-11-25 20:30',0);
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(3,1,'2020-11-25 14:00',0);







