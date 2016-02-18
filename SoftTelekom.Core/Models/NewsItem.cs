using System;
using SoftTelekom.Core.Models.Bases;

namespace SoftTelekom.Core.Models
{
    public class NewsItem :BaseModel
    {

        private DateTime _newsDate;

        /// <summary>
        /// Hír időpontja
        /// </summary>
        public DateTime news_time
        {
            get
            {
                return _newsDate;
            }
            set
            {
                if (_newsDate != value)
                {
                    _newsDate = value;
                    RaisePropertyChanged(() => news_time);
                }
            }
        }

        /// <summary>
        /// Hír címe
        /// </summary>
        public string news_title { get; set; }

        /// <summary>
        /// Hír szövege
        /// </summary>
        public string news_descr { get; set; }
    }
}