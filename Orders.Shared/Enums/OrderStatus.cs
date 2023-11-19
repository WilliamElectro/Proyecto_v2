using System.ComponentModel;

namespace PGCELL.Shared.Enums
{
    public enum OrderStatus
    {
        [Description("Nuevo")]
        New,

        [Description("Despachado")]
        Dispatched,

        [Description("Enviado")]
        Sent,

        [Description("Confirmado")]
        Confirmed,

        [Description("Confirmed")]
        Cancelled
    }
}