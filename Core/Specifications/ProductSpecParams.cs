using System.Linq;
namespace Core.Specifications;

public class ProductSpecParams
{
    private const int MaxPageSize = 50;
    private int _pageSize = 6;
    private List<string> _brands = [];
    private List<string> _types = [];
    private string? _search;

    public int PageIndex { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public int MyProperty { get; set; }
    public string? Sort { get; set; }
    public List<string> Brands
    {
        get => _brands;
        set
        {
            _brands = value.SelectMany(x => x.Split(',',
                StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }

    public List<string> Types
    {
        get => _types;
        set
        {
            _types = value.SelectMany(x => x.Split(',',
                StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }

    public string Search
    {
        get => _search ?? "";
        set => _search = value.ToLower();
    }
}

