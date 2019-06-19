using ShareLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalWpfClient
{
    public class CreateDemoBioValues
    {
        private int _HR = 60;
        private int _SpO2 = 90;

        public BioValues GetNewValue()
        {
            BioValues bioValues = new BioValues();

            bioValues.HR = _HR;
            _HR++;
            if (_HR > 120)
                _HR = 60;

            bioValues.SpO2 = _SpO2;
            _SpO2++;
            if (_SpO2 > 100)
                _SpO2 = 90;

            return bioValues;
        }
    }
}
