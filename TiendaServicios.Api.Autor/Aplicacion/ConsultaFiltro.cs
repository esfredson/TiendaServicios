using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            public Manejador(ContextoAutor Contexto, IMapper Mapper)
            {
                contexto = Contexto;
                mapper = Mapper;
            }
            public readonly ContextoAutor contexto;
            public readonly IMapper mapper;
            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await contexto.AutorLibro.Where(x=>x.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();
                if (autor == null)
                {
                    throw new Exception("No se encontró el autor");
                }

                var autorDto = mapper.Map<AutorLibro, AutorDto>(autor);

                return autorDto;
            }
        }
    }
}
