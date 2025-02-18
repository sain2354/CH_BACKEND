using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Models;
using CH_BACKEND.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class SubCategoriaLogica
    {
        private readonly SubCategoriaRepository _repository;

        public SubCategoriaLogica(SubCategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SubCategoriaResponse>> ObtenerSubCategorias()
        {
            var subCategorias = await _repository.ObtenerSubCategorias();
            return subCategorias.Select(s => new SubCategoriaResponse
            {
                IdSubCategoria = s.IdSubCategoria,
                Descripcion = s.Descripcion,
                IdCategoria = s.IdCategoria
            }).ToList();
        }

        public async Task<SubCategoriaResponse?> ObtenerSubCategoriaPorId(int id)
        {
            var subCategoria = await _repository.ObtenerSubCategoriaPorId(id);
            if (subCategoria == null) return null;

            return new SubCategoriaResponse
            {
                IdSubCategoria = subCategoria.IdSubCategoria,
                Descripcion = subCategoria.Descripcion,
                IdCategoria = subCategoria.IdCategoria
            };
        }

        public async Task AgregarSubCategoria(SubCategoriaRequest request)
        {
            var nuevaSubCategoria = new SubCategoria
            {
                Descripcion = request.Descripcion,
                IdCategoria = request.IdCategoria
            };

            await _repository.AgregarSubCategoria(nuevaSubCategoria);
        }

        public async Task<bool> ActualizarSubCategoria(int id, SubCategoriaRequest request)
        {
            var subCategoria = await _repository.ObtenerSubCategoriaPorId(id);
            if (subCategoria == null) return false;

            subCategoria.Descripcion = request.Descripcion;
            subCategoria.IdCategoria = request.IdCategoria;

            await _repository.ActualizarSubCategoria(subCategoria);
            return true;
        }

        public async Task<bool> EliminarSubCategoria(int id)
        {
            var subCategoria = await _repository.ObtenerSubCategoriaPorId(id);
            if (subCategoria == null) return false;

            await _repository.EliminarSubCategoria(id);
            return true;
        }
    }
}
