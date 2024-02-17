using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetTroll
{
    class RandomBlocksQueue
    {
        public RandomBlocksQueue(int count = 1)
        {
            FillRandomBlocks(count);
        }
        public void FillRandomBlocks(int count)
        {
			for (int i = RandomBlocksIdList.Count; i < count; i++)
				InsertRandomBlock();
		}
        public BlockBase PopBlock()
        {
            var block = PeekBlock().Reset();
            RandomBlocksIdList.RemoveAt(0);
			InsertRandomBlock();
			return block;
        }

		private void InsertRandomBlock() => RandomBlocksIdList.Add(_random.Next(BlocksInstances.Blocks.Count - 1) + 1);

        public BlockBase PeekBlock(int index = 0) => BlocksInstances.Blocks[RandomBlocksIdList[index]];

		public List<int> RandomBlocksIdList = new List<int>();
		private Random _random = new Random();
	}
}
