use 
go

IF OBJECT_ID('dbo.ContractTest', 'U') IS NOT NULL
    DROP TABLE dbo.ContractTest;

create table dbo.ContractTest
(
    IDContract int         not null
            primary key,
    IDPerson   int,
    Account    varchar(50) not null,
    IDAddress  int,
    Surname    varchar(100),
    Name       varchar(100),
    Patronic   varchar(100)
)

go

-- файл на сервере на диске D
BULK INSERT dbo.ContractTest
FROM 'D:\Temp\exp1102.csv'
WITH (
    DATAFILETYPE = 'widechar',  -- Файл в формате UTF-16
    FIELDTERMINATOR = ';',
    ROWTERMINATOR = '\n',
    FIRSTROW = 1  -- 2 - Пропускаем заголовок
   -- CODEPAGE = 'ACP' -- Используем ANSI-кодировку
);

select * from CONTRACTTEST;

SELECT *
FROM Contract c
WHERE NOT EXISTS (
    SELECT 1 FROM ContractTest ct WHERE c.IDContract = ct.IDContract
);

SELECT IDContract FROM Contract
EXCEPT
SELECT IDContract FROM ContractTest;


INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (921801, 921417, N'0721015', 925103, N'ЖКХ Сапаров', N'Жамбулат', null);
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (921881, 921497, N'0731002', 925184, N'ЖКХ Есенбаева', N'Калиман', null);
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (921889, 921505, N'0731010', 925192, N'ЖКХ Буцына', N'Марина', N'Александровна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (921937, 921553, N'0731058', 925240, N'ЖКХ Суровядина', N'Наталья', N'Владимировна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (921939, 921555, N'0731060', 925242, N'ЖКХ Шамсутдинов', N'Вадим', N'Равилевич');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (921944, 921560, N'0731065', 925247, N'ЖКХ Шибучикова', N'Ботагоз', N'Балабаевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (921946, 921562, N'0731067', 925249, N'ЖКХ Калеганова', N'Олеся', N'Юрьевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (921973, 921589, N'1792050', 925276, N'Даулетова', N'Гульшат', N'Оразымбетовна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922133, 921749, N'0712005', 925438, N'ЖКХ ', N'', N'');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922136, 921752, N'0712008', 925441, N'ЖКХ Агатаев', N'Курмаш', N'Байсалович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922137, 921753, N'0712009', 925442, N'ЖКХ Гриднева', N'Елена', N'Николаевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922138, 921754, N'0712010', 925443, N'ЖКХ Бейсен', N'Толеген', N'Маратулы');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922139, 921755, N'0712011', 925444, N'ЖКХ Шафигуллин', N'Ренат', N'Раисович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922142, 921758, N'0712014', 925447, N'ЖКХ Федоровский', N'Андрей', N'Владимирович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922143, 921759, N'0712015', 925448, N'ЖКХ Калинина', N'Лариса', N'Николаевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922150, 921766, N'0712022', 925455, N'ЖКХ Яковенко', N'Наталья', N'Александровна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922152, 921768, N'0712024', 925457, N'ЖКХ Максимова', N'Мария', N'Сергеевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922153, 921769, N'0712025', 925458, N'ЖКХ Макишева', N'Зада', N'Жанайдаровна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922154, 921770, N'0712026', 925459, N'ЖКХ Гаврилова', N'Галина', N'Павловна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922157, 921773, N'0712029', 925462, N'ЖКХ Исмаилов', N'Данияр', N'Серикович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922159, 921775, N'0712031', 925464, N'ЖКХ Нашкенов', N'Дастен', N'Нурлубекович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922160, 921776, N'0712032', 925465, N'ЖКХ Тулегенов', N'Аскер', N'Досатаевич');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922164, 921780, N'0712036', 925469, N'ЖКХ ', N'', N'');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922165, 921781, N'0712037', 925470, N'ЖКХ Кыйлажова', N'Кульбан', N'Елюсизовна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922166, 921782, N'0712038', 925471, N'ЖКХ Бакубаева', N'Лязат', N'Макеновна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922172, 921788, N'0712044', 925477, N'ЖКХ Хамитова', N'Салтанат', N'Рамазановна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922173, 921789, N'0712045', 925478, N'ЖКХ Сабанеева', N'Ирина', N'Сергеевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922176, 921792, N'0712048', 925481, N'ЖКХ Войт', N'Леонид', N'Николаевич');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922177, 921793, N'0712049', 925482, N'ЖКХ Мазлова', N'Ольга', N'Викторовна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922178, 921794, N'0712050', 925483, N'ЖКХ Сединкина', N'Ольга', N'Викторовна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922179, 921795, N'0712051', 925484, N'ЖКХ Шонов', N'Ерик', N'Тулегенович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922185, 921801, N'0712057', 925490, N'ЖКХ Балгабаева', N'Дилором', N'Юнусхоновна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922190, 921806, N'0712062', 925495, N'ЖКХ Фатхулисламова', N'Райся', N'Рафасовна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922193, 921809, N'0712065', 925498, N'ЖКХ Малышева', N'Оксана', N'Юрьевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922194, 921810, N'0712066', 925499, N'ЖКХ Нургалиева', N'Жанара', N'Жумабаевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922196, 921812, N'0712068', 925501, N'ЖКХ Ильясова', N'Алина', N'Сергеевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922199, 921815, N'0712071', 925504, N'ЖКХ Жумагалин', N'Даурен', N'Жумагазинович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922202, 921818, N'0712074', 925507, N'ЖКХ Шапошник', N'Марина', N'Александровна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922207, 921823, N'0712079', 925512, N'ЖКХ Мукашева', N'Гульнара', N'Уахитовна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922210, 921826, N'0712082', 925515, N'ЖКХ Кононова', N'Валентина', N'Пантелеевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922211, 921827, N'0712083', 925516, N'ЖКХ Васильева', N'Валентина', N'Сергеевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922213, 921829, N'0712085', 925518, N'ЖКХ Валеева', N'Сабира', N'Мусагуловна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922216, 921832, N'0712088', 925521, N'ЖКХ Касенова', N'Алия', N'Амиржановна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922222, 921838, N'0712094', 925527, N'ЖКХ Ахмедьярова', N'Рысты', N'Калимолдиновна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922226, 921842, N'0712098', 925531, N'ЖКХ Келигов', N'Тимур', N'Владимирович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922227, 921843, N'0712099', 925532, N'ЖКХ Деньгаева', N'Анастасия', N'Дмитриевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922228, 921844, N'0712100', 925533, N'ЖКХ Крель', N'Виталий', N'Викторович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922234, 921850, N'0712106', 925539, N'ЖКХ Курочкин', N'Виталий', N'Олегович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922235, 921851, N'0712107', 925540, N'ЖКХ Тулегенова', N'Жибек', N'Маратовна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922236, 921852, N'0712108', 925541, N'ЖКХ Попова', N'Надежда', N'Сергеевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922244, 921860, N'0712116', 925549, N'ЖКХ Садвакасова', N'Гульжан', N'Серикбаевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922246, 921862, N'0712118', 925551, N'ЖКХ Мукаева', N'Диана', N'Канатовна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922250, 921866, N'0712122', 925555, N'ЖКХ Шарапова', N'Маргарита', N'Сергеевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922252, 921868, N'0712124', 925557, N'ЖКХ Дюсембина', N'Гульдана', N'Сайлауовна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922253, 921869, N'0712125', 925558, N'ЖКХ Белова', N'Светлана', N'Сергеевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922254, 921870, N'0712126', 925559, N'ЖКХ Омарова', N'Орынбасар', N'Заитовна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922256, 921872, N'0712128', 925561, N'ЖКХ Нестерова', N'Наталья', N'Владимировна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922385, 922001, N'0741001', 925691, N'ЖКХ', null, null);
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922387, 922003, N'0741003', 925693, N'Ведом кв ТОО ВИС', null, null);
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922400, 922016, N'0741016', 925706, N'ЖКХ', null, null);
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922403, 922019, N'0741019', 925709, N'Ведом кв РГУ "Департ. эконом. расслед. по СКО" Батаев', N'Талгат', N'Канатович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922404, 922020, N'0741020', 925710, N'Ведом кв РГУ "Департ. эконом. расслед. по СКО" Жармагамбетов', N'Тимур', N'Бугумбаевич');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922405, 922021, N'0741021', 925711, N'ЖКХ Куртаев', N'Тимур', N'Жомартович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922406, 922022, N'0741022', 925712, N'ЖКХ', N'', N'');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922407, 922023, N'0741023', 925713, N'ЖКХ Сенкебаев', N'Руслан', N'Исабекович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922417, 922033, N'0741033', 925723, N'ЖКХ', null, null);
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922418, 922034, N'0741034', 925724, N'ЖКХ', null, null);
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922419, 922035, N'0741035', 925725, N'ЖКХ Тилеген', N'Менер', N'Тилегенулы');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922421, 922037, N'0741037', 925727, N'ЖКХ Болат', N'Манас', N'Маратулы');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922422, 922038, N'0741038', 925728, N'Ведом кв РГУ "Департ. эконом. расслед. по СКО" Балагазин', N'Аскар', N'Алтаевич');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922423, 922039, N'0741039', 925729, N'ЖКХ Ризаханов', N'Бейбит', N'Болатович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922424, 922040, N'0741040', 925730, N'Ведом кв РГУ "Департ. эконом. расслед. по СКО" Исимбеков', N'Расул', N'Бауржанович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922438, 922054, N'0742014', 925744, N'Попов', N'Николай', N'Викторович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922448, 922064, N'0742024', 925754, N'Тугаев', N'Азамат', N'Саматович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922449, 922065, N'0742025', 925755, N'Молдашева', N'Сауле', N'Сериковна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922452, 922068, N'0742028', 925758, N'Антипина', N'Анастасия', N'Сергеевна');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922461, 922077, N'0742037', 925767, N'Ведом кв ТОО Retail Construction', null, null);
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922465, 922081, N'3331090', 925771, N'Аубакиров (кв 32-а)', N'Рустам', N'Муратович');
INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922466, 922082, N'5551036', 904130, N'Лацвиева', N'Александра', N'Алексеевна');

----

INSERT INTO dbo.Contract (IDContract, IDPerson, Account, IDAddress, Surname, Name, Patronic) VALUES (922465, 922081, N'3331090', 925771, N'Аубакиров (кв 32-а)', N'Рустам', N'Муратович');

INSERT INTO dbo.Address (IDAddress, IDTypeAddress, IDPlace, IDStreet, IDHouse, Flat) VALUES (925771, 1, 1, 68, 34019, N'');

select *
from Street where IDStreet = 68

select * from Address where IDAddress = 925771
select * from House
where IDHouse = 34019;



