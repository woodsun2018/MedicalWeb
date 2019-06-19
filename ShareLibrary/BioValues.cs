using System;
//using System.ComponentModel.DataAnnotations;

namespace ShareLibrary
{
    public class BioValues
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public int HR { get; set; } = -1;

        public int SpO2 { get; set; } = -1;

        //[DataType(DataType.DateTime)]
        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;
    }
}
