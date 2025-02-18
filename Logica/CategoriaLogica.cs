using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CH_BACKEND.DBCalzadosHuancayo;

public class CategoriaLogica
{
    private readonly CategoriaRepositorio _categoriaRepositorio;

    public CategoriaLogica(CategoriaRepositorio categoriaRepositorio)
    {
        _categoriaRepositorio = categoriaRepositorio;
    }

    public async Task<List<CategoriaResponse>> ObtenerCategorias()
    {
        var categorias = await _categoriaRepositorio.ObtenerTodas();
        return categorias.ConvertAll(c => new CategoriaResponse(c));
    }

    public async Task<CategoriaResponse?> ObtenerCategoriaPorId(int id)
    {
        var categoria = await _categoriaRepositorio.ObtenerPorId(id);
        return categoria != null ? new CategoriaResponse(categoria) : null;
    }

    public async Task<bool> AgregarCategoria(CategoriaRequest request)
    {
        var categoria = new Categoria
        {
            Descripcion = request.Descripcion
        };

        return await _categoriaRepositorio.Crear(categoria);
    }

    public async Task<bool> ActualizarCategoria(int id, CategoriaRequest request)
    {
        var categoriaExistente = await _categoriaRepositorio.ObtenerPorId(id);
        if (categoriaExistente == null) return false;

        categoriaExistente.Descripcion = request.Descripcion;

        return await _categoriaRepositorio.Actualizar(categoriaExistente);
    }

    public async Task<bool> EliminarCategoria(int id)
    {
        return await _categoriaRepositorio.Eliminar(id);
    }
}
