-- ������������� ���� ������
-- 1 ���������� ������� ������
Insert into refReceptionPoints(Description) Values('���� ������ 1');
Insert into refReceptionPoints(Description) Values('���� ������ 2');
Insert into refReceptionPoints(Description) Values('���� ������ 3');

-- 2. ���������� �����
Insert into refServices(Description, TimeLimit) Values('������ 1 (5 ���)', 5);
Insert into refServices(Description, TimeLimit) Values('������ 2 (7 ���)', 7);
Insert into refServices(Description, TimeLimit) Values('������ 3 (10 ���)', 10);
Insert into refServices(Description, TimeLimit) Values('������ 4 (15 ���)', 15);

-- 3. ������ ����� ������ � ������
-- ������
Insert into tblPointsToServices(PointID, ServiceID) Values(1,1);
Insert into tblPointsToServices(PointID, ServiceID) Values(1,3);
Insert into tblPointsToServices(PointID, ServiceID) Values(1,4);
-- ������
Insert into tblPointsToServices(PointID, ServiceID) Values(2,1);
Insert into tblPointsToServices(PointID, ServiceID) Values(2,2);
Insert into tblPointsToServices(PointID, ServiceID) Values(2,3);
-- ������
Insert into tblPointsToServices(PointID, ServiceID) Values(3,2);
Insert into tblPointsToServices(PointID, ServiceID) Values(3,3);
Insert into tblPointsToServices(PointID, ServiceID) Values(3,4);

-- ������ � ��������� ������. ���� ��������� ��, ����� 
-- ������������ ����� �������� ������

-- ������ �����
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(1,3,'2020-11-25 09:00',0);
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(1,2,'2020-11-25 09:30',0);

-- ������ �����
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(2,2,'2020-11-25 09:30',0);
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(2,2,'2020-11-25 09:40',0);
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(2,3,'2020-11-25 09:42',0);
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(2,1,'2020-11-25 10:20',0);

-- ������ �����
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(3,2,'2020-11-25 09:30',0);
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(3,4,'2020-11-25 20:30',0);
Insert into tblJournalOfReception(PointID, ServiceID, StartPeriod, IsPassed)
Values(3,1,'2020-11-25 14:00',0);







