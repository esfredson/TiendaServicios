using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibroMaterialDto>
        {
            public Guid? LibroId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDto>
        {
            public Manejador(ContextoLibreria Contexto, IMapper Mapper)
            {
                contexto = Contexto;
                mapper = Mapper;
            }
            public readonly ContextoLibreria contexto;
            public readonly IMapper mapper;
            public async Task<LibroMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await contexto.LibreriaMaterial.Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();
                if (libro == null)
                {
                    throw new Exception("No se encontró el libro");
                }

                var libroDto = mapper.Map<LibreriaMaterial, LibroMaterialDto>(libro);

                return libroDto;
            }
        }
    }
}
