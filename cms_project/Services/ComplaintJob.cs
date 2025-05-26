using cms_project.Data;
using cms_project.Migrations;
using cms_project.Models.Entites;
using Microsoft.EntityFrameworkCore;

public class ComplaintJob
{
    private readonly ApplicationDbContext _context;

    public ComplaintJob(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CloseResolvedComplaints()
    {
        var now = DateTime.UtcNow;
        var histories = new List<ComplaintHistory>();


        var complaintsToClose = _context.Set<Complaint>().Include(x=>x.ComplaintHistories)
            .Where(c => c.StatusId == 4 && EF.Functions.DateDiffDay(c.ComplaintHistories.FirstOrDefault(x=>x.ActionStatus == "Resolved").CreatedDate, now) >= 5)
            .ToList();

        foreach (var complaint in complaintsToClose)
        {
            complaint.StatusId = 5;
            histories.Add(new ComplaintHistory(complaint.Id, "Closed", "Closed by system after 5 days from resolve", "System"));
        }
        _context.AddRange(histories);
        _context.SaveChanges();
    }
}
