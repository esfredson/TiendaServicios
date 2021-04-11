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
    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorDto>>
        {
        }

        public class Manejador : IRequestHandler<ListaAutor, List<AutorDto>>
        {
            public Manejador(ContextoAutor Contexto, IMapper Mapper)
            {
                contexto = Contexto;
                mapper = Mapper;
            }
            public readonly ContextoAutor contexto;
            public readonly IMapper mapper;
            public async Task<List<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await contexto.AutorLibro.ToListAsync();
                var autoresDto = mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);

                return autoresDto;
            }
        }
    }
}
