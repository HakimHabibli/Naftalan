namespace NaftalanHotelSystem.Application.DataTransferObject.Contact;

public abstract class ContactBaseDto 
{
    public List<string> Number { get; set; }
    public string Mail { get; set; }
    public string Adress { get; set; }
    public string InstagramLink { get; set; }
    public string FacebookLink { get; set; }
    public string TiktokLink { get; set; }
    public string YoutubeLink { get; set; }
    public string WhatsappNumber { get; set; }
} 