using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariantC.TaskClasses;
using VariantB.DelegateEventSort;

namespace VariantB.Storage
{
    delegate Dictionary<string, Order> SortDelegate(Dictionary<string, Order> storage); // делегат сортировки.
    class OrderStorage : Storage // Коллекция всех заказов
    {
        private Dictionary<string, Order> _storage = new Dictionary<string, Order>(); // ключ- номер телефона заказчика, value - заказ
        public OrderStorage() // Конструктор без параметров
        {
            storageName = "B-52";
            storagePlace = "Харьков";
        }
        public void AddOrder(string phone, Order newOrder) // Добавить заказ, телефон-заказ
        {
            _storage.Add(phone, newOrder);
            var productEvent = new ProductEvent(); // Класс события.
            productEvent.AddProducts += ProductEvent.Message; // добавление метода к событию.
            productEvent.ProductsEvent(newOrder); // Событие.
        }
        public void RemoveOrder(string phone) // Выполнить заказ. удалить
        {
            var productEvent = new ProductEvent(); // Класс события.
            productEvent.AddProducts += ProductEvent.MessageDelete; // добавление метода к событию.
            productEvent.ProductsEvent(_storage[phone]); // Событие.
            _storage.Remove(phone);
        }
        public void RemoveAll() // Очистить список заказов
        {
            _storage.Clear();
        }
        public override int Count // Свойство количества элементов в хранилище заказов
        {
            get { return _storage.Count; }
        }
        public (string, Order) this[int index] // Индексатор словаря
        {
            get
            {
                return (_storage.ElementAt(index).Key, _storage.ElementAt(index).Value);
            }
            set
            {
                if(index < 0 || index > _storage.Count - 1)
                {
                    throw new ArgumentException($"{value}"); // Исключение.
                }
                else
                {
                    string tKEy = _storage.ElementAt(index).Key;
                    _storage.Remove(tKEy);
                    _storage.Add(value.Item1, value.Item2);
                }
            }
        }
        public IEnumerator GetEnumerator() // Итератор словаря
        {
            for (int i = 0; i < _storage.Count; i++)
            {
                yield return _storage.ElementAt(i);
            }
        }
        public void Sort(SortDelegate sort) // Сортировка по разным алгоритмам
        {
            _storage = sort(_storage);
        }

    }
}
