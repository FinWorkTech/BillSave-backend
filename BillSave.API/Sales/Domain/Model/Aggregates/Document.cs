using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Shared.Domain.Model;

namespace BillSave.API.Sales.Domain.Model.Aggregates;

/// <summary>
/// This class represents the document aggregate root.
/// </summary>
public partial class Document
{
    public int Id { get; }
    public string Code { get; private set; }
    
    public SimpleDate IssueDate { get; private set; }
    public SimpleDate DueDate { get; private set; }
    
    public string RateType { get; private set; }
    public decimal RateValue { get; private set; }
    
    public int PortfolioId { get; }

    public Document()
    {
        Code = string.Empty;
        
        IssueDate = new SimpleDate(DateTime.Now);
        DueDate = new SimpleDate(DateTime.Now);
        
        RateType = string.Empty;
        RateValue = 0;
        PortfolioId = 0;
    }
    
    public Document(string code, SimpleDate issueDate, SimpleDate dueDate, string rateType, decimal rateValue, int portfolioId)
    {
        Code = code;
        IssueDate = issueDate;
        DueDate = dueDate;
        
        RateType = rateType;
        RateValue = rateValue;
        PortfolioId = portfolioId;
    }
    
    /// Document constructor
    /// <summary>
    /// This constructor creates a new document with the given command. It is used to create a new document.
    /// </summary>
    /// <param name="command">
    /// The create document command.
    /// </param>
    public Document(CreateDocumentCommand command)
    { 
        Code = command.Code;
        
        IssueDate = new SimpleDate(command.IssueDate);
        DueDate = new SimpleDate(command.DueDate);
        
        RateType = command.RateType;
        RateValue = command.RateValue;
        PortfolioId = command.PortfolioId;
    }
}