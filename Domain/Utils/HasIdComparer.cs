using Domain.Interfaces;

namespace Domain.Utils;

public class HasIdComparer : IEqualityComparer<IHasId>
{
    public bool Equals(IHasId? x, IHasId? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null) return false;
        if (y is null) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id.Equals(y.Id);
    }

    public int GetHashCode(IHasId obj)
    {
        return obj.Id.GetHashCode();
    }
}