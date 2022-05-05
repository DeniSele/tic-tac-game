using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AdditionalFunctionsTest
    {
        [Test]
        public void Hint()
        {
            EventService eventService = new EventService();
            AdvancedFeaturesController featuresController = new AdvancedFeaturesController(eventService);

            List<int> initData = new List<int>
            {
                1, 2, 0,
                0, 1, 2,
                1, 0, 2
            };

            var cells = HelperMethods.InitializeCells(initData);

            ICell cell = featuresController.Hint(cells);

            Assert.AreEqual(GameSignType.None, cell.CellSignType);
        }


        [Test]
        public void Undo()
        {
            EventService eventService = new EventService();
            AdvancedFeaturesController featuresController = new AdvancedFeaturesController(eventService);

            List<int> initData = new List<int>
            {
                1, 2, 0,
                0, 1, 2,
                1, 0, 2
            };

            List<int> expectedData = new List<int>
            {
                1, 2, 0,
                0, 1, 2,
                0, 0, 0
            };

            var cells = HelperMethods.InitializeCells(initData);
            foreach(var cell in cells)
            {
                if (cell.CellSignType != GameSignType.None)
                    eventService.TicTacEvents.OnCellUpdated(cell);
            }

            featuresController.Undo();

            var result = HelperMethods.FromCellsToInitData(cells);

            Assert.AreEqual(expectedData, result);
        }
    }
}
