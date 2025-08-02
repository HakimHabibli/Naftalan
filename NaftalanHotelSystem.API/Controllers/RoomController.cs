using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.API.ModelBinders;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.DataTransferObject.Room;
using Newtonsoft.Json;

namespace NaftalanHotelSystem.API.Controllers;


[Route("api/[controller]")] 
[ApiController]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

 
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromForm] BinableRoomCreateDto binableDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

     
        List<RoomTranslationCreateDto> translations;
        try
        {
            translations = JsonConvert.DeserializeObject<List<RoomTranslationCreateDto>>(binableDto.Translations);
            if (translations == null)
            {
                return BadRequest("Translations JSON boş və ya formatı düzgün deyil.");
            }
        }
        catch (Exception ex)
        {
          
            return BadRequest($"Tərcümələr JSON formatında xəta: {ex.Message}");
        }

      
        var roomCreateDto = new RoomCreateDto
        {
            Category = binableDto.Category,
            Area = binableDto.Area,
            Price = binableDto.Price,
            Member = binableDto.Member,
            YoutubeVideoLink = binableDto.YoutubeVideoLink,
            Translations = translations, 
            EquipmentIds = binableDto.EquipmentIds,
            ChildIds  = binableDto.ChildIds,    
            ImageFiles = binableDto.ImageFiles 
        };

        try
        {
            await _roomService.CreateRoomAsync(roomCreateDto);
            return StatusCode(StatusCodes.Status201Created, "Otaq uğurla yaradıldı.");
        }
        catch (Exception ex)
        {
           
            Console.WriteLine($"Error in Create Room: {ex.Message}"); 
            return StatusCode(StatusCodes.Status500InternalServerError, $"Otaq yaradılanda xəta baş verdi: {ex.Message}");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoomGetDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms); // 200 OK
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in Get All Rooms: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Məlumatlar yüklənərkən xəta baş verdi: {ex.Message}");
        }
    }


    // --- GET ROOM BY ID ---
    /// <summary>
    /// Verilən ID-yə görə bir otağı geri qaytarır.
    /// </summary>
    /// <param name="id">Otağın ID-si.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomGetDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                return NotFound($"ID {id} olan otaq tapılmadı."); // 404 Not Found
            }
            return Ok(room); // 200 OK
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in Get Room By Id: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Məlumatlar yüklənərkən xəta baş verdi: {ex.Message}");
        }
    }

    // --- UPDATE ROOM ---
    /// <summary>
    /// Mövcud otağı yeniləyir.
    /// Translations JSON string olaraq, şəkillər FromForm olaraq gəlməlidir.
    /// </summary>
    /// <param name="id">Yenilənəcək otağın ID-si.</param>
    /// <param name="binableDto">Yenilənmiş otağın məlumatları və faylları.</param>
    [HttpPut("{id}")]
    [Consumes("multipart/form-data")] // Fayl yükləmə dəstəyi üçün
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromForm] BinableRoomUpdateDto binableDto) // <-- Burda BinableRoomUpdateDto istifadə edildi
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // URL-dəki ID ilə DTO-dakı ID-nin uyğunluğunu yoxlayın
        if (id != binableDto.Id)
        {
            return BadRequest("URL-dəki ID ilə göndərilən məlumatdakı ID uyğun gəlmir.");
        }

        // Translations JSON string-dən List<RoomTranslationUpdateDto>-ya çevrilir
        List<RoomTranslationUpdateDto> translations;
        try
        {
            translations = JsonConvert.DeserializeObject<List<RoomTranslationUpdateDto>>(binableDto.Translations);
            if (translations == null)
            {
                return BadRequest("Translations JSON boş və ya formatı düzgün deyil.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Tərcümələr JSON formatında xəta: {ex.Message}");
        }

        // RoomService-ə ötürüləcək əsl DTO-nu yaradın
        var roomUpdateDto = new RoomUpdateDto
        {
            Id = binableDto.Id,
            Category = binableDto.Category,
            Area = binableDto.Area,
            Price = binableDto.Price,
            Member = binableDto.Member,
            YoutubeVideoLink = binableDto.YoutubeVideoLink,
            Translations = translations, // Deserializasiya edilmiş tərcümələr
            EquipmentIds = binableDto.EquipmentIds,
            NewImageFiles = binableDto.NewImageFiles // Yeni şəkil faylları
            ,ChildIds = binableDto.ChildIds,
            
        };

        try
        {
            await _roomService.UpdateRoomAsync(id, roomUpdateDto); // <-- UpdateRoomAsync çağırılır
            return NoContent(); // 204 No Content
        }
        catch (Exception ex) when (ex.Message.Contains("Otaq tapılmadı"))
        {
            return NotFound(ex.Message); // 404 Not Found
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in Update Room: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Otaq yenilənərkən xəta baş verdi: {ex.Message}");
        }
    }

    // --- DELETE ROOM ---
    /// <summary>
    /// Verilən ID-yə görə otağı silir.
    /// </summary>
    /// <param name="id">Silinəcək otağın ID-si.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _roomService.DeleteRoomAsync(id);
            return NoContent(); // 204 No Content
        }
        catch (Exception ex) when (ex.Message.Contains("Otaq tapılmadı"))
        {
            return NotFound(ex.Message); // 404 Not Found
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in Delete Room: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Otaq silinərkən xəta baş verdi: {ex.Message}");
        }
    }
}
