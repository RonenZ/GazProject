using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using GazProjec.Models;

namespace GazProjec.Services
{
    public static class ImageService
    {
        static ImageService() {}

        public static Image GenerateBillImage(UserBillModel model)
        {
            var fileName = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\billsImage.png";
            var valuesAndPositions = new TitleAndPosition[]
            {
                new TitleAndPosition(model.Name, new Rectangle(475, 117, 200, 50)), 
                new TitleAndPosition(model.Amount, new Rectangle(650, 360, 200, 50)), 
                new TitleAndPosition(model.Date.ToString("dd/MM/yy"), new Rectangle(60, 117, 400, 50)), 
                new TitleAndPosition(model.Counter, new Rectangle(540, 117, 400, 50)), 
                new TitleAndPosition(model.BillId, new Rectangle(810, 117, 400, 50)), 
            };

            return SetImage(fileName, valuesAndPositions);
        }

        #region Private Methods
        private static Image SetImage(string filePath, IEnumerable<TitleAndPosition> values)
        {
            var resultImage = Image.FromFile(filePath);

            foreach (var item in values)
            {
                resultImage.CreateRectangle(item.Value, item.Position);
            }

            return resultImage;
        }

        private static void CreateRectangle(this Image currImage, string name, Rectangle rect)
        {
            var message = name;
            var fillColor = Color.Transparent;
            var fillBrush = new SolidBrush(fillColor);
            var fillRectangle = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            var textFont = new Font("Comic Sans MS", 10);
            var textBrush = new SolidBrush(Color.Black);
            var textFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var drawingSurface = Graphics.FromImage(currImage);
            drawingSurface.FillRectangle(fillBrush, fillRectangle);
            drawingSurface.DrawString(message, textFont, textBrush, fillRectangle, textFormat);
        }

        private class TitleAndPosition
        {
            public TitleAndPosition(string value, Rectangle position)
            {
                this.Value = value;
                this.Position = position;
            }

            public string Value { get; private set; }
            public Rectangle Position { get; private set; }
        }
        #endregion
    }
}