using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariantC.TaskClasses;
using VariantB.Storage;

namespace VariantC.Program
{
    static class Functions // Класс реализует функции с задания
    {
        public static void SearchOrdersWithSumAndCOuntOfProducts(OrderStorage orderList, int sum, int countProducts) // Вывести номера заказов, сумма которых не превосходит заданную и количество различных товаров равно заданному.
        {
            for (int i = 0; i < orderList.Count; i++) // перебирает все заказы
            {
                if (orderList[i].Item2.CountSumOfProducts() <= sum && orderList[i].Item2.ProductsInOrder.Count == countProducts)// если сумма в заказе меньше заданной и кол-во товаров == заданному
                {
                    Console.WriteLine(orderList[i].Item2.OrderNumber);// Вывести номер
                }
            }
        }
        public static void SearchThisProduction(OrderStorage orderList, string productName)// Вывести номера заказов, содержащих заданный товар.
        {
            for (int i = 0; i < orderList.Count; i++)// перебирает все заказы
            {
                for(int j = 0; j<orderList[i].Item2.ProductsInOrder.Count;j++) // Перебирает все товары в заказе
                {
                    if (orderList[i].Item2.ProductsInOrder[j].ProductIn.ProductName == productName)// если совпадаю имя
                    {
                        Console.WriteLine(orderList[i].Item2.OrderNumber);// Вывести номер
                    }
                }
            }
        }
        public static void SearchNotContainsProductAndToday(OrderStorage orderList, string productName, int day) //Вывести номера заказов, не содержащих заданный товар и поступивших в течение текущего дня.
        {
            bool isContains = false; // если заказы есть
            for (int i = 0; i < orderList.Count; i++)// перебирает все заказы
            {
                for (int j = 0; j < orderList[i].Item2.ProductsInOrder.Count; j++)// Перебирает все товары в заказе
                {
                    if (orderList[i].Item2.ProductsInOrder[j].ProductIn.ProductName == productName) //если такой товар есть
                    {
                        isContains = true;
                    }
                }
                if (!isContains && orderList[i].Item2.ReceiptDay.Date.Day == day)// если такого товара нет и день== заданному дню
                {
                    Console.WriteLine(orderList[i].Item2.OrderNumber);// Вывести номер
                }
                isContains = false;//сбросить
            }
        }
        public static Order CreateOrder(OrderStorage orderList, int day) //Сформировать новый заказ, состоящий из товаров, заказанных в текущий день.
        {
            List<ProductInOrder> productsOrder = new List<ProductInOrder>(); // для составления товаров список товаров
            for (int i = 0; i < orderList.Count; i++)// перебирает все заказы
            {
                if (orderList[i].Item2.ReceiptDay.Date.Day == day) // если нашел нужный день
                {
                    for (int j = 0; j < orderList[i].Item2.ProductsInOrder.Count; j++)// Перебирает все товары в заказе этого дня
                    {
                        productsOrder.Add(orderList[i].Item2.ProductsInOrder[j]); // и добавляет их в список товаров
                    }
                }
            }
            var newOrder = new Order(1000 + day, 17, productsOrder); // Создается новый заказ
            return newOrder;// Возвращается
        }
        public static void RemoveOrdersThisProductThisAmount(ref OrderStorage orderList, string productName, int amount)//Удалить все заказы, в которых присутствует заданное количество заданного товара.
        {
            int countOfProduct = 0; // считает кол-во заданного товара
            for (int i = 0; i < orderList.Count; i++)// перебирает все заказы
            {
                for (int j = 0; j < orderList[i].Item2.ProductsInOrder.Count; j++)// Перебирает все товары в заказе
                {
                    if (orderList[i].Item2.ProductsInOrder[j].ProductIn.ProductName == productName)// Если имена совпадают в этом заказе
                    {
                        countOfProduct++; // Увеличить счетчик
                    }
                }
                if (countOfProduct == amount) // Если счетчик заданного товара достиг заданного количества
                    orderList.RemoveOrder(orderList[i].Item1); // Удалить заказ
                countOfProduct = 0; // Сбросить счетчик
            }
        }
    }
}
