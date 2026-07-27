
namespace E_CommerceProject.Entities.ViewModels.DataTable;

/// <summary>
/// View model to hold the dataTable response data
/// </summary>
public class DatatableResult
{
    /// <summary>
    /// Draw counter.
    /// This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables (Ajax requests are asynchronous and thus can return out of sequence).
    /// This is used as part of the draw return parameter (see below).
    /// </summary>
    public int draw;

    /// <summary>
    /// Total Records count
    /// </summary>
    public long recordsTotal;

    /// <summary>
    /// Filtered records count
    /// </summary>
    public long recordsFiltered;

    /// <summary>
    /// Table Data
    /// </summary>
    public List<object> data = [];
}