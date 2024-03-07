using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common.Response
{
    public class PetitionResponse
    {
        /* Variable que indica el estado de la respuesta. */
        public bool Success { get; set; }
        /* Variable que indica el mensaje de la respuesta. */
        public string Message { get; set; }
        /* Variable que indica el modulo de donde se genera la respuesta. */
        public string Module { get; set; }
        /* Variable que indica el Objeto de la respuesta. */
        public object Result { get; set; }
    }
}
