using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ResultTests
    {
        private GameResultController resultController = new GameResultController();

        [Test]
        public void Player1Win()
        {
            List<int> initData = new List<int>
            {
                0, 0, 1,
                2, 2, 1,
                0, 0, 1
            };

            var cells = HelperMethods.InitializeCells(initData);
            resultController.CheckGameCompletion(cells, GameSignType.Cross, out GameResultType gameResult);

            Assert.AreEqual(gameResult, GameResultType.Player1Win);
        }

        [Test]
        public void Player2Win()
        {
            List<int> initData = new List<int>
            {
                0, 0, 1,
                2, 2, 2,
                1, 0, 1
            };

            var cells = HelperMethods.InitializeCells(initData);
            resultController.CheckGameCompletion(cells, GameSignType.Zero, out GameResultType gameResult);

            Assert.AreEqual(gameResult, GameResultType.Player2Win);
        }

        [Test]
        public void Draw()
        {
            List<int> initData = new List<int>
            {
                1, 1, 2,
                2, 1, 1,
                1, 2, 2
            };

            var cells = HelperMethods.InitializeCells(initData);

            resultController.CheckGameCompletion(cells, GameSignType.Cross, out GameResultType gameResult);

            Assert.AreEqual(gameResult, GameResultType.Draw);
        }
    }
}
