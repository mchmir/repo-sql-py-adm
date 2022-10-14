/**
 * Хочу сказать по стилю кода: как-то уж так сложилось,
 * что я ключевые слова пишу строчными буквами для Informix, PostgreSQL, MySQL
 * а ПРОПИСНЫми пишу ключевые слова для MS SQL.
 * Как-то это сложилась в больших командах, чтобы отличать код по стилю.
 * 
 * поэтому вот такой стиль.
*/


-- Task #1
/**
 * Напишите запрос, который выберет из таблицы orderdetails топ-10 строк
 * с максимальной общей стоимостью конкретного товара в рамках заказа.
 * Для каждой строки выведите номер заказа, артикул товара и общую стоимость товара
 * в заказе.
*/
select
       od.orderNumber as 'номер заказа',
       od.productCode as 'артикул товара',
       (od.quantityOrdered * od.priceEach) as 'общая стоимость товара в заказе'
  from orderdetails as od
 order by (od.quantityOrdered * od.priceEach) desc
       limit 10;



-- Task #2
/**
 * Напишите запрос, который вернёт только те заказы,
 * итоговая стоимость которых превышает 59000 долларов.
*/
select
       od.orderNumber as 'номер заказа',
       sum(od.quantityordered * od.priceeach) as 'общая стоимость товара в заказе'
  from orderdetails as od
 group by od.ordernumber
having sum(od.quantityordered * od.priceeach) > 59000
 order by sum(od.quantityordered * od.priceeach) desc;



-- Task #3
/**
 * Напишите запрос, который вернёт только те заказы, 
 * итоговая стоимость которых превышает 59 000 долларов.
 * + узнать дату, когда эти заказы были сделаны, и статусы, в которых они находятся.
*/
select
       ord.orderNumber     as 'Номер заказа',
       ord.orderDate       as 'Дата заказа',
       ord.status          as 'Статус заказа',
       sub_more59.subTotal as 'Сумма свыше $59 тыс.'
  from orders as ord
    inner join 
       (select
               od.orderNumber as orderNumberSub,
               sum(od.quantityOrdered * od.priceEach) as subTotal
          from orderdetails as od
         group by od.orderNumber
        having sum(od.quantityOrdered * od.priceEach) > 59000
       ) as sub_more59 on ord.orderNumber = sub_more59.orderNumberSub
 order by sub_more59.subTotal desc;



-- Task #4
/**
 * Напишите запрос, который вернёт только те заказы, 
 * итоговая стоимость которых превышает 59 000 долларов.
 * + узнать дату, когда эти заказы были сделаны, и статусы, в которых они находятся.
 * + кто сделал эти заказы
*/
select
       c.contactFirstName  as 'Имя клиента',
       c.contactLastName   as 'Фамилия клиента',
       c.country           as 'Страна',
       ord.orderNumber     as 'Номер заказа',
       ord.orderDate       as 'Дата заказа',
       ord.status          as 'Статус заказа',
       sub_more59.subTotal as 'Сумма свыше $59 тыс.'
  from customers as c
    inner join orders as ord on ord.customerNumber = c.customerNumber 
    inner join
       (select
               od.orderNumber as orderNumberSub,
               sum(od.quantityOrdered * od.priceEach) as subTotal
          from orderdetails as od
         group by od.orderNumber
        having sum(od.quantityOrdered * od.priceEach) > 59000
       ) as sub_more59 on ord.orderNumber = sub_more59.orderNumberSub
 order by sub_more59.subTotal desc;



 -- Task #5
/**
 * топ-10 названий (productName) товаров, на которые было потрачено больше всего денег  
*/
select
       p.productName as 'Название продукта',
       sum(od.quantityordered * od.priceeach) as 'общая стоимость товара в заказе'
  from products as p
    inner join orderdetails as od on p.productCode = od.productCode
 group by p.productName
 order by sum(od.quantityordered * od.priceeach) desc
       limit 10;



-- Task #6.1
/**
 * Сотрудников, которые обслуживали клиентов.
*/
select distinct
       emp.firstName as 'Имя сотрудника',
       emp.lastName  as 'Фамилия сотрудника'
  from employees as emp
    inner join customers as c on c.salesRepEmployeeNumber = emp.employeeNumber;

-- Task #6.2
/**
 * Клиенты, которые были обслужены сотрудниками.
*/
select distinct
       c.contactFirstName as 'Имя клиента',
       c.contactLastName  as 'Фамилия клиента'
  from employees as emp
    inner join customers as c on c.salesRepEmployeeNumber = emp.employeeNumber;

-- Task #6.3
/**
 * Сотрудники, которые никого ещё не обслуживали
*/
select distinct
       emp.firstName as 'Имя сотрудника',
       emp.lastName  as 'Фамилия сотрудника' 
  from employees as emp
    left join customers as c on c.salesRepEmployeeNumber = emp.employeeNumber
 where c.salesRepEmployeeNumber is null;

-- Task #6.4
/**
 * Клиенты, которых никто пока не обслужил.
 *
 * Хочу пояснить, про не использование RIGHT join:
 *  - на приактике это считается не очень хорошим стилем,
 *  - мы просто делаем LEFT и меняем таблицы местами
*/
select distinct
       c.contactFirstName as 'Имя клиента',
       c.contactLastName  as 'Фамилия клиента'
  from customers as c
    left join employees as emp on c.salesRepEmployeeNumber = emp.employeeNumber
 where emp.employeeNumber is null;

-- Task #6.5
/**
 * СУБД MySQL не поддерживает FULL JOIN в явном виде, 
 * Напишите запрос, который реализует FULL JOIN для таблиц employees и customers 
 * и выводит имена и фамилии сотрудников (firstName и lastName) 
 * и клиентов (contactFirstName и contactLastName). 
 *
 * FULL JOIN = LEFT JOIN + RIGHT JOIN (UNOIN уберет дубликаты)
*/
select 
       emp.firstName      as 'Имя сотрудника',
       emp.lastName       as 'Фамилия сотрудника',
       c.contactFirstName as 'Имя клиента',
       c.contactLastName  as 'Фамилия клиента'
  from customers as c
    left join employees as emp on c.salesRepEmployeeNumber = emp.employeeNumber

UNION

select 
       emp.firstName      as 'Имя сотрудника',
       emp.lastName       as 'Фамилия сотрудника',
       c.contactFirstName as 'Имя клиента',
       c.contactLastName  as 'Фамилия клиента'
  from customers as c
    right join employees as emp on c.salesRepEmployeeNumber = emp.employeeNumber;



-- Task #7
/**
 * Руководители и их подчиненные
*/
select 
       emp.firstName     as 'Имя сотрудника',
       emp.lastName      as 'Фамилия сотрудника',
       emp.jobTitle      as 'Должность',   
       sub_emp.firstName as 'Имя подчиненного сотрудника',
       sub_emp.lastName  as 'Фамилия подчиненного сотрудника'
  from  employees as emp
    left join employees as sub_emp on emp.employeeNumber = sub_emp.reportsTo;

