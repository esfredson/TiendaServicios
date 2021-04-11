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
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibroMaterialDto>> { }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDto>>
        {
            public readonly ContextoLibreria contexto;
            public readonly IMapper mapper;
            public Manejador(ContextoLibreria Contexto, IMapper Mapper)
            {
                contexto = Contexto;
                mapper = Mapper;
            }
            
            public async Task<List<LibroMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await contexto.LibreriaMaterial.ToListAsync();
                var librosDto = mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDto>>(libros);

                return librosDto;
            }
        }
    }
}
