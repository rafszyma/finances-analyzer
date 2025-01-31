using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Endpoints.Accounts.PatchAccount;

public class PatchAccountRequest
{
    [BindNever]
    public int Id { get; set; }
    
    public string? Label { get; set; }
    
    public string? Color { get; set; }
}