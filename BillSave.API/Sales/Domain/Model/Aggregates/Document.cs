using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Domain.Model.ValueObjects;
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
    
    public Rate Rate { get; private set; }
    public Currency Currency { get; private set; }
    
    public int PortfolioId { get; }

    public Document()
    {
        Code = string.Empty;
        
        IssueDate = new SimpleDate(DateTime.Now);
        DueDate = new SimpleDate(DateTime.Now);
        
        Rate = new Rate(0,"Nominal"); 
        Currency = new Currency("USD");

        PortfolioId = 0;
    }
    
    public Document(string code, SimpleDate issueDate, SimpleDate dueDate, Rate rate, Currency currency, int portfolioId)
    {
        Code = code;
        IssueDate = issueDate;
        DueDate = dueDate;
        
        Rate = rate;
        Currency = currency;

        PortfolioId = portfolioId;
    }
    
    /// <summary>
    /// Constructor that initializes a Document from a command.
    /// </summary>
    public Document(CreateDocumentCommand command)
    { 
        Code = command.Code;
        
        IssueDate = new SimpleDate(command.IssueDate);
        DueDate = new SimpleDate(command.DueDate);
        
        Rate = new Rate(command.RateValue, command.RateType);
        Currency = new Currency(command.Currency);

        PortfolioId = command.PortfolioId;
    }
}