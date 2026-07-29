namespace E_CommerceProject.Entities.ViewModels.DataTable;

/// <summary>
/// DataTable Params View Model
/// </summary>
public class DataTableParamsViewModel
{
    /// <summary>
    /// Draw
    /// </summary>
    public int Draw { get; set; }
    /// <summary>
    /// Start
    /// </summary>
    public string Start { get; set; } = "0";
    /// <summary>
    /// Length
    /// </summary>
    public string Length { get; set; } = "10";
    /// <summary>
    /// SortColumn
    /// </summary>
    public string SortColumn { get; set; } = string.Empty;
    /// <summary>
    /// SortColumnDirection
    /// </summary>
    public string SortColumnDirection { get; set; } = "desc";
    /// <summary>
    /// SearchValue
    /// </summary>
    public string SearchValue { get; set; } = string.Empty;
    /// <summary>
    /// PageSize
    /// </summary>
    public int PageSize { get { return Length != null ? Convert.ToInt32(Length) : 0; } }
    /// <summary>
    /// Skip
    /// </summary>
    public int Skip { get { return Start != null ? Convert.ToInt32(Start) : 0; } }

    // Extra Filters
    public string CategoryId { get; set; }
}

