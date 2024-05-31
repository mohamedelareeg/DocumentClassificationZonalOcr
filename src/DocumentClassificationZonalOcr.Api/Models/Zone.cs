using DocumentClassificationZonalOcr.Api.Enums;
using DocumentClassificationZonalOcr.Api.Results;
using System;

namespace DocumentClassificationZonalOcr.Api.Models
{
    public class Zone : BaseEntity
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double ActualWidth { get; private set; }
        public double ActualHeight { get; private set; }
        public double ActualImageWidth { get; private set; }
        public double ActualImageHeight { get; private set; }
        public int? FieldId { get; private set; }
        public string Regex { get; private set; }
        public string WhiteList { get; private set; }
        public bool IsDuplicated { get; private set; }
        public ZoneFieldType ZoneFieldType { get; private set; }
        public int FormSampleId { get; private set; }
        public bool IsAnchorPoint { get; private set; }
        public string? AnchorPointPath { get; private set; }

        private Zone() { }

        private Zone(
            double x,
            double y,
            double actualWidth,
            double actualHeight,
            double actualImageWidth,
            double actualImageHeight,
            int formSampleId,
            int? fieldId = null,
            string regex = "",
            string whiteList = "",
            bool isDuplicated = true,
            ZoneFieldType zoneFieldType = ZoneFieldType.SideText,
            bool isAnchorPoint = false,
            string? anchorPointPath = null)
        {
            X = x;
            Y = y;
            ActualWidth = actualWidth;
            ActualHeight = actualHeight;
            ActualImageWidth = actualImageWidth;
            ActualImageHeight = actualImageHeight;
            FieldId = fieldId;
            Regex = regex;
            WhiteList = whiteList;
            IsDuplicated = isDuplicated;
            ZoneFieldType = zoneFieldType;
            FormSampleId = formSampleId;
            IsAnchorPoint = isAnchorPoint;
            AnchorPointPath = anchorPointPath;
        }

        public static Result<Zone> Create(
            double x,
            double y,
            double actualWidth,
            double actualHeight,
            double actualImageWidth,
            double actualImageHeight,
            int formSampleId,
            int? fieldId = null,
            string regex = "",
            string whiteList = "",
            bool isDuplicated = true,
            ZoneFieldType zoneFieldType = ZoneFieldType.SideText,
            bool isAnchorPoint = false,
            string? anchorPointPath = null)
        {
            if (formSampleId <= 0)
                return Result.Failure<Zone>("Zone.Create", "Invalid form sample ID.");

            if (actualWidth <= 0)
                return Result.Failure<Zone>("Zone.Create", "Actual width must be greater than zero.");

            if (actualHeight <= 0)
                return Result.Failure<Zone>("Zone.Create", "Actual height must be greater than zero.");

            if (actualImageWidth <= 0)
                return Result.Failure<Zone>("Zone.Create", "Actual image width must be greater than zero.");

            if (actualImageHeight <= 0)
                return Result.Failure<Zone>("Zone.Create", "Actual image height must be greater than zero.");

            var zone = new Zone(
                x,
                y,
                actualWidth,
                actualHeight,
                actualImageWidth,
                actualImageHeight,
                formSampleId,
                fieldId,
                regex,
                whiteList,
                isDuplicated,
                zoneFieldType,
                isAnchorPoint,
                anchorPointPath);
            return Result.Success(zone);
        }

        public Result<bool> ModifyZoneProperties(
            double x,
            double y,
            double actualWidth,
            double actualHeight,
            double actualImageWidth,
            double actualImageHeight,
            int? fieldId = null,
            string regex = "",
            string whiteList = "",
            bool isDuplicated = true,
            ZoneFieldType zoneFieldType = ZoneFieldType.SideText,
            bool isAnchorPoint = false)
        {
            if (actualWidth <= 0)
                return Result.Failure<bool>("Zone.ModifyZoneProperties", "Actual width must be greater than zero.");

            if (actualHeight <= 0)
                return Result.Failure<bool>("Zone.ModifyZoneProperties", "Actual height must be greater than zero.");

            if (actualImageWidth <= 0)
                return Result.Failure<bool>("Zone.ModifyZoneProperties", "Actual image width must be greater than zero.");

            if (actualImageHeight <= 0)
                return Result.Failure<bool>("Zone.ModifyZoneProperties", "Actual image height must be greater than zero.");

            X = x;
            Y = y;
            ActualWidth = actualWidth;
            ActualHeight = actualHeight;
            ActualImageWidth = actualImageWidth;
            ActualImageHeight = actualImageHeight;
            FieldId = fieldId;
            Regex = regex;
            WhiteList = whiteList;
            IsDuplicated = isDuplicated;
            ZoneFieldType = zoneFieldType;
            IsAnchorPoint = isAnchorPoint;

            return Result.Success(true);
        }

        public Result<bool> ModifyAnchorPointPath(string? anchorPointPath)
        {
            if (!IsAnchorPoint)
                return Result.Failure<bool>("Zone.ModifyAnchorPointPath", "AnchorPointPath can only be modified for zones marked as anchor points.");

            if (string.IsNullOrEmpty(anchorPointPath))
                return Result.Failure<bool>("Zone.ModifyAnchorPointPath", "AnchorPointPath cannot be null or empty.");

            AnchorPointPath = anchorPointPath;
            return Result.Success(true);
        }

    }
}
