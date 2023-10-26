using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameOfLife.Domain
{
    public class GameBoard
    {
        public int BoardId { get; set; }
        [NotMapped]
        public int[][] Board { get; set; }

        public string BoardJson
        {
            get => JsonConvert.SerializeObject(Board);
            set => Board = JsonConvert.DeserializeObject<int[][]>(value);
        }
    }
}
