using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeApi.Data.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Column("Id")]
        public Guid Id { get; set; } = Guid.NewGuid(); // Уникальный идентификатор комнаты

        [Column("AddDate")]
        public DateTime AddDate { get; set; } = DateTime.Now; // Дата добавления записи

        [Column("Name")]
        public string Name { get; set; } // Имя комнаты

        [Column("Area")]
        public double? Area { get; set; } // Площадь комнаты в квадратных метрах

        [Column("Voltage")]
        public int? Voltage { get; set; } // Напряжение, допустимое для электрической сети в комнате

        [Column("GasConnected")]
        public bool? GasConnected { get; set; } // Подключена ли комната к газоснабжению

        [Column("WaterConnected")]
        public bool? WaterConnected { get; set; } // Подключена ли комната к водоснабжению
    }
}