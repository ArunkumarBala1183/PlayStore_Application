using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Playstore.Migrations.DateConvertor
{
    class DateOnlyConvertor : ValueConverter<DateOnly , DateTime>
    {
        public DateOnlyConvertor() : base(
            dateonly => dateonly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime))
        {}
    }
}