namespace LogOT.Domain.Entities;

public class InterviewProcess
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int JobDescriptionId { get; set; }
    public string Info { get; set; }
    public string DayTime { get; set; }
    public string Place { get; set; }
    public bool Status { get; set; }
    public string FeedBack { get; set; }
    public string Result { get; set; }

    // Relationship
    public virtual JobDescription JobDescription { get; set; }

#warning uncomment later after merge branch
    //public virtual Employee Employee { get; set; }
}