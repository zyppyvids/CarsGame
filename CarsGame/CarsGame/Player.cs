using System;
namespace CarsGame
{
    public class Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CarsGame.Player"/> class.
        /// </summary>
        public Player()
        {
            this.Y = 14;
            this.X = 2;
        }

        private int y;
        private int x;

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public int Y
        {
            get => y;
            set => y = value;
        }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public int X
        {
            get => x;
            set 
            { 
                if (value >= 0 && value <= 4) 
                x = value; 
            }
        }
    }
}
