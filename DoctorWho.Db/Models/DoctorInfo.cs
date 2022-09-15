namespace DoctorWho.Db.Models;

public class DoctorInfo
{
    public int DoctorId { get; set; }
    public int DoctorNumber { get; set; }
    public string DoctorName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime FirstEpisodeDate { get; set; }
    public DateTime LastEpisodeDate { get; set; }
}