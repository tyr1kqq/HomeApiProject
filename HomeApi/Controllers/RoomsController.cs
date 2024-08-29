using System.Threading.Tasks;
using AutoMapper;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;
        
        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //TODO: Задание - добавить метод на получение всех существующих комнат
        
        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost] 
        [Route("")] 
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }
            
            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateRoom(string name, [FromBody] UpdateRoomRequest request)
        {
            // Ищем существующую комнату по имени
            var room = await _repository.GetRoomByName(name);
            if (room == null)
            {
                return NotFound($"Комната с именем '{name}' не найдена.");
            }

            // Обновляем свойства комнаты
            room.Area = request.Area ?? room.Area;
            room.Voltage = request.Voltage ?? room.Voltage;
            room.GasConnected = request.GasConnected ?? room.GasConnected;
            room.WaterConnected = request.WaterConnected ?? room.WaterConnected;

            // Сохраняем изменения
            await _repository.UpdateRoom(room);

            return Ok($"Комната '{name}' успешно обновлена.");
        }


    }
    public class UpdateRoomRequest
    {
        public int? Area { get; set; }
        public int? Voltage { get; set; }
        public bool? GasConnected { get; set; }
        public bool? WaterConnected { get; set; }
    }
}