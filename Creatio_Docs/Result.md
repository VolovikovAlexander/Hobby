## Ответы на вопросы тестового задания
Редакция: **2022-07-22**

1. Используя документацию , переделать SQL-запрос в esq (EntitySchemaQuery) C#.
Дан следующий пример:
```sql
Select  sur.Name
From SysAdminUnit sau WITH(NOLOCK)
Left join SysUserRole sur
On sau.Id= sur.SysUserRoleId
```

Ответ: Согласно документации,  стр 343 - 399, система Creatio работает с запросами посредством вызова
собственных, отдельных методов. Так как, у меня нет примера перед глазами и главное, **опыта** работы. Попробую написать.
[Так же, попросим Google мне помочь с примерами](https://github-wiki-see.page/m/Academy-Creatio/TrainingProgramm/wiki/Overlapping-Activities)

```csharp

 /// <summary>
 /// Получить список наименований пользователей, которые имеют права администратора. 
 /// </summary>
public IList<string?> GetUserRolesWithEsq()
{
    var manager = SystemUserConnection.EntitySchemaManager;
    var sysAdminUnitSchema = manager.GetInstanceByName("SysAdminUnit") as EntitySchema;
    var sysAdminQuery = new EntitySchemaQuery(sysAdminUnitSchema);
    
    var sysUserRoleSchema = manager.GetInstanceByName("SysUserRole") as EntitySchema;
    var sysUserRoleQuery = new EntitySchemaQuery(sysUserRoleSchema);
    
    var sysAdminResult = sysAdminQuery.GetEntityCollection(UserConnection);
    var sysUserRoleResult = sysUserRoleQuery.GetEntityCollection(UserConnection);
    
    return CompleteQueriesForGetUserRoles(sysAdminResult, sysUserRoleResult,
                (rootItem, childItems) => rootItem.Id == childItem.SysUserRoleId ? childItem.Name : null);
 }   
 
 
 /// <summary>
 /// Универсальный метод для сопоставления данных из выборок EntitySchemaQuery
 /// </summary>
 private IList<string?> CompleteQueriesForGetUserRoles(object? rootTable, object? childTable, Func<object, object, string?> action)
 {
    var result = new List<string?>();
    foreach(var rootItem in rootTable)
    {
        var resultItem = null;
        foreach(var childItem in  childTable)
        {
             resultItem = action(rootItem, childItem);
        }
        
        result.Add(resultItem);
    }
 }
```

### Комментарий. 
Данный запрос в Creatio с использованием методов     **EntitySchemaQuery** крайне не оптимальный т.к. приходится по сути делать две выборки.
Тащить данные с сервера и уже в Middleware данные объединять. Если других вариантов нет, то лучше не использовать EntitySchemaQuery или получить матереализацию данных
и сделать объединение с помощью распараллеливания обхода циклов.  

2. Используя документацию найти и описать, как отправить сообщения, через механизм сообщений(PUBLISH) ext.js.

Ответ. Согласно документации, (3.4.1.4.1 Обмен сообщениями между модулями) нужно в дизайнере сконфигурировать сообщения. Далее, вызвать событие с нужным сообщением и наконец (в другом  конце тунеля)
обработать это сообщение. Пример из документации. Так же, зарегистрировать типы сообщение можно через ExtJs вызов - "на лету"

````csharp
// Объявление и регистрация сообщения.
var messages = {
"MessageWithResult": {
mode: Terrasoft.MessageMode.PTP,
direction: Terrasoft.MessageDirectionType.SUBSCRIBE
}
};
this.sandbox.registerMessages(messages);
// Подписка на сообщение.
this.sandbox.subscribe("MessageWithResult", this.onMessageSubscribe, this,
["resultTag"]);
...
// Метод-обработчик реализован в модуле-подписчике.
// args — объект, передаваемый при публикации сообщения.
onMessageSubscribe: function(args) {
// Изменение параметра.
args.arg1 = 15;
args.arg2 = "new arg2";
// Обязательный возврат результата.
return args;
},
````
3. Описать, что делает данная конструкция

```csharp
this.sandbox.subscribe("NewUserSet", this.onNewUserSet, this);
```

Ответ. Это метод в ExtJs с помощью которого можно зарегистрировать обработчик на подписку с именем **NewUserSet**

4. Что такое this, в ext.js

Ответ. Насколько я помню, this - это перегруженный класс, от типового js. В нем будет содержаться контекст текущего класса. Если ты вызываешь метод 
класса из другого класса, то this будет контекстом НЕ объекта вызывающего класса, а будет контекстом класса метода. Однако, возможно - это не так. ExtJs имеет несколько версий. 
Данное поведение можно исправить. Ну и наконец, я просто могу ошибаться.

5. Что описано в данном коде IAppEventListener

```csharp
public class UsrSingleRequestListener: IAppEventListener
{
#region Methods: Public
    public void OnAppStart(AppEventContext context) {
        ClassFactory.Bind<SingleRequest, UsrSingleRequest>();
    }
    public void OnAppEnd(AppEventContext context) {
    }
    public void OnSessionStart(AppEventContext context) {
    }
    public void OnSessionStart(AppEventContext context) {
    }
#endregion
}
```

Ответ. В данном курсе кода описан класс, который реализует интерфейс IAppEventListener. Согласно документации, стр 1160
Это дополнительная реализация, которая подключает к приложение допполнительную обработку событий. Класс UsrSingleRequest - 
реализация Custom механизма для поиска и исключения дублей. В примере, в документации, испольуется ИНН для поиска дублей.

6. Что хранит в UserConnection и SystemUserConnection

Ответ. Не нашел в документации. Полагаю, что это объект в котором включена информация пользователя о подключение и правах.
SystemUserConnection - [это какой-то особый пользователь / права](https://customerfx.com/article/getting-the-current-user-in-bpmonline/)

7. Зачем в архитектуре приложения использовать redis.

Ответ. Согласно описанию, стр. 33 Redis используется для организации кеширования. 

8. Что будет если передать null в journal?

```csharp
if (journal.isCheckListFailed == true)
{
    response.Result = ServiceResult.Error;
    response.ErrorList = new List<ErrorList>
    {
        new ErrorList{
        Error = new ErrorItem
        {
            ErrorCode = "ChecklistFailed",
            ErrorDesc = "Checklist check is failed"
        }
    }
}
```

Ответ. C# при компиллировании выдаст Warning. В Runtime выдет выдано исключение [NullPointerException](https://learn.microsoft.com/ru-ru/dotnet/api/system.nullreferenceexception?view=net-6.0)
Чтобы этого избежать, достаточно немного подправить код. Например так:

```csharp
if ( journal?.isCheckListFailed ?? false )
{
    response.Result = ServiceResult.Error;
    response.ErrorList = new List<ErrorList>
    {
        new ErrorList{
        Error = new ErrorItem
        {
            ErrorCode = "ChecklistFailed",
            ErrorDesc = "Checklist check is failed"
        }
    }
};
```
9.Получите результат Select используя документацию в любой вид
объекта , чтобы закончить код.

```csharp
UserConnection userConnection = Get<UserConnection>("UserConnection");
List<LogResultMonCRMCB> LogResultList = new List<LogResultMonCRMCB>();
var currentTime = DateTime.Now;
var lastTime = currentTime.AddHours(-2);
var select = new Select(userConnection)
    .Column("CreatedOn")
    .Column("NorbitEntityName")
    .Column("NorbitCorrelationId")
    .Column("NorbitDirection")
    .Column("NorbitQueue")
    .Column("NorbitMessage")
    .Column("NorbitErrorText")
    .Column("NorbitStatusCode")
    .From("NorbitIntegrationLog")
    .Where("NorbitStatusCode").IsEqual(Column.Parameter("ERROR"))
    .And("NorbitErrorText").IsLike(Column.Parameter("System.Exception: City by column NrbCpCrIntegrationId%"))
    .And("CreatedOn").IsGreaterOrEqual(Column.Parameter(lastTime)) as Select;
```

Ответ. Необходимо выполнить "виртуальный" запрос. Можно так (стр 330) 
Обратите внимание, что в приведенном примере используется приведение **AS Select**. Поэтому переменная может иметь Nullable тип.

```csharp
using var dbExecutor = UserConnection.EnsureDBConnection();
using var dataReader = select?.ExecuteReader(dbExecutor) ?? throw new InvalidOperationException("Пустой объект типа Select!");
while (dataReader.Read()) 
{
    //...
}
```

10.Опишите верхнеуровнево, что делает данный код JS, используя
документацию.

```javascript
init: function(callback, scope) {
this.callParent(arguments);
this.set("IsAddRecordButtonVisible", false);
this.set("CombinedModeActionsButtonVisible", false);
this.set("SeparateModeActionsButtonVisible", false);

ServiceHelper.callService(
    "RshbRequestOpenService",
    "StartSearchNewRequestOpen",
    function (response) {
        this.console.log(response);
        if (response && response.StartSearchNewRequestOpenResult !==
        Terrasoft.GUID_EMPTY){
            const activeRow =
                this.getActiveRow(response.StartSearchNewRequestOpenResult);
            
            ProcessModuleUtilities.executeProcess({
                sysProcessName: activeRow.$RshbApplicationOrigin.value
                            === RshbDigitalClientConstants.RshbApplicationOrigin.Msp
                                    ? "RshbCreateRequestOpenAccountProcessForMsp"
                                    : "RshbCreateRequestOpenAccountProcess" ,
                parameters: {
                    RequestOpenId: response.StartSearchNewRequestOpenResult
                    }
                });
        }
    },
    null,
    this
);
```

Ответ. Чисто гадаю, потому что нормально описать, что тут делается без контекста - не возможно)
Вызываем метод StartSearchNewRequestOpen, класса RshbRequestOpenService. В качестве параметра методу передается action который обрабатывает ответ от сервера.
Далее, если ответ валиден и существует, запускается постобработка данных. Происходит какой-то поиск данных.
В рамках этого поиска идет сравнение полученных данных с данными, которые нахожятся в контексте текущего класса
и происходит в разрезе каждого элемента запуск обработчика **RshbCreateRequestOpenAccountProcessForMsp** или **RshbCreateRequestOpenAccountProcess**.


