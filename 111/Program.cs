using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        // Установка желаемого времени включения и выключения компьютера
        int turnOnHour = 12;
        int turnOnMinute = 0;
        int turnOffHour = 12;
        int turnOffMinute = 30;

        // Получение текущего времени
        DateTime currentTime = DateTime.Now;

        // Установка времени включения для сегодня
        DateTime turnOnTimeToday = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, turnOnHour, turnOnMinute, 0);

        // Установка времени выключения для сегодня
        DateTime turnOffTimeToday = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, turnOffHour, turnOffMinute, 0);

        // Проверка, прошло ли время включения сегодня
        if (currentTime > turnOnTimeToday)
        {
            // Если да, установите время включения на следующий день
            turnOnTimeToday = turnOnTimeToday.AddDays(1);
        }

        // Проверка, прошло ли время выключения сегодня
        if (currentTime > turnOffTimeToday)
        {
            // Если да, установите время выключения на следующий день
            turnOffTimeToday = turnOffTimeToday.AddDays(1);
        }

        // Вычисление времени задержки до времени включения
        TimeSpan turnOnDelay = turnOnTimeToday - currentTime;

        // Вычисление времени задержки до времени выключения
        TimeSpan turnOffDelay = turnOffTimeToday - currentTime;

        // Вычисление задержки в миллисекундах
        int turnOnDelayMilliseconds = (int)turnOnDelay.TotalMilliseconds;
        int turnOffDelayMilliseconds = (int)turnOffDelay.TotalMilliseconds;

        // Планирование задачи включения компьютера
        Timer turnOnTimer = new Timer(TurnOnComputer, null, turnOnDelayMilliseconds, Timeout.Infinite);

        // Планирование задачи выключения компьютера
        Timer turnOffTimer = new Timer(TurnOffComputer, null, turnOffDelayMilliseconds, Timeout.Infinite);

        // Ожидание завершения программы
        Console.ReadLine();
    }

    static void TurnOnComputer(object state)
    {
        // Использование команды «shutdown» для включения компьютера
        Process.Start("shutdown", "/s /t 0");
    }

    static void TurnOffComputer(object state)
    {
        // Использование команды «shutdown» для выключения компьютера
        Process.Start("shutdown", "/r /t 0");
    }
}