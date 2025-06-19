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

    // Antes devolvías 'bool'. Ahora devolvemos 'CategoriaResponse?'.
    // Si la creación falla, devolvemos null. Si es exitosa, devolvemos el objeto recién creado.
    public async Task<CategoriaResponse?> AgregarCategoria(CategoriaRequest request)
    {
        var nueva = new Categoria
        {
            Descripcion = request.Descripcion
        };

        var resultado = await _categoriaRepositorio.Crear(nueva);
        if (!resultado) return null;

        // Ahora que la categoría está guardada en DB, 'nueva' debería tener su Id.
        // Devolvemos el objeto 'CategoriaResponse' para que el Controller retorne JSON.
        return new CategoriaResponse(nueva);
    }

    // Similar para actualizar
    public async Task<CategoriaResponse?> ActualizarCategoria(int id, CategoriaRequest request)
    {
        var categoriaExistente = await _categoriaRepositorio.ObtenerPorId(id);
        if (categoriaExistente == null) return null;

        categoriaExistente.Descripcion = request.Descripcion;
        var resultado = await _categoriaRepositorio.Actualizar(categoriaExistente);
        if (!resultado) return null;

        return new CategoriaResponse(categoriaExistente);
    }

    public async Task<bool> EliminarCategoria(int id)
    {
        return await _categoriaRepositorio.Eliminar(id);
    }
}
