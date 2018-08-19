using System;

namespace SimpleFullTextSearcher.Extensions
{
    /// <summary>
    /// Методы расширения для типа long
    /// </summary>
    public static class LongExtension
    {
        /// <summary>
        /// Переводит значение миллисекунд типа long в строковое представление, содержащее дни, часы, минуты, секунды и миллесекунды
        /// </summary>
        /// <param name="l">Значение в миллисекундах</param>
        /// <returns>Возвращает значение в виде строки, представляющей полное время (в днях, часах, минутах, секундах и миллесекундах) из миллисекунд</returns>
        public static string MillisecondsToTimeString(this long l)
        {
            var ts = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(l));
            return $"{ts.Days} д, {ts.Hours} ч, {ts.Minutes} м, {ts.Seconds} с, {ts.Milliseconds} мс";
        }
    }
}
