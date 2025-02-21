namespace ItemParserWorker.Entities;

public class RegGroup
{
    public string Content { get; set; } = "";
    
    public bool IsParticipating { get; set; }
    
    public int GroupNum { get; set; }
    
    public int StartPos { get; set; }
    
    public int EndPos { get; set; }
}