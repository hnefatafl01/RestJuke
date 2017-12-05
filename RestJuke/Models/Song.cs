using System;
namespace RestJuke.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public string Artist { get; set; }
        public string Album { get; set; }
        public int Duration { get; set; }
    }
}
