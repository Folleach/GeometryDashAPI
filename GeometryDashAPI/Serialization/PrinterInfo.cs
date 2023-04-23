using System.Linq.Expressions;
using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Serialization
{
    internal class PrinterInfo<T>
    {
        public TypeDescriptorHelper.Printer<T> Printer { get; }
        public TypeDescriptorHelper.Getter<T, bool> IsDefault { get; }
        public GamePropertyAttribute Attribute { get; }

#if DEBUG
        public Expression<TypeDescriptorHelper.Printer<T>> PrinterExp { get; set; }
        public Expression<TypeDescriptorHelper.Getter<T, bool>> IsDefaultExp { get; set; }
#endif

        public PrinterInfo(
            TypeDescriptorHelper.Printer<T> printer,
            TypeDescriptorHelper.Getter<T, bool> isDefault,
            GamePropertyAttribute attribute)
        {
            Printer = printer;
            IsDefault = isDefault;
            Attribute = attribute;
        }
    }
}
