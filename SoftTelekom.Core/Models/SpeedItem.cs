using SoftTelekom.Core.Models.Bases;

namespace SoftTelekom.Core.Models
{
    public class SpeedItem : BaseModel
    {
        /// <summary>
        /// Inernet sebbessége formázott szöveges formában
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Internet sebessége számokkal
        /// </summary>
        public int Mbit { get; set; }
    }
}