﻿// Home work 3. Delegates, events and exceptions

/*
    Зачем нужно отлавливать исключения?

    Есть блок try-catch-finally, которая необходима для того, чтобы отлавливать возможные ошибки там,
    где они могут произойти, то есть в неопределенных местах кода.
    
    В блоке try выполняется необходимый код
        ЕСЛИ блок try выполнился без ошибок,
            то (при наличии блока finally) попадаем в блок finally и выполняем еще какую-то логику.
        ИНАЧЕ, если блок try отвалился,
            то (при наличии блока catch) попадаем в блок catch и обрабатываем исключение.
            также после catch может выполниться блок finally

    Например, есть код:
*/
public class Player
{
    [SerializedField] private GameObject _playerPrefab;
    
    // ...

    if (_playerPrefab != null) 
    {
        // Выполняется какая-то логика
    }
    else
    {
        // Например в качестве сообщения исключения пишем своё, чтобы понять из-за чего не работает.
        // По хорошему конечно drag-and-drop'ом лучше не делать, а указывать через код,
        // но для примера подойдёт.
        throw new Exception("В _playerPrefab не указан объект. Кто-то что-то забыл перетащить. Проверь это.");
    }
}

// Еще пример:
try
{
    int x = 5;
    int y = x / 0;
    Console.WriteLine($"Результат: {y}");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка при делении! {ex.Message}");
}
finally
{
    // Выполнение дополнительной логики. Попадем после блока try или catch
    Console.WriteLine("Доп. логика");
}

/*
    Использование блока try-catch в unity не желательно, так как она дорога в использовании.
    При необходимости использования, ставить блок в неопределенных местах кода и записывать исключения в логи
*/


