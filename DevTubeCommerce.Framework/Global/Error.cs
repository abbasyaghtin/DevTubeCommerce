using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Framework.Global
{
    public class Error
    {
        public int Id { get; private set; }
        public String? EnglishTitle { get; private set; }
        public String? PersianTitle { get; private set; }

        public static readonly Error InvalidId = new Error()
        {
            Id = 1,
            EnglishTitle = "Invalid Id! ",
            PersianTitle = "شناسه نامعتبر می باشد.",
        };
        public static readonly Error InvalidTitle = new Error()
        {
            Id = 2,
            EnglishTitle = "Invalid Title! ",
            PersianTitle = "عنوان نامعتبر می باشد.",
        };
        public static readonly Error InvalidDescription = new Error()
        {
            Id = 3,
            EnglishTitle = "Invalid Description! ",
            PersianTitle = "عنوان نامعتبر می باشد.",
        };
        public static readonly Error InvalidSortOrder = new Error()
        {
            Id = 4,
            EnglishTitle = "Invalid Sort Order!",
            PersianTitle = "ترتیب نمایش نامعتبر می باشد.",
        };
        public static readonly Error InvalidPrice = new Error()
        {
            Id = 5,
            EnglishTitle = "Invalid Price!",
            PersianTitle = "قیمت نامعتبر می باشد.",
        };
        public static readonly Error FeatureNotFound = new Error()
        {
            Id = 6,
            EnglishTitle = "Feature Not Found!",
            PersianTitle = "ویژگی مورد نظر یافت نشد.",
        };
        public static readonly Error CategoryNotFound = new Error()
        {
            Id = 7,
            EnglishTitle = "Category Not Found!",
            PersianTitle = "کاتالوگ مورد نظر یافت نشد.",
        };
    }
}
